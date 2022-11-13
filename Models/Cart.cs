using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanGiay.Models
{
    [Table("GioHang")]
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        [Range(0, double.MaxValue)]
        public string Price { get; set; }
        [Range(0, double.MaxValue)]
        public string PriceSale { get; set; }
        public DateTime? NgayDatHang { get; set; }
    }
}
