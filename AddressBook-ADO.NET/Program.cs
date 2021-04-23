using System;

namespace AddressBook_ADO.NET
{
    class Program
    {
        public static AddressBookRepository repository = new AddressBookRepository();
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to AddressBook System program using ADO.NET");
            ///Creating Instance object of AddressBookRepository class.
            AddressBookRepository repository = new AddressBookRepository();
            ///UC1 Creating a method for checking for the validity of the connection.
            repository.EnsureDataBaseConnection();
            /// UC2 Getting all the stored records in the address book service table by fetching all the records
            repository.GetAllContact();
            /// UC 3: Adds the new contact into DB table.
            AddNewContactDetails();
            /// UC 4 Ability to Edit the contactType of the existing contact.
            Console.WriteLine(repository.EditContactUsingName("Neha", "Singh", "Friend") ? "Update done successfully\n" : "Update failed");
            /// UC5 Ability to  Deletes the contact with given first name and last name.
            Console.WriteLine(repository.DeleteContact("Neha", "Singh") ? "Deleted Contact successfully\n" : "Update failed");
            /// UC6 Retrieves the contacts from a given state or city.
            repository.RetrieveContactFromCityOrStateName();
            ///  UC7 Gets the count of people in a state or city.
            GetCountByCityOrState();
        }
        /// UC 3: Adds the new contact into DB table.
        public static void AddNewContactDetails()
        {
            AddressBookRepository repository = new AddressBookRepository();
            AddressBookModel model = new AddressBookModel();
            model.FirstName = "Neha";
            model.LastName = "Singh";
            model.Address = "MANSAROVAR";
            model.City = "jaipur";
            model.State = "Rajasthan";
            model.Zip = 788776;
            model.PhoneNumber = 887788779;
            model.EmailId = "neha@gmail.com";
            model.AddressBookName = "neha";
            model.AddressBookType = "Friend";
            Console.WriteLine(repository.AddDataToTable(model) ? "Record inserted successfully\n" : "Failed");
        }
        /// Method driver for the city or state count by particular city or state
        public static void GetCountByCityOrState()
        {
            Console.WriteLine("Enter the choice you want to retrieve data");
            Console.WriteLine("1.City.");
            Console.WriteLine("2.State.");
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the City or State by which you want the data -");
            string cityOrState = Console.ReadLine();
            repository.GetCountOfCityOrState(cityOrState, choice);
        }
    }
}