using Tutor_SP23_BL2_NET104.Services.Implements;
using Tutor_SP23_BL2_NET104.Services.Interfaces;
using Tutor_SP23_BL2_NET104.Models;
using Microsoft.AspNetCore.Mvc;

namespace Tutor_SP23_BL2_NET104.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IProductServices _productServices;
        private readonly ICategoryServices _categoryServices;

        public ProductController(IHttpContextAccessor httpContextAccessor)
        {
            _productServices = new ProductServices();
            _categoryServices = new CategoryServices();
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _productServices.GetAllAsync();
            ViewBag.listProduct = list.Where(c => c.Status != 1).ToList();

            return View();
        }

        public async Task<IActionResult> Details(Guid id)
        {
            ViewBag.product = await _productServices.GetByIdAsync(id);
            ViewBag.listCategory = await _categoryServices.GetAllAsync();

            ViewData["Title"] = ViewBag.product.Name;

            return View();
        }

        public async Task<IActionResult> Create(Product obj)
        {
            if (ModelState.IsValid)
            {
                var result = await _productServices.AddAsync(obj);

                if (result)
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.listCategory = await _categoryServices.GetAllAsync();
            obj = new Product()
            {
                Id = Guid.NewGuid()
            };

            return View(obj);
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var obj = await _productServices.GetByIdAsync(id);
            
            ViewBag.oldProduct = _httpContextAccessor.HttpContext.Session.GetObjectFromJson<Product>($"{id}");

            ViewBag.listCategory = await _categoryServices.GetAllAsync();
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Product obj)
        {
            if (ModelState.IsValid)
            {
                var result = await _productServices.UpdateAsync(obj.Id, obj);

                if (result)
                {
                    _httpContextAccessor.HttpContext.Session.SetObjectAsJson($"{obj.Id}", obj);
                    
                    return RedirectToAction("Index");
                }
            }

            ViewBag.listCategory = await _categoryServices.GetAllAsync();

            return View(obj);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            ViewBag.listProduct = await _productServices.RemoveAsync(id);

            return RedirectToAction("Index");
        }
    }
}
