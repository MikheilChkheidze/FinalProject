using LiteraturePortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiteraturePortal.ViewModel
{
    public class BookAndWriterViewModel
    {
        public ApplicationUser UserObj { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
