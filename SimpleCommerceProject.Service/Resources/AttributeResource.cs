using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SimpleCommerceProject.Service.Resources
{
    public class AttributeResource
    {
        [Required(ErrorMessage = "id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "name is required")]
        public string Name { get; set; }

        [JsonIgnore]
        public int CategoryId { get; set; }
    }
}
