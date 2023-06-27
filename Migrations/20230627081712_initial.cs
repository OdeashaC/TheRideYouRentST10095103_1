using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheRideYouRentST10095103_1.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarBodyType",
                columns: table => new
                {
                    CarBodyTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarBodyTypeDescription = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CarBodyT__2BA49AEBC687853F", x => x.CarBodyTypeID);
                });

            migrationBuilder.CreateTable(
                name: "CarMake",
                columns: table => new
                {
                    CarMakeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarMakeDescription = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CarMake__A125EE7C5138D4A0", x => x.CarMakeID);
                });

            migrationBuilder.CreateTable(
                name: "Driver",
                columns: table => new
                {
                    DriverID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Driver_Name = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Driver_Address = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Driver_Email = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Driver_Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Driver__F1B1CD246E2D936D", x => x.DriverID);
                });

            migrationBuilder.CreateTable(
                name: "Inspector",
                columns: table => new
                {
                    Inspector_no = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Inspector_Name = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Inspector_Email = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Inspector_Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Inspecto__F49FBEAFAAC179A4", x => x.Inspector_no);
                });

            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    Car_No = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CarMakeID = table.Column<int>(type: "int", nullable: false),
                    CarBodyTypeID = table.Column<int>(type: "int", nullable: false),
                    CarModel = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    KilometresTravelled = table.Column<int>(type: "int", nullable: false),
                    ServiceKilometres = table.Column<int>(type: "int", nullable: false),
                    Available = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Car__52362C6AE703E4E1", x => x.Car_No);
                    table.ForeignKey(
                        name: "FK__Car__CarBodyType__403A8C7D",
                        column: x => x.CarBodyTypeID,
                        principalTable: "CarBodyType",
                        principalColumn: "CarBodyTypeID");
                    table.ForeignKey(
                        name: "FK__Car__CarMakeID__3F466844",
                        column: x => x.CarMakeID,
                        principalTable: "CarMake",
                        principalColumn: "CarMakeID");
                });

            migrationBuilder.CreateTable(
                name: "Rental",
                columns: table => new
                {
                    Rental_No = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Driverid = table.Column<int>(type: "int", nullable: false),
                    Inspector_no = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Car_No = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Rental_Fee = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Rental__9D238DBDD99C8D3B", x => x.Rental_No);
                    table.ForeignKey(
                        name: "FK__Rental__Car_No__44FF419A",
                        column: x => x.Car_No,
                        principalTable: "Car",
                        principalColumn: "Car_No");
                    table.ForeignKey(
                        name: "FK__Rental__Driverid__4316F928",
                        column: x => x.Driverid,
                        principalTable: "Driver",
                        principalColumn: "DriverID");
                    table.ForeignKey(
                        name: "FK__Rental__Inspecto__440B1D61",
                        column: x => x.Inspector_no,
                        principalTable: "Inspector",
                        principalColumn: "Inspector_no");
                });

            migrationBuilder.CreateTable(
                name: "ReturnCar",
                columns: table => new
                {
                    Return_No = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverID = table.Column<int>(type: "int", nullable: false),
                    Inspector_no = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Car_No = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Rental_No = table.Column<int>(type: "int", nullable: false),
                    Return_Date = table.Column<DateTime>(type: "date", nullable: false),
                    Elapsed_Date = table.Column<int>(type: "int", nullable: true),
                    Fine = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ReturnCa__0F4F6B99A0E9E958", x => x.Return_No);
                    table.ForeignKey(
                        name: "FK__ReturnCar__Car_N__49C3F6B7",
                        column: x => x.Car_No,
                        principalTable: "Car",
                        principalColumn: "Car_No");
                    table.ForeignKey(
                        name: "FK__ReturnCar__Drive__47DBAE45",
                        column: x => x.DriverID,
                        principalTable: "Driver",
                        principalColumn: "DriverID");
                    table.ForeignKey(
                        name: "FK__ReturnCar__Inspe__48CFD27E",
                        column: x => x.Inspector_no,
                        principalTable: "Inspector",
                        principalColumn: "Inspector_no");
                    table.ForeignKey(
                        name: "FK__ReturnCar__Renta__4AB81AF0",
                        column: x => x.Rental_No,
                        principalTable: "Rental",
                        principalColumn: "Rental_No");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Car_CarBodyTypeID",
                table: "Car",
                column: "CarBodyTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Car_CarMakeID",
                table: "Car",
                column: "CarMakeID");

            migrationBuilder.CreateIndex(
                name: "IX_Rental_Car_No",
                table: "Rental",
                column: "Car_No");

            migrationBuilder.CreateIndex(
                name: "IX_Rental_Driverid",
                table: "Rental",
                column: "Driverid");

            migrationBuilder.CreateIndex(
                name: "IX_Rental_Inspector_no",
                table: "Rental",
                column: "Inspector_no");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnCar_Car_No",
                table: "ReturnCar",
                column: "Car_No");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnCar_DriverID",
                table: "ReturnCar",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnCar_Inspector_no",
                table: "ReturnCar",
                column: "Inspector_no");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnCar_Rental_No",
                table: "ReturnCar",
                column: "Rental_No");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReturnCar");

            migrationBuilder.DropTable(
                name: "Rental");

            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropTable(
                name: "Driver");

            migrationBuilder.DropTable(
                name: "Inspector");

            migrationBuilder.DropTable(
                name: "CarBodyType");

            migrationBuilder.DropTable(
                name: "CarMake");
        }
    }
}
