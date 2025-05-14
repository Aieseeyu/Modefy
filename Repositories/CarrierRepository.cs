using System.Data;
using Microsoft.Data.SqlClient;
using ModefyEcommerce.Data;
using ModefyEcommerce.Interfaces;
using ModefyEcommerce.Models;

namespace ModefyEcommerce.Repositories
{
    public class CarrierRepository : IRepository<Carrier>
    {
        private readonly SqlConnectionFactory _factory;

        public CarrierRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<Carrier> GetAll()
        {
            List<Carrier> carriers = new List<Carrier>();

            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Carrier", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Carrier carrier = new Carrier
                            {
                                Id = Convert.ToInt32(reader["carrier_id"]),
                                Name = reader["carrier_name"].ToString(),
                                ContactEmail = reader["carrier_contact_email"].ToString(),
                                ContactPhone = reader["carrier_contact_phone"].ToString(),
                                HeadquarterAddress = reader["carrier_headquarter_address"].ToString(),
                                Country = reader["carrier_country"].ToString(),
                                ShippingZones = reader["carrier_shipping_zones"].ToString(),
                                TrackingUrl = reader["carrier_tracking_url"] != DBNull.Value ? reader["carrier_tracking_url"].ToString() : null,
                                LogoUrl = reader["carrier_logo_url"] != DBNull.Value ? reader["carrier_logo_url"].ToString() : null,
                                CreatedAt = Convert.ToDateTime(reader["carrier_created_at"]),
                                IsActive = Convert.ToBoolean(reader["carrier_is_active"])
                            };

                            carriers.Add(carrier);
                        }
                    }
                }
            }

            return carriers;
        }

        public Carrier? GetById(int id)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Carrier WHERE carrier_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Carrier
                            {
                                Id = Convert.ToInt32(reader["carrier_id"]),
                                Name = reader["carrier_name"].ToString(),
                                ContactEmail = reader["carrier_contact_email"].ToString(),
                                ContactPhone = reader["carrier_contact_phone"].ToString(),
                                HeadquarterAddress = reader["carrier_headquarter_address"].ToString(),
                                Country = reader["carrier_country"].ToString(),
                                ShippingZones = reader["carrier_shipping_zones"].ToString(),
                                TrackingUrl = reader["carrier_tracking_url"] != DBNull.Value ? reader["carrier_tracking_url"].ToString() : null,
                                LogoUrl = reader["carrier_logo_url"] != DBNull.Value ? reader["carrier_logo_url"].ToString() : null,
                                CreatedAt = Convert.ToDateTime(reader["carrier_created_at"]),
                                IsActive = Convert.ToBoolean(reader["carrier_is_active"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        public Carrier? Add(Carrier carrier)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    INSERT INTO Carrier (
                        carrier_name, carrier_contact_email, carrier_contact_phone,
                        carrier_headquarter_address, carrier_country, carrier_shipping_zones,
                        carrier_tracking_url, carrier_logo_url, carrier_created_at, carrier_is_active
                    )
                    VALUES (
                        @name, @email, @phone,
                        @address, @country, @zones,
                        @tracking, @logo, @created, @active
                    );
                    SELECT CAST(SCOPE_IDENTITY() AS INT);", connection))
                {
                    command.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = carrier.Name;
                    command.Parameters.Add("@email", SqlDbType.NVarChar, 100).Value = carrier.ContactEmail;
                    command.Parameters.Add("@phone", SqlDbType.NVarChar, 20).Value = carrier.ContactPhone;
                    command.Parameters.Add("@address", SqlDbType.NVarChar, 255).Value = carrier.HeadquarterAddress;
                    command.Parameters.Add("@country", SqlDbType.NVarChar, 50).Value = carrier.Country;
                    command.Parameters.Add("@zones", SqlDbType.NVarChar, 100).Value = carrier.ShippingZones;
                    command.Parameters.Add("@tracking", SqlDbType.NVarChar, 255).Value = (object?)carrier.TrackingUrl ?? DBNull.Value;
                    command.Parameters.Add("@logo", SqlDbType.NVarChar, 255).Value = (object?)carrier.LogoUrl ?? DBNull.Value;
                    command.Parameters.Add("@created", SqlDbType.DateTime).Value = carrier.CreatedAt;
                    command.Parameters.Add("@active", SqlDbType.Bit).Value = carrier.IsActive;

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

        public bool Update(Carrier carrier)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    UPDATE Carrier
                    SET carrier_name = @name,
                        carrier_contact_email = @email,
                        carrier_contact_phone = @phone,
                        carrier_headquarter_address = @address,
                        carrier_country = @country,
                        carrier_shipping_zones = @zones,
                        carrier_tracking_url = @tracking,
                        carrier_logo_url = @logo,
                        carrier_created_at = @created,
                        carrier_is_active = @active
                    WHERE carrier_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = carrier.Id;
                    command.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = carrier.Name;
                    command.Parameters.Add("@email", SqlDbType.NVarChar, 100).Value = carrier.ContactEmail;
                    command.Parameters.Add("@phone", SqlDbType.NVarChar, 20).Value = carrier.ContactPhone;
                    command.Parameters.Add("@address", SqlDbType.NVarChar, 255).Value = carrier.HeadquarterAddress;
                    command.Parameters.Add("@country", SqlDbType.NVarChar, 50).Value = carrier.Country;
                    command.Parameters.Add("@zones", SqlDbType.NVarChar, 100).Value = carrier.ShippingZones;
                    command.Parameters.Add("@tracking", SqlDbType.NVarChar, 255).Value = (object?)carrier.TrackingUrl ?? DBNull.Value;
                    command.Parameters.Add("@logo", SqlDbType.NVarChar, 255).Value = (object?)carrier.LogoUrl ?? DBNull.Value;
                    command.Parameters.Add("@created", SqlDbType.DateTime).Value = carrier.CreatedAt;
                    command.Parameters.Add("@active", SqlDbType.Bit).Value = carrier.IsActive;

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

                using (SqlCommand command = new SqlCommand("DELETE FROM Carrier WHERE carrier_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
