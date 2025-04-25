using System.Data;
using Microsoft.Data.SqlClient;
using ModefyEcommerce.Data;
using ModefyEcommerce.Models;

namespace ModefyEcommerce.Repositories
{
    public class ProductImageRepository
    {
        private readonly SqlConnectionFactory _factory;

        public ProductImageRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<ProductImage> GetAll()
        {
            List<ProductImage> images = new List<ProductImage>();

            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM ProductImage", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductImage image = new ProductImage
                            {
                                ProductImageId = Convert.ToInt32(reader["product_image_id"]),
                                ProductId = Convert.ToInt32(reader["product_image_product_id"]),
                                Url = reader["product_image_url"].ToString()
                            };

                            images.Add(image);
                        }
                    }
                }
            }

            return images;
        }

        public ProductImage? GetById(int id)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM ProductImage WHERE product_image_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ProductImage
                            {
                                ProductImageId = Convert.ToInt32(reader["product_image_id"]),
                                ProductId = Convert.ToInt32(reader["product_image_product_id"]),
                                Url = reader["product_image_url"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }

        public ProductImage? Add(ProductImage image)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    INSERT INTO ProductImage (product_image_product_id, product_image_url)
                    VALUES (@productId, @url);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);", connection))
                {
                    command.Parameters.Add("@productId", SqlDbType.Int).Value = image.ProductId;
                    command.Parameters.Add("@url", SqlDbType.NVarChar, 255).Value = image.Url;

                    object? result = command.ExecuteScalar();

                    if (result != null && result.ToString() != "")
                    {
                        int newId = Convert.ToInt32(result.ToString());
                        return GetById(newId);
                    }

                    return null;
                }
            }
        }

        public bool Update(ProductImage image)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    UPDATE ProductImage
                    SET product_image_product_id = @productId,
                        product_image_url = @url
                    WHERE product_image_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = image.ProductImageId;
                    command.Parameters.Add("@productId", SqlDbType.Int).Value = image.ProductId;
                    command.Parameters.Add("@url", SqlDbType.NVarChar, 255).Value = image.Url;

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool Delete(int id)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("DELETE FROM ProductImage WHERE product_image_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
