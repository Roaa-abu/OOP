using Services;

namespace OOP
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
