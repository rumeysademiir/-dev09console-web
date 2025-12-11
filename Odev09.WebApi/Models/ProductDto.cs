using System.ComponentModel.DataAnnotations;

namespace Odev09.WebApi.Models
{
    public class ProductDto
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ürün adı zorunludur.")]
        [StringLength(30, ErrorMessage = "Ad en fazla 30 karakter olabilir.")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Açıklama en fazla 200 karakter olabilir.")]
        public string Description { get; set; }

        [Range(1, 10000, ErrorMessage = "Fiyat 1 ile 10.000 arasında olmalıdır.")]
        public decimal Price { get; set; }

        [Range(0, 5000, ErrorMessage = "Stok 0 ile 5000 arasında olmalıdır.")]
        public int Stock { get; set; }
    }
}
