namespace Tutor_SP23_BL2_NET104.Models.ViewModels
{
    public class CategoryForDashboard
    {
        public string CategoryName { get; set; }
        public int TotalAmountOfProduct { get; set; }
        public int TotalInCartAndBill { get; set; }
        public int TotalOrder { get; set; }
        public int TotalDelivering { get; set; }
        public double TotalEarning { get; set; }
    }
}
