namespace RepoLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    //using CloudinaryDotNet;
    //using CloudinaryDotNet.Actions;
    using CommonLayer.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using MySql.Data.MySqlClient;
    using RepoLayer.Interface;

    /// <summary>
    ///  Service class for Interface 
    /// </summary>
    /// <seealso cref="RepoLayer.Interface.IBookRL" />
    public class BookRL : IBookRL
    {
        
        private MySqlConnection sqlConnection;

       
        public BookRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

      
        private IConfiguration Configuration { get; }

      
        public AddBookModel AddBook(AddBookModel book)
        {
            try
            {
                this.sqlConnection = new MySqlConnection(this.Configuration["ConnectionStrings:Bookstore"]);
                MySqlCommand cmd = new MySqlCommand("AddBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("_bookName", book.BookName);
                cmd.Parameters.AddWithValue("_authorName", book.AuthorName);
                cmd.Parameters.AddWithValue("_rating", book.Rating);
                cmd.Parameters.AddWithValue("_totalRating", book.TotalRating);
                cmd.Parameters.AddWithValue("_discountPrice", book.DiscountPrice);
                cmd.Parameters.AddWithValue("_originalPrice", book.OriginalPrice);
                cmd.Parameters.AddWithValue("_description", book.Description);
                cmd.Parameters.AddWithValue("_bookImage", book.BookImage);
                cmd.Parameters.AddWithValue("_BookCount", book.BookCount);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return book;
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

        
        public BookModel UpdateBook(BookModel book)
        {
            try
            {
                this.sqlConnection = new MySqlConnection(this.Configuration["ConnectionStrings:Bookstore"]);
                MySqlCommand cmd = new MySqlCommand("UpdateBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("_bookId", book.BookId);
                cmd.Parameters.AddWithValue("_bookName", book.BookName);
                cmd.Parameters.AddWithValue("_authorName", book.AuthorName);
                cmd.Parameters.AddWithValue("_rating", book.Rating);
                cmd.Parameters.AddWithValue("_totalRating", book.TotalRating);
                cmd.Parameters.AddWithValue("_discountPrice", book.DiscountPrice);
                cmd.Parameters.AddWithValue("_originalPrice", book.OriginalPrice);
                cmd.Parameters.AddWithValue("_description", book.Description);
                cmd.Parameters.AddWithValue("_bookImage", book.BookImage);
                cmd.Parameters.AddWithValue("_BookCount", book.BookCount);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return book;
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

       
        public bool DeleteBook(int bookId)
        {
            try
            {
                this.sqlConnection = new MySqlConnection(this.Configuration["ConnectionStrings:Bookstore"]);
                MySqlCommand cmd = new MySqlCommand("DeleteBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("_bookId", bookId);
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

        
        public BookModel GetBookByBookId(int bookId)
        {
            try
            {
                this.sqlConnection = new MySqlConnection(this.Configuration["ConnectionStrings:Bookstore"]);
                MySqlCommand cmd = new MySqlCommand("GetBookByBookId", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("_bookId", bookId);
                this.sqlConnection.Open();
                BookModel bookModel = new BookModel();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        bookModel.BookId = Convert.ToInt32(reader["bookId"]);
                        bookModel.BookName = reader["bookName"].ToString();
                        bookModel.AuthorName = reader["authorName"].ToString();
                        bookModel.Rating = Convert.ToInt32(reader["rating"]);
                        bookModel.TotalRating = Convert.ToInt32(reader["totalRating"]);
                        bookModel.DiscountPrice = Convert.ToDecimal(reader["discountPrice"]);
                        bookModel.OriginalPrice = Convert.ToDecimal(reader["originalPrice"]);
                        bookModel.Description = reader["description"].ToString();
                        bookModel.BookImage = reader["bookImage"].ToString();
                        bookModel.BookCount = Convert.ToInt32(reader["BookCount"]);
                    }

                    this.sqlConnection.Close();
                    return bookModel;
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

       
        public List<BookModel> GetAllBooks()
        {
            try
            {
                List<BookModel> book = new List<BookModel>();
                this.sqlConnection = new MySqlConnection(this.Configuration["ConnectionStrings:Bookstore"]);
                MySqlCommand cmd = new MySqlCommand("GetAllBook", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                this.sqlConnection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        book.Add(new BookModel
                        {
                            BookId = Convert.ToInt32(reader["bookId"]),
                            BookName = reader["bookName"].ToString(),
                            AuthorName = reader["authorName"].ToString(),
                            Rating = Convert.ToInt32(reader["rating"]),
                            TotalRating = Convert.ToInt32(reader["totalRating"]),
                            DiscountPrice = Convert.ToDecimal(reader["discountPrice"]),
                            OriginalPrice = Convert.ToDecimal(reader["originalPrice"]),
                            Description = reader["description"].ToString(),
                            BookImage = reader["bookImage"].ToString(),
                            BookCount = Convert.ToInt32(reader["BookCount"])
                        });
                    }

                    this.sqlConnection.Close();
                    return book;
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