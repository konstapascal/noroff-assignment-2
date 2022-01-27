using Microsoft.Data.SqlClient;
using ChinookReader.Models;
using System;
using System.Collections.Generic;

namespace ChinookReader.DataAccess
{
    internal class CustomerDA
    {
        /// <summary>
        /// Retrieves a customer by id from the Customer table
        /// </summary>
        /// <param name="id">The id of the wanted customer</param>
        /// <returns>Returns a Customer object</returns>
        internal Customer GetCustomer(int id)
        {
            Customer customer = new();

            // The SQL query to be used
            string sqlQuery = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email " +
                $"FROM Customer WHERE CustomerId = @id;";

            // Opening a connection to the database, with conection string
            using (SqlConnection conn = new SqlConnection(ConnectionHelper.GetConnectionString()))
            {
                conn.Open();

                // Creating an SqlCommand object with the SQL query and the connection this will execute on
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    // Replacing parameters with actual values
                    cmd.Parameters.AddWithValue("@id", id);

                    // Executing the query
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // For every row, 1 in this case, do what is inside the while loop
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

        /// <summary>
        /// Retrieves a customer by name from the Customer table
        /// </summary>
        /// <param name="name">The name of the wanted customer</param>
        /// <returns>Returns a Customer object</returns>
        internal List<Customer> GetCustomer(string name)
        {
            List<Customer> allCustomersList = new();

            string sqlQuery = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email " +
                $"FROM Customer WHERE FirstName LIKE @name;";

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

        /// <summary>
        /// Retrieves all customers from the Customer table
        /// </summary>
        /// <returns>Returns a list of Customer objects</returns>
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

        /// <summary>
        /// Retrieves an amount of customers from the Customer table starting at the offset
        /// </summary>
        /// <param name="amount">The amount of customers to retrieve</param>
        /// <param name="offset">Table row of the retrieval to start at</param>
        /// <returns>Returns a list of Customer objects</returns>
        internal List<Customer> Get(int amount, int offset)
        {
            List<Customer> allCustomersList = new ();

            string sqlQuery = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email " +
                $"FROM Customer " +
                $"ORDER BY CustomerId " +
                $"OFFSET @offset ROWS FETCH NEXT @amount ROWS ONLY;";

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

        /// <summary>
        /// Adds a new customer to the Customers table
        /// </summary>
        /// <param name="newCustomer">The Customer object to add</param>
        /// <returns>Returns the number of affected rows in the table after the operation, 1 meaning a success</returns>
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

                    // These are optional values, so we are checking to see if a NULL needs to be introduced
                    cmd.Parameters.AddWithValue("@country", (object) newCustomer.Country ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@postalCode", (object) newCustomer.PostalCode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@phone", (object) newCustomer.PhoneNumber ?? DBNull.Value);
                    
                    cmd.Parameters.AddWithValue("@email", newCustomer.Email);

                    // Executing the command, updating the customer and returning number of affecter rows
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Updates an existing customer on the Customer table
        /// </summary>
        /// <param name="newCustomer">The Customer object you want to update, identified by id, containing the new values</param>
        /// <returns>Returns the number of affected rows in the table after the operation, 1 meaning a success</returns>
        internal int UpdateCustomer(Customer newCustomer)
        {
            // The SQL query to be executed
            string sqlQuery = "UPDATE Customer SET FirstName = @name, LastName = @lastName, " +
                "Country = @country, PostalCode = @postalCode, " +
                "Phone = @phone, Email = @email " +
               "WHERE CustomerId = @id;";

            // Retrieving the customer the user wants to update, with its original value
            Customer originalCustomer = GetCustomer(newCustomer.CustomerId);

            // If the field is not null, so provided, update that field on the original Customer object
            if (!(newCustomer.PhoneNumber is null)) originalCustomer.PhoneNumber = newCustomer.PhoneNumber;
            if (!(newCustomer.Country is null)) originalCustomer.Country = newCustomer.Country;
            if (!(newCustomer.PostalCode is null)) originalCustomer.PostalCode = newCustomer.PostalCode;
            if (!(newCustomer.FirstName is null)) originalCustomer.FirstName = newCustomer.FirstName;
            if (!(newCustomer.LastName is null)) originalCustomer.LastName = newCustomer.LastName;
            if (!(newCustomer.Email is null)) originalCustomer.Email = newCustomer.Email;

            using (SqlConnection conn = new SqlConnection(ConnectionHelper.GetConnectionString()))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    // Replacing parameters with either original customers value or the new provided value
                    cmd.Parameters.AddWithValue("@id", originalCustomer.CustomerId);
                    cmd.Parameters.AddWithValue("@name", originalCustomer.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", originalCustomer.LastName);
                    cmd.Parameters.AddWithValue("@country", (object)originalCustomer.Country ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@postalCode", (object)originalCustomer.PostalCode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@phone", (object)originalCustomer.PhoneNumber ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@email", originalCustomer.Email);

                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Returns a list of top 5 countries with most users ordered in descending order
        /// </summary>
        /// <returns>Returns a list of CustomerCountry objects</returns>
        internal List<CustomerCountry> CustomerCountByCountry()
        {
            List<CustomerCountry> customerCountCountryList = new();

            string sqlQuery = "SELECT TOP 5 Country, Count(*) AS CustomerCount " +
                "FROM Customer " +
                "GROUP BY Country " +
                "ORDER BY CustomerCount DESC;";

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

        /// <summary>
        /// Gets the top 5 highest spending customers and the amount they have spent, in a descending order
        /// </summary>
        /// <returns>Returns a list of CustomerSpender objects, containing customer name and amount spent</returns>
        internal List<CustomerSpender> TopHighestSpenders()
        {
            List<CustomerSpender> highestSpendersList = new();

            string sqlQuery = "SELECT TOP 5 C.FirstName, I.Total AS Total " +
                "FROM Customer AS C " +
                "INNER JOIN Invoice AS I ON C.CustomerId = I.CustomerId " +
                "GROUP BY C.FirstName, I.Total " +
                "ORDER BY I.Total DESC;";

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

        /// <summary>
        /// Gets the top 1 genre, with ties, of the customer with specified id.
        /// </summary>
        /// <param name="customerId">The id of the customer you want to get the top genre of</param>
        /// <returns>Returns a list of CustomerGenre objects, containing the customers name, genre name and the amount of tracks for that genre</returns>
        internal List<CustomerGenre> GetCustomerMostPopularGenre(int customerId)
        {
            List<CustomerGenre> popularGenreList = new();


            // monkaW
            string sqlQuery = "SELECT TOP 1 WITH TIES C.FirstName, G.Name, Count(*) AS Count " +
                "FROM Customer AS C " +
                "INNER JOIN Invoice AS I ON C.CustomerId = I.CustomerId " +
                "INNER JOIN InvoiceLine AS IL ON I.InvoiceId = IL.InvoiceId " +
                "INNER JOIN Track AS T ON T.TrackId = IL.TrackId " +
                "INNER JOIN Genre AS G ON G.GenreId = T.GenreId " +
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

        // Source: https://stackoverflow.com/questions/1772025/sql-data-reader-handling-null-column-values/1772037#1772037

        /// <summary>
        /// Safe alternative to SqlDataReader objects GetString method, that will first check if entry is null in the the column
        /// </summary>
        /// <param name="reader">The object instance of SqlDataReader</param>
        /// <param name="colIndex">Column index that you want to check the value for</param>
        /// <returns>Returns either the value itself as a string or and empty string if that would otherwise be null</returns>
        private string SafeGetString(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }
    }
}
