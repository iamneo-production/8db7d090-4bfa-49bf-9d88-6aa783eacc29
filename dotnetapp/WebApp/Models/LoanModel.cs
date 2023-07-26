using System.ComponentModel.DataAnnotations;
using System;

namespace WebApp.Models
{
    public class LoginModel
    { 
            [Key]
         public int ID { get; set; }
         public string email { get; set; }
         public string password { get; set; }
        }
    }