using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlDataReader.Models
{
    internal class CustomerCountry
    {
        public IEnumerable<Customer> Customers { get; init; }
        public string CountryName { get; set; }
    }
}
