using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AddressBook_ADO.NET
{
    public class DBConnection
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetConnection()
        {
            /// Specifying the connectionString from the sql server connection.
            string connectiongString = @"Data Source=DESKTOP-TSL9UFS;Initial Catalog=addressbook_service;Integrated Security=True;User ID=akshay;Password=1997";
            SqlConnection connection = new SqlConnection(connectiongString);
            return connection;
        }
    }
}