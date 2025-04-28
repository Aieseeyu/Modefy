using System.Data;
using Microsoft.Data.SqlClient;
using ModefyEcommerce.Data;
using ModefyEcommerce.Interfaces;
using ModefyEcommerce.Models;

namespace ModefyEcommerce.Repositories
{
    public class PurchaseItemRepository : IRepository<PurchaseItem>
    {
        private readonly SqlConnectionFactory _factory;

        public PurchaseItemRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<PurchaseItem> GetAll()
        {
            List<PurchaseItem> items = new List<PurchaseItem>();

            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM PurchaseItem", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PurchaseItem item = new PurchaseItem
                            {
                                PurchaseItemId = Convert.ToInt32(reader["purchase_item_id"]),
                                PurchaseId = Convert.ToInt32(reader["purchase_item_purchase_id"]),
                                ProductVariantId = Convert.ToInt32(reader["purchase_item_variant_id"]),
                                Quantity = Convert.ToInt32(reader["purchase_item_quantity"]),
                                UnitPrice = Convert.ToDecimal(reader["purchase_item_unit_price"]),
                                ProductName = reader["purchase_item_product_name"].ToString(),
                                Color = reader["purchase_item_color"] != DBNull.Value ? reader["purchase_item_color"].ToString() : null,
                                Size = reader["purchase_item_size"] != DBNull.Value ? reader["purchase_item_size"].ToString() : null,
                                Material = reader["purchase_item_material"] != DBNull.Value ? reader["purchase_item_material"].ToString() : null,
                                ProductReference = reader["purchase_item_product_reference"] != DBNull.Value ? reader["purchase_item_product_reference"].ToString() : null
                            };

                            items.Add(item);
                        }
                    }
                }
            }

            return items;
        }

        public PurchaseItem? GetById(int id)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM PurchaseItem WHERE purchase_item_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PurchaseItem
                            {
                                PurchaseItemId = Convert.ToInt32(reader["purchase_item_id"]),
                                PurchaseId = Convert.ToInt32(reader["purchase_item_purchase_id"]),
                                ProductVariantId = Convert.ToInt32(reader["purchase_item_variant_id"]),
                                Quantity = Convert.ToInt32(reader["purchase_item_quantity"]),
                                UnitPrice = Convert.ToDecimal(reader["purchase_item_unit_price"]),
                                ProductName = reader["purchase_item_product_name"].ToString(),
                                Color = reader["purchase_item_color"] != DBNull.Value ? reader["purchase_item_color"].ToString() : null,
                                Size = reader["purchase_item_size"] != DBNull.Value ? reader["purchase_item_size"].ToString() : null,
                                Material = reader["purchase_item_material"] != DBNull.Value ? reader["purchase_item_material"].ToString() : null,
                                ProductReference = reader["purchase_item_product_reference"] != DBNull.Value ? reader["purchase_item_product_reference"].ToString() : null
                            };
                        }
                    }
                }
            }

            return null;
        }

        public PurchaseItem? Add(PurchaseItem item)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    INSERT INTO PurchaseItem (
                        purchase_item_purchase_id, purchase_item_variant_id,
                        purchase_item_quantity, purchase_item_unit_price,
                        purchase_item_product_name, purchase_item_color,
                        purchase_item_size, purchase_item_material,
                        purchase_item_product_reference
                    )
                    VALUES (
                        @purchaseId, @variantId,
                        @quantity, @unitPrice,
                        @productName, @color,
                        @size, @material,
                        @reference
                    );
                    SELECT CAST(SCOPE_IDENTITY() AS INT);", connection))
                {
                    command.Parameters.Add("@purchaseId", SqlDbType.Int).Value = item.PurchaseId;
                    command.Parameters.Add("@variantId", SqlDbType.Int).Value = item.ProductVariantId;
                    command.Parameters.Add("@quantity", SqlDbType.Int).Value = item.Quantity;
                    command.Parameters.Add("@unitPrice", SqlDbType.Decimal).Value = item.UnitPrice;
                    command.Parameters.Add("@productName", SqlDbType.NVarChar, 100).Value = item.ProductName;
                    command.Parameters.Add("@color", SqlDbType.NVarChar, 30).Value = (object?)item.Color ?? DBNull.Value;
                    command.Parameters.Add("@size", SqlDbType.NVarChar, 10).Value = (object?)item.Size ?? DBNull.Value;
                    command.Parameters.Add("@material", SqlDbType.NVarChar, 30).Value = (object?)item.Material ?? DBNull.Value;
                    command.Parameters.Add("@reference", SqlDbType.NVarChar, 50).Value = (object?)item.ProductReference ?? DBNull.Value;

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

        public bool Update(PurchaseItem item)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    UPDATE PurchaseItem
                    SET purchase_item_purchase_id = @purchaseId,
                        purchase_item_variant_id = @variantId,
                        purchase_item_quantity = @quantity,
                        purchase_item_unit_price = @unitPrice,
                        purchase_item_product_name = @productName,
                        purchase_item_color = @color,
                        purchase_item_size = @size,
                        purchase_item_material = @material,
                        purchase_item_product_reference = @reference
                    WHERE purchase_item_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = item.PurchaseItemId;
                    command.Parameters.Add("@purchaseId", SqlDbType.Int).Value = item.PurchaseId;
                    command.Parameters.Add("@variantId", SqlDbType.Int).Value = item.ProductVariantId;
                    command.Parameters.Add("@quantity", SqlDbType.Int).Value = item.Quantity;
                    command.Parameters.Add("@unitPrice", SqlDbType.Decimal).Value = item.UnitPrice;
                    command.Parameters.Add("@productName", SqlDbType.NVarChar, 100).Value = item.ProductName;
                    command.Parameters.Add("@color", SqlDbType.NVarChar, 30).Value = (object?)item.Color ?? DBNull.Value;
                    command.Parameters.Add("@size", SqlDbType.NVarChar, 10).Value = (object?)item.Size ?? DBNull.Value;
                    command.Parameters.Add("@material", SqlDbType.NVarChar, 30).Value = (object?)item.Material ?? DBNull.Value;
                    command.Parameters.Add("@reference", SqlDbType.NVarChar, 50).Value = (object?)item.ProductReference ?? DBNull.Value;

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

                using (SqlCommand command = new SqlCommand("DELETE FROM PurchaseItem WHERE purchase_item_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
