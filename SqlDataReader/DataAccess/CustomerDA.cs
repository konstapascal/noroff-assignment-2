using SqlDataReader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlDataReader.DataAccess
{
    internal class CustomerDA
    {
        internal Customer GetCustomer(int id)
        {
            throw new NotImplementedException();
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
