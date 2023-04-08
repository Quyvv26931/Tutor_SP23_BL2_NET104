using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tutor_SP23_BL2_NET104.Migrations
{
    public partial class Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCategory = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ReducedPrice = table.Column<double>(type: "float", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Category_IdCategory",
                        column: x => x.IdCategory,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdRole = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Role_IdRole",
                        column: x => x.IdRole,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bill_User_IdUser",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartDetails",
                columns: table => new
                {
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdProduct = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartDetails", x => new { x.IdProduct, x.IdUser });
                    table.ForeignKey(
                        name: "FK_CartDetails_Product_IdProduct",
                        column: x => x.IdProduct,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartDetails_User_IdUser",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductBill",
                columns: table => new
                {
                    IdBill = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdProduct = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ReducedPrice = table.Column<double>(type: "float", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBill", x => new { x.IdProduct, x.IdBill });
                    table.ForeignKey(
                        name: "FK_ProductBill_Bill_IdBill",
                        column: x => x.IdBill,
                        principalTable: "Bill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductBill_Product_IdProduct",
                        column: x => x.IdProduct,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedTime", "Name", "Status" },
                values: new object[,]
                {
                    { new Guid("1871ad42-6960-473d-aa75-aabc6edf5014"), new DateTime(2023, 4, 8, 13, 53, 17, 590, DateTimeKind.Local).AddTicks(1292), "Tranh Trừu Tượng", 0 },
                    { new Guid("2871ad42-6960-473d-aa75-aabc6edf5014"), new DateTime(2023, 4, 8, 13, 53, 17, 590, DateTimeKind.Local).AddTicks(1295), "Tranh Tối Giản", 0 },
                    { new Guid("3871ad42-6960-473d-aa75-aabc6edf5014"), new DateTime(2023, 4, 8, 13, 53, 17, 590, DateTimeKind.Local).AddTicks(1296), "Tranh Sơn Dầu", 0 },
                    { new Guid("4871ad42-6960-473d-aa75-aabc6edf5014"), new DateTime(2023, 4, 8, 13, 53, 17, 590, DateTimeKind.Local).AddTicks(1298), "Tranh Tĩnh Vật", 0 },
                    { new Guid("5871ad42-6960-473d-aa75-aabc6edf5014"), new DateTime(2023, 4, 8, 13, 53, 17, 590, DateTimeKind.Local).AddTicks(1299), "Khác", 0 }
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedTime", "Name", "Status" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 4, 8, 13, 53, 17, 590, DateTimeKind.Local).AddTicks(1149), "Customer", 0 },
                    { new Guid("9871ad42-6960-473d-aa75-aabc6edf5014"), new DateTime(2023, 4, 8, 13, 53, 17, 590, DateTimeKind.Local).AddTicks(1136), "Admin", 0 }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Amount", "CreatedTime", "Description", "IdCategory", "Image", "Name", "Price", "ReducedPrice", "Status" },
                values: new object[,]
                {
                    { new Guid("1071ad42-6960-473d-aa75-aabc6edf5014"), 100, new DateTime(2023, 4, 8, 13, 53, 17, 590, DateTimeKind.Local).AddTicks(1399), "TRANH TREO TƯỜNG TG02", new Guid("1871ad42-6960-473d-aa75-aabc6edf5014"), "https://tuongvip.vn/public/uploads/products/57505/tranh-treo-tuong-nghe-thuat-hinh-hoc-toi-gian-02.jpg", "TRANH TREO TƯỜNG TG02", 600000.0, 500000.0, 0 },
                    { new Guid("1171ad42-6960-473d-aa75-aabc6edf5014"), 100, new DateTime(2023, 4, 8, 13, 53, 17, 590, DateTimeKind.Local).AddTicks(1396), "TRANH TREO TƯỜNG TG01", new Guid("1871ad42-6960-473d-aa75-aabc6edf5014"), "https://tuongvip.vn/public/uploads/products/57505/tranh-treo-tuong-nghe-thuat-hinh-hoc-toi-gian-02.jpg", "TRANH TREO TƯỜNG TG01", 600000.0, 500000.0, 0 },
                    { new Guid("4071ad42-6960-473d-aa75-aabc6edf5014"), 100, new DateTime(2023, 4, 8, 13, 53, 17, 590, DateTimeKind.Local).AddTicks(1405), "TRANH TREO TƯỜNG SD02", new Guid("4871ad42-6960-473d-aa75-aabc6edf5014"), "https://tuongvip.vn/public/uploads/products/59925/tranh-treo-tuong-son-dau-phong-canh-lang-que-dep-11.jpg", "TRANH TREO TƯỜNG SD02", 600000.0, 500000.0, 0 },
                    { new Guid("4471ad42-6960-473d-aa75-aabc6edf5014"), 100, new DateTime(2023, 4, 8, 13, 53, 17, 590, DateTimeKind.Local).AddTicks(1402), "TRANH TREO TƯỜNG SD01", new Guid("4871ad42-6960-473d-aa75-aabc6edf5014"), "https://tuongvip.vn/public/uploads/products/59925/tranh-treo-tuong-son-dau-phong-canh-lang-que-dep-11.jpg", "TRANH TREO TƯỜNG SD01", 600000.0, 500000.0, 0 },
                    { new Guid("5071ad42-6960-473d-aa75-aabc6edf5014"), 100, new DateTime(2023, 4, 8, 13, 53, 17, 590, DateTimeKind.Local).AddTicks(1410), "TRANH TREO TƯỜNG TV02", new Guid("5871ad42-6960-473d-aa75-aabc6edf5014"), "https://tuongvip.vn/public/uploads/products/36271/tranh-treo-tuong-binh-hoa-nghe-thuat-10-1.jpg", "TRANH TREO TƯỜNG TV02", 600000.0, 500000.0, 0 },
                    { new Guid("5571ad42-6960-473d-aa75-aabc6edf5014"), 100, new DateTime(2023, 4, 8, 13, 53, 17, 590, DateTimeKind.Local).AddTicks(1407), "TRANH TREO TƯỜNG TV01", new Guid("5871ad42-6960-473d-aa75-aabc6edf5014"), "https://tuongvip.vn/public/uploads/products/36271/tranh-treo-tuong-binh-hoa-nghe-thuat-10-1.jpg", "TRANH TREO TƯỜNG TV01", 600000.0, 500000.0, 0 },
                    { new Guid("9071ad42-6960-473d-aa75-aabc6edf5014"), 100, new DateTime(2023, 4, 8, 13, 53, 17, 590, DateTimeKind.Local).AddTicks(1393), "TRANH TREO TƯỜNG TT02", new Guid("1871ad42-6960-473d-aa75-aabc6edf5014"), "https://tuongvip.vn/public/uploads/products/44768/tranh-treo-tuong-nghe-thuat-truu-tuong.jpg", "TRANH TREO TƯỜNG TT02", 600000.0, 500000.0, 0 },
                    { new Guid("9971ad42-6960-473d-aa75-aabc6edf5014"), 100, new DateTime(2023, 4, 8, 13, 53, 17, 590, DateTimeKind.Local).AddTicks(1389), "TRANH TREO TƯỜNG TT01", new Guid("1871ad42-6960-473d-aa75-aabc6edf5014"), "https://tuongvip.vn/public/uploads/products/44768/tranh-treo-tuong-nghe-thuat-truu-tuong.jpg", "TRANH TREO TƯỜNG TT01", 600000.0, 500000.0, 0 }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedTime", "Email", "FullName", "IdRole", "Password", "Status", "Username" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2023, 4, 8, 13, 53, 17, 590, DateTimeKind.Local).AddTicks(1276), "nlhg090602@gmail.com", "nlhg090602", new Guid("00000000-0000-0000-0000-000000000000"), "nlhg090602", 0, "nlhg090602" },
                    { new Guid("9871ad42-6960-473d-aa75-aabc6edf5014"), new DateTime(2023, 4, 8, 13, 53, 17, 590, DateTimeKind.Local).AddTicks(1275), "giangnlh.forworking@gmail.com", "giangnlh.forworking", new Guid("00000000-0000-0000-0000-000000000000"), "giangnlh.forworking", 0, "giangnlh.forworking" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bill_IdUser",
                table: "Bill",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_IdUser",
                table: "CartDetails",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Product_IdCategory",
                table: "Product",
                column: "IdCategory");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBill_IdBill",
                table: "ProductBill",
                column: "IdBill");

            migrationBuilder.CreateIndex(
                name: "IX_User_IdRole",
                table: "User",
                column: "IdRole");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartDetails");

            migrationBuilder.DropTable(
                name: "ProductBill");

            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
