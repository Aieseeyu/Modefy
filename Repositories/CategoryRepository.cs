using System.Data;
using Microsoft.Data.SqlClient;
using ModefyEcommerce.Data;
using ModefyEcommerce.Models;

namespace ModefyEcommerce.Repositories
{
    public class CategoryRepository
    {
        private readonly SqlConnectionFactory _factory;

        public CategoryRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<Category> GetAll()
        {
            List<Category> categories = new List<Category>();

            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Category", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Category category = new Category
                        {
                            CategoryId = Convert.ToInt32(reader["category_id"]),
                            CategoryName = reader["category_name"].ToString(),
                            CategoryDescription = reader["category_description"] != DBNull.Value ? reader["category_description"].ToString() : null,
                            ParentCategoryId = reader["category_parent_id"] != DBNull.Value ? Convert.ToInt32(reader["category_parent_id"]) : (int?)null
                        };

                        categories.Add(category);
                    }
                }
            }

            return categories;
        }

        public Category? GetById(int id)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Category WHERE category_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Category
                            {
                                CategoryId = Convert.ToInt32(reader["category_id"]),
                                CategoryName = reader["category_name"].ToString(),
                                CategoryDescription = reader["category_description"] != DBNull.Value ? reader["category_description"].ToString() : null,
                                ParentCategoryId = reader["category_parent_id"] != DBNull.Value ? Convert.ToInt32(reader["category_parent_id"]) : (int?)null
                            };
                        }
                    }
                }
            }

            return null;
        }

        public Category? Add(Category category)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    INSERT INTO Category (category_name, category_description, category_parent_id)
                    VALUES (@name, @description, @parentId);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);", connection))
                {
                    command.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = category.CategoryName;
                    command.Parameters.Add("@description", SqlDbType.NVarChar, 255).Value = (object?)category.CategoryDescription ?? DBNull.Value;
                    command.Parameters.Add("@parentId", SqlDbType.Int).Value = (object?)category.ParentCategoryId ?? DBNull.Value;

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

        public bool Update(Category category)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    UPDATE Category
                    SET category_name = @name,
                        category_description = @description,
                        category_parent_id = @parentId
                    WHERE category_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = category.CategoryId;
                    command.Parameters.Add("@name", SqlDbType.NVarChar, 50).Value = category.CategoryName;
                    command.Parameters.Add("@description", SqlDbType.NVarChar, 255).Value = (object?)category.CategoryDescription ?? DBNull.Value;
                    command.Parameters.Add("@parentId", SqlDbType.Int).Value = (object?)category.ParentCategoryId ?? DBNull.Value;

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

                using (SqlCommand command = new SqlCommand("DELETE FROM Category WHERE category_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
