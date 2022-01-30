using ChinookReader.Models;
using System.Collections.Generic;

namespace ChinookReader.DataAccess.Interfaces
{
	internal interface ICustomerRepository
	{
		Customer GetCustomer(int customerId);
		List<Customer> GetCustomer(string name);
		List<Customer> GetAllCustomers();
		List<Customer> GetCustomers(int amount, int offset);
		int AddCustomer(Customer newCustomer);
		int UpdateCustomer(Customer newCustomer);
		List<CustomerCountry> CustomerCountByCountry();
		List<CustomerSpender> TopHighestSpenders();
		List<CustomerGenre> GetCustomerMostPopularGenre(int customerId);
	}
}
