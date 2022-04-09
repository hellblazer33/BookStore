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

    
    public class AddressRL : IAddressRL
    {
       
        private MySqlConnection sqlConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddressRL"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public AddressRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

     
        private IConfiguration Configuration { get; }

      
        public string AddAddress(AddressModel add, int userId)
        {
            try
            {
                this.sqlConnection = new MySqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                MySqlCommand cmd = new MySqlCommand("AddAddress", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("_Address", add.Address);
                cmd.Parameters.AddWithValue("_City", add.City);
                cmd.Parameters.AddWithValue("_State", add.State);
                cmd.Parameters.AddWithValue("_TypeId", add.TypeId);
                cmd.Parameters.AddWithValue("_UserId", userId);
                this.sqlConnection.Open();
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                this.sqlConnection.Close();
                if (i == 2)
                {
                    return "Enter Correct TypeId For Adding Address";
                }
                else
                {
                    return " Address Added Successfully";
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

    
        public AddressModel UpdateAddress(AddressModel add, int addressId, int userId)
        {
            try
            {
                this.sqlConnection = new MySqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                MySqlCommand cmd = new MySqlCommand("UpdateAddress", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("_Address", add.Address);
                cmd.Parameters.AddWithValue("_City", add.City);
                cmd.Parameters.AddWithValue("_State", add.State);
                cmd.Parameters.AddWithValue("_TypeId", add.TypeId);
                cmd.Parameters.AddWithValue("_UserId", userId);
                cmd.Parameters.AddWithValue("_AddressId", addressId);
                this.sqlConnection.Open();
                int i = cmd.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return add;
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

      
        public bool DeleteAddress(int addressId, int userId)
        {
            try
            {
                this.sqlConnection = new MySqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                MySqlCommand cmd = new MySqlCommand("DeleteAddress", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("_AddressId", addressId);
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

        
        public List<AddressModel> GetAllAddress(int userId)
        {
            try
            {
                this.sqlConnection = new MySqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                MySqlCommand cmd = new MySqlCommand("GetAllAddress", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("_UserId", userId);
                this.sqlConnection.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    List<AddressModel> addressModel = new List<AddressModel>();
                    while (reader.Read())
                    {
                        addressModel.Add(new AddressModel
                        {
                            Address = reader["Address"].ToString(),
                            City = reader["City"].ToString(),
                            State = reader["State"].ToString(),
                            TypeId = Convert.ToInt32(reader["TypeId"]),
                            AddressId = Convert.ToInt32(reader["AddressId"]),
                            UserId = Convert.ToInt32(reader["UserId"])
                        });
                    }

                    this.sqlConnection.Close();
                    return addressModel;
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