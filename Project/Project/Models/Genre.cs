using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Genre
    {
        [Key]
        public int GenreID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public String GenreName { get; set; }
    }
}