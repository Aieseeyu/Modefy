using System.Data;
using Microsoft.Data.SqlClient;
using ModefyEcommerce.Data;
using ModefyEcommerce.Interfaces;
using ModefyEcommerce.Models;

namespace ModefyEcommerce.Repositories
{
    public class AdminUserRepository : IRepository<AdminUser>
    {
        private readonly SqlConnectionFactory _factory;

        public AdminUserRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<AdminUser> GetAll()
        {
            List<AdminUser> users = new List<AdminUser>();

            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM AdminUser", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AdminUser user = new AdminUser
                            {
                                Id = Convert.ToInt32(reader["admin_user_id"]),
                                FirstName = reader["admin_user_first_name"].ToString(),
                                LastName = reader["admin_user_last_name"].ToString(),
                                Email = reader["admin_user_email"].ToString(),
                                Password = reader["admin_user_password"].ToString(),
                                RoleId = Convert.ToInt32(reader["admin_user_role_id"])
                            };

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }

        public AdminUser? GetById(int id)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM AdminUser WHERE admin_user_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new AdminUser
                            {
                                Id = Convert.ToInt32(reader["admin_user_id"]),
                                FirstName = reader["admin_user_first_name"].ToString(),
                                LastName = reader["admin_user_last_name"].ToString(),
                                Email = reader["admin_user_email"].ToString(),
                                Password = reader["admin_user_password"].ToString(),
                                RoleId = Convert.ToInt32(reader["admin_user_role_id"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        public AdminUser? Add(AdminUser user)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    INSERT INTO AdminUser (
                        admin_user_first_name, admin_user_last_name, admin_user_email,
                        admin_user_password, admin_user_role_id
                    )
                    VALUES (
                        @firstName, @lastName, @email,
                        @password, @roleId
                    );
                    SELECT CAST(SCOPE_IDENTITY() AS INT);", connection))
                {
                    command.Parameters.Add("@firstName", SqlDbType.NVarChar, 50).Value = user.FirstName;
                    command.Parameters.Add("@lastName", SqlDbType.NVarChar, 50).Value = user.LastName;
                    command.Parameters.Add("@email", SqlDbType.NVarChar, 100).Value = user.Email;
                    command.Parameters.Add("@password", SqlDbType.NVarChar, 255).Value = user.Password;
                    command.Parameters.Add("@roleId", SqlDbType.Int).Value = user.RoleId;

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

        public bool Update(AdminUser user)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    UPDATE AdminUser
                    SET admin_user_first_name = @firstName,
                        admin_user_last_name = @lastName,
                        admin_user_email = @email,
                        admin_user_password = @password,
                        admin_user_role_id = @roleId
                    WHERE admin_user_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = user.Id;
                    command.Parameters.Add("@firstName", SqlDbType.NVarChar, 50).Value = user.FirstName;
                    command.Parameters.Add("@lastName", SqlDbType.NVarChar, 50).Value = user.LastName;
                    command.Parameters.Add("@email", SqlDbType.NVarChar, 100).Value = user.Email;
                    command.Parameters.Add("@password", SqlDbType.NVarChar, 255).Value = user.Password;
                    command.Parameters.Add("@roleId", SqlDbType.Int).Value = user.RoleId;

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

                using (SqlCommand command = new SqlCommand("DELETE FROM AdminUser WHERE admin_user_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
