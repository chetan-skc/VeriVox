using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeriVox.Core.DataTransferObjects;
using VeriVox.Database.Context;
using VeriVox.Database.DatabaseObjects;
using VeriVox.Repository.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace VeriVox.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly CFA_DbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public ProductRepository(CFA_DbContext dbContext,IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        
        public async Task<object> PostProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Products>(productDto);
            var userClaims = _httpContextAccessor.HttpContext.User.Claims;
            var userIdClaim = userClaims.FirstOrDefault(c => c.Type == "Id");
            string userId = userIdClaim.Value;
            Guid userGuid = Guid.Parse(userId);

            product.CreatedBy = userGuid;
            product.ModifiedBy = userGuid;

            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            var result = new { Message = "Company Posted" };
            return result;
        }

        public async Task<List<ProductDisplayDto>> DisplayProduct(Guid id)
        {
            var products = _dbContext.Products
        .Where(p => p.CompanyId == id && p.IsDeleted == false)
        .Select(p => new ProductDisplayDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Type = p.Type,
            LogoImage = p.LogoImage,
            ShortName = p.ShortName,
            IsActive = p.IsActive
        })
        .ToList();

            return products;

        }

        public async Task<object> GetProductById(Guid id)
        {
            var product = _dbContext.Products.SingleOrDefault(x => x.Id == id);

            if (product == null)
            {
                var result = new { Message = "Product not Found" };
                return result;
            }
            else
            {
                var productDto = new ProductDisplayDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Type = product.Type,
                    LogoImage = product.LogoImage,
                    ShortName = product.ShortName,
                    IsActive = product.IsActive
                };
                var result = new { Product = productDto };
                return result;
            }
        }



        public async Task<object> DeleteProduct(Guid id)
        {
            var product = _dbContext.Products.SingleOrDefault(x => x.Id == id);
            if (product == null)
            {
                var result = new { Message = "Product not Deleted" };
                return result;
            }
            else
            {
                product.IsDeleted = true;
                _dbContext.SaveChanges();
                var result = new { Message = "Product Deleted" };
                return result;
            }

        }

        public async Task<object> UpdateProduct(Guid id, ProductUpdateDto productUpdateDto)
        {
            var updateproduct =  _dbContext.Products.SingleOrDefault(x => x.Id == id);
            if (updateproduct == null)
            {
                var result = new { Message = "Product not Updated" };
                return result;
            }
            else
            {
                _mapper.Map(productUpdateDto, updateproduct);
                _dbContext.SaveChanges();
                var result = new { Message = "Product Updated" };
                return result;
            }
            
        }

        public async Task<object> ProductStateUpdate(Guid id, ActiveStateDto activedto)
        {
            var updateproduct = _dbContext.Products.SingleOrDefault(x => x.Id == id);
            if (updateproduct == null)
            {
                var result = new { Message = "Product state not updated" };
                return result;

            }
            else
            {
                updateproduct.IsActive = activedto.IsActive;
                _dbContext.SaveChanges();
                var result = new { Message = "Product state updated" };
                return result;
            }

        }



    }
}
