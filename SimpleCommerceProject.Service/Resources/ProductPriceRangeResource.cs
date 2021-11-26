using System.ComponentModel.DataAnnotations;

namespace SimpleCommerceProject.Service.Resources
{
    public class ProductPriceRangeResource
    {
        [Required(ErrorMessage ="min price is required")]
        public int MinPrice { get; set; }

        [Required(ErrorMessage = "max price is required")]
        public int MaxPrice { get; set; }
    }
}
