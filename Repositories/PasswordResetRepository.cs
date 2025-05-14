using System.Data;
using Microsoft.Data.SqlClient;
using ModefyEcommerce.Data;
using ModefyEcommerce.Interfaces;
using ModefyEcommerce.Models;

namespace ModefyEcommerce.Repositories
{
    public class PasswordResetRepository : IRepository<PasswordReset>
    {
        private readonly SqlConnectionFactory _factory;

        public PasswordResetRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<PasswordReset> GetAll()
        {
            List<PasswordReset> resets = new List<PasswordReset>();

            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM PasswordReset", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PasswordReset reset = new PasswordReset
                            {
                                Id = Convert.ToInt32(reader["password_reset_id"]),
                                CustomerId = Convert.ToInt32(reader["password_reset_customer_id"]),
                                Token = reader["password_reset_token"].ToString(),
                                Expiration = Convert.ToDateTime(reader["password_reset_expiration"]),
                                Used = Convert.ToBoolean(reader["password_reset_used"])
                            };

                            resets.Add(reset);
                        }
                    }
                }
            }

            return resets;
        }

        public PasswordReset? GetById(int id)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM PasswordReset WHERE password_reset_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PasswordReset
                            {
                                Id = Convert.ToInt32(reader["password_reset_id"]),
                                CustomerId = Convert.ToInt32(reader["password_reset_customer_id"]),
                                Token = reader["password_reset_token"].ToString(),
                                Expiration = Convert.ToDateTime(reader["password_reset_expiration"]),
                                Used = Convert.ToBoolean(reader["password_reset_used"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        public PasswordReset? Add(PasswordReset reset)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    INSERT INTO PasswordReset (
                        password_reset_customer_id, password_reset_token,
                        password_reset_expiration, password_reset_used
                    )
                    VALUES (
                        @customerId, @token, @expiration, @used
                    );
                    SELECT CAST(SCOPE_IDENTITY() AS INT);", connection))
                {
                    command.Parameters.Add("@customerId", SqlDbType.Int).Value = reset.CustomerId;
                    command.Parameters.Add("@token", SqlDbType.NVarChar, 255).Value = reset.Token;
                    command.Parameters.Add("@expiration", SqlDbType.DateTime).Value = reset.Expiration;
                    command.Parameters.Add("@used", SqlDbType.Bit).Value = reset.Used;

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

        public bool Update(PasswordReset reset)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    UPDATE PasswordReset
                    SET password_reset_customer_id = @customerId,
                        password_reset_token = @token,
                        password_reset_expiration = @expiration,
                        password_reset_used = @used
                    WHERE password_reset_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = reset.Id;
                    command.Parameters.Add("@customerId", SqlDbType.Int).Value = reset.CustomerId;
                    command.Parameters.Add("@token", SqlDbType.NVarChar, 255).Value = reset.Token;
                    command.Parameters.Add("@expiration", SqlDbType.DateTime).Value = reset.Expiration;
                    command.Parameters.Add("@used", SqlDbType.Bit).Value = reset.Used;

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

                using (SqlCommand command = new SqlCommand("DELETE FROM PasswordReset WHERE password_reset_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
