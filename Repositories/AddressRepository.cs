using System.Data;
using Microsoft.Data.SqlClient;
using ModefyEcommerce.Data;
using ModefyEcommerce.Interfaces;
using ModefyEcommerce.Models;

namespace ModefyEcommerce.Repositories
{
    public class AddressRepository : IRepository<Address>
    {
        private readonly SqlConnectionFactory _factory;

        public AddressRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<Address> GetAll()
        {
            List<Address> addresses = new List<Address>();

            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Address", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Address address = new Address
                            {
                                AddressId = Convert.ToInt32(reader["address_id"]),
                                CustomerId = Convert.ToInt32(reader["address_customer_id"]),
                                Line1 = reader["address_line"].ToString(),
                                Line2 = reader["address_line2"] != DBNull.Value ? reader["address_line2"].ToString() : null,
                                City = reader["address_city"].ToString(),
                                PostalCode = reader["address_postal_code"].ToString(),
                                Country = reader["address_country"].ToString(),
                                Type = reader["address_type"].ToString()
                            };

                            addresses.Add(address);
                        }
                    }
                }
            }

            return addresses;
        }

        public Address? GetById(int id)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Address WHERE address_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Address
                            {
                                AddressId = Convert.ToInt32(reader["address_id"]),
                                CustomerId = Convert.ToInt32(reader["address_customer_id"]),
                                Line1 = reader["address_line"].ToString(),
                                Line2 = reader["address_line2"] != DBNull.Value ? reader["address_line2"].ToString() : null,
                                City = reader["address_city"].ToString(),
                                PostalCode = reader["address_postal_code"].ToString(),
                                Country = reader["address_country"].ToString(),
                                Type = reader["address_type"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }

        public Address? Add(Address address)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    INSERT INTO Address (address_customer_id, address_line, address_line2, address_city, address_postal_code, address_country, address_type)
                    VALUES (@customerId, @line1, @line2, @city, @postalCode, @country, @type);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);", connection))
                {
                    command.Parameters.Add("@customerId", SqlDbType.Int).Value = address.CustomerId;
                    command.Parameters.Add("@line1", SqlDbType.NVarChar, 255).Value = address.Line1;
                    command.Parameters.Add("@line2", SqlDbType.NVarChar, 255).Value = (object?)address.Line2 ?? DBNull.Value;
                    command.Parameters.Add("@city", SqlDbType.NVarChar, 50).Value = address.City;
                    command.Parameters.Add("@postalCode", SqlDbType.NVarChar, 10).Value = address.PostalCode;
                    command.Parameters.Add("@country", SqlDbType.NVarChar, 50).Value = address.Country;
                    command.Parameters.Add("@type", SqlDbType.NVarChar, 20).Value = address.Type;

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

        public bool Update(Address address)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    UPDATE Address
                    SET address_customer_id = @customerId,
                        address_line = @line1,
                        address_line2 = @line2,
                        address_city = @city,
                        address_postal_code = @postalCode,
                        address_country = @country,
                        address_type = @type
                    WHERE address_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = address.AddressId;
                    command.Parameters.Add("@customerId", SqlDbType.Int).Value = address.CustomerId;
                    command.Parameters.Add("@line1", SqlDbType.NVarChar, 255).Value = address.Line1;
                    command.Parameters.Add("@line2", SqlDbType.NVarChar, 255).Value = (object?)address.Line2 ?? DBNull.Value;
                    command.Parameters.Add("@city", SqlDbType.NVarChar, 50).Value = address.City;
                    command.Parameters.Add("@postalCode", SqlDbType.NVarChar, 10).Value = address.PostalCode;
                    command.Parameters.Add("@country", SqlDbType.NVarChar, 50).Value = address.Country;
                    command.Parameters.Add("@type", SqlDbType.NVarChar, 20).Value = address.Type;

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

                using (SqlCommand command = new SqlCommand("DELETE FROM Address WHERE address_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
