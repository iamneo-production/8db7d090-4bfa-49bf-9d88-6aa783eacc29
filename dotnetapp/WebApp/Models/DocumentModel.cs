using System.ComponentModel.DataAnnotations;

namespace Loans.Models
{
    public class DocumentModel
    {
        internal int loanId;

        [Key]
        public int documentId { get; set; }

        public string documenttype { get; set; }

        public byte[] documentupload { get; set; }
        public bool Verified { get; internal set; }
    }
}
