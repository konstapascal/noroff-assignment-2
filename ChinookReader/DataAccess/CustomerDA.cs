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
            string sqlQuery = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email" +
                $" FROM Customer WHERE CustomerId = @id";

            using (SqlConnection conn = new SqlConnection(ConnectionHelper.GetConnectionString()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    // using (SqlDataReader reader = cmd.ExecuteReader()) { }
                }
            }
        }

        internal Customer GetCustomer(string name)
        {
            throw new NotImplementedException();
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
