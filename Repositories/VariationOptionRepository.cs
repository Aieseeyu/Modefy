using System.Data;
using Microsoft.Data.SqlClient;
using ModefyEcommerce.Data;
using ModefyEcommerce.Interfaces;
using ModefyEcommerce.Models;

namespace ModefyEcommerce.Repositories
{
    public class VariationOptionRepository : IRepository<VariationOption>
    {
        private readonly SqlConnectionFactory _factory;

        public VariationOptionRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<VariationOption> GetAll()
        {
            List<VariationOption> options = new List<VariationOption>();

            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM VariationOption", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            VariationOption option = new VariationOption
                            {
                                VariationOptionId = Convert.ToInt32(reader["variation_option_id"]),
                                VariationId = Convert.ToInt32(reader["variation_option_variation_id"]),
                                Value = reader["variation_option_value"].ToString()
                            };

                            options.Add(option);
                        }
                    }
                }
            }

            return options;
        }

        public VariationOption? GetById(int id)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM VariationOption WHERE variation_option_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new VariationOption
                            {
                                VariationOptionId = Convert.ToInt32(reader["variation_option_id"]),
                                VariationId = Convert.ToInt32(reader["variation_option_variation_id"]),
                                Value = reader["variation_option_value"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }

        public VariationOption? Add(VariationOption option)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    INSERT INTO VariationOption (variation_option_variation_id, variation_option_value)
                    VALUES (@variationId, @value);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);", connection))
                {
                    command.Parameters.Add("@variationId", SqlDbType.Int).Value = option.VariationId;
                    command.Parameters.Add("@value", SqlDbType.NVarChar, 50).Value = option.Value;

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

        public bool Update(VariationOption option)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    UPDATE VariationOption
                    SET variation_option_variation_id = @variationId,
                        variation_option_value = @value
                    WHERE variation_option_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = option.VariationOptionId;
                    command.Parameters.Add("@variationId", SqlDbType.Int).Value = option.VariationId;
                    command.Parameters.Add("@value", SqlDbType.NVarChar, 50).Value = option.Value;

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

                using (SqlCommand command = new SqlCommand("DELETE FROM VariationOption WHERE variation_option_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
