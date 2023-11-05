using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeriVox.Business.Interfaces;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Database.DatabaseObjects;
using VeriVox.Repository.Interfaces;

namespace VeriVox.Business
{
    public class ProductService : IProductService
    {
        private IProductRepository _productService;
        public ProductService(IProductRepository productService)
        {
            _productService = productService;
        }

        public async Task<object> PostProduct(ProductDto productDto)
        {
            return await _productService.PostProduct(productDto);
        }

        public async Task<List<ProductDisplayDto>> DisplayProduct(Guid id)
        {
            return await _productService.DisplayProduct(id);
        }

        public async Task<object> GetProductById(Guid id)
        {
            return await _productService.GetProductById(id);
            
        }

        public async Task<object> DeleteProduct(Guid id)
        {
            return await _productService.DeleteProduct(id);
        }


        public async Task<object> UpdateProduct(Guid id, ProductUpdateDto productUpdateDto)
        {
            return await _productService.UpdateProduct(id, productUpdateDto);
            
        }

        public async Task<object> ProductStateUpdate(Guid id, ActiveStateDto activeStateDto)
        {
            return await _productService.ProductStateUpdate(id, activeStateDto);
        }

    }

}
