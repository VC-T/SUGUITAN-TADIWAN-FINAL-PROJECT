using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShopManagement.Models
{
    public class Book
    {
        public int BId { get; set; }
        public string BTitle { get; set; }
        public string BAuthor { get; set; }
        public string BCat { get; set; }
        public int BQty { get; set; }
        public decimal BPrice { get; set; }
    }
}
