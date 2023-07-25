using Azure.Core;
using Loans.Data;
using Loans.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System;

//using EMI.Data;
//using EMI.Model;

namespace Admin.Controller
{
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        /* private List<LoanApplicantModel> loans;
         public AdminController()
         {
             loans = new List<LoanApplicantModel>();
         }


 public class LoanDbContext : DbContext
     {
         public DbSet<LoanApplicantModel> LoanApplicant { get; set; }
         public DbSet<DocumentModel> Documents { get; set; }
         public DbSet<LoanApplicantModel> LoanApplicants  { get; set; }

         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
             optionsBuilder.UseSqlServer("Server=DESKTOP-HMARLB3"); // Replace with your actual database connection string
         }
     }



         [HttpPost("admin/approveLoan/{loanId}")]
         public void ApproveBusinessLoan(int loanId)
         {
             LoanApplicantModel loan = _context.LoanApplicant.FirstOrDefault(l => l.loanId == loanId);

             if (loan != null && IsEligibleForBusinessLoan(loan))
             {
                 return BadRequest();

             }
                 return Ok();

             }
         }

         public void VerifyDocuments(int documentId)
         {
             DocumentModel document = _context.Document.FirstOrDefault(d => d.documentId == documentId);

             if (document != null)
                {
                 // Perform document verification logic her
                     // Document is valid
                     Console.WriteLine("Document with ID " + documentId + " has been verified.");
               }
             else

                     // Document is not valid
                     Console.WriteLine("Document with ID " + documentId + " verification failed.");


             else
             {
                 Console.WriteLine("Document with ID " + documentId + " does not exist.");
             }
         }



         public void EditLoan(int loanId)
             {
                 LoanApplicantModel loan = loans.Find(l => l.loanId == loanId);
                 if (loan != null)
                 {
                     // Perform the loan editing logic here
                     Console.WriteLine("Loan with ID " + loanId + " has been edited.");
                 }
                 else
                 {
                     Console.WriteLine("Loan with ID " + loanId + " does not exist.");
                 }
             }
         public void DeleteLoan(int loanId)
         {
             LoanApplicantModel loan = loans.Find(l => l.loanId == loanId);
             if (loan != null)
             {
                 loans.Remove(loan);
                 Console.WriteLine("Loan with ID " + loanId + " has been deleted.");
             }
             else
             {
                 Console.WriteLine("Loan with ID " + loanId + " does not exist.");
             }
         }
         public void GenerateSchedule(int loanId)
         {
             LoanApplicantModel loan = loans.Find(l => l.loanId == loanId);
             if (loan != null)
             {
                 // Perform the repayment schedule generation logic here
                 int scheduleId = GenerateUniqueScheduleId();
                 LoanApplicantModel.Add(loanId);
                 Console.WriteLine("Repayment schedule generated for loan with ID " + loanId + ".");
             }
             else
             {
                 Console.WriteLine("Loan with ID " + loanId + " does not exist.");
             }
         }
         public void EditSchedule(int loanId)
         {
             int scheduleId = repaymentSchedules.Find(s => s == loanId);
             if (scheduleId != 0)
             {
                 // Perform the repayment schedule editing logic here
                 Console.WriteLine("Repayment schedule for loan with ID " + loanId + " has been edited.");
             }
             else
             {
                 Console.WriteLine("Repayment schedule with ID " + loanId + " does not exist.");
             }
         }

         // Method to delete the repayment schedule
         public void DeleteSchedule(int loanId)
         {
             int scheduleId = repaymentSchedules.Find(s => s == loanId);
             if (scheduleId != 0)
             {
                 repaymentSchedules.Remove(scheduleId);
                 Console.WriteLine("Repayment schedule with ID " + loanId + " has been deleted.");
             }
             else
             {
                 Console.WriteLine("Repayment schedule with ID " + loanId + " does not exist.");
             }
         }








         private bool IsEligibleForBusinessLoan(LoanApplicantModel loan)
         {
             // Perform eligibility checks here based on the loan data
             // Example: LoanAmountRequired <= 5000000
             int loanAmount = int.Parse(loan.loanAmountRequired);

             if (loanAmount <= 5000000)
             {
                 return true;
             }
             else
             {
                 return false;
             }
         }
     }
 }*/
        [HttpPost("admin/generateRepaymentSchedule/{loanId}")]
        public Dictionary<int, decimal> GenerateSchedule(int loanId)
        {
            LoanApplicantModel loan = _context.LoanApplicant.FirstOrDefault(l => l.loanId == loanId);

            if (loan == null)
            {
                return null;
            }

            decimal loanAmount = Convert.ToDecimal(loan.loanAmountRequired);
            decimal interestRate = 0.05m;
            int loanTermInMonths = Convert.ToInt32(loan.loanRepaymentMonths);


            Dictionary<int, decimal> schedule = new Dictionary<int, decimal>();
            decimal monthlyInterestRate = interestRate / 12;

            decimal monthlyInstallment = (loanAmount * monthlyInterestRate) /
                                         (1 - (decimal)Math.Pow(1 + (double)monthlyInterestRate, -loanTermInMonths));


            for (int month = 1; month <= loanTermInMonths; month++)
            {
                decimal roundedMonthlyInstallment = Math.Round(monthlyInstallment, 0);
                schedule.Add(month, roundedMonthlyInstallment);
            }

            return schedule;
        }






