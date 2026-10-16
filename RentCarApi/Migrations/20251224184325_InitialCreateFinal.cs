using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 

namespace RentCarApi.Migrations
{
    
    public partial class InitialCreateFinal : Migration
    {
       
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MsCars",
                columns: table => new
                {
                    Car_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    License_plate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number_of_car_seats = table.Column<int>(type: "int", nullable: false),
                    Transmission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price_per_day = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsCars", x => x.Car_id);
                });

            migrationBuilder.CreateTable(
                name: "MsCustomers",
                columns: table => new
                {
                    Customer_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Driver_license_number = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsCustomers", x => x.Customer_id);
                });

            migrationBuilder.CreateTable(
                name: "MsEmployees",
                columns: table => new
                {
                    Employee_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone_number = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsEmployees", x => x.Employee_id);
                });

            migrationBuilder.CreateTable(
                name: "MsCarImages",
                columns: table => new
                {
                    Image_car_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Car_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Image_link = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsCarImages", x => x.Image_car_id);
                    table.ForeignKey(
                        name: "FK_MsCarImages_MsCars_Car_id",
                        column: x => x.Car_id,
                        principalTable: "MsCars",
                        principalColumn: "Car_id");
                });

            migrationBuilder.CreateTable(
                name: "TrRentals",
                columns: table => new
                {
                    Rental_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Rental_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Return_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total_price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Payment_status = table.Column<int>(type: "int", nullable: false),
                    Customer_id = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Car_id = table.Column<string>(type: "nvarchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrRentals", x => x.Rental_id);
                    table.ForeignKey(
                        name: "FK_TrRentals_MsCars_Car_id",
                        column: x => x.Car_id,
                        principalTable: "MsCars",
                        principalColumn: "Car_id");
                    table.ForeignKey(
                        name: "FK_TrRentals_MsCustomers_Customer_id",
                        column: x => x.Customer_id,
                        principalTable: "MsCustomers",
                        principalColumn: "Customer_id");
                });

            migrationBuilder.CreateTable(
                name: "TrMaintenances",
                columns: table => new
                {
                    Maintenance_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Maintenance_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Car_id = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Employee_id = table.Column<string>(type: "nvarchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrMaintenances", x => x.Maintenance_id);
                    table.ForeignKey(
                        name: "FK_TrMaintenances_MsCars_Car_id",
                        column: x => x.Car_id,
                        principalTable: "MsCars",
                        principalColumn: "Car_id");
                    table.ForeignKey(
                        name: "FK_TrMaintenances_MsEmployees_Employee_id",
                        column: x => x.Employee_id,
                        principalTable: "MsEmployees",
                        principalColumn: "Employee_id");
                });

            migrationBuilder.CreateTable(
                name: "LtPayments",
                columns: table => new
                {
                    Payment_id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Payment_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Payment_method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rental_id = table.Column<string>(type: "nvarchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LtPayments", x => x.Payment_id);
                    table.ForeignKey(
                        name: "FK_LtPayments_TrRentals_Rental_id",
                        column: x => x.Rental_id,
                        principalTable: "TrRentals",
                        principalColumn: "Rental_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MsCars",
                columns: new[] { "Car_id", "License_plate", "Model", "Name", "Number_of_car_seats", "Price_per_day", "Status", "Transmission", "Year" },
                values: new object[,]
                {
                    { "C001", "B 1234 ABC", "AT", "Daihatsu Ayla", 5, 300000m, 1, "Automatic", 2019 },
                    { "C002", "B 5678 DEF", "GL", "Suzuki Ertiga", 7, 450000m, 1, "Automatic", 2019 },
                    { "C003", "B 9012 GHI", "AT", "Honda HR-V", 5, 900000m, 1, "Automatic", 2023 }
                });

            migrationBuilder.InsertData(
                table: "MsCustomers",
                columns: new[] { "Customer_id", "Address", "Driver_license_number", "Email", "Name", "Password", "Phone_number" },
                values: new object[] { "CUST001", "Jl. Sudirman No. 1", "", "putra44@gmail.com", "Putra", "password123", "08123456789" });

            migrationBuilder.InsertData(
                table: "MsCarImages",
                columns: new[] { "Image_car_id", "Car_id", "Image_link" },
                values: new object[,]
                {
                    { "IMG001", "C001", "https://www.shutterstock.com/image-photo/silver-hatchback-daihatsu-ayla-12r-260nw-2404487523.jpg" },
                    { "IMG002", "C002", "https://thumbs.dreamstime.com/b/bronze-suzuki-ertiga-gl-street-medan-north-sumatra-indonesia-aug-suzuki-ertiga-gl-398161807.jpg" },
                    { "IMG003", "C003", "https://cdn.jdpower.com/JDP_2023%20Honda%20HR-V%20EX-L%20Nordic%20Green%20Pearl%20Front%20Quarter%20View.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LtPayments_Rental_id",
                table: "LtPayments",
                column: "Rental_id");

            migrationBuilder.CreateIndex(
                name: "IX_MsCarImages_Car_id",
                table: "MsCarImages",
                column: "Car_id");

            migrationBuilder.CreateIndex(
                name: "IX_TrMaintenances_Car_id",
                table: "TrMaintenances",
                column: "Car_id");

            migrationBuilder.CreateIndex(
                name: "IX_TrMaintenances_Employee_id",
                table: "TrMaintenances",
                column: "Employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_TrRentals_Car_id",
                table: "TrRentals",
                column: "Car_id");

            migrationBuilder.CreateIndex(
                name: "IX_TrRentals_Customer_id",
                table: "TrRentals",
                column: "Customer_id");
        }

        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LtPayments");

            migrationBuilder.DropTable(
                name: "MsCarImages");

            migrationBuilder.DropTable(
                name: "TrMaintenances");

            migrationBuilder.DropTable(
                name: "TrRentals");

            migrationBuilder.DropTable(
                name: "MsEmployees");

            migrationBuilder.DropTable(
                name: "MsCars");

            migrationBuilder.DropTable(
                name: "MsCustomers");
        }
    }
}
