namespace SimpleCommerceProject.Data.Models.Core.Domain
{
    public class ProductsAttributesValues
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public Attributes Attributes { get; set; }
        public int AttributeId { get; set; }
        public Values Values { get; set; }
        public int ValueId { get; set; }
    }
}
