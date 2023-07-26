
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using System;


namespace WebApp.Models
    {
        public class DocumentModel
        {
            [Key]
            public int documentId { get; set; }

            
            public string documenttype { get; set; }

            [NotMapped]
            public IFormFile DocumentUploads { get; set; }

        public byte[] documentupload { get; set; }

            public string documentVerified { get; set; }
        }
    }