namespace RepoLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    using CommonLayer.Models;
    using Microsoft.Extensions.Configuration;
    using MySql.Data.MySqlClient;
    using RepoLayer.Interface;

    /// <summary>
    ///  Service Class of Repo Layer
    /// </summary>
    /// <seealso cref="RepoLayer.Interface.ICartRL" />
    public class CartRL : ICartRL
    {
      
        private MySqlConnection sqlConnection;

        
        public CartRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

       
        private IConfiguration Configuration { get; }

       
        public Cart AddCart(Cart cart, int userId)
        {
            try
            {
                this.sqlConnection = new MySqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                MySqlCommand cmd = new MySqlCommand("AddCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("_Quantity", cart.Quantity);
                cmd.Parameters.AddWithValue("_BookId", cart.BookId);
                cmd.Parameters.AddWithValue("_UserId", userId);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return cart;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

       
        public bool DeleteCart(int cartId, int userId)
        {
            try
            {
                this.sqlConnection = new MySqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                MySqlCommand cmd = new MySqlCommand("DeleteCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("_CartId", cartId);
                cmd.Parameters.AddWithValue("_UserId", userId);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

       
        public Cart UpdateCart(Cart cart, int userId)
        {
            try
            {
                this.sqlConnection = new MySqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                MySqlCommand cmd = new MySqlCommand("UpdateCart", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("_Quantity", cart.Quantity);
                cmd.Parameters.AddWithValue("_BookId", cart.BookId);
                cmd.Parameters.AddWithValue("_UserId", userId);
                cmd.Parameters.AddWithValue("_CartId", cart.CartId);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return cart;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }

    
        public List<CartModel> GetCartDetails(int userId)
        {
            try
            {
                this.sqlConnection = new MySqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                MySqlCommand cmd = new MySqlCommand("GetCartbyUser", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("_UserId", userId);
                this.sqlConnection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    List<CartModel> cartmodel = new List<CartModel>();
                    while (reader.Read())
                    {
                        BookModel booksModel = new BookModel();
                        CartModel cart = new CartModel();

                        booksModel.BookName = reader["bookName"].ToString();
                        booksModel.AuthorName = reader["authorName"].ToString();
                        booksModel.OriginalPrice = Convert.ToDecimal(reader["originalPrice"]);
                        booksModel.DiscountPrice = Convert.ToDecimal(reader["discountPrice"]);
                        booksModel.BookImage = reader["bookImage"].ToString();
                        cart.UserId = Convert.ToInt32(reader["UserId"]);
                        cart.BookId = Convert.ToInt32(reader["BookId"]);
                        cart.CartId = Convert.ToInt32(reader["CartId"]);
                        cart.Quantity = Convert.ToInt32(reader["Quantity"]);
                        cart.Bookmodel = booksModel;
                        cartmodel.Add(cart);
                    }

                    this.sqlConnection.Close();
                    return cartmodel;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
    }
}