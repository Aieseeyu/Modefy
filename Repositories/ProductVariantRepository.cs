using System.Data;
using Microsoft.Data.SqlClient;
using ModefyEcommerce.Data;
using ModefyEcommerce.Models;

namespace ModefyEcommerce.Repositories
{
    public class ProductVariantRepository
    {
        private readonly SqlConnectionFactory _factory;

        public ProductVariantRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<ProductVariant> GetAll()
        {
            List<ProductVariant> variants = new List<ProductVariant>();

            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM ProductVariant", connection)) 
                { 
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductVariant variant = new ProductVariant
                            {
                                ProductVariantId = Convert.ToInt32(reader["product_variant_id"]),
                                ProductId = Convert.ToInt32(reader["product_variant_product_id"]),
                                Sku = reader["product_variant_sku"].ToString(),
                                Price = Convert.ToDecimal(reader["product_variant_price"]),
                                Stock = Convert.ToInt32(reader["product_variant_stock"])
                            };

                            variants.Add(variant);
                        }
                    }
                }
            }

            return variants;
        }

        public ProductVariant? GetById(int id)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM ProductVariant WHERE product_variant_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ProductVariant
                            {
                                ProductVariantId = Convert.ToInt32(reader["product_variant_id"]),
                                ProductId = Convert.ToInt32(reader["product_variant_product_id"]),
                                Sku = reader["product_variant_sku"].ToString(),
                                Price = Convert.ToDecimal(reader["product_variant_price"]),
                                Stock = Convert.ToInt32(reader["product_variant_stock"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        public ProductVariant? Add(ProductVariant variant)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    INSERT INTO ProductVariant (product_variant_product_id, product_variant_sku, product_variant_price, product_variant_stock)
                    VALUES (@productId, @sku, @price, @stock);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);", connection))
                {
                    command.Parameters.Add("@productId", SqlDbType.Int).Value = variant.ProductId;
                    command.Parameters.Add("@sku", SqlDbType.NVarChar, 50).Value = variant.Sku;
                    command.Parameters.Add("@price", SqlDbType.Decimal).Value = variant.Price;
                    command.Parameters.Add("@stock", SqlDbType.Int).Value = variant.Stock;

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

        public bool Update(ProductVariant variant)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    UPDATE ProductVariant
                    SET product_variant_product_id = @productId,
                        product_variant_sku = @sku,
                        product_variant_price = @price,
                        product_variant_stock = @stock
                    WHERE product_variant_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = variant.ProductVariantId;
                    command.Parameters.Add("@productId", SqlDbType.Int).Value = variant.ProductId;
                    command.Parameters.Add("@sku", SqlDbType.NVarChar, 50).Value = variant.Sku;
                    command.Parameters.Add("@price", SqlDbType.Decimal).Value = variant.Price;
                    command.Parameters.Add("@stock", SqlDbType.Int).Value = variant.Stock;

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

                using (SqlCommand command = new SqlCommand("DELETE FROM ProductVariant WHERE product_variant_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
