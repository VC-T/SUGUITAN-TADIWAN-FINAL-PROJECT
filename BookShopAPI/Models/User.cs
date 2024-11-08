using System.ComponentModel.DataAnnotations;

namespace BookShopAPI.Models
{
    public class User
    {
        [Key]
        public int UId { get; set; }
        public string? UName { get; set; }
        public string? UPhone { get; set; }
        public string? UAdd { get; set; }
        public string? UPass { get; set; }
    }
}
