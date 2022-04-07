namespace RepoLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using CommonLayer.Models;
    using CommonLayer.Token;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using MySql.Data.MySqlClient;
    using RepoLayer.Interface;


    public class UserRL : IUserRL
    {
      
        private MySqlConnection sqlConnection;

        
        public UserRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        private IConfiguration Configuration { get; }

        
        public UserModel Register(UserModel user)
        {
            try
            {
                this.sqlConnection = new MySqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                MySqlCommand com = new MySqlCommand("UserRegister", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("_Fullname", user.Fullname);
                com.Parameters.AddWithValue("_Email", user.Email);
                com.Parameters.AddWithValue("_Password", user.Password);
                com.Parameters.AddWithValue("_MobileNumber", user.MobileNumber);
                this.sqlConnection.Open();
                int i = com.ExecuteNonQuery();
                this.sqlConnection.Close();
                if (i >= 1)
                {
                    return user;
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

       
        public UserAccount UserLogin(string email, string password)
        {
            try
            {
                this.sqlConnection = new MySqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                MySqlCommand com = new MySqlCommand("UserLogin", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("_Email", email);
                com.Parameters.AddWithValue("_Password", password);
                this.sqlConnection.Open();
                MySqlDataReader rd = com.ExecuteReader();
                if (rd.HasRows)
                {
                    UserAccount user = new UserAccount();
                    while (rd.Read())
                    {
                        user.Email = Convert.ToString(rd["Email"] == DBNull.Value ? default : rd["Email"]);
                        user.UserId = Convert.ToInt32(rd["UserId"] == DBNull.Value ? default : rd["UserId"]);
                        user.FullName = Convert.ToString(rd["FullName"] == DBNull.Value ? default : rd["FullName"]);
                    }

                    this.sqlConnection.Close();
                    user.Token = this.GenerateJWTToken(user);
                    return user;
                }
                else
                {
                    this.sqlConnection.Close();
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

        
        public string GenerateJWTToken(UserAccount user)
        {
            // header
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // payload
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, "User"),
                new Claim("Email", user.Email),
                new Claim("Id", user.UserId.ToString()),
            };

            // signature
            var token = new JwtSecurityToken(
                this.Configuration["Jwt:Issuer"],
                this.Configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

     
        public string ForgotPassword(string email)
        {
            try
            {
                this.sqlConnection = new MySqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                MySqlCommand com = new MySqlCommand("UserForgotPassword", this.sqlConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                com.Parameters.AddWithValue("_Email", email);
                this.sqlConnection.Open();
                MySqlDataReader rd = com.ExecuteReader();
                if (rd.HasRows)
                {
                    int userId = 0;
                    while (rd.Read())
                    {
                        email = Convert.ToString(rd["Email"] == DBNull.Value ? default : rd["Email"]);
                        userId = Convert.ToInt32(rd["UserId"] == DBNull.Value ? default : rd["UserId"]);
                    }

                    this.sqlConnection.Close();
                    var token = this.GenerateJWTTokenForPassword(email, userId);
                    new MSMQ().Sender(token);
                    return token;
                }
                else
                {
                    this.sqlConnection.Close();
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

        public string GenerateJWTTokenForPassword(string email, int userId)
        {
            // header
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // payload
            var claims = new[]
            {
                new Claim("Email", email),
                new Claim("Id", userId.ToString()),
            };

            // signature
            var token = new JwtSecurityToken(
                this.Configuration["Jwt:Issuer"],
                this.Configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public bool ResetPassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                if (newPassword == confirmPassword)
                {
                    this.sqlConnection = new MySqlConnection(this.Configuration["ConnectionStrings:BookStore"]);
                    MySqlCommand com = new MySqlCommand("UserResetPassword", this.sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    com.Parameters.AddWithValue("_Email", email);
                    com.Parameters.AddWithValue("_Password", confirmPassword);
                    this.sqlConnection.Open();
                    int i = com.ExecuteNonQuery();
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

    }
}