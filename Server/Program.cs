using ESimonMensageria.Server;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            new RabbitMQListen().Listen();
        }
    }
}
