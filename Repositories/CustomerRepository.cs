using System.Data;
using Microsoft.Data.SqlClient;
using ModefyEcommerce.Data;
using ModefyEcommerce.Interfaces;
using ModefyEcommerce.Models;

namespace ModefyEcommerce.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly SqlConnectionFactory _factory;

        public CustomerRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Customer", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer
                            {
                                CustomerId = Convert.ToInt32(reader["customer_id"]),
                                FirstName = reader["customer_first_name"].ToString(),
                                LastName = reader["customer_last_name"].ToString(),
                                Sex = Convert.ToByte(reader["customer_sex"]),
                                Email = reader["customer_email"].ToString(),
                                Password = reader["customer_password"].ToString(),
                                PhoneNumber = reader["customer_phone_number"].ToString(),
                                CreatedAt = (DateTime)reader["customer_created_at"],
                                Status = reader["customer_status"].ToString()
                            };

                            customers.Add(customer);
                        }
                    }
                }
            }

            return customers;
        }

        public Customer? GetById(int id)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE customer_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Customer
                            {
                                CustomerId = Convert.ToInt32(reader["customer_id"]),
                                FirstName = reader["customer_first_name"].ToString(),
                                LastName = reader["customer_last_name"].ToString(),
                                Sex = Convert.ToByte(reader["customer_sex"]),
                                Email = reader["customer_email"].ToString(),
                                Password = reader["customer_password"].ToString(),
                                PhoneNumber = reader["customer_phone_number"].ToString(),
                                CreatedAt = (DateTime)reader["customer_created_at"],
                                Status = reader["customer_status"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }

        public Customer? Add(Customer customer)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    INSERT INTO Customer (customer_first_name, customer_last_name, customer_sex, customer_email, customer_password, customer_phone_number, customer_status)
                    VALUES (@firstName, @lastName, @sex, @email, @password, @phone, @status);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);", connection))
                {
                    command.Parameters.Add("@firstName", SqlDbType.NVarChar, 50).Value = customer.FirstName;
                    command.Parameters.Add("@lastName", SqlDbType.NVarChar, 50).Value = customer.LastName;
                    command.Parameters.Add("@sex", SqlDbType.TinyInt).Value = customer.Sex;
                    command.Parameters.Add("@email", SqlDbType.NVarChar, 100).Value = customer.Email;
                    command.Parameters.Add("@password", SqlDbType.NVarChar, 255).Value = customer.Password;
                    command.Parameters.Add("@phone", SqlDbType.NVarChar, 20).Value = customer.PhoneNumber;
                    command.Parameters.Add("@status", SqlDbType.NVarChar, 20).Value = customer.Status;

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

        public bool Update(Customer customer)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    UPDATE Customer
                    SET customer_first_name = @firstName,
                        customer_last_name = @lastName,
                        customer_sex = @sex,
                        customer_email = @email,
                        customer_password = @password,
                        customer_phone_number = @phone,
                        customer_status = @status
                    WHERE customer_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = customer.CustomerId;
                    command.Parameters.Add("@firstName", SqlDbType.NVarChar, 50).Value = customer.FirstName;
                    command.Parameters.Add("@lastName", SqlDbType.NVarChar, 50).Value = customer.LastName;
                    command.Parameters.Add("@sex", SqlDbType.TinyInt).Value = customer.Sex;
                    command.Parameters.Add("@email", SqlDbType.NVarChar, 100).Value = customer.Email;
                    command.Parameters.Add("@password", SqlDbType.NVarChar, 255).Value = customer.Password;
                    command.Parameters.Add("@phone", SqlDbType.NVarChar, 20).Value = customer.PhoneNumber;
                    command.Parameters.Add("@status", SqlDbType.NVarChar, 20).Value = customer.Status;

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

                using (SqlCommand command = new SqlCommand("DELETE FROM Customer WHERE customer_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // Ajout de la méthode GetByEmail dans CustomerRepository
        public Customer? GetByEmail(string email)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Customer WHERE customer_email = @Email", connection))
                {
                    command.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = email;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Customer
                            {
                                CustomerId = Convert.ToInt32(reader["customer_id"]),
                                FirstName = reader["customer_first_name"].ToString(),
                                LastName = reader["customer_last_name"].ToString(),
                                Sex = Convert.ToByte(reader["customer_sex"]),
                                Email = reader["customer_email"].ToString(),
                                Password = reader["customer_password"].ToString(),
                                PhoneNumber = reader["customer_phone_number"].ToString(),
                                CreatedAt = (DateTime)reader["customer_created_at"],
                                Status = reader["customer_status"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }

    }
}


