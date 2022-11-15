using Microsoft.EntityFrameworkCore;
using RestaurantEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantDAL.Repost
{
    public class AdminRepost : IAdminRepost
    {
        RestaurantDbContext _dbContext;//default private

        public AdminRepost(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

   

        public void DeleteAdmin(int adminId)
        {
            var admin = _dbContext.tbl_Admin.Find(adminId);
            _dbContext.tbl_Admin.Remove(admin);
            _dbContext.SaveChanges();
        }

        public Admin GetAdminById(int adminId)
        {
            return _dbContext.tbl_Admin.Find(adminId);
        }

        public IEnumerable<Admin> GetAdmins()
        {
            return _dbContext.tbl_Admin.ToList();
        }

        public Admin Login(Admin admin)
        {
            Admin adminInfo = null;
            var result = _dbContext.tbl_Admin.Where(obj => obj.AdminEmail == admin.AdminEmail && obj.AdminPassword == admin.AdminPassword).ToList();
            if (result.Count > 0)
            {
                adminInfo = result[0];
            }
            return adminInfo;
        }

        public void Register(Admin adminInfo)
        {
            _dbContext.tbl_Admin.Add(adminInfo);
            _dbContext.SaveChanges();
        }

        public void UpdateAdmin(Admin admin)
        {
            _dbContext.Entry(admin).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
