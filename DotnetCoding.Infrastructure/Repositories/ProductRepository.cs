using DotnetCoding.Core.Enum;
using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using DotnetCoding.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<ProductDetail>, IProductRepository
    {
        public ProductRepository(RetailDBContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<ProductDetail>> GetActiveProductList(SearchParams request, int userId)
        {
            var query = _dbContext.ProductDetails.Where(x => x.UserId == userId && x.ApprovalStatus == (int)ApprovalStatus.Approved).AsQueryable();
            if (request != null)
            {
                if (!string.IsNullOrEmpty(request.Name))
                    query = query.Where(y => y.Name.ToLower() == request.Name.ToLower());
                if (request.MinPrice > 0)
                    query = query.Where(y => y.Price >= request.MinPrice);
                if (request.MaxPrice > 0)
                    query = query.Where(y => y.Price <= request.MaxPrice);
                if (request.RequestStartDate.HasValue)
                    query = query.Where(y => y.CreatedDate >= request.RequestStartDate);
                if (request.RequestEndDate.HasValue)
                    query = query.Where(y => y.CreatedDate <= request.RequestEndDate);
            }

            return await query.OrderByDescending(z => z.UpdatedDate).ToListAsync<ProductDetail>();
        }


        public async Task<IEnumerable<ProductDetail>> GetInActiveProductList(int userId)
        {
            return await _dbContext.ProductDetails.Where(x => x.UserId == userId && x.ApprovalStatus == (int)ApprovalStatus.Pending).ToListAsync<ProductDetail>();
        }
    }
}
