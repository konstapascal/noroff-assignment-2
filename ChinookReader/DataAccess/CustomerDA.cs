using Microsoft.Data.SqlClient;
using ChinookReader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookReader.DataAccess
{
    internal class CustomerDA
    {
        internal Customer GetCustomer(int id)
        {
            Customer customer = new();
            string sqlQuery = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email" +
                $" FROM Customer WHERE CustomerId = @id";

            using (SqlConnection conn = new SqlConnection(ConnectionHelper.GetConnectionString()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customer.CustomerId = reader.GetInt32(0);
                            customer.FirstName = reader.GetString(1);
                            customer.LastName = reader.GetString(2);
                            customer.Country = reader.GetString(3);
                            customer.PostalCode = reader.GetString(4);
                            customer.PhoneNumber = reader.GetString(5);
                            customer.Email = reader.GetString(6);
                        }

                    }
                }
            }
            return customer;
        }

        internal Customer GetCustomer(string name)
        {
            Customer customer = new();
            string sqlQuery = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email" +
                $" FROM Customer WHERE FirstName = @name";

            using (SqlConnection conn = new SqlConnection(ConnectionHelper.GetConnectionString()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@name", name);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customer.CustomerId = int.Parse(reader.GetString(0));
                            customer.FirstName = reader.GetString(1);
                            customer.LastName = reader.GetString(2);
                            customer.Country = reader.GetString(3);
                            customer.PostalCode = reader.GetString(4);
                            customer.PhoneNumber = reader.GetString(5);
                            customer.Email = reader.GetString(6);
                        }

                    }
                }
            }
            return customer;
        }
    

        internal List<Customer> GetAllCustomers()
        {
            throw new NotImplementedException();
        }

        internal List<Customer> Get(int amount, int offset)
        {
            throw new NotImplementedException();
        }

        internal void UpdateCustomer(Customer newCustomer)
        {
            throw new NotImplementedException();
        }
        internal string CustomerCountByCountry(int id)
        {
            throw new NotImplementedException();
        }

        internal List<CustomerSpender> TopHighestSpenders(int amount)
        {
            throw new NotImplementedException();
        }

    }
}
