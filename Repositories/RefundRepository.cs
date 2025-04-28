using System.Data;
using Microsoft.Data.SqlClient;
using ModefyEcommerce.Data;
using ModefyEcommerce.Interfaces;
using ModefyEcommerce.Models;

namespace ModefyEcommerce.Repositories
{
    public class RefundRepository : IRepository<Refund>
    {
        private readonly SqlConnectionFactory _factory;

        public RefundRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<Refund> GetAll()
        {
            List<Refund> refunds = new List<Refund>();

            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Refund", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Refund refund = new Refund
                            {
                                RefundId = Convert.ToInt32(reader["refund_id"]),
                                PurchaseItemId = Convert.ToInt32(reader["refund_purchase_item_id"]),
                                Quantity = Convert.ToInt32(reader["refund_quantity"]),
                                Amount = Convert.ToDecimal(reader["refund_amount"]),
                                Reason = reader["refund_reason"] != DBNull.Value ? reader["refund_reason"].ToString() : null,
                                Date = Convert.ToDateTime(reader["refund_date"]),
                                AdminUserId = reader["refund_admin_user_id"] != DBNull.Value ? Convert.ToInt32(reader["refund_admin_user_id"]) : (int?)null
                            };

                            refunds.Add(refund);
                        }
                    }
                }
            }

            return refunds;
        }

        public Refund? GetById(int id)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Refund WHERE refund_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Refund
                            {
                                RefundId = Convert.ToInt32(reader["refund_id"]),
                                PurchaseItemId = Convert.ToInt32(reader["refund_purchase_item_id"]),
                                Quantity = Convert.ToInt32(reader["refund_quantity"]),
                                Amount = Convert.ToDecimal(reader["refund_amount"]),
                                Reason = reader["refund_reason"] != DBNull.Value ? reader["refund_reason"].ToString() : null,
                                Date = Convert.ToDateTime(reader["refund_date"]),
                                AdminUserId = reader["refund_admin_user_id"] != DBNull.Value ? Convert.ToInt32(reader["refund_admin_user_id"]) : (int?)null
                            };
                        }
                    }
                }
            }

            return null;
        }

        public Refund? Add(Refund refund)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    INSERT INTO Refund (
                        refund_purchase_item_id, refund_quantity, refund_amount,
                        refund_reason, refund_date, refund_admin_user_id
                    )
                    VALUES (
                        @itemId, @qty, @amount,
                        @reason, @date, @adminId
                    );
                    SELECT CAST(SCOPE_IDENTITY() AS INT);", connection))
                {
                    command.Parameters.Add("@itemId", SqlDbType.Int).Value = refund.PurchaseItemId;
                    command.Parameters.Add("@qty", SqlDbType.Int).Value = refund.Quantity;
                    command.Parameters.Add("@amount", SqlDbType.Decimal).Value = refund.Amount;
                    command.Parameters.Add("@reason", SqlDbType.NVarChar, 255).Value = (object?)refund.Reason ?? DBNull.Value;
                    command.Parameters.Add("@date", SqlDbType.DateTime).Value = refund.Date;
                    command.Parameters.Add("@adminId", SqlDbType.Int).Value = (object?)refund.AdminUserId ?? DBNull.Value;

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

        public bool Update(Refund refund)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    UPDATE Refund
                    SET refund_purchase_item_id = @itemId,
                        refund_quantity = @qty,
                        refund_amount = @amount,
                        refund_reason = @reason,
                        refund_date = @date,
                        refund_admin_user_id = @adminId
                    WHERE refund_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = refund.RefundId;
                    command.Parameters.Add("@itemId", SqlDbType.Int).Value = refund.PurchaseItemId;
                    command.Parameters.Add("@qty", SqlDbType.Int).Value = refund.Quantity;
                    command.Parameters.Add("@amount", SqlDbType.Decimal).Value = refund.Amount;
                    command.Parameters.Add("@reason", SqlDbType.NVarChar, 255).Value = (object?)refund.Reason ?? DBNull.Value;
                    command.Parameters.Add("@date", SqlDbType.DateTime).Value = refund.Date;
                    command.Parameters.Add("@adminId", SqlDbType.Int).Value = (object?)refund.AdminUserId ?? DBNull.Value;

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

                using (SqlCommand command = new SqlCommand("DELETE FROM Refund WHERE refund_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
