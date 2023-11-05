using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Database.DatabaseObjects;

namespace VeriVox.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<object> PostProduct(ProductDto productDto);

        Task<List<ProductDisplayDto>> DisplayProduct(Guid id);

        Task<object> GetProductById(Guid id);

        Task<object> DeleteProduct(Guid id);

        Task<object> UpdateProduct(Guid id, ProductUpdateDto productUpdateDto);

        Task<object> ProductStateUpdate(Guid id, ActiveStateDto activedto);
    }
}
