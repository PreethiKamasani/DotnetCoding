using DotnetCoding.Core.Enum;
using DotnetCoding.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoding.Services.Extension
{
    public static class ProductExtension
    {
        public static ProductDetail AsProductDetail(this ProductRequest request)
        {
            return new ProductDetail()
            {
                Name = request.Name,
                Description = request.Description,
                UpdatedDate = DateTime.Now,
                Price = request.Price
            };

        }

        public static IEnumerable<ProductResponse> AsProducts(this IEnumerable<ProductDetail> details)
        {            
            return details?.Select(x => x?.AsProductDetail()).ToList() ?? new List<ProductResponse>();
        }

        public static ProductResponse AsProductDetail(this ProductDetail detail)
        {
            return new ProductResponse()
            {
                Name = detail.Name,
                Description = detail.Description,
                UpdatedDate = detail.UpdatedDate,
                CreatedDate = detail.CreatedDate,
                Price = detail.Price,
                Status = detail.Status,
                UserId = detail.UserId,
                Id = detail.Id  
            };

        }
        public static IEnumerable<QueuedProductDetails> AsInActiveProducts(this IEnumerable<ProductDetail> details)
        {
            return details?.Select(x => x?.AsQueuedProductDetail()).ToList() ?? new List<QueuedProductDetails>();
        }
        public static QueuedProductDetails AsQueuedProductDetail(this ProductDetail detail)
        {
            return new QueuedProductDetails()
            {
                Name = detail.Name,              
                RequestedDate = detail.UpdatedDate,
                Price = detail.Price,
                Reason = Enum.GetName(typeof(ApprovalStatus),(int)detail.ApproveReason)?? string.Empty,
                Id = detail.Id
            };

        }
    }
}
