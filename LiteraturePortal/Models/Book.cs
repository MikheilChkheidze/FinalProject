using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LiteraturePortal.Models
{
    public class Book
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Title { get; set; }
        public string Genre { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public double Pages{ get; set; }
        public string Language { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
