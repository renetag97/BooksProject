using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BooksProject.Models
{
    public class Book

    {
        [Key]
        public int BookID { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 1)]
        public String Title { get; set; }

        [Display(Name = "Release date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Writer")]
        public int WriterID { get; set; }
        public virtual Writer Writer { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public int GenreID { get; set; }
        public virtual Genre Genre { get; set; }

        [StringLength(500, MinimumLength = 1)]
        public String Description { get; set; }
    }
}