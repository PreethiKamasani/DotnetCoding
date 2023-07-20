using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Models;
using DotnetCoding.Services.Contracts;

namespace DotnetCoding.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDetail>> GetAllProducts();

        Task<ProductResponse> CreateProduct(ProductRequest request);

        Task<ProductResponse> UpdateProduct(ProductRequest request, int productId);

        Task DeleteProduct(int productId);

        Task ApproveOrRejectProduct(ApprovalRequest request);

        Task<ProductDetail> GetProductById(int productId);

        Task<IEnumerable<ProductResponse>> GetActiveProductList(SearchParams request, int userId);

        Task<IEnumerable<QueuedProductDetails>> GetQueuedProductList(int userId);
    }
}
