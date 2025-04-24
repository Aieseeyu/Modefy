using System.Data;
using Microsoft.Data.SqlClient;
using ModefyEcommerce.Data;
using ModefyEcommerce.Models;

namespace ModefyEcommerce.Repositories
{
    public class ProductRepository
    {
        private readonly SqlConnectionFactory _factory;

        // On recupere l'objet pour la connexion
        public ProductRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        // GET ALL PRODUCTS
        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Product", connection)) { 
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product();

                            product.ProductId = Convert.ToInt32(reader["product_id"]);
                            product.ProductName = reader["product_name"].ToString();
                            product.ProductDescription = reader["product_description"].ToString();
                            product.ProductStatus = reader["product_status"].ToString();
                            product.ProductCreatedAt = (DateTime)reader["product_created_at"];
                            product.ProductCategoryId = Convert.ToInt32(reader["product_category_id"]);

                            products.Add(product);
                        }
                    }
                }
            }

            return products;
        }

        // GET PRODUCT BY ID
        public Product? GetById(int id)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Product WHERE product_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Product product = new Product();

                            product.ProductId = Convert.ToInt32(reader["product_id"]);
                            product.ProductName = reader["product_name"].ToString();
                            product.ProductDescription = reader["product_description"].ToString();
                            product.ProductStatus = reader["product_status"].ToString();
                            product.ProductCreatedAt = (DateTime)reader["product_created_at"];
                            product.ProductCategoryId = Convert.ToInt32(reader["product_category_id"]);
            
                            return product;
                        }
                    }
                }
            }
            return null;
        }


        // ADD PRODUCT
        public Product? Add(Product product)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                INSERT INTO Product (product_name, product_description, product_status, product_category_id)
                VALUES (@name, @description, @status, @categoryId);
                SELECT CAST(SCOPE_IDENTITY() AS INT);", connection))
                {
                    command.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = product.ProductName;
                    command.Parameters.Add("@description", SqlDbType.Text).Value = product.ProductDescription;
                    command.Parameters.Add("@status", SqlDbType.NVarChar, 20).Value = product.ProductStatus;
                    command.Parameters.Add("@categoryId", SqlDbType.Int).Value = product.ProductCategoryId;

                    object? result = command.ExecuteScalar();

                    if (result != null)
                    {
                        int newId = Convert.ToInt32(result.ToString());
                        return GetById(newId);
                    }

                    return null;
                }
            }
        }


        // UPDATE PRODUCT
        public bool Update(Product product)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    UPDATE Product
                    SET product_name = @name,
                        product_description = @description,
                        product_status = @status,
                        product_category_id = @categoryId
                    WHERE product_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = product.ProductId;
                    command.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = product.ProductName;
                    command.Parameters.Add("@description", SqlDbType.Text).Value = product.ProductDescription;
                    command.Parameters.Add("@status", SqlDbType.NVarChar, 20).Value = product.ProductStatus;
                    command.Parameters.Add("@categoryId", SqlDbType.Int).Value = product.ProductCategoryId;

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // DELETE PRODUCT
        public bool Delete(int id)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM Product WHERE product_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

    }

}
