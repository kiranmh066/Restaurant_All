using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantBLL.Services;
using RestaurantEntity;
using System.Collections.Generic;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("GetAdmins")]//
        public IEnumerable<Admin> GetAdmins()
        {
            return _adminService.GetAdmin();
        }

   

        [HttpDelete("DeleteAdmin")]
        public IActionResult DeleteAdmin(int adminId)
        {
            _adminService.DeleteAdmin(adminId);
            return Ok("Admin deleted Successfully");
        }

        [HttpPut("UpdateAdmin")]
        public IActionResult UpdateAdmin([FromBody] Admin admin)
        {
            _adminService.UpdateAdmin(admin);
            return Ok("Admin Updated Successfully");
        }

        [HttpGet("GetAdminById")]
        public Admin GetAdminById(int adminId)
        {
            return _adminService.GetAdminById(adminId);
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] Admin adminInfo)
        {
            _adminService.Register(adminInfo);
            return Ok("Register successfully!!");
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody] Admin adminInfo)
        {
            Admin admin = _adminService.Login(adminInfo);
            if (admin != null)
                return Ok("Login success!!");
            else
                return NotFound();
        }
    }
}
