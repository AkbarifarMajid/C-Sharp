using System;
using System.Collections.Generic;
using System.Windows.Controls;
using GolddiggerGmbh.Model;
using MySql.Data.MySqlClient;



namespace GolddiggerGmbh.DAL
{
    public class CustomerRepository : ICustomerRepository
    {

        public void Add(Customer cus)
        {
            try 
            { 
            using (var conn = SqlConnectionFactory.GetOpenConnection())
            {

                var cmd = new MySqlCommand(@"INSERT INTO customers
                                          (FirstName, LastName, Street, No, PostalCode, City, Email, PhoneNumber, BirthDate)
                                        VALUES
                                          (@FirstName, @LastName, @Street, @No, @PostalCode, @City, @Email, @PhoneNumber, @BirthDate)", conn);

                cmd.Parameters.AddWithValue("@FirstName", cus.FirstName);
                cmd.Parameters.AddWithValue("@LastName", cus.LastName);
                cmd.Parameters.AddWithValue("@Street", cus.Street);
                cmd.Parameters.AddWithValue("@No", cus.No);
                cmd.Parameters.AddWithValue("@PostalCode", cus.PostalCode);
                cmd.Parameters.AddWithValue("@City", cus.City);
                cmd.Parameters.AddWithValue("@Email", cus.Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", cus.PhoneNumber);
                cmd.Parameters.AddWithValue("@BirthDate", cus.BirthDate);
                cmd.ExecuteNonQuery();

            }
        }
            catch (Exception ex)
            {
                Logger.LogException(ex);

            }
}


        public void Delete(int id)
        {
            try { 
            using (var conn = SqlConnectionFactory.GetOpenConnection())

            {
                var cmd = new MySqlCommand(@"DELETE FROM customers WHERE Id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
 
            }
        }





        public List<Customer> GetAll()
        {
            try
            {

                var list = new List<Customer>();
                using (var conn = SqlConnectionFactory.GetOpenConnection())
                {
                    var cmd = new MySqlCommand(@"
                                SELECT Id, FirstName, LastName, Street, No, PostalCode, City, Email, PhoneNumber, BirthDate
                                FROM customers ", conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(MapReaderToCustomer(reader));
                        }
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }
        




        public Customer GetById(int id)
        {
            try
            {
                using (var conn = SqlConnectionFactory.GetOpenConnection())
            {
                
                var cmd = new MySqlCommand(@"
                            SELECT Id, FirstName, LastName, Street, No, PostalCode, City, Email, PhoneNumber, BirthDate
                            FROM customers
                            WHERE Id = @Id", conn);

                cmd.Parameters.AddWithValue("@Id", id);
                using (var reader = cmd.ExecuteReader())
                    { 
                if (reader.Read())
                    return MapReaderToCustomer(reader);
            }
                return null;
            }
        }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
}




        public void Update(Customer cus)
        {
            try
            {



                using (var conn = SqlConnectionFactory.GetOpenConnection())
                {
                    var cmd = new MySqlCommand(@"
                UPDATE customers SET
                    FirstName   = @FirstName,
                    LastName    = @LastName,
                    Street      = @Street,
                    No          = @No,
                    PostalCode  = @PostalCode,
                    City        = @City,
                    Email       = @Email,
                    PhoneNumber = @PhoneNumber,
                    BirthDate   = @BirthDate
                WHERE Id = @Id", conn);


                    cmd.Parameters.AddWithValue("@Id", cus.Id);
                    cmd.Parameters.AddWithValue("@FirstName", cus.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", cus.LastName);
                    cmd.Parameters.AddWithValue("@Street", cus.Street);
                    cmd.Parameters.AddWithValue("@No", cus.No);
                    cmd.Parameters.AddWithValue("@PostalCode", cus.PostalCode);
                    cmd.Parameters.AddWithValue("@City", cus.City);
                    cmd.Parameters.AddWithValue("@Email", cus.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", cus.PhoneNumber);
                    cmd.Parameters.AddWithValue("@BirthDate", cus.BirthDate);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                
            }

        }


  


        public Customer GetByEmail(string email)
        {
            try
            {
                using (var conn = SqlConnectionFactory.GetOpenConnection())
                {
                    var cmd = new MySqlCommand(@"
                SELECT Id, FirstName, LastName, Street, No, PostalCode, City, Email, PhoneNumber, BirthDate
                FROM customers
                WHERE Email = @Email
                LIMIT 1", conn);

                    cmd.Parameters.AddWithValue("@Email", email);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapReaderToCustomer(reader);
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }



        private Customer MapReaderToCustomer(MySqlDataReader reader)
        {
            return new Customer
            {
                Id = Convert.ToInt32(reader["Id"]),
                FirstName = reader["FirstName"].ToString(),
                LastName = reader["LastName"].ToString(),
                Street = reader["Street"].ToString(),
                No = reader["No"].ToString(),
                PostalCode = reader["PostalCode"].ToString(),
                City = reader["City"].ToString(),
                Email = reader["Email"].ToString(),
                PhoneNumber = reader["PhoneNumber"].ToString(),
                BirthDate = Convert.ToDateTime(reader["BirthDate"])

            };
            
        }
    }
}

