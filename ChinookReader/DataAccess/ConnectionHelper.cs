using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookReader.DataAccess
{
    internal static class ConnectionHelper
    {
        /// <summary>
        /// A method that builds and returns the connection string
        /// </summary>
        /// <returns>The connection string</returns>
        public static string GetConnectionString()
        {
            SqlConnectionStringBuilder builder = new();
            
            builder.DataSource = @"ND-5CG92747KC\SQLEXPRESS";
            builder.InitialCatalog = "Chinook";
            builder.IntegratedSecurity = true;

            return builder.ConnectionString;
        }

    }
}
