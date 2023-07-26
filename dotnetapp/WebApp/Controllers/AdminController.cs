using WebApp.Data;
using WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

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
        [HttpGet("admin/getAllLoans")]
        public async Task<IActionResult> getAllLoan()
        {
            var appliedloans = await _context.LoanApplicant.ToListAsync();

            if (appliedloans == null || appliedloans.Count == 0)
            {
                return NotFound(new
                {
                    Message = "No user found"
                });
            }

            return Ok(appliedloans);

        }

        [HttpPost("admin/getAllLoans/approve/{id}")]
        public IActionResult ApproveLoan(int id, [FromBody] LoanApplicantModel request)
        {
            var result = _context.LoanApplicant.Where(x => x.loanId == id).FirstOrDefault();
            if (result != null)
            {
                result.loanStatus = "approved";
                _context.SaveChanges();
                return Ok(new { Status = "Success" });
            }
            else
            {
                return BadRequest(new { Status = "Error", Error = "Error in updating approval status" });
            }

        }

        [HttpPost("admin/getAllLoans/reject/{id}")]
        public IActionResult RejectLoan(int id, [FromBody] LoanApplicantModel request)
        {
            var loanApplicant = _context.LoanApplicant.FirstOrDefault(a => a.loanId == id);
            if (loanApplicant == null)
            {
                return BadRequest(new { Status = "Error", Error = "Loan applicant not found" });
            }

            loanApplicant.loanStatus = "rejected";

            _context.SaveChanges();

            return Ok(new { Status = "Success" });

        }
        [HttpPut("approve")]
        public IActionResult ApproveLoans([FromBody] int[] loanIds)
        {
            var loans = _context.LoanApplicant.Where(x => loanIds.Contains(x.loanId));
            foreach (var loan in loans)
            {
                loan.loanStatus = "approved";
            }

            _context.SaveChanges();

            return Ok(new { Status = "Success", Message = "Loans approved successfully" });
        }

        [HttpPut("reject")]
        public IActionResult RejectLoans([FromBody] int[] loanIds)
        {
            var loans = _context.LoanApplicant.Where(x => loanIds.Contains(x.loanId));
            foreach (var loan in loans)
            {
                loan.loanStatus = "rejected";
            }

            _context.SaveChanges();

            return Ok(new { Status = "Success", Message = "Loans rejected successfully" });
        }

         [HttpGet("admin/generateSchedule")]
            public async Task<IActionResult>generateSchedule ()
            {
                var generateSchedule = await _context.LoanApplicant.ToListAsync();

                if (generateSchedule == null || generateSchedule.Count == 0)
                {
                    return NotFound(new
                    {
                        Message = "No user found"
                    });
                }

                return Ok(new
                {
                    Message = "Schedule generated",
                    UserDetails = generateSchedule
                });

            }

        [HttpGet("admin/getAllLoans/{loanId}")]
        public async Task<IActionResult> viewLoan(int loanId)
        {
            var appliedLoan = await _context.LoanApplicant.FirstOrDefaultAsync(p => p.loanId == loanId);

            if (appliedLoan == null)
            {
                return NotFound(new
                {
                    Message = "No user found"
                });
            }

            return Ok(appliedLoan);
        }
        [HttpGet("admin/getLoans/{name}")]
        public async Task<IActionResult> viewLoan(string name)
        {
            var appliedLoan = await _context.LoanApplicant.FirstOrDefaultAsync(p => p.applicantName == name);

            if (appliedLoan == null)
            {
                return NotFound(new
                {
                    Message = "No user found"
                });
            }

            return Ok(appliedLoan);
        }

        [HttpPut("admin/editLoan/{loanId}")]
        public async Task<IActionResult> editLoan(int loanId, [FromBody] LoanApplicantModel loanApplicantModel)
        {
            if (loanApplicantModel == null || loanId != loanApplicantModel.loanId)
            {
                return BadRequest();
            }

            var loanApplicant = await _context.LoanApplicant.FindAsync(loanId);

            if (loanApplicant == null)
            {
                return NotFound();
            }

            // loanApplicant.loanId = loanApplicantModel.loanId;
            loanApplicant.loantype = loanApplicantModel.loantype;
            loanApplicant.applicantName = loanApplicantModel.applicantName;
            loanApplicant.applicantAddress = loanApplicantModel.applicantAddress;
            loanApplicant.applicantMobile = loanApplicantModel.applicantMobile;
            loanApplicant.applicantEmail = loanApplicantModel.applicantEmail;
            loanApplicant.applicantAadhaar = loanApplicantModel.applicantAadhaar;
            loanApplicant.applicantPan = loanApplicantModel.applicantPan;
            loanApplicant.applicantSalary = loanApplicantModel.applicantSalary;
            loanApplicant.loanAmountRequired = loanApplicantModel.loanAmountRequired;
            loanApplicant.loanRepaymentMonths = loanApplicantModel.loanRepaymentMonths;



            _context.LoanApplicant.Update(loanApplicant);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Edit Applied loan",
                loandetails = loanApplicant
            });
        }
        [HttpDelete("admin/deleteLoan/{loanId}")]
        public async Task<IActionResult> DeleteLoan(int loanId)
        {
            var loanApplicant = await _context.LoanApplicant.FindAsync(loanId);
            if (loanApplicant == null)
            {
                return NotFound();
            }
            _context.LoanApplicant.Remove(loanApplicant);
            await _context.SaveChangesAsync();

            return Ok("Loan Deleted");
        }

    }
}



 /* [HttpPost("admin/approveLoan")]
          public async Task<IActionResult> ApproveLoan(int loanId, int documentId)
          {
              var loanApplicant = await _context.LoanApplicant.FindAsync(loanId);

              if (loanApplicant == null)
              {
                  return NotFound();
              }
              var document = await _context.Document.FindAsync(documentId);

              if (document == null)
              {
                  return NotFound();
              }
              Console.WriteLine(document.documentVerified);

              if (loanApplicant.MonthlyEMI < int.Parse(loanApplicant.applicantSalary) && string.Compare(document.documentVerified, "Approved") == 0)
              {
                  loanApplicant.loanStatus = "Approved";
                  await _context.SaveChangesAsync();
                  return Ok("Approved");
              }
              loanApplicant.loanStatus = "Rejected";
              await _context.SaveChangesAsync();
              return NotFound("Rejected");

          }
          public enum DocumentType
          {
              AadharCard,
              PanCard,
              PaySlip,
              BankStatement


          }

          public enum DocumentStatus
          {
              Approved,
              Rejected,

          }
        [HttpPost("admin/verifyDocuments")]
        public async Task<IActionResult> VerifyDocuments(int documentId)
        {
            var document = await _context.Document.FindAsync(documentId);

            if (document == null)
            {
                return NotFound();
            }

            if (string.Compare(document.documenttype, Convert.ToString(DocumentType.AadharCard)) == 0)
            {

                document.documentVerified = Convert.ToString(DocumentStatus.Approved);

            }
            else if (string.Compare(document.documenttype, Convert.ToString(DocumentType.PanCard)) == 0)
            {
                document.documentVerified = Convert.ToString(DocumentStatus.Approved);

            }
            else if (string.Compare(document.documenttype, Convert.ToString(DocumentType.PaySlip)) == 0)
            {

                document.documentVerified = Convert.ToString(DocumentStatus.Approved);

            }
            else if (string.Compare(document.documenttype, Convert.ToString(DocumentType.BankStatement)) == 0)
            {

                document.documentVerified = Convert.ToString(DocumentStatus.Approved);
            }

            else
            {

                document.documentVerified = Convert.ToString(DocumentStatus.Rejected);

            }
            await _context.SaveChangesAsync();
            return NotFound(Convert.ToString(document.documentVerified));*/


