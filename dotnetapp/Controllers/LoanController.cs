using Loans.Context;
using Loans.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace Loan.Controller
{

    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public LoanController(ApplicationDbContext applicationdbcontext)
        {
            _context = applicationdbcontext;
        }
        [HttpPost("user/addLoan")]
        public async Task<IActionResult> addLoan([FromBody] LoanApplicantModel loanobj)
        {
            if (loanobj == null)
            {
                return BadRequest();
            }

            await _context.LoanApplicant.AddAsync(loanobj);
            await _context.SaveChangesAsync();
            string loanAmountRequired = loanobj.loanAmountRequired;
            string loanRepayMonths = loanobj.loanRepaymentMonths;
            string salary = loanobj.applicantSalary;

            decimal loanAmount = Convert.ToDecimal(loanAmountRequired);
            int loanMonths = Convert.ToInt32(loanRepayMonths);
            decimal applicantsalary = Convert.ToDecimal(salary);

            decimal totalRepaymentAmount = loanMonths * applicantsalary;
            decimal interestAmount = totalRepaymentAmount - loanAmount;
            decimal monthlyInterestRate = (interestAmount / loanAmount) / loanMonths / 100;
            decimal emi = loanAmount * monthlyInterestRate *
                          (decimal)Math.Pow(1 + (double)monthlyInterestRate, loanMonths) /
                          ((decimal)Math.Pow(1 + (double)monthlyInterestRate, loanMonths) - 1);

            loanobj.MonthlyEMI = emi;

            await _context.SaveChangesAsync();

            return Ok(new { message = "success" });
        }

        [HttpPut("user/editLoan/{loanId}")]
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
        [HttpGet("user/viewLoan/{loanId}")]
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


        [HttpGet("user/viewLoan")]
            public async Task<IActionResult> viewLoan()
            {
                var viewLoan = await _context.User.ToListAsync();

                if (viewLoan == null || viewLoan.Count == 0)
                {
                    return NotFound(new
                    {
                        Message = "No user found"
                    });
                }

                return Ok(new
                {
                    Message = "View Loan",
                    UserDetails = viewLoan
                });

            }
        
        


        [HttpDelete("user/deleteLoan/{loanId}")]
        public async Task<IActionResult> deleteloan(int loanId)
        {
            var Loandetails = await _context.LoanApplicant.FindAsync(loanId);

            if (Loandetails == null)
            {
                return NotFound();
            }

            _context.LoanApplicant.Remove(Loandetails);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Delete loanapplication",
                loandetails = Loandetails
            });
        }
        [HttpPost("user/addDocuments")]
        public async Task<IActionResult> AddDocument([FromForm] DocumentModel documentModel)
        {
            if (documentModel == null || documentModel.DocumentUploads == null)
            {
                return BadRequest();
            }

            using (var memoryStream = new MemoryStream())
            {
                await documentModel.DocumentUploads.CopyToAsync(memoryStream);
                documentModel.documentupload = memoryStream.ToArray();

                _context.Document.Add(documentModel);
                await _context.SaveChangesAsync();



                return Ok(documentModel.documentId);
            }
        }
         [HttpGet("user/getDocuments/{loanId}")]
         public async Task<IActionResult> getDocuments(int loanId)
         {

            var appliedLoanDocs = await _context.Document.FirstOrDefaultAsync(p => p.documentId == loanId);

            if (appliedLoanDocs == null)
            {
                return Ok(new
                 {
                    Message = "No document found"
               });
             }

             return Ok(appliedLoanDocs);
         }



         [HttpGet("user/getDocuments")]
            public async Task<IActionResult> getDocuments()
            {
                var UserDocuments = await _context.User.ToListAsync();

                if (UserDocuments == null || UserDocuments.Count == 0)
                {
                    return NotFound(new
                    {
                        Message = "No user found"
                    });
                }

                return Ok(new
                {
                    Message = "View Documents",
                    UserDetails = UserDocuments
                });

            }



        [HttpPut("user/editDocuments/{documentId}")]
        public async Task<IActionResult> editDocuments(int documentId, [FromBody] DocumentModel documentModel)
        {
            if (documentModel == null || documentId != documentModel.documentId)
            {
                return BadRequest();
            }

            var Documentdetails = await _context.Document.FindAsync(documentId);

            if (Documentdetails == null)
            {
                return NotFound();
            }
            Documentdetails.documenttype = documentModel.documenttype;
            Documentdetails.documentupload = documentModel.documentupload;
            _context.Document.Update(Documentdetails);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Edit documents",
                loandetails = Documentdetails
            });
        }

        [HttpDelete("user/deletedocuments/{documentId}")]
        public async Task<IActionResult> deletedocuments(int documentId)
        {
            var documentdetails = await _context.Document.FindAsync(documentId);

            if (documentdetails == null)
            {
                return NotFound();
            }

            _context.Document.Remove(documentdetails);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Delete documents",
                loandetails = documentdetails
            });
        }
    }
}