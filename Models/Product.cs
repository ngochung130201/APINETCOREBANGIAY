using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BanGiay.Models
{
    [Table("SanPham")]
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public bool? isTrend { get; set; }
        public string Gallery { get; set; }
        [Range(0,double.MaxValue)]
        public double? Price { get; set; }
        [Range(0,double.MaxValue)]
        public double? PriceSale { get; set; }
 
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
       
    }
}
