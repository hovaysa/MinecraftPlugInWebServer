﻿using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IWebServerRepository
    {
        #region user actions

        Task<int> CreateUser(UserEntity userEntity);
        Task<int> UpdateUser(UserEntity userEntity);
        Task<int> DeleteUser(Guid id);
        Task<UserEntity> GetUserById(Guid id);

        #endregion user actions

        #region product actions

        Task<int> CreateProduct(ProductEntity productEntity);
        Task<int> UpdateProduct(ProductEntity productEntity);
        Task<int> DeleteProduct(Guid id);
        Task<ProductEntity> GetProductById(Guid id);
        Task<List<ProductEntity>> GetProducts();

        #endregion product actions

        #region bundle actions

        Task<Guid> CreateBundle(BundleEntity bundleEntity);
        Task<int> UpdateBundle(BundleEntity bundleEntity);
        Task<int> DeleteBundle(Guid id);
        Task<BundleEntity> GetBundleById(Guid id);
        Task<List<BundleEntity>> GetBundles();

        #endregion bundle actions

        #region bundle product actions

        Task<int> AddProductsToBundle(List<BundleProductEntity> bundleProductEntities);
        Task<int> AddProductToBundle(BundleProductEntity bundleProductEntity);
        Task<int> DeleteProductFromBundle(BundleProductEntity bundleProductEntity);

        #endregion bundle product actions

    }
}