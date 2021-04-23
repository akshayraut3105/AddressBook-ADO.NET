using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AddressBook_ADO.NET
{
    public class AddressBookRepository
    {
        /// Ensuring the established connection using the Sql connection specifying the property. 
        public static SqlConnection connection { get; set; }
        /// <summary>
        ///UC1 Creating a method for checking for the validity of the connection.
        /// </summary>
        public void EnsureDataBaseConnection()
        {
            /// Creates a new connection for every method to avoid "ConnectionString property not initialized" exception
            DBConnection dbc = new DBConnection();
            connection = dbc.GetConnection();
            using (connection)
            {
                Console.WriteLine("The Connection is created");
            }
            connection.Close();
        }
        /// <summary>
        /// UC2 Getting all the stored records in the address book service table by fetching all the records
        /// </summary>
        public void GetAllContact()
        {
            ///Creates a new connection for every method to avoid "ConnectionString property not initialized" exception
            DBConnection dbc = new DBConnection();
            connection = dbc.GetConnection();
            AddressBookModel model = new AddressBookModel();
            try
            {
                using (connection)
                {
                    /// Query to get all the data from the table
                    string query = @"select * from dbo.Address_Book";
                    /// Impementing the command on the connection fetched database table
                    SqlCommand command = new SqlCommand(query, connection);
                    ///Opening the connection.
                    connection.Open();
                    /// executing the sql data reader to fetch the records
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        /// Mapping the data to the employee model class object
                        while (reader.Read())
                        {
                            model.FirstName = reader.GetString(0);
                            model.LastName = reader.GetString(1);
                            model.Address = reader.GetString(2);
                            model.City = reader.GetString(3);
                            model.State = reader.GetString(4);
                            model.Zip = reader.GetInt32(5);
                            model.PhoneNumber = reader.GetInt64(6);
                            model.EmailId = reader.GetString(7);
                            model.AddressBookType = reader.GetString(8);
                            model.AddressBookName = reader.GetString(9);
                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", model.FirstName, model.LastName,
                                model.Address, model.City, model.State, model.Zip, model.PhoneNumber, model.EmailId, model.AddressBookType, model.AddressBookName);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    reader.Close();
                }
            }
            /// Catching the null record exception
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            /// Always ensuring the closing of the connection
            finally
            {
                connection.Close();
            }

        }
        /// <summary>
        /// UC3 Method to insert contact to the table using a stored procedure
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddDataToTable(AddressBookModel model)
        {
            /// Creates a new connection for every method to avoid "ConnectionString property not initialized" exception
            DBConnection dbc = new DBConnection();
            connection = dbc.GetConnection();
            try
            {
                /// Using the connection established
                using (connection)
                {
                    /// Implementing the stored procedure
                    SqlCommand command = new SqlCommand("dbo.spAddContactDetails", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", model.FirstName);
                    command.Parameters.AddWithValue("@LastName", model.LastName);
                    command.Parameters.AddWithValue("@AddressDetails", model.Address);
                    command.Parameters.AddWithValue("@City", model.City);
                    command.Parameters.AddWithValue("@StateName", model.State);
                    command.Parameters.AddWithValue("@Zip", model.Zip);
                    command.Parameters.AddWithValue("@PhoneNo", model.PhoneNumber);
                    command.Parameters.AddWithValue("@Email", model.EmailId);
                    command.Parameters.AddWithValue("@addressBookType", model.AddressBookType);
                    command.Parameters.AddWithValue("@addressBookName", model.AddressBookName);

                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    /// Return the result of the transaction i.e. the dml operation to update data
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            /// Catching any type of exception generated during the run time 
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        ///  UC 4 Ability to Edit the contactType of the existing contact.
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="addressBookType"></param>
        /// <returns></returns>
        public bool EditContactUsingName(string FirstName, string LastName, string addressBookType)
        {
            /// Creates a new connection for every method to avoid "ConnectionString property not initialized" exception.
            DBConnection dbc = new DBConnection();
            connection = dbc.GetConnection();
            try
            {
                using (connection)
                {
                    connection.Open();
                    string query = @"update dbo.Address_Book set addressBookType = @parameter1
                    where FirstName = @parameter2 and LastName = @parameter3";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@parameter1", addressBookType);
                    command.Parameters.AddWithValue("@parameter2", FirstName);
                    command.Parameters.AddWithValue("@parameter3", LastName);
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            /// Catching any type of exception generated during the run time 
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// UC5 Ability to  Deletes the contact with given first name and last name.
        /// </summary>
        /// <returns></returns>
        public bool DeleteContact(string FirstName, string LastName)
        {
            ///Create a new conection for every method to avoid "ConnectionString property not initialized" exception.
            DBConnection dbc = new DBConnection();
            connection = dbc.GetConnection();
            try
            {
                using (connection)
                {
                    connection.Open();
                    string query = "delete from dbo.Address_Book where FirstName = @parameter1 and LastName =@parameter2";
                    SqlCommand command = new SqlCommand(query, connection);
                    /// Binding the parameter to the formal parameters
                    command.Parameters.AddWithValue("@parameter1", FirstName);
                    command.Parameters.AddWithValue("@parameter2", LastName);
                    /// Storing the result of the executed query
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            /// Catching any type of exception generated during the run time
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// UC6 Retrieves the contacts from a given state or city.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="city"></param>
        public void RetrieveContactFromCityOrStateName()
        {
            Console.WriteLine("Enter the city name:");
            string city = Console.ReadLine();
            Console.WriteLine("Enter the state name");
            string state = Console.ReadLine();
            ///Creates a new connection for every method to avoid "ConnectionString property not initialized" exception
            DBConnection dbc = new DBConnection();
            connection = dbc.GetConnection();
            AddressBookModel model = new AddressBookModel();
            try
            {
                using (connection)
                {
                    /// Query to get all the data from the table
                    string query = $@"select * from dbo.Address_Book where StateName='{state}' or City='{city}'";
                    /// Impementing the command on the connection fetched database table
                    SqlCommand command = new SqlCommand(query, connection);
                    ///Opening the connection.
                    connection.Open();
                    /// executing the sql data reader to fetch the records
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        /// Mapping the data to the employee model class object
                        while (reader.Read())
                        {
                            model.FirstName = reader.GetString(0);
                            model.LastName = reader.GetString(1);
                            model.Address = reader.GetString(2);
                            model.City = reader.GetString(3);
                            model.State = reader.GetString(4);
                            model.Zip = reader.GetInt32(5);
                            model.PhoneNumber = reader.GetInt64(6);
                            model.EmailId = reader.GetString(7);
                            model.AddressBookType = reader.GetString(8);
                            model.AddressBookName = reader.GetString(9);
                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", model.FirstName, model.LastName,
                                model.Address, model.City, model.State, model.Zip, model.PhoneNumber, model.EmailId, model.AddressBookType, model.AddressBookName);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    reader.Close();
                }
            }
            /// Catching the null record exception
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        ///  UC7 Gets the count of people in a state or city.
        /// </summary>
        /// <param name="newData"></param>
        /// <param name="choice"></param>
        public void GetCountOfCityOrState(string newData, int choice)
        {
            /// Creates a new connection for every method to avoid "ConnectionString property not initialized" exception
            DBConnection dbc = new DBConnection();
            /// Calling the Get connection method to establish the connection to the Sql Server
            connection = dbc.GetConnection();
            string query = "";
            try
            {
                using (connection)
                {
                    if (choice == 1)
                    {
                        /// Query to get the data from the table
                        query = @"select Count(FirstName) from dbo.Address_Book
                                   where City=@parameter group by City";
                    }
                    else if (choice == 2)
                    {
                        // Query to get the data from the table
                        query = @"select Count(firstName) from dbo.Address_Book
                                   where StateName=@parameter group by StateName";
                    }
                    else
                    {
                        Console.WriteLine("Wrong Choice....");
                    }
                    /// Impementing the command on the connection fetched database table
                    SqlCommand command = new SqlCommand(query, connection);
                    /// Binding the parameter to the formal parameters
                    command.Parameters.AddWithValue("@parameter", newData);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int count = reader.GetInt32(0);
                            Console.WriteLine($"Counting Number of Contacts Stored in {newData} = {count}");
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data found");
                    }
                    reader.Close();
                }
            }
            /// Catching any type of exception generated during the run time
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}