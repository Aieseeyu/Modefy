using System.Data;
using Microsoft.Data.SqlClient;
using ModefyEcommerce.Data;
using ModefyEcommerce.Interfaces;
using ModefyEcommerce.Models;

namespace ModefyEcommerce.Repositories
{
    public class VariationRepository : IRepository<Variation>
    {
        private readonly SqlConnectionFactory _factory;

        public VariationRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<Variation> GetAll()
        {
            List<Variation> variations = new List<Variation>();

            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Variation", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Variation variation = new Variation
                            {
                                VariationId = Convert.ToInt32(reader["variation_id"]),
                                Label = reader["variation_label"].ToString()
                            };

                            variations.Add(variation);
                        }
                    }
                }
            }

            return variations;
        }

        public Variation? GetById(int id)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Variation WHERE variation_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Variation
                            {
                                VariationId = Convert.ToInt32(reader["variation_id"]),
                                Label = reader["variation_label"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }

        public Variation? Add(Variation variation)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    INSERT INTO Variation (variation_label)
                    VALUES (@label);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);", connection))
                {
                    command.Parameters.Add("@label", SqlDbType.NVarChar, 50).Value = variation.Label;

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

        public bool Update(Variation variation)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    UPDATE Variation
                    SET variation_label = @label
                    WHERE variation_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = variation.VariationId;
                    command.Parameters.Add("@label", SqlDbType.NVarChar, 50).Value = variation.Label;

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

                using (SqlCommand command = new SqlCommand("DELETE FROM Variation WHERE variation_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
