namespace FashionShopAPI.Models.DTOs
{
    public class DashboardResponse
    {
        public int TotalProducts { get; set; }
        public int TotalOrders { get; set; }
        public int TotalUsers { get; set; }
        public int NewOrdersCount { get; set; }
        public decimal TodayRevenue { get; set; }
        public decimal MonthRevenue { get; set; }
        public int NewUsersToday { get; set; }
    }
}