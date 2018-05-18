using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LiteraturePortal.Models
{
    public class Review
    {
        public int Id { get; set; }
        public double Pages { get; set; }
        public double Price { get; set; }
        public string Details { get; set; }

        [DisplayFormat(DataFormatString ="{0:d}")]
        public DateTime DateAdded { get; set; }

        public int BookId { get; set; }

        public int Reviewer { get; set; }

        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }

        [ForeignKey("Reviewer")]
        public virtual ReviewType ReviewType { get; set; }
    }
}
