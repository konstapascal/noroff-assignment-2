using ChinookReader.DataAccess;
using ChinookReader.DataAccess.Interfaces;
using ChinookReader.Models;
using System;
using System.Collections.Generic;

namespace ChinookReader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ICustomerRepository customerRepo = new CustomerRepository();

            Console.WriteLine("---------- 1 ----------");
            List<Customer> allCustomersList = customerRepo.GetAllCustomers();
            foreach (var cust in allCustomersList) Console.WriteLine(cust.ToString());

            Console.WriteLine("---------- 2 ----------");
            Customer customerById = customerRepo.GetCustomer(10);
            Console.WriteLine(customerById.ToString());

            Console.WriteLine("---------- 3 ----------");
            List<Customer> customerByName = customerRepo.GetCustomer("Daan");
            foreach (var cust in customerByName) Console.WriteLine(cust.ToString());

            Console.WriteLine("---------- 4 ----------");
            List<Customer> allCustomersWithOffset = customerRepo.GetCustomers(10, 5);
            foreach (var cust in allCustomersWithOffset) Console.WriteLine(cust.ToString());

            Console.WriteLine("---------- 5 ----------");
            int addCustomerReturn = customerRepo.AddCustomer(new Customer { FirstName = "Konstantinos",  LastName = "Pascal", Country = "Greece", Email = "konstapascal@gmail.com"});
            Console.WriteLine(addCustomerReturn);

            Console.WriteLine("---------- 6 ----------");
            int updateCustomerReturn = customerRepo.UpdateCustomer(new Customer { CustomerId = 60, FirstName = "Konstantinos", LastName = "Pascal", Country = "Greece", Email = "konstapascal@gmail.com" });
            Console.WriteLine(updateCustomerReturn);

            Console.WriteLine("---------- 7 ----------");
            List<CustomerCountry> countryCustomerCount = customerRepo.CustomerCountByCountry();
            foreach (var entry in countryCustomerCount)
                Console.WriteLine(entry.CountryName + ": " + entry.CustomerCount);

            Console.WriteLine("---------- 8 ----------");
            List<CustomerSpender> customerSpentAmount = customerRepo.TopHighestSpenders();
            foreach (var entry in customerSpentAmount)
                Console.WriteLine(entry.CustomerName + ": " + entry.TotalAmountSpent);

            Console.WriteLine("---------- 9 ----------");
            List<CustomerGenre> mostPopularGenre = customerRepo.GetCustomerMostPopularGenre(14);
            foreach (var entry in mostPopularGenre)
                Console.WriteLine(entry.CustomerName + ", " + entry.GenreName + ": " 
                    + entry.GenreCount + " tracks");
        }
    }
}
