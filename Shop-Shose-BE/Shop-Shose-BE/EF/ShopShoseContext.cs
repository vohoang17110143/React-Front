using ExamToeicOnline_BackEnd_Clients.Models;
using Microsoft.EntityFrameworkCore;
using Shop_Shose_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Shose_BE.EF
{
    public class ShopShoseContext: DbContext
    {
        public ShopShoseContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Seed data
            modelBuilder.Seed();
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCar> ShoppingCars { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart_of_Product> Cart_of_Products { get; set; }
        public DbSet<Color_Size_Product> Color_Size_Products { get; set; }
        public DbSet<LoginInfo> LoginInfos { get; set; }
        public DbSet<Favorite_Product> Favorite_Products { get; set; }


    }
}
