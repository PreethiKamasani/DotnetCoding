using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Enum;
using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using DotnetCoding.Services.Contracts;
using DotnetCoding.Services.Exceptions;
using DotnetCoding.Services.Extension;
using DotnetCoding.Services.Interfaces;

namespace DotnetCoding.Services
{
    public class ProductService : IProductService
    {
        public IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ProductResponse> CreateProduct(ProductRequest request)
        {
            if (request == null)
                throw new RequestNullException("Request cannot be null");
            try {
                var product = request.AsProductDetail();
                product.CreatedDate = DateTime.Now;
                product.Status = (int)ProductStatus.Created;
                SetApprovalStatus(product);               

                _unitOfWork.Products.Insert(product);
                await _unitOfWork.SaveAsync();

                return product.AsProductDetail();
               }
            catch (Exception ex)
            {
                throw new UnHandledException(ex.Message);
            }
           
        }
        private  void SetApprovalStatus (ProductDetail current, ProductDetail? previous = null)
        {
            if(current.Price > 5000){
                current.ApprovalStatus = (int)ApprovalStatus.Pending;
                current.ApproveReason = (current.Status == (int)ProductStatus.Created) ? (int)ReasonType.PriceMoreThanLimit : (int)ReasonType.PriceChange;
            }else if (previous != null){
                var percentage = ((current.Price - previous.Price) / previous.Price) * 100;
                if (percentage > 50)
                {
                    current.ApprovalStatus = (int)ApprovalStatus.Pending;
                    current.ApproveReason = (int)ReasonType.PriceMoreThanLimit;
                }

            }
            else{
                current.ApprovalStatus = (int)ApprovalStatus.Approved;
            }

           
        }
        public async Task<ProductResponse> UpdateProduct(ProductRequest request, int productId)
        {
            if (request == null)
                throw new RequestNullException("Request cannot be null");
            if (productId <= 0)
                throw new RequestNullException("ProductId must be provided");
            try
            {
                var existingProduct = await _unitOfWork.Products.GetById(productId);
                if (existingProduct == null)
                    throw new ResourceNotFoundException($"Product with Id :{productId} doesnt exists ");

                var product = request.AsProductDetail();                
                product.Status = (int)ProductStatus.Updated;
                SetApprovalStatus(product, existingProduct);               
                _unitOfWork.Products.Update(product);
                await _unitOfWork.SaveAsync();

                return product.AsProductDetail();
            }
            catch (Exception ex)
            {
                throw new UnHandledException(ex.Message);
            }

        }

        public async Task DeleteProduct(int productId)
        {
         
            if (productId <= 0)
                throw new RequestNullException("ProductId must be provided");
            try
            {
                var existingProduct = await _unitOfWork.Products.GetById(productId);
                if (existingProduct == null)
                    throw new ResourceNotFoundException($"Product with Id :{productId} doesnt exists ");

                existingProduct.Status = (int)ProductStatus.Deleted;
                existingProduct.ApprovalStatus = (int)ApprovalStatus.Pending;
                existingProduct.ApproveReason = (int)ReasonType.Delete;
                existingProduct.UpdatedDate = DateTime.UtcNow;
                existingProduct.DeletedDate = DateTime.UtcNow;
               
                _unitOfWork.Products.Update(existingProduct);
                await _unitOfWork.SaveAsync();

                return;
            }
            catch (Exception ex)
            {
                throw new UnHandledException(ex.Message);
            }

        }

        
        public async Task<IEnumerable<ProductResponse>> GetActiveProductList(SearchParams request,int userId)
        {
            if (userId == 0)
                throw new RequestNullException("User Id is required");

            var productDetailsList = await _unitOfWork.Products.GetActiveProductList(request, userId);
            
            return productDetailsList.AsProducts();
        }

        public async Task<IEnumerable<QueuedProductDetails>> GetQueuedProductList(int userId)
        {
            if (userId == 0)
                throw new RequestNullException("User Id is required");

            var productDetailsList = await _unitOfWork.Products.GetInActiveProductList(userId);

            return productDetailsList.AsInActiveProducts();
        }

        public async Task ApproveOrRejectProduct(ApprovalRequest request)
        {
            if(request == null)
                throw new RequestNullException("ApprovalRequest cannot be null");

            try
            {
                var existingProduct = await _unitOfWork.Products.GetById(request.ProductId);
                if (existingProduct == null)
                    throw new ResourceNotFoundException($"Product with Id :{request.ProductId} doesnt exists ");

                //Assuming the rejected productc doesny show up in active list and also queued list
                existingProduct.ApprovalStatus = (int)request.Status;
                existingProduct.ApproveDate = DateTime.Now;
                existingProduct.ApprovedBy = request.ApprovedBy;    
                existingProduct.UpdatedDate = DateTime.UtcNow;
               
                _unitOfWork.Products.Update(existingProduct);
                await _unitOfWork.SaveAsync();

                return;
            }
            catch (Exception ex)
            {
                throw new UnHandledException(ex.Message);
            }


        }


        public async Task<ProductDetail> GetProductById(int productId)
        {
            if (productId == 0)
                throw new RequestNullException("Id cannot be null");
            var productDetail = await _unitOfWork.Products.GetById(productId);
            return productDetail;
        }
        public async Task<IEnumerable<ProductDetail>> GetAllProducts()
        {
            var productDetailsList = await _unitOfWork.Products.GetAll();
            return productDetailsList;
        }
       
       
    }
}
