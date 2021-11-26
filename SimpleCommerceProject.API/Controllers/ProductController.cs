using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleCommerceProject.API.Extensions;
using SimpleCommerceProject.Service.Features.Commands;
using SimpleCommerceProject.Service.Resources;
using SimpleCommerceProject.Service.Services.Abstract;
using SimpleCommerceProject.Shared.ControllerBases;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleCommerceProject.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : CustomBaseController
    {
        private readonly IProductService _productService;
        private readonly IMediator _mediator;

        public ProductController(IProductService productService, IMediator mediator)
        {
            _productService = productService;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductResource productResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            else
            {
                var response = await _productService.AddAsync(productResource);
                return CreateActionResultInstance(response);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _productService.DeleteAsync(id);

            return CreateActionResultInstance(response);
        }

        [HttpGet("GetByName/{productName}")]
        public async Task<IActionResult> GetByName(string productName)
        {
            var productResponse = await _productService.GetByNameAsync(productName);

            return CreateActionResultInstance(productResponse);
        }

        [ResponseCache(Duration =10)]
        [HttpGet("GetListByCategoryName/{categoryName}")]
        public async Task<IActionResult> GetListByCategoryName(string categoryName)
        {
            var productListResponse = await _productService.GetListByCategoryNameAsync(categoryName);

            return CreateActionResultInstance(productListResponse);
        }


        [HttpPost()]
        public async Task<IActionResult> GetListByPriceRange(ProductPriceRangeResource productPriceRange)
        {
            var productListResponse = await _productService.GetListByPriceRangeAsync(productPriceRange);

            return CreateActionResultInstance(productListResponse);
        }

        [HttpPost("/GetListByProductAttributes")]
        public async Task<IActionResult> GetListByProductAttributes(IEnumerable<ProductsAttributesValueResource> productsAttributesValues)
        {
            var productListResponse = await _productService.GetListByProductAttributesAsync(productsAttributesValues);

            return CreateActionResultInstance(productListResponse);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(ProductResource productResource, int id)
        {
            var response = _productService.Update(productResource, id);

            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWithMediatR(int id)
        {
            var command = new DeleteProductCommand() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

    }
}
