using System;
using System.Collections.Generic;
using GolddiggerGmbh.Model;
using MySql.Data.MySqlClient;

namespace GolddiggerGmbh.DAL
{
    public class UserRepository : IUserRepository
    {

        public User GetByUserAndPassword(string userName, string password)
        {
            try
            {
                using (var conn = SqlConnectionFactory.GetOpenConnection())
                {
                    var cmd = new MySqlCommand(@"SELECT * FROM Users 
                        WHERE UserName=@UserName 
                        and 
                        Password = @Password", conn);

                    cmd.Parameters.AddWithValue("@UserName", userName);
                    cmd.Parameters.AddWithValue("@Password", password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                
                                Id = Convert.ToInt32(reader["Id"]),
                                UserName = reader["UserName"].ToString(),

                                Password = reader["Password"].ToString(),

                            };
                            
                        }
                    }
                        return null;
                    
                }
            }
            catch (Exception ex)
            {
                
                Logger.LogException(ex);
                throw;
            }
        }
    }
}
