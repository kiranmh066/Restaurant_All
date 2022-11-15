using Microsoft.EntityFrameworkCore;
using RestaurantEntity;
using System;

namespace RestaurantDAL
{
    public class RestaurantDbContext:DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {

        }
        public DbSet<Admin> tbl_Admin { get; set; }
        public DbSet<Bill> tbl_Bill { get; set; }
        public DbSet<Employee> tbl_Employee { get; set; }
        public DbSet<Feedback> tbl_Feedback { get; set; }

        public DbSet<Food> tbl_Food { get; set; }

        public DbSet<HallTable> tbl_HallTable { get; set; }

        public DbSet<Order> tbl_Order { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer("Data Source=VDC01LTC2179; Initial Catalog = Restaurant_Chandan1; Integrated Security=True;");
        }
    }
}
