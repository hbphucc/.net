using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitCloudDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCategories",
                columns: table => new
                {
                    categoryID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    categoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategories", x => x.categoryID);
                });

            migrationBuilder.CreateTable(
                name: "tblUsers",
                columns: table => new
                {
                    userID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    fullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUsers", x => x.userID);
                });

            migrationBuilder.CreateTable(
                name: "tblClothes",
                columns: table => new
                {
                    productID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    productName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    categoryID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(12,0)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblClothes", x => x.productID);
                    table.ForeignKey(
                        name: "FK_tblClothes_tblCategories_categoryID",
                        column: x => x.categoryID,
                        principalTable: "tblCategories",
                        principalColumn: "categoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblOrders",
                columns: table => new
                {
                    orderID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    userID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    totalAmount = table.Column<decimal>(type: "decimal(14,0)", nullable: false),
                    fullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    paymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOrders", x => x.orderID);
                    table.ForeignKey(
                        name: "FK_tblOrders_tblUsers_userID",
                        column: x => x.userID,
                        principalTable: "tblUsers",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblProductSizes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    size = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductSizes", x => x.id);
                    table.ForeignKey(
                        name: "FK_tblProductSizes_tblClothes_productID",
                        column: x => x.productID,
                        principalTable: "tblClothes",
                        principalColumn: "productID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblOrderDetails",
                columns: table => new
                {
                    detailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    orderID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    productID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    productName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(12,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOrderDetails", x => x.detailID);
                    table.ForeignKey(
                        name: "FK_tblOrderDetails_tblClothes_productID",
                        column: x => x.productID,
                        principalTable: "tblClothes",
                        principalColumn: "productID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblOrderDetails_tblOrders_orderID",
                        column: x => x.orderID,
                        principalTable: "tblOrders",
                        principalColumn: "orderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblClothes_categoryID",
                table: "tblClothes",
                column: "categoryID");

            migrationBuilder.CreateIndex(
                name: "IX_tblOrderDetails_orderID",
                table: "tblOrderDetails",
                column: "orderID");

            migrationBuilder.CreateIndex(
                name: "IX_tblOrderDetails_productID",
                table: "tblOrderDetails",
                column: "productID");

            migrationBuilder.CreateIndex(
                name: "IX_tblOrders_userID",
                table: "tblOrders",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_tblProductSizes_productID_size",
                table: "tblProductSizes",
                columns: new[] { "productID", "size" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblOrderDetails");

            migrationBuilder.DropTable(
                name: "tblProductSizes");

            migrationBuilder.DropTable(
                name: "tblOrders");

            migrationBuilder.DropTable(
                name: "tblClothes");

            migrationBuilder.DropTable(
                name: "tblUsers");

            migrationBuilder.DropTable(
                name: "tblCategories");
        }
    }
}
