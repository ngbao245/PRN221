using AutoMapper;
using CodeInBlue.Entities;
using Repository.Interfaces;
using Repository.Models;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<ProductModel> GetAll()
        {
            var products = _unitOfWork.Product.GetAll();
            return _mapper.Map<IEnumerable<ProductModel>>(products);
        }

        public IEnumerable<ProductModel> SearchByName(string name)
        {
            var products = _unitOfWork.Product.GetAll().Where(_ => _.ProductName.Contains(name));
            return _mapper.Map<IEnumerable<ProductModel>>(products);
        }

        public bool AddProduct(ProductModel model)
        {
            try
            {
                var product = _mapper.Map<Product>(model);

                int maxProductId = _unitOfWork.Product.GetAll().Max(_ => _.ProductId);
                product.ProductId = maxProductId + 1;

                _unitOfWork.Product.Insert(product);
                _unitOfWork.Completed();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteProduct(int id)
        {
            var productToDelete = _unitOfWork.Product.GetAll().FirstOrDefault(_ => _.ProductId.Equals(id));
            if (productToDelete != null)
            {
                _unitOfWork.Product.Delete(productToDelete);
                _unitOfWork.Completed();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}