using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SimpleCommerceProject.Service.Resources
{
    public class CategoryResource
    {
        [JsonIgnore]
        public int Id { get; set; }

        [Required(ErrorMessage = "name is required")]
        public string Name { get; set; }
    }
}