        [HttpPost("admin/approveLoan")]
        public async Task<IActionResult> ApproveLoan([FromBody] LoanApplicantModel loanobj)
        {
            if (loanobj == null)
            {
                return BadRequest();
            }

            if (!int.TryParse(loanobj.applicantSalary, out int salary) || salary < 50000)
            {
                return BadRequest("Applicant's salary is insufficient for loan approval");
            }

            bool documentsVerified = VerifyAllDocuments();

            if (!documentsVerified)
            {
                return BadRequest("Documents verification failed");
            }


            return Ok(new
            {
                Message = "Loan approved"
            });
        }

        private bool VerifyAllDocuments()
        {
            if (!AreAllDocumentsVerified())
            {
                return false;
            }

            return true;
        }

        private bool AreAllDocumentsVerified()
        {

            foreach (var document in _context.Document)
            {
                if (!document.Verified)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

/* [HttpPut("admin/verifyDocuments/{documentId}")]
 public async Task<IActionResult> VerifyDocuments(int documentId, [FromBody] DocumentModel verifyDocument)
 {
     if (verifyDocument == null || documentId != verifyDocument.documentId)
     {
         return BadRequest();
     }

     var document = await _context.Document.FindAsync(documentId);

     if (document == null)
     {
         return NotFound();
     }

     document.Verified = true;

     await _context.SaveChangesAsync();

     return Ok(new
     {
         Message = "Document verified successfully"
     });
 }*/





/*  [HttpPut("admin/editRepaymentSchedule/{loanId}")]
  public async Task<IActionResult> EditRepaymentSchedule(int loanId, [FromBody] Dictionary<string, decimal> modifiedSchedule)
  {
      LoanApplicantModel loan = await _context.LoanApplicant.FindAsync(loanId);

      if (loan == null)
      {
          return NotFound();
      }

      JsonObject jsonObject = new JsonObject(modifiedSchedule);
      loan.loanRepaymentMonths = jsonObject.ToString();

      _context.Update(loan);
      await _context.SaveChangesAsync();

      return Ok(new
      {
          Message = "Repayment schedule updated"
      });
  }




[HttpDelete("admin/deleteRepaymentSchedule/{loanId}")]
public async Task<IActionResult> DeletePaymentSchedule(int loanId)
{
    LoanApplicantModel loanApplicant = await _context.LoanApplicant.FindAsync(loanId);

    if (loanApplicant == null)
    {
        return NotFound();
    }

    _context.RemoveRange(loanApplicant.loanRepaymentMonths);

    await _context.SaveChangesAsync();

    return Ok(new
    {
        Message = "Payment schedule deleted"
    });
}

/* [HttpGet("admin/repaymentschedule/{loanId}")]
public async Task<IActionResult> GetRepaymentSchedule(int loanId)
{
    var userprofile = await _context.LoanApplicant.FirstOrDefaultAsync(p => p.loanId == loanId);

    if (userprofile == null)
    {
        return NotFound(new
        {
            Message = "No user found"
        });
    }

    string loanAmountRequired = amt.loanAmountRequired;
    string loanRepayMonths = amt.loanRepaymentMonths;
    string salary = amt.applicantSalary;

    decimal loanAmount = Convert.ToDecimal(loanAmountRequired);
    int loanMonths = Convert.ToInt32(loanRepayMonths);
    decimal applicantsalary = Convert.ToDecimal(salary);

    decimal totalRepaymentAmount = Convert.ToDecimal(loanMonths * applicantsalary);
    decimal interestAmount = Convert.ToDecimal(totalRepaymentAmount - loanAmount);
    decimal monthlyInterestRate = ((interestAmount / loanAmount) / loanMonths) / 100;
    decimal emi = loanAmount * monthlyInterestRate *
                 (decimal)Math.Pow((double)(1 + monthlyInterestRate), loanMonths) /
                 ((decimal)Math.Pow((double)(1 + monthlyInterestRate), loanMonths) - 1);

    LoanApplicantModel emiModel = new LoanApplicantModel
    {
       // LoanApplicantId = loanId,
        MonthlyEMI = emi
    };

    await _context.LoanApplicant.AddAsync(emiModel);
    await _context.SaveChangesAsync();

    return Ok(emiModel);
}




/*[HttpPost("admin/RepaymentSchedule/{loanId}")]
public async Task<IActionResult> Create([FromBody] LoanApplicantModel amt)
{
    string loanAmountRequired = amt.loanAmountRequired;
    string loanRepayMonths = amt.loanRepaymentMonths;
    string salary = amt.applicantSalary;

    decimal loanAmount = Convert.ToDecimal(loanAmountRequired);
    int loanMonths = Convert.ToInt32(loanRepayMonths);
    decimal applicantsalary = Convert.ToDecimal(salary);

    decimal totalRepaymentAmount = loanMonths * applicantsalary;
    decimal interestAmount = totalRepaymentAmount - loanAmount;
    decimal monthlyInterestRate = (interestAmount / loanAmount) / loanMonths / 100;
    decimal emi = loanAmount * monthlyInterestRate *
                  (decimal)Math.Pow(1 + (double)monthlyInterestRate, loanMonths) /
                  ((decimal)Math.Pow(1 + (double)monthlyInterestRate, loanMonths) - 1);

    amt.MonthlyEMI = emi;

    // Set the relationship between EmiModel and LoanApplicantMode

    await _context.LoanApplicant.AddAsync(amt);
    await _context.SaveChangesAsync();

    return Ok(amt);
}*/


/* [HttpGet("loanId:int")]
         public ActionResult<EmiModel> GetById(int loanId)
         {
             var amount_to_pay = _dbContext.EMI.Find(loanId);
             if (amount_to_pay == null)
             {
                 return NoContent();
             }
             return Ok(amount_to_pay);
         }
         [HttpGet]
          public ActionResult<IEnumerable<EmiModel>> GetAll()
          {
              var amount_to_pay = _dbContext.EMI.ToList();
              if (amount_to_pay == null)
              {
                  return NoContent();
              }
              return Ok(amount_to_pay);

         [HttpPut("loanId:int")]
         public ActionResult<EmiModel> Update(int loanId, [FromBody] EmiModel emi)
         {
             if (loanId != emi.loanId || emi == null)
             {
                 return BadRequest();
             }
             var emiDb = _dbContext.EMI.Find(loanId);

             if (emiDb == null)
             {
                 return NotFound();
             }
             emiDb.monthlyemi = emi.monthlyemi;
             _dbContext.EMI.Update(emiDb);
             _dbContext.SaveChanges();
             return Ok();
         }
         [HttpDelete]
         public ActionResult DeleteById(int loanId, [FromBody] EmiModel s)
         {
             var deleteData = _dbContext.EMI.Find(loanId);
             deleteData.monthlyemi = s.monthlyemi;
             _dbContext.EMI.Remove(deleteData);
             _dbContext.SaveChanges();
             return Ok();
         }
     }






[HttpPost]
public IHttpActionResult ApproveLoan(LoanApplicantModel data)
{
    // Verify loan application and approve it
    bool isLoanValid = VerifyLoanApplication(data);
    if (isLoanValid)
    {
        // Add the approved loan to the list of loans
        loans.Add(data);

        return Ok();
    }

    // Loan application is not valid, handle the appropriate response
    return BadRequest("Loan application verification failed.");
}

private bool VerifyLoanApplication(LoanApplicantModel data)
{
    // Perform verification logic here
    // For example, you can check applicant details, documents, credit score, etc.
    // Return true if the loan application is valid, otherwise return false

    // Sample verification logic (replace with your actual verification code):
    if (int.TryParse(data.applicantSalary, out int salary) && int.TryParse(data.loanAmountRequired, out int loanAmount))
    {
        if (salary >= 50000 && loanAmount <= 1000000)
        {
            // Loan application is valid
            return true;
        }
    }

    // Loan application is not valid
    return false;
}*/







