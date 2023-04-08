using Tutor_SP23_BL2_NET104.Models.ViewModels;
using Tutor_SP23_BL2_NET104.Models;

namespace Tutor_SP23_BL2_NET104.Services.Interfaces
{
    public interface IDashboardServices
    {
        public Task<List<ProductForDashboard>> GetAllProductForDashboardAsync(DateTime start, DateTime end);
        public Task<List<CategoryForDashboard>> GetAllCategoryForDashboardAsync(DateTime start, DateTime end);
    }
}
