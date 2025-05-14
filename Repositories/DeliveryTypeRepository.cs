using System.Data;
using Microsoft.Data.SqlClient;
using ModefyEcommerce.Data;
using ModefyEcommerce.Interfaces;
using ModefyEcommerce.Models;

namespace ModefyEcommerce.Repositories
{
    public class DeliveryTypeRepository : IRepository<DeliveryType>
    {
        private readonly SqlConnectionFactory _factory;

        public DeliveryTypeRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<DeliveryType> GetAll()
        {
            List<DeliveryType> deliveryTypes = new List<DeliveryType>();

            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM DeliveryType", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DeliveryType deliveryType = new DeliveryType
                            {
                                Id = Convert.ToInt32(reader["delivery_type_id"]),
                                Name = reader["delivery_type_name"].ToString(),
                                Description = reader["delivery_type_description"] != DBNull.Value ? reader["delivery_type_description"].ToString() : null,
                                Price = Convert.ToDecimal(reader["delivery_type_price"]),
                                DelayDays = Convert.ToInt32(reader["delivery_type_delay_days"]),
                                CarrierId = Convert.ToInt32(reader["delivery_type_carrier_id"])
                            };

                            deliveryTypes.Add(deliveryType);
                        }
                    }
                }
            }

            return deliveryTypes;
        }

        public DeliveryType? GetById(int id)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM DeliveryType WHERE delivery_type_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DeliveryType
                            {
                                Id = Convert.ToInt32(reader["delivery_type_id"]),
                                Name = reader["delivery_type_name"].ToString(),
                                Description = reader["delivery_type_description"] != DBNull.Value ? reader["delivery_type_description"].ToString() : null,
                                Price = Convert.ToDecimal(reader["delivery_type_price"]),
                                DelayDays = Convert.ToInt32(reader["delivery_type_delay_days"]),
                                CarrierId = Convert.ToInt32(reader["delivery_type_carrier_id"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        public DeliveryType? Add(DeliveryType deliveryType)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    INSERT INTO DeliveryType (
                        delivery_type_name, delivery_type_description,
                        delivery_type_price, delivery_type_delay_days, delivery_type_carrier_id
                    )
                    VALUES (
                        @name, @description, @price, @delayDays, @carrierId
                    );
                    SELECT CAST(SCOPE_IDENTITY() AS INT);", connection))
                {
                    command.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = deliveryType.Name;
                    command.Parameters.Add("@description", SqlDbType.NVarChar, 255).Value = (object?)deliveryType.Description ?? DBNull.Value;
                    command.Parameters.Add("@price", SqlDbType.Decimal).Value = deliveryType.Price;
                    command.Parameters.Add("@delayDays", SqlDbType.Int).Value = deliveryType.DelayDays;
                    command.Parameters.Add("@carrierId", SqlDbType.Int).Value = deliveryType.CarrierId;

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

        public bool Update(DeliveryType deliveryType)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    UPDATE DeliveryType
                    SET delivery_type_name = @name,
                        delivery_type_description = @description,
                        delivery_type_price = @price,
                        delivery_type_delay_days = @delayDays,
                        delivery_type_carrier_id = @carrierId
                    WHERE delivery_type_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = deliveryType.Id;
                    command.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = deliveryType.Name;
                    command.Parameters.Add("@description", SqlDbType.NVarChar, 255).Value = (object?)deliveryType.Description ?? DBNull.Value;
                    command.Parameters.Add("@price", SqlDbType.Decimal).Value = deliveryType.Price;
                    command.Parameters.Add("@delayDays", SqlDbType.Int).Value = deliveryType.DelayDays;
                    command.Parameters.Add("@carrierId", SqlDbType.Int).Value = deliveryType.CarrierId;

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

                using (SqlCommand command = new SqlCommand("DELETE FROM DeliveryType WHERE delivery_type_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
