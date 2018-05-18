using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteraturePortal.Models
{
    public class IndividualButtonPartial
    {
        public string ButtonType { get; set; }
        public string Action { get; set; }
        public string Glyph { get; set; }
        public string Text { get; set; }

        public int? ReviewId { get; set; }
        public string WriterId { get; set; }
        public int? BookId { get; set; }

        public string ActionParameters
        {
            get
            {
                var param = new StringBuilder(@"/");
                if (ReviewId != 0 && ReviewId != null)
                {
                    param.Append(String.Format("{0}", ReviewId));
                }
                if (WriterId != null && WriterId.Length > 0)
                {
                    param.Append(String.Format("{0}", WriterId));
                }
                if (BookId != 0 && BookId != null)
                {
                    param.Append(String.Format("{0}", BookId));
                }
                return param.ToString().Substring(0, param.Length);
            }
        }
    }
}
