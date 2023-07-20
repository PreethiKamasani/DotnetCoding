using DotnetCoding.Core.Models;
using DotnetCoding.Services.Contracts;

namespace DotnetCoding.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<ProductDetail>
    {
        Task<IEnumerable<ProductDetail>> GetActiveProductList(SearchParams request, int userId);

        Task<IEnumerable<ProductDetail>> GetInActiveProductList(int userId);
    }
}
