using System.Text.Json.Serialization;

namespace SimpleCommerceProject.Service.Resources
{
    public class ProductsAttributesValueResource
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public int ProductId { get; set; }

        public int AttributeId { get; set; }
        public int ValueId { get; set; }
    }
}
