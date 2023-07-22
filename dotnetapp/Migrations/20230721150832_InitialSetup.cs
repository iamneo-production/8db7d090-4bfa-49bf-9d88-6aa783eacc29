using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetapp.Migrations
{
    public partial class InitialSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    documentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    documenttype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    documentupload = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    documentVerified = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.documentId);
                });

            migrationBuilder.CreateTable(
                name: "LoanApplicant",
                columns: table => new
                {
                    loanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    loantype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    applicantName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    applicantAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    applicantMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    applicantEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    applicantAadhaar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    applicantPan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    applicantSalary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    loanAmountRequired = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    loanRepaymentMonths = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthlyEMI = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    loanStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanApplicant", x => x.loanId);
                });

            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    confirmpassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "LoanApplicant");

            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
