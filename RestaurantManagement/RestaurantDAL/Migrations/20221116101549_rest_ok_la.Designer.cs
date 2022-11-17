﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestaurantDAL;

namespace RestaurantDAL.Migrations
{
    [DbContext(typeof(RestaurantDbContext))]
    [Migration("20221116101549_rest_ok_la")]
    partial class rest_ok_la
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("RestaurantEntity.Admin", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("AdminEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminPassword")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminId");

                    b.ToTable("tbl_Admin");
                });

            modelBuilder.Entity("RestaurantEntity.AssignWork", b =>
                {
                    b.Property<int>("AssignId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("EmpId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("AssignId");

                    b.HasIndex("EmpId");

                    b.HasIndex("OrderId");

                    b.ToTable("tbl_AssignWork");
                });

            modelBuilder.Entity("RestaurantEntity.Bill", b =>
                {
                    b.Property<int>("BillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("BillDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("BillStatus")
                        .HasColumnType("bit");

                    b.Property<double>("BillTotal")
                        .HasColumnType("float");

                    b.Property<int>("HallTableId")
                        .HasColumnType("int");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BillId");

                    b.HasIndex("HallTableId");

                    b.ToTable("tbl_Bill");
                });

            modelBuilder.Entity("RestaurantEntity.Employee", b =>
                {
                    b.Property<int>("EmpId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("EmpDesignation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmpEmail")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("EmpGender")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("EmpName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmpPassword")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<double>("EmpPhone")
                        .HasColumnType("float");

                    b.Property<string>("EmpSpeciality")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmpId");

                    b.ToTable("tbl_Employee");
                });

            modelBuilder.Entity("RestaurantEntity.Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Comments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("FeedbackStatus")
                        .HasColumnType("bit");

                    b.Property<int>("HallTableId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("FeedbackId");

                    b.HasIndex("HallTableId");

                    b.ToTable("tbl_Feedback");
                });

            modelBuilder.Entity("RestaurantEntity.Food", b =>
                {
                    b.Property<int>("FoodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<double>("FoodCost")
                        .HasColumnType("float");

                    b.Property<string>("FoodCuisine")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("FoodImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FoodName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FoodType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FoodId");

                    b.ToTable("tbl_Food");
                });

            modelBuilder.Entity("RestaurantEntity.HallTable", b =>
                {
                    b.Property<int>("HallTableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("HallTableSize")
                        .HasColumnType("int");

                    b.Property<bool>("HallTableStatus")
                        .HasColumnType("bit");

                    b.HasKey("HallTableId");

                    b.ToTable("tbl_HallTable");
                });

            modelBuilder.Entity("RestaurantEntity.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("FoodId")
                        .HasColumnType("int");

                    b.Property<int>("HallTableId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("OrderStatus")
                        .HasColumnType("bit");

                    b.Property<int>("OrderTotal")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("FoodId");

                    b.HasIndex("HallTableId");

                    b.ToTable("tbl_Order");
                });

            modelBuilder.Entity("RestaurantEntity.AssignWork", b =>
                {
                    b.HasOne("RestaurantEntity.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestaurantEntity.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("RestaurantEntity.Bill", b =>
                {
                    b.HasOne("RestaurantEntity.HallTable", "HallTable")
                        .WithMany()
                        .HasForeignKey("HallTableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HallTable");
                });

            modelBuilder.Entity("RestaurantEntity.Feedback", b =>
                {
                    b.HasOne("RestaurantEntity.HallTable", "HallTable")
                        .WithMany()
                        .HasForeignKey("HallTableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HallTable");
                });

            modelBuilder.Entity("RestaurantEntity.Order", b =>
                {
                    b.HasOne("RestaurantEntity.Food", "Food")
                        .WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RestaurantEntity.HallTable", "HallTable")
                        .WithMany()
                        .HasForeignKey("HallTableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("HallTable");
                });
#pragma warning restore 612, 618
        }
    }
}
