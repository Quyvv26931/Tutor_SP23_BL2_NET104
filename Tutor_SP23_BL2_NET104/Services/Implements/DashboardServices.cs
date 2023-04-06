using GiangNLH.ArtShop.Models.ViewModels;
using GiangNLH.ArtShop.Services.Interfaces;
using Tutor_SP23_BL2_NET104.ArtShopDbContext;
using Tutor_SP23_BL2_NET104.Models;
using Microsoft.EntityFrameworkCore;

namespace GiangNLH.ArtShop.Services.Implements
{
    public class DashboardServices : IDashboardServices
    {
        private readonly ArtShopContext _dbContext;

        public DashboardServices()
        {
            this._dbContext = new ArtShopContext();
        }

        public async Task<List<CategoryForDashboard>> GetAllCategoryForDashboardAsync(DateTime start, DateTime end)
        {
            var listCategory = await _dbContext.Categories.ToListAsync();
            var listProductForDashboard = new List<ProductForDashboard>();
            var listCategoryForDashboard = new List<CategoryForDashboard>();

            listProductForDashboard = await GetAllProductForDashboardAsync(start,end);

            listCategoryForDashboard = listCategory.Select(c => new CategoryForDashboard()
            {
                CategoryName = c.Name,
                TotalAmountOfProduct = listProductForDashboard.Count(x => x.IdCategory == c.Id),
                TotalOrder = listProductForDashboard.Sum(x => {if (x.IdCategory == c.Id) return x.TotalOrder;else return 0;}),
                TotalDelivering = listProductForDashboard.Sum(x => { if (x.IdCategory == c.Id) return x.TotalDelivering; else return 0; }),
                TotalEarning = listProductForDashboard.Sum(x => { if (x.IdCategory == c.Id) return x.TotalEarning; else return 0; }),
                TotalInCartAndBill = listProductForDashboard.Sum(x => { if (x.IdCategory == c.Id) return x.TotalInCartAndBill; else return 0; }),
            }).ToList();

            return listCategoryForDashboard;
        }

        public async Task<List<ProductForDashboard>> GetAllProductForDashboardAsync(DateTime start, DateTime end)
        {
            var listProduct = await _dbContext.Products.ToListAsync();
            var listProductBill = await _dbContext.ProductBills.ToListAsync();
            var listProductInCart = await _dbContext.CartDetailses.ToListAsync();

            var listProductForDashboard = new List<ProductForDashboard>();
            var listCategoryForDashboard = new List<CategoryForDashboard>();

            //TotalInCartAndBill
            var listproBill = listProductBill.Where(c => c.CreatedTime >= start && c.CreatedTime <= end).GroupBy(c => c.IdProduct)
                                            .Select(group => new
                                            {
                                                IdProduct = group.Key,
                                                TotalOrder = group.Count(x => x.Status == 0),
                                                TotalDelivering = group.Count(x => x.Status == 2),
                                                TotalEarning = group.Count(x => x.Status == 3),
                                                Total = group.Count(x => x.Status == 0 || x.Status == 2 || x.Status == 3)
                                            })
                                            .ToList();

            var listproCart = listProductInCart.Where(c => c.CreatedTime >= start && c.CreatedTime <= end).GroupBy(c => c.IdProduct)
                                            .Select(group => new
                                            {
                                                IdProduct = group.Key,
                                                Total = group.Count(x => x.Status == 0),
                                            })
                                            .ToList();

            listProductForDashboard = listProduct.Select(c => new ProductForDashboard()
            {
                ProductName = c.Name,
                TotalOrder = listproBill.FirstOrDefault(x => x.IdProduct == c.Id) == null ? 0 : listproBill.FirstOrDefault(x => x.IdProduct == c.Id).TotalOrder,
                TotalDelivering = listproBill.FirstOrDefault(x => x.IdProduct == c.Id) == null ? 0 : listproBill.FirstOrDefault(x => x.IdProduct == c.Id).TotalDelivering,
                TotalEarning = listproBill.FirstOrDefault(x => x.IdProduct == c.Id) == null ? 0 : listproBill.FirstOrDefault(x => x.IdProduct == c.Id).TotalEarning,
                TotalInCartAndBill = listproBill.FirstOrDefault(x => x.IdProduct == c.Id) == null ?
                                    (listproCart.FirstOrDefault(x => x.IdProduct == c.Id) == null ? 0 : listproCart.FirstOrDefault(x => x.IdProduct == c.Id).Total) :
                                    (listproBill.FirstOrDefault(x => x.IdProduct == c.Id).Total + listproCart.FirstOrDefault(x => x.IdProduct == c.Id).Total)
            })
            .ToList();

            return listProductForDashboard;
        }
    }
}