/* [HttpPut("admin/editLoan/{loanId}")]
 public async Task<IActionResult> EditLoan(int loanId)
 {
     var loanApplicant = await _context.LoanApplicant.FindAsync(loanId);

     if (loanApplicant == null)
     {
         return NotFound();
     }

     if (int.Parse(loanApplicant.applicantSalary) - 5000 < loanApplicant.MonthlyEMI && string.Compare(loanApplicant.loanStatus, "Approved") == 0)
     {
         loanApplicant.loanStatus = "Rejected";
         await _context.SaveChangesAsync();

     }
     return Ok("Loan Edited");
 }
 [HttpDelete]
 [Route("api/admin/deleteLoan/{loanId}")]
 public async Task<IActionResult> DeleteLoan(int loanId)
 {
     var loanApplicant = await _context.LoanApplicant.FindAsync(loanId);
     if (loanApplicant == null)
     {
         return NotFound();
     }
     _context.LoanApplicant.Remove(loanApplicant);
     await _context.SaveChangesAsync();

     return Ok("Loan Deleted");
 }


 [HttpGet("admin/RepaymentSchedule/{loanId}")]
 public async Task<IActionResult> GetMonthlyEMI(int loanId)
 {
     // Retrieve the LoanApplicantModel from the database using the loanId
     var loanApplicant = await _context.LoanApplicant.FindAsync(loanId);

     if (loanApplicant == null)
     {
         return NotFound();
     }

     string loanAmountRequired = loanApplicant.loanAmountRequired;
     string loanRepayMonths = loanApplicant.loanRepaymentMonths;
     string salary = loanApplicant.applicantSalary;

     decimal loanAmount = Convert.ToDecimal(loanAmountRequired);
     int loanMonths = Convert.ToInt32(loanRepayMonths);
     decimal applicantsalary = Convert.ToDecimal(salary);

     decimal totalRepaymentAmount = loanMonths * applicantsalary;
     decimal interestAmount = totalRepaymentAmount - loanAmount;
     decimal monthlyInterestRate = (interestAmount / loanAmount) / loanMonths / 100;
     decimal emi = loanAmount * monthlyInterestRate *
                   (decimal)Math.Pow(1 + (double)monthlyInterestRate, loanMonths) /
                   ((decimal)Math.Pow(1 + (double)monthlyInterestRate, loanMonths) - 1);

     loanApplicant.MonthlyEMI = emi;

     await _context.SaveChangesAsync();

     return Ok(loanApplicant);
 }
 [HttpPut("admin/editRepaymentSchedule/{loanId}")]
 public async Task<IActionResult> EditRepaymentSchedule(int loanId, [FromBody] LoanApplicantModel loanApplicantModel)
 {
     var editEmi = await _context.LoanApplicant.FindAsync(loanId);
     if (editEmi == null)
     {
         return BadRequest("Monthly EMI not generated");
     }
     editEmi.MonthlyEMI = loanApplicantModel.MonthlyEMI;
     _context.LoanApplicant.Update(editEmi);
     await _context.SaveChangesAsync();

     return Ok(new
     {
         Message = "Emi edited"

     });
 }
 [HttpDelete("admin/deleteRepaymentSchedule/{loanId}")]
 public async Task<IActionResult> deleteRepaymentSchedule(int loanId, [FromBody] LoanApplicantModel loanApplicantModel)
 {
     var editEmi = await _context.LoanApplicant.FindAsync(loanId);
     if (editEmi == null)
     {
         return BadRequest("Monthly EMI not generated");
     }
     editEmi.MonthlyEMI = loanApplicantModel.MonthlyEMI;
     _context.LoanApplicant.Remove(editEmi);
     await _context.SaveChangesAsync();

     return Ok(new
     {
         Message = "Emi deleted"

     });
 }*/