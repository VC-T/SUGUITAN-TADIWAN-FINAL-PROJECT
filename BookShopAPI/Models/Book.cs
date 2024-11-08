using System.ComponentModel.DataAnnotations;

namespace BookShopAPI.Models
{
    public class Book
    {
        [Key]
        public int BId { get; set; }
        public string? BTitle { get; set; }
        public string? BAuthor { get; set; }
        public string? BCat { get; set; }
        public int BQty { get; set; }
        public decimal BPrice { get; set; }
    }
}
