using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientApp.Services.IServices
{
    public interface IHomepageService
    {
        // Get all Homepage info
        public Task<IEnumerable<HomepageInfoDTO>> GetAllHomepageInfo();
    }
}
