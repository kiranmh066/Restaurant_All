﻿using Microsoft.EntityFrameworkCore;
using RestaurantEntity;
using System;

namespace RestaurantDAL
{
    public class RestaurantDbContext:DbContext
    {
        public RestaurantDbContext()
        {


        }
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {

        }

        public DbSet<Bill> tbl_Bill { get; set; }
        public DbSet<Employee> tbl_Employee { get; set; }
        public DbSet<Feedback> tbl_Feedback { get; set; }

        public DbSet<Food> tbl_Food { get; set; }

        public DbSet<HallTable> tbl_HallTable { get; set; }

        public DbSet<Order> tbl_Order { get; set; }

        public DbSet<AssignWork> tbl_AssignWork { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {



            dbContextOptionsBuilder.UseSqlServer("Data Source=VDC01LTC2164; Initial Catalog = Restaurant_Kiran1; Integrated Security=True;");




        }
    }
}
