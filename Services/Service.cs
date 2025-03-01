
using Domains.Entities;

namespace Services
{
    public class Service
    {
        private List<UserInfo> _users;
        public Service()
        {
            if (_users == null)
            {
                _users = new List<UserInfo>();
            }
        }
        private void AddUser(string name, string email, string address, List<string> phones)
        {
            UserInfo user = new UserInfo();
            user.Name = name;
            user.Email_User = email;
            user.Adress = address;
            user.Phone = phones;
            AddUser(user);
        }
        private void AddUser(UserInfo user)
        {
            _users.Add(user);
        }
        public List<UserInfo> GetUsers()
        {
            return _users;
        }
        public void Serve()
        { bool exit = false;
            while (!exit)
            {
                Console.WriteLine("1. Add User");
                Console.WriteLine("2. List Users");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        Console.Write("Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Email: ");
                        string email = Console.ReadLine();
                        Console.Write("Address: ");
                        string address = Console.ReadLine();
                        Console.Write("Phones (comma separated): ");
                        string phones = Console.ReadLine();
                        List<string> phoneList = phones.Split(',').ToList();
                        AddUser(name, email, address, phoneList);
                        break;
                    case "2":
                        List<UserInfo> users = GetUsers();
                        foreach (UserInfo user in users)
                        {
                            Console.WriteLine($"Name: {user.Name}");
                            Console.WriteLine($"Email: {user.Email_User}");
                            Console.WriteLine($"Address: {user.Adress}");
                            Console.WriteLine("Phones:");
                            foreach (string phone in user.Phone)
                            {
                                Console.WriteLine(phone);
                            }
                            Console.WriteLine();
                        }
                        break;
                    case "3":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
    }
}
