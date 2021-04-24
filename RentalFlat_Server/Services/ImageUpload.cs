using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RentalFlat_Server.Services.IServices;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RentalFlat_Server.Services
{
    public class ImageUpload : IImageUpload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        // Uncomment all before publish and delete IHttpContextAccessor _httpContextAccessor!
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly IConfiguration _configuration;

        public ImageUpload(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor
            /*IConfiguration configuration*/)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            //_configuration = configuration;
        }
        public bool DeleteImage(string fileName)
        {
            try
            {
                var path = $"{_webHostEnvironment.WebRootPath}\\homeimages\\{fileName}";

                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> UploadImage(IBrowserFile file)
        {
            try
            {
                var fileInfo = new FileInfo(file.Name);
                var fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
                var folderDirectory = $"{_webHostEnvironment.WebRootPath}\\homeimages";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "homeimages", fileName);
                var memoryStream = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(memoryStream);

                if (!Directory.Exists(folderDirectory))
                {
                    Directory.CreateDirectory(folderDirectory);
                }

                await using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    memoryStream.WriteTo(fileStream);
                }

                // url - содержит полный путь до файла.
                var url = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}/";
                //var url = $"{_configuration.GetValue<string>("ServerUrl")}";
                var fullPath = $"{url}homeimages/{fileName}";

                return fullPath;
            }
            catch (Exception ex)
            {
                return ex.Message;
                //or throw ex;
            }
        }
    }
}
