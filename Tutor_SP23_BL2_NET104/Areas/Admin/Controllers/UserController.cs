using Tutor_SP23_BL2_NET104.Services.Implements;
using Tutor_SP23_BL2_NET104.Services.Interfaces;
using Tutor_SP23_BL2_NET104.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Tutor_SP23_BL2_NET104.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;

        public UserController()
        {
            _userServices = new UserServices();
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.listUser = await _userServices.GetAllAsync();

            return View();
        }

        public async Task<IActionResult> Details(Guid id)
        {
            ViewBag.user = await _userServices.GetByIdAsync(id);

            return View();
        }

        public async Task<IActionResult> Create(User obj)
        {
            var result = await _userServices.AddAsync(obj);

            if (result)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<IActionResult> Update(User obj)
        {
            var result = await _userServices.UpdateAsync(obj.Id, obj);

            if (result)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            ViewBag.listUser = await _userServices.RemoveAsync(id);

            return RedirectToAction("Index");
        }
    }
}
