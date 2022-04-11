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

    
    public class WishListRL : IWishListRL
    {
       
        private MySqlConnection sqlConnection;

       
        public WishListRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

       
        private IConfiguration Configuration { get; }

      
        public string AddInWishlist(int bookId, int userId)
        {
            try
            {
                this.sqlConnection = new MySqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                MySqlCommand cmd = new MySqlCommand("AddInWishlist", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("_UserId", userId);
                cmd.Parameters.AddWithValue("_BookId", bookId);
                this.sqlConnection.Open();
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                this.sqlConnection.Close();
                if (i == 2)
                {
                    return "Book is Already in Wishlist";
                }

                if (i == 1)
                {
                    return "Choose Correct BookID";
                }
                else
                {
                    return "Book added in Wishlist";
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

    
        public bool DeleteFromWishlist(int userId, int wishlistId)
        {
            try
            {
                this.sqlConnection = new MySqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                MySqlCommand cmd = new MySqlCommand("DeleteFromWishlist", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("_UserId", userId);
                cmd.Parameters.AddWithValue("_WishlistId", wishlistId);
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

     
        public List<WishModel> GetAllFromWishlist(int userId)
        {
            try
            {
                this.sqlConnection = new MySqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                MySqlCommand cmd = new MySqlCommand("GetAllRecordsFromWishlist", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("_UserId", userId);
                this.sqlConnection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    List<WishModel> wishModel = new List<WishModel>();
                    while (reader.Read())
                    {
                        BookModel bookModel = new BookModel();
                        WishModel wish = new WishModel();
                        bookModel.BookName = reader["bookName"].ToString();
                        bookModel.AuthorName = reader["authorName"].ToString();
                        bookModel.OriginalPrice = Convert.ToDecimal(reader["originalPrice"]);
                        bookModel.DiscountPrice = Convert.ToDecimal(reader["discountPrice"]);
                        bookModel.BookImage = reader["bookImage"].ToString();
                        wish.WishlistId = Convert.ToInt32(reader["WishlistId"]);
                        wish.UserId = Convert.ToInt32(reader["UserId"]);
                        wish.BookId = Convert.ToInt32(reader["BookId"]);
                        wish.Bookmodel = bookModel;
                        wishModel.Add(wish);
                    }

                    this.sqlConnection.Close();
                    return wishModel;
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