using System.Data;
using Microsoft.Data.SqlClient;
using ModefyEcommerce.Data;
using ModefyEcommerce.Interfaces;
using ModefyEcommerce.Models;

namespace ModefyEcommerce.Repositories
{
    public class PromotionRepository : IRepository<Promotion>
    {
        private readonly SqlConnectionFactory _factory;

        public PromotionRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<Promotion> GetAll()
        {
            List<Promotion> promotions = new List<Promotion>();

            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Promotion", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Promotion promo = new Promotion
                            {
                                PromotionId = Convert.ToInt32(reader["promotion_id"]),
                                Name = reader["promotion_name"].ToString(),
                                Type = reader["promotion_type"].ToString(),
                                Value = Convert.ToDecimal(reader["promotion_value"]),
                                Unit = reader["promotion_unit"].ToString(),
                                StartDate = Convert.ToDateTime(reader["promotion_start_date"]),
                                EndDate = reader["promotion_end_date"] != DBNull.Value ? Convert.ToDateTime(reader["promotion_end_date"]) : null,
                                Code = reader["promotion_code"] != DBNull.Value ? reader["promotion_code"].ToString() : null
                            };

                            promotions.Add(promo);
                        }
                    }
                }
            }

            return promotions;
        }

        public Promotion? GetById(int id)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Promotion WHERE promotion_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Promotion
                            {
                                PromotionId = Convert.ToInt32(reader["promotion_id"]),
                                Name = reader["promotion_name"].ToString(),
                                Type = reader["promotion_type"].ToString(),
                                Value = Convert.ToDecimal(reader["promotion_value"]),
                                Unit = reader["promotion_unit"].ToString(),
                                StartDate = Convert.ToDateTime(reader["promotion_start_date"]),
                                EndDate = reader["promotion_end_date"] != DBNull.Value ? Convert.ToDateTime(reader["promotion_end_date"]) : null,
                                Code = reader["promotion_code"] != DBNull.Value ? reader["promotion_code"].ToString() : null
                            };
                        }
                    }
                }
            }

            return null;
        }

        public Promotion? Add(Promotion promo)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    INSERT INTO Promotion (promotion_name, promotion_type, promotion_value, promotion_unit, promotion_start_date, promotion_end_date, promotion_code)
                    VALUES (@name, @type, @value, @unit, @startDate, @endDate, @code);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);", connection))
                {
                    command.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = promo.Name;
                    command.Parameters.Add("@type", SqlDbType.NVarChar, 20).Value = promo.Type;
                    command.Parameters.Add("@value", SqlDbType.Decimal).Value = promo.Value;
                    command.Parameters.Add("@unit", SqlDbType.NVarChar, 10).Value = promo.Unit;
                    command.Parameters.Add("@startDate", SqlDbType.Date).Value = promo.StartDate;
                    command.Parameters.Add("@endDate", SqlDbType.Date).Value = (object?)promo.EndDate ?? DBNull.Value;
                    command.Parameters.Add("@code", SqlDbType.NVarChar, 30).Value = (object?)promo.Code ?? DBNull.Value;

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

        public bool Update(Promotion promo)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    UPDATE Promotion
                    SET promotion_name = @name,
                        promotion_type = @type,
                        promotion_value = @value,
                        promotion_unit = @unit,
                        promotion_start_date = @startDate,
                        promotion_end_date = @endDate,
                        promotion_code = @code
                    WHERE promotion_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = promo.PromotionId;
                    command.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = promo.Name;
                    command.Parameters.Add("@type", SqlDbType.NVarChar, 20).Value = promo.Type;
                    command.Parameters.Add("@value", SqlDbType.Decimal).Value = promo.Value;
                    command.Parameters.Add("@unit", SqlDbType.NVarChar, 10).Value = promo.Unit;
                    command.Parameters.Add("@startDate", SqlDbType.Date).Value = promo.StartDate;
                    command.Parameters.Add("@endDate", SqlDbType.Date).Value = (object?)promo.EndDate ?? DBNull.Value;
                    command.Parameters.Add("@code", SqlDbType.NVarChar, 30).Value = (object?)promo.Code ?? DBNull.Value;

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

                using (SqlCommand command = new SqlCommand("DELETE FROM Promotion WHERE promotion_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
