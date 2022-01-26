﻿using Microsoft.Data.SqlClient;
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
                $" FROM Customer WHERE CustomerId = @id;";

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
                            customer.Country = SafeGetString(reader, 3);
                            customer.PostalCode = SafeGetString(reader, 4);
                            customer.PhoneNumber = SafeGetString(reader, 5);
                            customer.Email = reader.GetString(6);
                        }

                    }
                }
            }

            return customer;
        }

        internal List<Customer> GetCustomer(string name)
        {
            List<Customer> allCustomersList = new();
            string sqlQuery = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email" +
                $" FROM Customer WHERE FirstName LIKE @name;";

            using (SqlConnection conn = new SqlConnection(ConnectionHelper.GetConnectionString()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@name", $"%{name}%");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new ();

                            customer.CustomerId = reader.GetInt32(0);
                            customer.FirstName = reader.GetString(1);
                            customer.LastName = reader.GetString(2);
                            customer.Country = SafeGetString(reader, 3);
                            customer.PostalCode = SafeGetString(reader, 4);
                            customer.PhoneNumber = SafeGetString(reader, 5);
                            customer.Email = reader.GetString(6);

                            allCustomersList.Add(customer);
                        }

                    }
                }
            }

            return allCustomersList;
        }
    

        internal List<Customer> GetAllCustomers()
        {
            List<Customer> allCustomersList = new ();

            string sqlQuery = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email" +
                $" FROM Customer;";

            using (SqlConnection conn = new SqlConnection(ConnectionHelper.GetConnectionString()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new ();

                            customer.CustomerId = reader.GetInt32(0);
                            customer.FirstName = reader.GetString(1);
                            customer.LastName = reader.GetString(2);
                            customer.Country = SafeGetString(reader, 3);
                            customer.PostalCode = SafeGetString(reader, 4);
                            customer.PhoneNumber = SafeGetString(reader, 5);
                            customer.Email = reader.GetString(6);

                            allCustomersList.Add(customer);
                        }

                    }
                }
            }

            return allCustomersList;
        }

        internal List<Customer> Get(int amount, int offset)
        {
            List<Customer> allCustomersList = new ();

            string sqlQuery = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email" +
                $" FROM Customer ORDER BY CustomerId OFFSET @offset ROWS FETCH NEXT @amount ROWS ONLY;";

            using (SqlConnection conn = new SqlConnection(ConnectionHelper.GetConnectionString()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.Parameters.AddWithValue("@offset", offset);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new();

                            customer.CustomerId = reader.GetInt32(0);
                            customer.FirstName = reader.GetString(1);
                            customer.LastName = reader.GetString(2);
                            customer.Country = reader.GetString(3);
                            customer.PostalCode = reader.GetString(4);
                            customer.PhoneNumber = reader.GetString(5);
                            customer.Email = reader.GetString(6);

                            allCustomersList.Add(customer);
                        }

                    }
                }
            }

            return allCustomersList;
        }
        internal int AddCustomer(Customer newCustomer)
        {
            string sqlQuery = "INSERT INTO Customer (FirstName, LastName, Country, PostalCode, Phone, Email)" +
                $" VALUES (@name, @lastName, @country, @postalCode, @phone, @email);";

            using (SqlConnection conn = new SqlConnection(ConnectionHelper.GetConnectionString()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@name", newCustomer.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", newCustomer.LastName);
                    cmd.Parameters.AddWithValue("@country", (object) newCustomer.Country ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@postalCode", (object) newCustomer.PostalCode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@phone", (object) newCustomer.PhoneNumber ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@email", newCustomer.Email);

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        internal int UpdateCustomer(Customer newCustomer)
        {
            string sqlQuery = "UPDATE Customer SET FirstName = @name, LastName = @lastName, " +
                "Country = @country, PostalCode = @postalCode, " +
                "Phone = @phone, Email = @email " +
               "WHERE CustomerId = @id;";

            Customer oldCustomer = GetCustomer(newCustomer.CustomerId);

            if (!(newCustomer.PhoneNumber is null)) oldCustomer.PhoneNumber = newCustomer.PhoneNumber;
            if (!(newCustomer.Country is null)) oldCustomer.Country = newCustomer.Country;
            if (!(newCustomer.PostalCode is null)) oldCustomer.PostalCode = newCustomer.PostalCode;
            if (!(newCustomer.FirstName is null)) oldCustomer.FirstName = newCustomer.FirstName;
            if (!(newCustomer.LastName is null)) oldCustomer.LastName = newCustomer.LastName;
            if (!(newCustomer.Email is null)) oldCustomer.Email = newCustomer.Email;

            using (SqlConnection conn = new SqlConnection(ConnectionHelper.GetConnectionString()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@id", oldCustomer.CustomerId);
                    cmd.Parameters.AddWithValue("@name", oldCustomer.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", oldCustomer.LastName);
                    cmd.Parameters.AddWithValue("@country", (object)oldCustomer.Country ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@postalCode", (object)oldCustomer.PostalCode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@phone", (object)oldCustomer.PhoneNumber ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@email", oldCustomer.Email);

                    return cmd.ExecuteNonQuery();
                }
            }
        }
        internal List<CustomerCountry> CustomerCountByCountry()
        {
            List<CustomerCountry> customerCountCountryList = new();

            string sqlQuery = "SELECT TOP 5 Country, Count(*) as CustomerCount FROM Customer " +
                "GROUP BY Country ORDER BY CustomerCount DESC;";

            using (SqlConnection conn = new SqlConnection(ConnectionHelper.GetConnectionString()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CustomerCountry countryCustomerCount = new();

                            countryCustomerCount.CountryName = reader.GetString(0);
                            countryCustomerCount.CustomerCount = reader.GetInt32(1);

                            customerCountCountryList.Add(countryCustomerCount);
                        }

                    }
                }

                return customerCountCountryList;
            }
        }

        internal List<CustomerSpender> TopHighestSpenders()
        {
            List<CustomerSpender> highestSpendersList = new();

            string sqlQuery = "SELECT TOP 5 C.FirstName, I.Total as Total FROM Customer as C " +
                "INNER JOIN Invoice as I ON C.CustomerId = I.CustomerId " +
                "GROUP BY C.FirstName, I.Total ORDER BY I.Total DESC;";

            using (SqlConnection conn = new SqlConnection(ConnectionHelper.GetConnectionString()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CustomerSpender custSpender = new();

                            custSpender.CustomerName = reader.GetString(0);
                            custSpender.TotalAmountSpent = reader.GetDecimal(1);

                            highestSpendersList.Add(custSpender);
                        }

                    }
                }

                return highestSpendersList;
            }
        }

        internal List<CustomerGenre> GetCustomerMostPopularGenre(int customerId)
        {
            List<CustomerGenre> popularGenreList = new();

            string sqlQuery = "SELECT TOP 1 WITH TIES C.FirstName, G.Name as Genre, Count(*) as Count " +
                "FROM((((Customer as C " +
                "INNER JOIN Invoice as I ON C.CustomerId = I.CustomerId) " +
                "INNER JOIN InvoiceLine as IL ON I.InvoiceId = IL.InvoiceId) " +
                "INNER JOIN Track as T ON T.TrackId = IL.TrackId) " +
                "INNER JOIN Genre as G ON G.GenreId = T.GenreId) " +
                "WHERE C.CustomerId = @id GROUP BY C.FirstName, G.Name ORDER BY Count DESC;";

            using (SqlConnection conn = new SqlConnection(ConnectionHelper.GetConnectionString()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@id", customerId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CustomerGenre custGenre = new();

                            custGenre.CustomerName = reader.GetString(0);
                            custGenre.GenreName = reader.GetString(1);
                            custGenre.GenreCount = reader.GetInt32(2);

                            popularGenreList.Add(custGenre);
                        }

                    }
                }

                return popularGenreList;
            }
        }
        private string SafeGetString(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }
    }
}
