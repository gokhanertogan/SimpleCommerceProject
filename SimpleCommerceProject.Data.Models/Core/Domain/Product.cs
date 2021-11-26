namespace SimpleCommerceProject.Data.Models.Core.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
