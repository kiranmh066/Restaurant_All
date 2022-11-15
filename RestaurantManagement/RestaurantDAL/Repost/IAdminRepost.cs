using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantDAL.Repost
{
    public interface IAdminRepost
    {
       

        void UpdateAdmin(Admin admin);

        void DeleteAdmin(int adminId);

        Admin GetAdminById(int adminId);

        IEnumerable<Admin> GetAdmins();

        void Register(Admin adminInfo);
        Admin Login(Admin admin);
    }
}
