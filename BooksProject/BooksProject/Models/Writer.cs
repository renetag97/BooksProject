using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BooksProject.Models
{
    public class Writer
    {
        [Key]
        public int WriterID { get; set; }

        [Display(Name = "First Name")]
        [StringLength(200, MinimumLength = 1)]
        public String FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(200, MinimumLength = 1)]
        public String LastName { get; set; }

        [Display(Name = "User Name")]
        [StringLength(200, MinimumLength = 1)]
        public String UserName { get; set; }

    }
}