namespace GiangNLH.ArtShop.Models.ViewModels
{
    public class ProductForDashboard
    {
        public string ProductName { get; set; }
        public Guid IdCategory { get; set; }
        public int TotalInCartAndBill { get; set; }
        public int TotalOrder { get; set; }
        public int TotalDelivering { get; set; }
        public double TotalEarning { get; set; }
    }
}
