using LiteraturePortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteraturePortal.ViewModel
{
    public class BookAndReviewsViewModel
    {

        public int bookId { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public string UserId { get; set; }

        public Review NewReviewObj { get; set; }
        public IEnumerable<Review> PastReviewsObj { get; set; }
        public List<ReviewType> ReviewTypesObj { get; set; }
    }
}
