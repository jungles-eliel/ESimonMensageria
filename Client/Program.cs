using ESimonMensageria.Model.Wrappers;
using LabMensageria.Client;
using System;

namespace ClienteLabMensageria.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientWrapper client = new ClientWrapper();
            Publish publish = new Publish();

            for (int i = 0; i < 2; i++)
            {
                client.IdClient = Guid.NewGuid(); 
                client.Name = "Fulano de Tal " + i.ToString();
                client.DateCreated = DateTime.Now;

                publish.PublishMessage(client);
            }
            


            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
        }
    }
}
