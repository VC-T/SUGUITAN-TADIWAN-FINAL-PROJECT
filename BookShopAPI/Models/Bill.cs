using System.ComponentModel.DataAnnotations;

namespace BookShopAPI.Models
{
    public class Bill
    {
        [Key]
        public int BillId { get; set; }
        public string? UName { get; set; }
        public string? ClientName { get; set; }
        public int Amount { get; set; }
    }
}
