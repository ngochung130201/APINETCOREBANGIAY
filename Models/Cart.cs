using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanGiay.Models
{
    [Table("GioHang")]
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CartID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        [Range(0, double.MaxValue)]
        public double PriceSale { get; set; }
        public int Amount { get; set; } 
        public DateTime? NgayDatHang { get; set; }
    }
}
