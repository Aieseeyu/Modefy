using System.Data;
using Microsoft.Data.SqlClient;
using ModefyEcommerce.Data;
using ModefyEcommerce.Interfaces;
using ModefyEcommerce.Models;

namespace ModefyEcommerce.Repositories
{
    public class AdminRoleRepository : IRepository<AdminRole>
    {
        private readonly SqlConnectionFactory _factory;

        public AdminRoleRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<AdminRole> GetAll()
        {
            List<AdminRole> roles = new List<AdminRole>();

            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM AdminRole", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AdminRole role = new AdminRole
                            {
                                Id = Convert.ToInt32(reader["admin_role_id"]),
                                Name = reader["admin_role_name"].ToString(),
                                Permissions = reader["admin_role_permissions"].ToString()
                            };

                            roles.Add(role);
                        }
                    }
                }
            }

            return roles;
        }

        public AdminRole? GetById(int id)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM AdminRole WHERE admin_role_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new AdminRole
                            {
                                Id = Convert.ToInt32(reader["admin_role_id"]),
                                Name = reader["admin_role_name"].ToString(),
                                Permissions = reader["admin_role_permissions"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }

        public AdminRole? Add(AdminRole role)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    INSERT INTO AdminRole (
                        admin_role_name, admin_role_permissions
                    )
                    VALUES (
                        @name, @permissions
                    );
                    SELECT CAST(SCOPE_IDENTITY() AS INT);", connection))
                {
                    command.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = role.Name;
                    command.Parameters.Add("@permissions", SqlDbType.NVarChar).Value = role.Permissions;

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

        public bool Update(AdminRole role)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    UPDATE AdminRole
                    SET admin_role_name = @name,
                        admin_role_permissions = @permissions
                    WHERE admin_role_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = role.Id;
                    command.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = role.Name;
                    command.Parameters.Add("@permissions", SqlDbType.NVarChar).Value = role.Permissions;

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

                using (SqlCommand command = new SqlCommand("DELETE FROM AdminRole WHERE admin_role_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
