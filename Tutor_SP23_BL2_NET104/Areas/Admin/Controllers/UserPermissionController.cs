using Tutor_SP23_BL2_NET104.Services.Implements;
using Tutor_SP23_BL2_NET104.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Tutor_SP23_BL2_NET104.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserPermissionController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IRoleServices _roleServices;

        public UserPermissionController()
        {
            _userServices = new UserServices();
            _roleServices = new RoleServices();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
