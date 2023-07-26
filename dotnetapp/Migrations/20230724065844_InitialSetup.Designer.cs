﻿// <auto-generated />
using System;
using Loans.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace dotnetapp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230724065844_InitialSetup")]
    partial class InitialSetup
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Loans.Models.AdminModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userRole")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("Loans.Models.DocumentModel", b =>
                {
                    b.Property<int>("documentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("documentId"), 1L, 1);

                    b.Property<string>("documentVerified")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("documenttype")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("documentupload")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("documentId");

                    b.ToTable("Document");
                });

            modelBuilder.Entity("Loans.Models.LoanApplicantModel", b =>
                {
                    b.Property<int>("loanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("loanId"), 1L, 1);

                    b.Property<decimal>("MonthlyEMI")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("applicantAadhaar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("applicantAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("applicantEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("applicantMobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("applicantName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("applicantPan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("applicantSalary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("loanAmountRequired")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("loanRepaymentMonths")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("loanStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("loantype")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("loanId");

                    b.ToTable("LoanApplicant");
                });

            modelBuilder.Entity("Loans.Models.LoginModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Login");
                });

            modelBuilder.Entity("Loans.Models.UserModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("confirmpassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userRole")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
