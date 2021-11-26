using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SimpleCommerceProject.Service.Resources
{
    public class ProductResource
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required(ErrorMessage = "CategoyId is required")]
        public int ProductCategoryId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
    }
}
