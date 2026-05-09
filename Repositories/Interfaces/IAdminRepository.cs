using FashionShopAPI.Models.DTOs;

namespace FashionShopAPI.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        Task<DashboardResponse> GetDashboardStatsAsync();
    }
}