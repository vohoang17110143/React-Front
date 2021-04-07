using Microsoft.EntityFrameworkCore;
using Shop_Shose_BE.Common;
using Shop_Shose_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace Shop_Shose_BE.EF
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            DateTime dateTime = DateTime.Now;
            var US1 = Guid.NewGuid();
            var US2 = Guid.NewGuid();
            modelBuilder.Entity<Account>().HasData(
                new Account()
                {
                    AccountId = 1,
                    Username = "thanhnhan",
                    Password =BC.HashPassword("12345678"),
                    DateCreate = dateTime,
                    Status = Status.Active.ToString(),
                    CustomerId = US1
                },
                new Account()
                {
                    AccountId = 2,
                    Username = "Vohoang",
                    Password = BC.HashPassword("12345678"),
                    DateCreate = dateTime,
                    Status = Status.Active.ToString(),
                    CustomerId=US2
                }
             );
            modelBuilder.Entity<Customer>().HasData(
                new Customer()
                {
                    CustomerId = US1,
                    Name = "Nguyễn Thanh Nhân",
                    BirthDay = dateTime,
                    PhoneNumber = "0363677482",
                    Email = "thanhnhan02677@gmail",
                    Image = ""
                   
                }, new Customer()
                {
                    CustomerId = US2,
                    Name = "Nguyễn Võ Hoàng",
                    BirthDay = dateTime,
                    PhoneNumber = "0773829123",
                    Email = "Vohoang@gmail",
                    Image = ""
                   
                }
            );
            modelBuilder.Entity<Brand>().HasData(
                new Brand()
                {
                    BrandId = 1,
                    Name = "NUSD"
                },
                new Brand()
                {
                    BrandId = 2,
                    Name = "NUDE"
                },
                new Brand()
                {
                    BrandId = 3,
                    Name = "NUTT"
                },

                new Brand()
                {
                    BrandId = 4,
                    Name = "NUCG"
                }
            );
            modelBuilder.Entity<Size>().HasData(
                new Size()
                {
                    SizeId = 1,
                    SizeNumber = 35
                },
                new Size()
                {
                    SizeId = 2,
                    SizeNumber = 36
                },
                new Size()
                {
                    SizeId = 3,
                    SizeNumber = 37
                },
                new Size()
                {
                    SizeId = 4,
                    SizeNumber = 38
                },
                new Size()
                {
                    SizeId = 5,
                    SizeNumber = 39
                },
                new Size()
                {
                    SizeId = 6,
                    SizeNumber = 40
                }
            );
            modelBuilder.Entity<Color>().HasData(
                new Color()
                {
                    ColorId = 10,
                    Name = "Đen"
                },
                new Color()
                {
                    ColorId = 11,
                    Name = "Trắng"
                },
                new Color()
                {
                    ColorId = 12,
                    Name = "Kem"
                },
                new Color()
                {
                    ColorId = 13,
                    Name = "Xám"
                },
                new Color()
                {
                    ColorId = 14,
                    Name = "Vàng"
                }
            );
            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    CategoryId =1,
                    Name="Giày thể thao nam"
                },
                new Category()
                {
                    CategoryId = 2,
                    Name = "Giày thể thao nữ"
                },
                new Category()
                {
                    CategoryId = 3,
                    Name = "Giày thể cao gót"
                }

            );
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    ProductId=1,
                    Name= "Dép nam MWC NADE- 7675",
                    Price= 195.000,
                    Sex= Sex.Male.ToString(),
                    Description = "- Dép quai ngang với chất liệu da cao cấp kết hợp đế cao su êm nhẹ " +
                    "chống trơn trượt giúp bạn Nam luôn thoải mái, tự tin khi đi chơi, dạo phố," +
                    " đi làm nơi công sở hay đi dự tiệc.",
                    BrandId=2,
                    Image=null,
                    CategoryId=1
                },
                new Product()
                {
                    ProductId = 2,
                    Name = "Giày thể thao nữ MWC NUTT- 0523",
                    Price = 295.000,
                    Sex = Sex.Female.ToString(),
                    Description = " Đế cao su mềm, êm ái giúp bạn cảm thấy dễ chịu khi di chuyển trong thời gian dài.",
                    BrandId = 3,
                    Image = null,
                    CategoryId = 2
                }

            );
            
        }
    }
}
