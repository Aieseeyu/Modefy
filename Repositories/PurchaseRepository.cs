using System.Data;
using Microsoft.Data.SqlClient;
using ModefyEcommerce.Data;
using ModefyEcommerce.Interfaces;
using ModefyEcommerce.Models;

namespace ModefyEcommerce.Repositories
{
    public class PurchaseRepository : IRepository<Purchase>
    {
        private readonly SqlConnectionFactory _factory;

        public PurchaseRepository(SqlConnectionFactory factory)
        {
            _factory = factory;
        }

        public List<Purchase> GetAll()
        {
            List<Purchase> purchases = new List<Purchase>();

            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Purchase", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Purchase purchase = new Purchase
                            {
                                PurchaseId = Convert.ToInt32(reader["purchase_id"]),
                                CustomerId = Convert.ToInt32(reader["purchase_customer_id"]),
                                Date = Convert.ToDateTime(reader["purchase_date"]),
                                CustomerFullName = reader["purchase_customer_full_name"].ToString(),
                                BillingAddress = reader["purchase_billing_address"].ToString(),
                                Status = reader["purchase_status"].ToString(),
                                TotalAmount = Convert.ToDecimal(reader["purchase_total_amount"]),
                                ShippingAddress = reader["purchase_shipping_address"].ToString(),
                                PaymentMethod = reader["purchase_payment_method"].ToString(),
                                StripeId = reader["purchase_stripe_id"] != DBNull.Value ? reader["purchase_stripe_id"].ToString() : null,
                                PromotionId = reader["purchase_promotion_id"] != DBNull.Value ? Convert.ToInt32(reader["purchase_promotion_id"]) : (int?)null,
                                PromotionCode = reader["purchase_promotion_code"] != DBNull.Value ? reader["purchase_promotion_code"].ToString() : null,
                                DiscountAmount = Convert.ToDecimal(reader["purchase_discount_amount"])
                            };

                            purchases.Add(purchase);
                        }
                    }
                }
            }

            return purchases;
        }

        public Purchase? GetById(int id)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Purchase WHERE purchase_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Purchase
                            {
                                PurchaseId = Convert.ToInt32(reader["purchase_id"]),
                                CustomerId = Convert.ToInt32(reader["purchase_customer_id"]),
                                Date = Convert.ToDateTime(reader["purchase_date"]),
                                CustomerFullName = reader["purchase_customer_full_name"].ToString(),
                                BillingAddress = reader["purchase_billing_address"].ToString(),
                                Status = reader["purchase_status"].ToString(),
                                TotalAmount = Convert.ToDecimal(reader["purchase_total_amount"]),
                                ShippingAddress = reader["purchase_shipping_address"].ToString(),
                                PaymentMethod = reader["purchase_payment_method"].ToString(),
                                StripeId = reader["purchase_stripe_id"] != DBNull.Value ? reader["purchase_stripe_id"].ToString() : null,
                                PromotionId = reader["purchase_promotion_id"] != DBNull.Value ? Convert.ToInt32(reader["purchase_promotion_id"]) : (int?)null,
                                PromotionCode = reader["purchase_promotion_code"] != DBNull.Value ? reader["purchase_promotion_code"].ToString() : null,
                                DiscountAmount = Convert.ToDecimal(reader["purchase_discount_amount"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        public Purchase? Add(Purchase purchase)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    INSERT INTO Purchase (
                        purchase_customer_id, purchase_date, purchase_customer_full_name,
                        purchase_billing_address, purchase_status, purchase_total_amount,
                        purchase_shipping_address, purchase_payment_method,
                        purchase_stripe_id, purchase_promotion_id, purchase_promotion_code,
                        purchase_discount_amount
                    )
                    VALUES (
                        @customerId, @date, @fullName,
                        @billing, @status, @total,
                        @shipping, @payment,
                        @stripe, @promoId, @promoCode,
                        @discount
                    );
                    SELECT CAST(SCOPE_IDENTITY() AS INT);", connection))
                {
                    command.Parameters.Add("@customerId", SqlDbType.Int).Value = purchase.CustomerId;
                    command.Parameters.Add("@date", SqlDbType.DateTime).Value = purchase.Date;
                    command.Parameters.Add("@fullName", SqlDbType.NVarChar, 110).Value = purchase.CustomerFullName;
                    command.Parameters.Add("@billing", SqlDbType.NVarChar, 255).Value = purchase.BillingAddress;
                    command.Parameters.Add("@status", SqlDbType.NVarChar, 30).Value = purchase.Status;
                    command.Parameters.Add("@total", SqlDbType.Decimal).Value = purchase.TotalAmount;
                    command.Parameters.Add("@shipping", SqlDbType.NVarChar, 255).Value = purchase.ShippingAddress;
                    command.Parameters.Add("@payment", SqlDbType.NVarChar, 50).Value = purchase.PaymentMethod;
                    command.Parameters.Add("@stripe", SqlDbType.NVarChar, 100).Value = (object?)purchase.StripeId ?? DBNull.Value;
                    command.Parameters.Add("@promoId", SqlDbType.Int).Value = (object?)purchase.PromotionId ?? DBNull.Value;
                    command.Parameters.Add("@promoCode", SqlDbType.NVarChar, 30).Value = (object?)purchase.PromotionCode ?? DBNull.Value;
                    command.Parameters.Add("@discount", SqlDbType.Decimal).Value = purchase.DiscountAmount;

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

        public bool Update(Purchase purchase)
        {
            using (SqlConnection connection = _factory.CreateConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(@"
                    UPDATE Purchase
                    SET purchase_customer_id = @customerId,
                        purchase_date = @date,
                        purchase_customer_full_name = @fullName,
                        purchase_billing_address = @billing,
                        purchase_status = @status,
                        purchase_total_amount = @total,
                        purchase_shipping_address = @shipping,
                        purchase_payment_method = @payment,
                        purchase_stripe_id = @stripe,
                        purchase_promotion_id = @promoId,
                        purchase_promotion_code = @promoCode,
                        purchase_discount_amount = @discount
                    WHERE purchase_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = purchase.PurchaseId;
                    command.Parameters.Add("@customerId", SqlDbType.Int).Value = purchase.CustomerId;
                    command.Parameters.Add("@date", SqlDbType.DateTime).Value = purchase.Date;
                    command.Parameters.Add("@fullName", SqlDbType.NVarChar, 110).Value = purchase.CustomerFullName;
                    command.Parameters.Add("@billing", SqlDbType.NVarChar, 255).Value = purchase.BillingAddress;
                    command.Parameters.Add("@status", SqlDbType.NVarChar, 30).Value = purchase.Status;
                    command.Parameters.Add("@total", SqlDbType.Decimal).Value = purchase.TotalAmount;
                    command.Parameters.Add("@shipping", SqlDbType.NVarChar, 255).Value = purchase.ShippingAddress;
                    command.Parameters.Add("@payment", SqlDbType.NVarChar, 50).Value = purchase.PaymentMethod;
                    command.Parameters.Add("@stripe", SqlDbType.NVarChar, 100).Value = (object?)purchase.StripeId ?? DBNull.Value;
                    command.Parameters.Add("@promoId", SqlDbType.Int).Value = (object?)purchase.PromotionId ?? DBNull.Value;
                    command.Parameters.Add("@promoCode", SqlDbType.NVarChar, 30).Value = (object?)purchase.PromotionCode ?? DBNull.Value;
                    command.Parameters.Add("@discount", SqlDbType.Decimal).Value = purchase.DiscountAmount;

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

                using (SqlCommand command = new SqlCommand("DELETE FROM Purchase WHERE purchase_id = @id", connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
