using RestaurantDAL.Repost;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantBLL.Services
{
    public class AdminService
    {
        IAdminRepost _adminRepository;


        //Unable to resolve ====>>>> Object issues
        public AdminService(IAdminRepost adminRepository)
        {
            _adminRepository = adminRepository;
        }

 
        //Update Admin
        public void UpdateAdmin(Admin admin)
        {
            _adminRepository.UpdateAdmin(admin);
        }

        //Delete Admin
        public void DeleteAdmin(int adminId)
        {
            _adminRepository.DeleteAdmin(adminId);
        }

        //Get AdminByID
        public Admin GetAdminById(int adminId)
        {
            return _adminRepository.GetAdminById(adminId);
        }

        //Get Admins
        public IEnumerable<Admin> GetAdmin()
        {
            return _adminRepository.GetAdmins();
        }

        //Registering Admin
        public void Register(Admin adminInfo)
        {
            _adminRepository.Register(adminInfo);
        }

        //Logging Admin
        public Admin Login(Admin adminInfo)
        {
            return _adminRepository.Login(adminInfo);
        }
    }
}
