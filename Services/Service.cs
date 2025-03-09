
using PioneersAcademy.Domains.Entities;
using PioneersAcademy.Domains.Enums;

namespace PioneersAcademy.PhoneBookServices
{
    public class Service
    {
        #region Fields
        private List<PhoneContact> _users;
        #endregion

        #region Constructors
        public Service()
        {
            if (_users == null)
            {
                _users = new List<PhoneContact>();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add a new user to the phone book
        /// </summary>
        /// <param name="name">String Name</param>
        /// <param name="phones">Phone Numbers as a list of strings</param>
        private void AddUser(string name, List<string> phones)
        {
            PhoneContact user = new PhoneContact();
            user.Name = name;
            user.Phone = phones;
            _users.Add(user);
        }

        /// <summary>
        /// Search for a user by name or phone number
        /// </summary>
        /// <param name="query">String to detect type of searching</param>
        /// <returns>Returns list of Contacts if the search found the Contact if no returns null</returns>
        public List<PhoneContact> Search(string query)
        {
            List<PhoneContact> result = new List<PhoneContact>();
            try
            {
                if ((query.ToLower() != "n" && query.ToLower() != "p") || string.IsNullOrEmpty(query) || string.IsNullOrWhiteSpace(query))
                {
                    Console.WriteLine("Please enter a valid choice next time");
                    return null;
                }

                if (query.ToLower() == "n")
                {
                    Console.Write("Enter the name to start searching: ");
                    var name = Console.ReadLine();
                    if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                    {
                        Console.WriteLine("Please enter a valid name");
                        return null;
                    }
                    else if (_users != null && _users.Count != 0)
                    {
                        var found = (from user in _users
                                    where user.Name==name
                                    select user).FirstOrDefault();
                        result.Add(found);
                    }
                }
                else
                {
                    Console.Write("Enter the phone number to start searching: ");
                    var phone = Console.ReadLine();
                    if (string.IsNullOrEmpty(phone) || string.IsNullOrWhiteSpace(phone))
                    {
                        Console.WriteLine("Please enter a valid phone");
                        return null;
                    }
                    else if (_users!= null && _users.Count!=0)
                    {
                        var found = (from user in _users
                                     where user.Phone.Contains(phone)
                                     select user).FirstOrDefault();
                        result.Add(found);
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Print the users in the phone book
        /// </summary>
        /// <param name="contacts">Phone contact Object</param>
        public static void PrintUsers(List<PhoneContact> contacts)
        {
            foreach (PhoneContact contact in contacts)
            {
                Console.WriteLine($"Name: {contact.Name}");
                Console.WriteLine("Phones:");
                foreach (string phone in contact.Phone)
                {
                    Console.WriteLine(phone);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Edit the user info
        /// </summary>
        /// <param name="name">String Name</param>
        /// <returns>Returns the status for editing</returns>
        public Status EditUser(string name)
        {
            var user = new PhoneContact();
            foreach (var item in _users)
            {
                if (item.Name == name)
                {
                    user = item;
                    break;
                }
            }
            if (user == null)
            {
                Console.WriteLine("User not found");
                return Status.fail;
            }
            Console.WriteLine("Choose a number \n 1. For Editing Name \n 2. For Editing phone list\n 3. For Editing both");
            var isNumber = int.TryParse(Console.ReadLine(), out int choice);
            if (choice != null && !isNumber)
            {
                return Status.error;
            }
            switch (choice)
            {
                case 1:
                    Console.Write("Enter new name: ");
                    user.Name = Console.ReadLine();
                    break;
                case 2:
                    Console.Write("Enter new phones (comma separated): ");
                    string phones = Console.ReadLine();
                    user.Phone = phones.Split(',').ToList();
                    break;
                case 3:
                    Console.Write("Enter new name: ");
                    user.Name = Console.ReadLine();
                    Console.Write("Enter new phones (comma separated): ");
                    phones = Console.ReadLine();
                    user.Phone = phones.Split(',').ToList();
                    break;
                default:
                    Console.WriteLine("Please enter a valid choice next time");
                    return Status.error;
            }
            return Status.success;
        }

        /// <summary>
        ///     Delete the user from the phone book
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Returns the status for deleting</returns>
        public Status DeleteUser(string name)
        {
            dynamic user = null;
            if (_users != null && _users.Count != 0)
                foreach (var item in _users)
                {
                    if (item.Name == name)
                    {
                        user = item;
                        break;
                    }
                }
            if (user == null)
            {
                return Status.fail;
            }
            else
            {
                _users.Remove(user);
            }
            return Status.success;
        }

        /// <summary>
        /// Serve the phone book
        /// </summary>
        public void Serve()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("1. Add User");
                Console.WriteLine("2. List Users");
                Console.WriteLine("3. Search via Name or Phone");
                Console.WriteLine("4. Edite User");
                Console.WriteLine("5. Delete User");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        Console.Write("Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Phones (comma separated): ");
                        string phones = Console.ReadLine();
                        List<string> phoneList = phones.Split(',').ToList();
                        if (phoneList != null && phoneList.Count == 0 || string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                        {
                            Console.WriteLine("Please enter at least one phone number or valid name");
                            break;
                        }
                        AddUser(name, phoneList);
                        break;
                    case "2":
                        if (_users == null || _users.Count == 0)
                        {
                            Console.WriteLine("No users found");
                            break;
                        }
                        PrintUsers(_users);
                        break;
                    case "3":
                        Console.Write("Entar N for name search or P for phone search: ");
                        var resultContacts = Search(Console.ReadLine());
                        if (resultContacts == null || resultContacts.Count == 0)
                        {
                            Console.WriteLine("No results found");
                        }
                        else
                        {
                            PrintUsers(resultContacts);
                        }
                        break;
                    case "4":
                        Console.Write("Enter the name that you need to fix its info: ");
                        string nameToEdit = Console.ReadLine();
                        if (string.IsNullOrEmpty(nameToEdit))
                        {
                            Console.WriteLine("Please enter a valid name when you choose this option next time");
                        }
                        else
                        {
                            var exsist = EditUser(nameToEdit);
                            if (exsist == Status.success)
                            {
                                Console.WriteLine("User edited successfully");
                            }
                            else if (exsist == Status.fail)
                            {
                                Console.WriteLine("User not found");
                            }
                            else if (exsist == Status.error)
                            {
                                Console.WriteLine("Please enter a valid choice next time");
                            }
                        }
                        break;
                    case "5":
                        Console.Write("Enter the name that you need to delete: ");
                        string nameToDelete = Console.ReadLine();
                        if (string.IsNullOrEmpty(nameToDelete))
                        {
                            Console.WriteLine("Please enter a valid name when you choose this option next time");

                        }
                        else
                        {
                            var done = DeleteUser(nameToDelete);
                            if (done == Status.success)
                            {
                                Console.WriteLine("User deleted successfully");
                            }
                            else if (done == Status.fail)
                            {
                                Console.WriteLine("User not found");
                            }
                        }
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
        #endregion
    }
}
