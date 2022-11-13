using System.ComponentModel.DataAnnotations;

namespace BanGiay.ViewModels
{
    public class MenuModels
    {
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Link { get; set; }
    }
}
