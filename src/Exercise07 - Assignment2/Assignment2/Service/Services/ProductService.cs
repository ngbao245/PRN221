﻿using AutoMapper;
using CodeInBlue.Entities;
using Repository.Interfaces;
using Repository.Models;
using Repository.Repositories;
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
            var products = _unitOfWork.ProductRepository.GetAll();
            return _mapper.Map<IEnumerable<ProductModel>>(products);
        }

        public IEnumerable<ProductModel> SearchByName(string name)
        {
            var products = _unitOfWork.ProductRepository.Get(p => p.ProductName.Contains(name));
            return _mapper.Map<IEnumerable<ProductModel>>(products);
        }

        public bool AddProduct(ProductModel model)
        {
            try
            {
                var product = _mapper.Map<Product>(model);

                // Get the max ProductId
                int maxProductId = _unitOfWork.ProductRepository.Get(p => true).Any() ? _unitOfWork.ProductRepository.Get(p => true).Max(p => p.ProductId) : 0;
                product.ProductId = maxProductId + 1;

                _unitOfWork.ProductRepository.Insert(product);
                _unitOfWork.Completed();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateProduct(ProductModel model)
        {
            try
            {
                var existingProduct = _unitOfWork.ProductRepository.Get(_ => _.ProductId.Equals(model.ProductId)).FirstOrDefault();
                if (existingProduct != null)
                {
                    existingProduct.ProductName = model.ProductName;
                    existingProduct.UnitPrice = model.UnitPrice;
                    existingProduct.QuantityPerUnit = model.QuantityPerUnit;
                    existingProduct.CategoryId = model.CategoryId;

                    _unitOfWork.ProductRepository.Update(existingProduct);
                    _unitOfWork.Completed();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteProduct(int id)
        {
            var productToDelete = _unitOfWork.ProductRepository.Get(_ => _.ProductId.Equals(id)).FirstOrDefault();
            if (productToDelete != null)
            {
                _unitOfWork.ProductRepository.Delete(productToDelete);
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