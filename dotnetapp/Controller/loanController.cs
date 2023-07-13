using Loan.Data;
using Loan.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace loan.Controller
{

    [ApiController]
    public class loanController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public loanController(ApplicationDbContext applicationdbcontext)
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

            return Ok(new
            {
                Message = "Loan added"
            });
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
        [HttpGet("user/viewLoan")]
        public async Task<IActionResult> getLoan()
        {
            var userloandetails = await _context.LoanApplicant.ToListAsync();

            if (userloandetails == null || userloandetails.Count == 0)
            {
                return NotFound(new
                {
                    Message = "No user found"
                });
            }

            return Ok(new
            {
                Message = "View Applied loan",
                LoanDetails = userloandetails
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
        public async Task<IActionResult> addDocument([FromBody] DocumentModel loanobj)
        {
            if (loanobj == null)
            {
                return BadRequest();
            }

            await _context.Document.AddAsync(loanobj);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Document uploaded"
            });
        }
        [HttpGet("user/getDocuments")]
        public async Task<IActionResult> getDocuments()
        {
            var userloandetails = await _context.Document.ToListAsync();

            if (userloandetails == null || userloandetails.Count == 0)
            {
                return NotFound(new
                {
                    Message = "No user found"
                });
            }

            return Ok(new
            {
                Message = "View Documents",
                LoanDetails = userloandetails
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

