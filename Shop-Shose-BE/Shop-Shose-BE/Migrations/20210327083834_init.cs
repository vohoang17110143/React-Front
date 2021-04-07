using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop_Shose_BE.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    ColorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.ColorId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    BirthDay = table.Column<DateTime>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "LoginInfos",
                columns: table => new
                {
                    UserInfoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Create = table.Column<DateTime>(nullable: false),
                    Update = table.Column<DateTime>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    EmailId = table.Column<string>(nullable: false),
                    Username = table.Column<string>(maxLength: 16, nullable: false),
                    Password = table.Column<string>(maxLength: 255, nullable: false),
                    IsMailConfirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginInfos", x => x.UserInfoId);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Display = table.Column<bool>(nullable: false),
                    DatePost = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    SizeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SizeNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.SizeId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    BrandId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    DateCreate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    CustomerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favorite_Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(nullable: false),
                    CustomerId1 = table.Column<Guid>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false),
                    NumberClick = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorite_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorite_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorite_Products_Customers_CustomerId1",
                        column: x => x.CustomerId1,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCars",
                columns: table => new
                {
                    ShoppingCarId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalCost = table.Column<double>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCars", x => x.ShoppingCarId);
                    table.ForeignKey(
                        name: "FK_ShoppingCars_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Color_Size_Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    ColorId = table.Column<int>(nullable: false),
                    SizeId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color_Size_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Color_Size_Products_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "ColorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Color_Size_Products_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Color_Size_Products_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "SizeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cart_of_Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(nullable: false),
                    DateAdd = table.Column<DateTime>(nullable: false),
                    ShoppingCarId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart_of_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_of_Products_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cart_of_Products_ShoppingCars_ShoppingCarId",
                        column: x => x.ShoppingCarId,
                        principalTable: "ShoppingCars",
                        principalColumn: "ShoppingCarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOrder = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ShoppingCarId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_ShoppingCars_ShoppingCarId",
                        column: x => x.ShoppingCarId,
                        principalTable: "ShoppingCars",
                        principalColumn: "ShoppingCarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "BrandId", "Name" },
                values: new object[,]
                {
                    { 1, "NUSD" },
                    { 2, "NUDE" },
                    { 3, "NUTT" },
                    { 4, "NUCG" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Giày thể thao nam" },
                    { 2, "Giày thể thao nữ" },
                    { 3, "Giày thể cao gót" }
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "ColorId", "Name" },
                values: new object[,]
                {
                    { 14, "Vàng" },
                    { 13, "Xám" },
                    { 12, "Kem" },
                    { 11, "Trắng" },
                    { 10, "Đen" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "BirthDay", "Email", "Image", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("8e5c9bc0-e0f4-459d-8e72-5833800c5b7b"), new DateTime(2021, 3, 27, 15, 38, 33, 73, DateTimeKind.Local).AddTicks(4625), "thanhnhan02677@gmail", "", "Nguyễn Thanh Nhân", "0363677482" },
                    { new Guid("9f92e5f9-14c0-4989-8945-02f87c701d10"), new DateTime(2021, 3, 27, 15, 38, 33, 73, DateTimeKind.Local).AddTicks(4625), "Vohoang@gmail", "", "Nguyễn Võ Hoàng", "0773829123" }
                });

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "SizeId", "SizeNumber" },
                values: new object[,]
                {
                    { 1, 35 },
                    { 2, 36 },
                    { 3, 37 },
                    { 4, 38 },
                    { 5, 39 },
                    { 6, 40 }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountId", "CustomerId", "DateCreate", "Password", "Status", "Username" },
                values: new object[,]
                {
                    { 1, new Guid("8e5c9bc0-e0f4-459d-8e72-5833800c5b7b"), new DateTime(2021, 3, 27, 15, 38, 33, 73, DateTimeKind.Local).AddTicks(4625), "$2a$11$ju7c5RpEcWasRtEYelz2leTFtKQP.WRBb1TXnfNpHqyoApRhS9le2", "Active", "thanhnhan" },
                    { 2, new Guid("9f92e5f9-14c0-4989-8945-02f87c701d10"), new DateTime(2021, 3, 27, 15, 38, 33, 73, DateTimeKind.Local).AddTicks(4625), "$2a$11$aoDJkLvG0FjnTe42.NOONeUJfvncM03y4KwJYS8utstU6NPRfB5YC", "Active", "Vohoang" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "BrandId", "CategoryId", "Description", "Image", "Name", "Price", "Sex" },
                values: new object[,]
                {
                    { 1, 2, 1, "- Dép quai ngang với chất liệu da cao cấp kết hợp đế cao su êm nhẹ chống trơn trượt giúp bạn Nam luôn thoải mái, tự tin khi đi chơi, dạo phố, đi làm nơi công sở hay đi dự tiệc.", null, "Dép nam MWC NADE- 7675", 195.0, "Male" },
                    { 2, 3, 2, " Đế cao su mềm, êm ái giúp bạn cảm thấy dễ chịu khi di chuyển trong thời gian dài.", null, "Giày thể thao nữ MWC NUTT- 0523", 295.0, "Female" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CustomerId",
                table: "Accounts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_of_Products_ProductId",
                table: "Cart_of_Products",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_of_Products_ShoppingCarId",
                table: "Cart_of_Products",
                column: "ShoppingCarId");

            migrationBuilder.CreateIndex(
                name: "IX_Color_Size_Products_ColorId",
                table: "Color_Size_Products",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Color_Size_Products_ProductId",
                table: "Color_Size_Products",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Color_Size_Products_SizeId",
                table: "Color_Size_Products",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_Products_CategoryId",
                table: "Favorite_Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_Products_CustomerId1",
                table: "Favorite_Products",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShoppingCarId",
                table: "Orders",
                column: "ShoppingCarId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCars_CustomerId",
                table: "ShoppingCars",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Cart_of_Products");

            migrationBuilder.DropTable(
                name: "Color_Size_Products");

            migrationBuilder.DropTable(
                name: "Favorite_Products");

            migrationBuilder.DropTable(
                name: "LoginInfos");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "ShoppingCars");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
