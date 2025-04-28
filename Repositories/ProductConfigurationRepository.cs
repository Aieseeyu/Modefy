using System.Data;
using Microsoft.Data.SqlClient;
using ModefyEcommerce.Data;
using ModefyEcommerce.Interfaces;
using ModefyEcommerce.Models;

namespace ModefyEcommerce.Repositories
{
    public class ProductConfigurationRepository: IRepository<ProductConfiguration>
    {
        private readonly SqlConnectionFactory _factory;

        public ProductConfigurationRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<ProductConfiguration> GetAll()
        {
            List<ProductConfiguration> configs = new List<ProductConfiguration>();

            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM ProductConfiguration", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductConfiguration config = new ProductConfiguration
                            {
                                ProductConfigurationId = Convert.ToInt32(reader["product_config_id"]),
                                ProductVariantId = Convert.ToInt32(reader["product_config_variant_id"]),
                                VariationOptionId = Convert.ToInt32(reader["product_config_option_id"])
                            };

                            configs.Add(config);
                        }
                    }
                }
            }

            return configs;
        }

        public ProductConfiguration? GetById(int id)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM ProductConfiguration WHERE product_config_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ProductConfiguration
                            {
                                ProductConfigurationId = Convert.ToInt32(reader["product_config_id"]),
                                ProductVariantId = Convert.ToInt32(reader["product_config_variant_id"]),
                                VariationOptionId = Convert.ToInt32(reader["product_config_option_id"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        public ProductConfiguration? Add(ProductConfiguration config)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    INSERT INTO ProductConfiguration (product_config_variant_id, product_config_option_id)
                    VALUES (@variantId, @optionId);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);", connection))
                {
                    command.Parameters.Add("@variantId", SqlDbType.Int).Value = config.ProductVariantId;
                    command.Parameters.Add("@optionId", SqlDbType.Int).Value = config.VariationOptionId;

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

        public bool Update(ProductConfiguration config)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    UPDATE ProductConfiguration
                    SET product_config_variant_id = @variantId,
                        product_config_option_id = @optionId
                    WHERE product_config_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = config.ProductConfigurationId;
                    command.Parameters.Add("@variantId", SqlDbType.Int).Value = config.ProductVariantId;
                    command.Parameters.Add("@optionId", SqlDbType.Int).Value = config.VariationOptionId;

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

                using (SqlCommand command = new SqlCommand("DELETE FROM ProductConfiguration WHERE product_config_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
