﻿using Dapper;
using Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Z.Dapper.Plus;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class WebServerRepository : IWebServerRepository
    {
        private readonly IConfiguration _configuration;

        public WebServerRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        #region user actions

        public async Task<int> CreateUser(UserEntity userEntity)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                int rowsAffected = await connection.ExecuteAsync
                    ("INSERT INTO users (id, username, email, ip) VALUES (@Id, @Username, @Email, @Ip)",
                    new { Id = userEntity.Id, Username = userEntity.Username, Email = userEntity.Email, Ip = userEntity.Ip });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<int> UpdateUser(UserEntity userEntity)
        {
            
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                int rowsAffected = await connection.ExecuteAsync
                    ("UPDATE users SET username = @Username, email = @Email, ip = @Ip, lives = @Lives WHERE id = @Id",
                    new { Id = userEntity.Id, Username = userEntity.Username, Email = userEntity.Email, Ip = userEntity.Ip, Lives = userEntity.Lives });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<int> DeleteUser(Guid id)
        {
            try
            {
                var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var rowsAffected = await connection.ExecuteAsync
                    ("DELETE FROM users WHERE id = @Id", new { Id = id });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<UserEntity> GetUserById(Guid id)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var user = await connection.QuerySingleAsync<UserEntity>
                    (@"SELECT id, username, email, ip, lives FROM 
                   users WHERE id = @Id",
                    new { Id = id });

                return (UserEntity)user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new UserEntity();
            }
        }

        #endregion user actions

        #region product actions

        public async Task<int> CreateProduct(ProductEntity productEntity)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                int rowsAffected = await connection.ExecuteAsync
                    ("INSERT INTO products (name, description, type, price) VALUES (@Name, @Description, @Type, @Price)",
                    new { Name = productEntity.Name, Description = productEntity.Description, Type = productEntity.Type, Price = productEntity.Price });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<int> UpdateProduct(ProductEntity productEntity)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                int rowsAffected = await connection.ExecuteAsync
                    ("UPDATE products SET name = @Name, description = @Description, type = @Type, price = @Price WHERE id = @Id",
                   new { Id = productEntity.Id, Name = productEntity.Name, Description = productEntity.Description, Type = productEntity.Type, Price = productEntity.Price });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<int> DeleteProduct(Guid id)
        {
            try
            {
                var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var rowsAffected = await connection.ExecuteAsync
                    ("DELETE FROM products WHERE id = @Id", new { Id = id });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<ProductEntity> GetProductById(Guid id)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var product = await connection.QuerySingleAsync<ProductEntity>
                    (@"SELECT id, name, description, type, price FROM 
                   products WHERE id = @Id",
                    new { Id = id });

                return (ProductEntity)product;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ProductEntity();
            }
        }

        public async Task<List<ProductEntity>> GetProducts()
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var products = await connection.QueryAsync<ProductEntity>
                    ("SELECT id, name, description, type, price FROM products");

                return (List<ProductEntity>)products;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<ProductEntity>();
            }
        }

        #endregion product actions

        #region bundle actions

        public async Task<Guid> CreateBundle(BundleEntity bundleEntity)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                Guid createdBundleId = await connection.QuerySingleAsync<Guid>
                    (@"INSERT INTO bundles (name, discount) VALUES (@Name, @Discount) RETURNING id",
                    new { Name = bundleEntity.Name, Discount = bundleEntity.Discount });

                return createdBundleId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Guid.Empty;
            }
        }

        public async Task<int> UpdateBundle(BundleEntity bundleEntity)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                int rowsAffected = await connection.ExecuteAsync
                    ("UPDATE bundles SET name = @Name, discount = @Discount WHERE id = @Id",
                   new { Id = bundleEntity.Id, Name = bundleEntity.Name, Discount = bundleEntity.Discount});

                return rowsAffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<int> DeleteBundle(Guid id)
        {
            try
            {
                var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var rowsAffected = await connection.ExecuteAsync
                    ("DELETE FROM bundles WHERE id = @Id", new { Id = id });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<BundleEntity> GetBundleById(Guid id)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var bundle = await connection.QuerySingleAsync<BundleEntity>
                    (@"SELECT b.id, b.name, b.discount, array_remove(array_agg(bp.product_id), NULL) as product_ids
                       FROM bundles as b
                       LEFT JOIN bundle_products as bp ON b.id = bp.bundle_id
                       WHERE b.id = @Id
                       GROUP BY b.id",
                       new { Id = id });

                return (BundleEntity)bundle;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new BundleEntity();
            }
            
        }

        public async Task<List<BundleEntity>> GetBundles()
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var bundles = await connection.QueryAsync<BundleEntity>
                    (@"SELECT b.id, b.name, b.discount, array_remove(array_agg(bp.product_id), NULL) as product_ids
                       FROM bundles as b
                       LEFT JOIN bundle_products as bp ON b.id = bp.bundle_id
                       GROUP BY b.id");

                return (List<BundleEntity>)bundles;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<BundleEntity>();
            }
        }

        #endregion bundle actions

        #region bundle product actions

        public async Task<int> AddProductsToBundle(List<BundleProductEntity> bundleProducts)
        {
            try
            {
                DapperPlusManager.Entity<BundleProductEntity>().Table("bundle_products");
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));

                connection.BulkInsert<BundleProductEntity>(bundleProducts);

                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public async Task<int> AddProductToBundle(BundleProductEntity bundleProductEntity)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var rowsAffected = await connection.ExecuteAsync
                    (@"INSERT INTO bundle_products (bundle_id, product_id) VALUES (@BundleId, @ProductId)",
                    new { BundleId = bundleProductEntity.BundleId, ProductId = bundleProductEntity.ProductId });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        public async Task<int> DeleteProductFromBundle(Guid id)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var rowsAffected = await connection.ExecuteAsync
                    (@"DELETE FROM bundle_products WHERE id = @Id",
                    new { Id = id });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        #endregion bundle product actions

        #region department actions

        public async Task<int> CreateDepartment(DepartmentEntity departmentEntity)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                int rowsAffected = await connection.ExecuteAsync
                    ("INSERT INTO departments (id, name, description) VALUES (@Id, @Name, @Description)",
                    new { Id = departmentEntity.Id, Name = departmentEntity.Name, Description = departmentEntity.Description });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public async Task<int> UpdateDepartment(DepartmentEntity departmentEntity)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                int rowsAffected = await connection.ExecuteAsync
                    ("UPDATE departments SET name = @Name, description = @Description WHERE id = @Id",
                    new { Id = departmentEntity.Id, Name = departmentEntity.Name, Description = departmentEntity.Description });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public async Task<int> DeleteDepartment(int id)
        {
            try
            {
                var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var rowsAffected = await connection.ExecuteAsync
                    ("DELETE FROM departments WHERE id = @Id", new { Id = id });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public async Task<DepartmentEntity> GetDepartmentById(int id)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var department = await connection.QuerySingleAsync<DepartmentEntity>
                    (@"SELECT id, name, description FROM departments WHERE id = @Id",
                    new { Id = id });

                return (DepartmentEntity)department;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new DepartmentEntity();
            }
        }

        public async Task<List<DepartmentEntity>> GetDepartments()
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var departments = await connection.QueryAsync<DepartmentEntity>
                    ("SELECT id, name, description FROM departments");

                return (List<DepartmentEntity>)departments;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<DepartmentEntity>();
            }
        }

        #endregion department actions

        #region product type actions

        public async Task<int> CreateProductType(ProductTypeEntity productTypeEntity)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                int rowsAffected = await connection.ExecuteAsync
                    ("INSERT INTO product_types (id, name, department) VALUES (@Id, @Name, @Department)",
                    new { Id = productTypeEntity.Id, Name = productTypeEntity.Name, Department = productTypeEntity.Department });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public async Task<int> UpdateProductType(ProductTypeEntity productTypeEntity)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                int rowsAffected = await connection.ExecuteAsync
                    ("UPDATE product_types SET name = @Name, department = @Department WHERE id = @Id",
                   new { Id = productTypeEntity.Id, Name = productTypeEntity.Name, Department = productTypeEntity.Department });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public async Task<int> DeleteProductType(int id)
        {
            try
            {
                var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var rowsAffected = await connection.ExecuteAsync
                    ("DELETE FROM product_types WHERE id = @Id", new { Id = id });

                return rowsAffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public async Task<ProductTypeEntity> GetProductTypeById(int id)
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var productType = await connection.QuerySingleAsync<ProductTypeEntity>
                    (@"SELECT id, name, department FROM 
                   product_types WHERE id = @Id",
                    new { Id = id });

                return (ProductTypeEntity)productType;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ProductTypeEntity();
            }
        }

        public async Task<List<ProductTypeEntity>> GetProductTypes()
        {
            try
            {
                using var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"));
                var products = await connection.QueryAsync<ProductTypeEntity>
                    ("SELECT id, name, department FROM product_types");

                return (List<ProductTypeEntity>)products;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<ProductTypeEntity>();
            }
        }

        #endregion product type actions
    }
}