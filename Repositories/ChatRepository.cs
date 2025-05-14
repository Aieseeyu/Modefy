using System.Data;
using Microsoft.Data.SqlClient;
using ModefyEcommerce.Data;
using ModefyEcommerce.Interfaces;
using ModefyEcommerce.Models;

namespace ModefyEcommerce.Repositories
{
    public class ChatRepository : IRepository<Chat>
    {
        private readonly SqlConnectionFactory _factory;

        public ChatRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<Chat> GetAll()
        {
            List<Chat> chats = new List<Chat>();

            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Chat", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Chat chat = new Chat
                            {
                                Id = Convert.ToInt32(reader["chat_id"]),
                                CustomerId = Convert.ToInt32(reader["chat_customer_id"]),
                                AdminUserId = reader["chat_admin_user_id"] != DBNull.Value ? Convert.ToInt32(reader["chat_admin_user_id"]) : (int?)null,
                                CreatedAt = Convert.ToDateTime(reader["chat_created_at"]),
                                IsClosed = Convert.ToBoolean(reader["chat_is_closed"])
                            };

                            chats.Add(chat);
                        }
                    }
                }
            }

            return chats;
        }

        public Chat? GetById(int id)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Chat WHERE chat_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Chat
                            {
                                Id = Convert.ToInt32(reader["chat_id"]),
                                CustomerId = Convert.ToInt32(reader["chat_customer_id"]),
                                AdminUserId = reader["chat_admin_user_id"] != DBNull.Value ? Convert.ToInt32(reader["chat_admin_user_id"]) : (int?)null,
                                CreatedAt = Convert.ToDateTime(reader["chat_created_at"]),
                                IsClosed = Convert.ToBoolean(reader["chat_is_closed"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        public Chat? Add(Chat chat)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    INSERT INTO Chat (
                        chat_customer_id, chat_admin_user_id,
                        chat_created_at, chat_is_closed
                    )
                    VALUES (
                        @customerId, @adminUserId,
                        @createdAt, @isClosed
                    );
                    SELECT CAST(SCOPE_IDENTITY() AS INT);", connection))
                {
                    command.Parameters.Add("@customerId", SqlDbType.Int).Value = chat.CustomerId;
                    command.Parameters.Add("@adminUserId", SqlDbType.Int).Value = (object?)chat.AdminUserId ?? DBNull.Value;
                    command.Parameters.Add("@createdAt", SqlDbType.DateTime).Value = chat.CreatedAt;
                    command.Parameters.Add("@isClosed", SqlDbType.Bit).Value = chat.IsClosed;

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

        public bool Update(Chat chat)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    UPDATE Chat
                    SET chat_customer_id = @customerId,
                        chat_admin_user_id = @adminUserId,
                        chat_created_at = @createdAt,
                        chat_is_closed = @isClosed
                    WHERE chat_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = chat.Id;
                    command.Parameters.Add("@customerId", SqlDbType.Int).Value = chat.CustomerId;
                    command.Parameters.Add("@adminUserId", SqlDbType.Int).Value = (object?)chat.AdminUserId ?? DBNull.Value;
                    command.Parameters.Add("@createdAt", SqlDbType.DateTime).Value = chat.CreatedAt;
                    command.Parameters.Add("@isClosed", SqlDbType.Bit).Value = chat.IsClosed;

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

                using (SqlCommand command = new SqlCommand("DELETE FROM Chat WHERE chat_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
