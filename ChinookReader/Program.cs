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

            Console.WriteLine("---------- 1 ----------");
            List<Customer> allCustomersList = da.GetAllCustomers();
            foreach (var cust in allCustomersList) Console.WriteLine(cust.ToString());

            Console.WriteLine("---------- 2 ----------");
            Customer customerById = da.GetCustomer(10);
            Console.WriteLine(customerById.ToString());

            Console.WriteLine("---------- 3 ----------");
            List<Customer> customerByName = da.GetCustomer("Daan");
            foreach (var cust in customerByName) Console.WriteLine(cust.ToString());

            Console.WriteLine("---------- 4 ----------");
            List<Customer> allCustomersWithOffset = da.Get(10, 5);
            foreach (var cust in allCustomersWithOffset) Console.WriteLine(cust.ToString());

            Console.WriteLine("---------- 5 ----------");
            int addCustomerReturn = da.AddCustomer(new Customer { FirstName = "Konstantinos",  LastName = "Pascal", Country = "Greece", Email = "konstapascal@gmail.com"});
            Console.WriteLine(addCustomerReturn);

            Console.WriteLine("---------- 6 ----------");
            int updateCustomerReturn = da.UpdateCustomer(new Customer { CustomerId = 60, FirstName = "Konstantinos", LastName = "Pascal", Country = "Norway", Email = "konstapascal@gmail.com" });
            Console.WriteLine(updateCustomerReturn);

            Console.WriteLine("---------- 7 ----------");
            List<CustomerCountry> countryCustomerCount = da.CustomerCountByCountry();
            foreach (var entry in countryCustomerCount)
                Console.WriteLine(entry.CountryName + ": " + entry.CustomerCount);

            Console.WriteLine("---------- 8 ----------");
            List<CustomerSpender> customerSpentAmount = da.TopHighestSpenders();
            foreach (var entry in customerSpentAmount)
                Console.WriteLine(entry.CustomerName + ": " + entry.TotalAmountSpent);

            Console.WriteLine("---------- 9 ----------");
            List<CustomerGenre> mostPopularGenre = da.GetCustomerMostPopularGenre(14);
            foreach (var entry in mostPopularGenre)
                Console.WriteLine(entry.CustomerName + ", " + entry.GenreName + ": " 
                    + entry.GenreCount + " tracks");
        }
    }
}
