using ChinookReader.DataAccess;
using ChinookReader.Models;
using System;
using System.Collections.Generic;

namespace ChinookReader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CustomerDA da = new();

            List<Customer> allCustomersList = da.GetAllCustomers();
            foreach (var cust in allCustomersList) Console.WriteLine(cust.ToString());

            Console.WriteLine("---------------------");

            Customer customerById = da.GetCustomer(10);
            Console.WriteLine(customerById.ToString());
            
            Console.WriteLine("---------------------");

            List<Customer> customerByName = da.GetCustomer("Daan");
            foreach (var cust in customerByName) Console.WriteLine(cust.ToString());

            Console.WriteLine("---------------------");

            List<Customer> allCustomersWithOffset = da.Get(20, 10);
            foreach (var cust in allCustomersWithOffset) Console.WriteLine(cust.ToString());

            Console.WriteLine("---------------------");

            int addCustomerReturn = da.AddCustomer(new Customer { FirstName = "Konstantinos",  LastName = "Pascal", Country = "Greece", Email = "konstapascal@gmail.com"});
            Console.WriteLine(addCustomerReturn);

            Console.WriteLine("---------------------");

            int updateCustomerReturn = da.UpdateCustomer(new Customer { CustomerId = 60, FirstName = "Konstantinos", LastName = "Pascal", Country = "Norway", Email = "konstapascal@gmail.com" });
            Console.WriteLine(addCustomerReturn);

            Console.WriteLine("---------------------");
        }
    }
}
