using Loans.Models;
using Microsoft.EntityFrameworkCore;


namespace Loans.Context
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 

        }
        public DbSet<UserModel> User { get; set; }
        public DbSet<LoginModel> Login { get; set; }
        public DbSet<LoanApplicantModel> LoanApplicant { get; set; }
        public DbSet<DocumentModel> Document { get; set; }
        public DbSet<AdminModel> Admin { get; set; }
      

    }
}