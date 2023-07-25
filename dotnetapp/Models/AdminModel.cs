using System.ComponentModel.DataAnnotations;

namespace Loans.Models
{
    public class AdminModel
    {
        [Key]
        public int ID { get; set; }
        public String email { get; set; }

        
        public String password { get; set; }

        public String mobileNumber { get; set; }

        
        public String userRole { get; set; }
    }
}