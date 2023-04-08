using Tutor_SP23_BL2_NET104.Services.Implements;
using Tutor_SP23_BL2_NET104.Services.Interfaces;
using Tutor_SP23_BL2_NET104.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Tutor_SP23_BL2_NET104.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BillController : Controller
    {
        private readonly IBillServices _billServices;

        public BillController()
        {
            _billServices = new BillServices();
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.listBill = await _billServices.GetAllAsync();

            return View();
        }

        public async Task<IActionResult> ListBill(int status)
        {
            if (status != 0 && status != 1 && status != 2 && status != 3)
            {
                return RedirectToAction("Index");
            }
            var listBill = await _billServices.GetAllAsync();
            ViewBag.listBill = listBill.Where(c => c.Status == status).ToList();
            ViewData["Title"] = status == 0 ? "Đơn đặt hàng" : status == 1 ? "Đơn đã hủy" : status == 2 ? "Đơn đang giao" : "Đơn đã thanh toán";

            return View();
        }

        public async Task<IActionResult> Details(Guid id)
        {
            ViewBag.bill = await _billServices.GetByIdAsync(id);

            return View();
        }

        public async Task<IActionResult> Update(Bill obj)
        {
            var result = await _billServices.UpdateAsync(obj.Id, obj);

            if (result)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            ViewBag.listBill = await _billServices.RemoveAsync(id);

            return RedirectToAction("Index");
        }


    }
}
