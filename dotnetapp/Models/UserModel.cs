using System.ComponentModel.DataAnnotations;
using System;
namespace Loans.Models
{
    public class UserModel
    {
      
        [Key]
        public int ID { get; set; }

        
        public String email { get; set; }

       
        public String password { get; set; }

        public String confirmpassword { get; set; }

       
        public String userName { get; set; }

        
        public String mobileNumber { get; set; }

       
        public String userRole { get; set; }

        public String Token { get; set; }   

    }

}