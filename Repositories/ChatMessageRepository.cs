using System.Data;
using Microsoft.Data.SqlClient;
using ModefyEcommerce.Data;
using ModefyEcommerce.Interfaces;
using ModefyEcommerce.Models;

namespace ModefyEcommerce.Repositories
{
    public class ChatMessageRepository : IRepository<ChatMessage>
    {
        private readonly SqlConnectionFactory _factory;

        public ChatMessageRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<ChatMessage> GetAll()
        {
            List<ChatMessage> messages = new List<ChatMessage>();

            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM ChatMessage", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ChatMessage message = new ChatMessage
                            {
                                Id = Convert.ToInt32(reader["chat_message_id"]),
                                ChatId = Convert.ToInt32(reader["chat_message_chat_id"]),
                                SenderType = reader["chat_message_sender_type"].ToString(),
                                SenderId = Convert.ToInt32(reader["chat_message_sender_id"]),
                                Content = reader["chat_message_content"].ToString(),
                                Read = Convert.ToBoolean(reader["chat_message_read"]),
                                Date = Convert.ToDateTime(reader["chat_message_date"])
                            };

                            messages.Add(message);
                        }
                    }
                }
            }

            return messages;
        }

        public ChatMessage? GetById(int id)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM ChatMessage WHERE chat_message_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ChatMessage
                            {
                                Id = Convert.ToInt32(reader["chat_message_id"]),
                                ChatId = Convert.ToInt32(reader["chat_message_chat_id"]),
                                SenderType = reader["chat_message_sender_type"].ToString(),
                                SenderId = Convert.ToInt32(reader["chat_message_sender_id"]),
                                Content = reader["chat_message_content"].ToString(),
                                Read = Convert.ToBoolean(reader["chat_message_read"]),
                                Date = Convert.ToDateTime(reader["chat_message_date"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        public ChatMessage? Add(ChatMessage message)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    INSERT INTO ChatMessage (
                        chat_message_chat_id, chat_message_sender_type,
                        chat_message_sender_id, chat_message_content,
                        chat_message_read, chat_message_date
                    )
                    VALUES (
                        @chatId, @senderType, @senderId,
                        @content, @read, @date
                    );
                    SELECT CAST(SCOPE_IDENTITY() AS INT);", connection))
                {
                    command.Parameters.Add("@chatId", SqlDbType.Int).Value = message.ChatId;
                    command.Parameters.Add("@senderType", SqlDbType.NVarChar, 20).Value = message.SenderType;
                    command.Parameters.Add("@senderId", SqlDbType.Int).Value = message.SenderId;
                    command.Parameters.Add("@content", SqlDbType.NVarChar).Value = message.Content;
                    command.Parameters.Add("@read", SqlDbType.Bit).Value = message.Read;
                    command.Parameters.Add("@date", SqlDbType.DateTime).Value = message.Date;

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

        public bool Update(ChatMessage message)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    UPDATE ChatMessage
                    SET chat_message_chat_id = @chatId,
                        chat_message_sender_type = @senderType,
                        chat_message_sender_id = @senderId,
                        chat_message_content = @content,
                        chat_message_read = @read,
                        chat_message_date = @date
                    WHERE chat_message_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = message.Id;
                    command.Parameters.Add("@chatId", SqlDbType.Int).Value = message.ChatId;
                    command.Parameters.Add("@senderType", SqlDbType.NVarChar, 20).Value = message.SenderType;
                    command.Parameters.Add("@senderId", SqlDbType.Int).Value = message.SenderId;
                    command.Parameters.Add("@content", SqlDbType.NVarChar).Value = message.Content;
                    command.Parameters.Add("@read", SqlDbType.Bit).Value = message.Read;
                    command.Parameters.Add("@date", SqlDbType.DateTime).Value = message.Date;

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

                using (SqlCommand command = new SqlCommand("DELETE FROM ChatMessage WHERE chat_message_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
