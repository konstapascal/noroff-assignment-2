using ChinookReader.DataAccess;
using ChinookReader.Models;
using System;

namespace ChinookReader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            CustomerDA Da = new();
            Customer customer = Da.GetCustomer(1);
            Console.WriteLine($"{customer.CustomerId},{customer.FirstName},{customer.LastName},{customer.Country}");
        }
    }
}
