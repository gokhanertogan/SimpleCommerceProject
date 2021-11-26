using AutoMapper;
using SimpleCommerceProject.Data.Models.Core.Domain;
using SimpleCommerceProject.Service.Resources;

namespace SimpleCommerceProject.Service.Mapping
{
    public class GeneralMapping: Profile
    {
        public GeneralMapping()
        {
            CreateMap<ProductResource, Product>().ReverseMap();
            CreateMap<ProductsAttributesValueResource, ProductsAttributesValues>().ReverseMap();
            CreateMap<CategoryResource, Category>().ReverseMap();
            CreateMap<AttributeResource, Attributes>().ReverseMap();
        }
    }
}
