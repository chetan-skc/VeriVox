using Azure.Messaging;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VeriVox.Business.Interfaces;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Database.DatabaseObjects;
using VeriVox.Repository.Access_DB;
using static VeriVox.Core.DataTransferObjects.ProductUpdateDto;

namespace VeriVox.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> PostProduct(ProductDto productDto)
        {
            var validator = new ProductDtoValidator();
            var validationErrors = validator.ValidateAndGetErrors(productDto);

            if (validationErrors != null && validationErrors.Any())
            {
                return BadRequest(validationErrors);
            }

            await _productService.PostProduct(productDto);

            return Ok("Product created successfully.");
        }
        [Authorize]
        [HttpGet("productbycompanyid/{id}")]
        public async Task<List<ProductDisplayDto>> GetProduct(Guid id)
        {
            return await _productService.DisplayProduct(id);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async  Task<object> GetProductById(Guid id)
        {
            return await _productService.GetProductById(id);
        }
        [Authorize]
        [HttpDelete("{id}")]

        public async Task<ActionResult<List<Products>>> DeleteProduct(Guid id)
        {
            await _productService.DeleteProduct(id);
            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<object> UpdateProduct(Guid id, [FromBody] ProductUpdateDto productUpdateDto)
        {
            var validator = new ProductUpdateDtoValidator();
            var validationErrors = validator.ValidateAndGetErrors(productUpdateDto);

            if (validationErrors != null && validationErrors.Any())
            {
                return BadRequest(validationErrors);
            }

             await _productService.UpdateProduct(id, productUpdateDto);

            return Ok("Product updated successfully.");
        }
        [Authorize]
        [HttpPut("productstatechange/{id}")]
        public async Task<object> ProductStateUpdate(Guid id, [FromBody] ActiveStateDto activeDto)
        {
            return await _productService.ProductStateUpdate(id, activeDto);
        }
    }
}
