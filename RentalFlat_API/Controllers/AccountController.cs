using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.DTOs;
using RentalFlat_API.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RentalFlat_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly APISettings _aPISettings;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager, IOptions<APISettings> options)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _aPISettings = options.Value; // automaticly retreew all value's for APISettings.
        }

        // The IAuthenticationService(ClientApp) also needing these action methods.
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] UserRequestDTO userRequestDTO)
        {
            if (userRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new IdentityUser
            {
                UserName = userRequestDTO.Email,
                Email = userRequestDTO.Email,
                PhoneNumber = userRequestDTO.PhoneNumber,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, userRequestDTO.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponseDTO
                { Errors = errors, IsRegisterationSuccessful = false });
            }

            var roleResult = await _userManager.AddToRoleAsync(user, StaticDetails.Role_Customer);

            if (!roleResult.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new RegistrationResponseDTO
                { Errors = errors, IsRegisterationSuccessful = false });
            }

            return StatusCode(201);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] AuthenticationDTO authenticationDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(authenticationDTO.UserName, authenticationDTO.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(authenticationDTO.UserName);

                if (user == null)
                {
                    return Unauthorized(new AuthenticationResponseDTO
                    {
                        IsAuthenticationSuccessful = false,
                        ErrorMessage = "Invalid Authentication"
                    });
                }

                //If everything is valid and we need to login the user.
                var signingCredentials = GetSigningCredentials();
                var claimsAsync = await GetClaimsAsync(user);

                var tokenOptions = new JwtSecurityToken(
                    issuer: _aPISettings.ValidIssuer,
                    audience: _aPISettings.ValidAudience,
                    claims: claimsAsync,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: signingCredentials);

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new AuthenticationResponseDTO
                {
                    IsAuthenticationSuccessful = true,
                    Token = token,
                    UserDTO = new UserDTO
                    {
                        Name = user.Email,
                        Id = user.Id,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber
                    }
                });
            }
            else
            {
                return Unauthorized(new AuthenticationResponseDTO
                {
                    IsAuthenticationSuccessful = false,
                    ErrorMessage = "Invalid Authentication."
                });
            }
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_aPISettings.SecretKey));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaimsAsync(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Id", user.Id)
            };

            var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(user.Email));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
