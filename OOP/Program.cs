using PioneersAcademy.PhoneBookServices;

namespace PioneersAcademy.PhoneBook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var service = new Service();
            service.Serve();
        }
    }
}
