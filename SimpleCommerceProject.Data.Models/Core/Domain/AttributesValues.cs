namespace SimpleCommerceProject.Data.Models.Core.Domain
{
    public class AttributesValues
    {
        public int Id { get; set; }
        public Attributes Attributes { get; set; }
        public int AttributeId { get; set; }
        public Values Values { get; set; }
        public int ValueId { get; set; }
    }
}
