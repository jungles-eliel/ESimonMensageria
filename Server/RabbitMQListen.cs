using ESimonMensageria.Model.Wrappers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace ESimonMensageria.Server
{
    public class RabbitMQListen
    {
        public void Listen()
        {
            try
            {
                Console.Write("Conectando ao servidor.... ");
                
                ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
                using IConnection connection = factory.CreateConnection();
                Console.Write("conectado!\r\n");

                using IModel channel = connection.CreateModel();

                channel.QueueDeclare("lab-mensageria", false, false, false, null);
                Console.Write("Iniciado! Aguardando mensagens...\r\n");

                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, data) =>
                {
                    String json = Encoding.UTF8.GetString(data.Body.ToArray());
                    ClientWrapper client = Newtonsoft.Json.JsonConvert.DeserializeObject<ClientWrapper>(json);

                    Console.Write($"Mensagem recebida: {client.IdClient} - {client.Name} - {String.Format("{0:d/M/yyyy HH:mm:ss.fff}", client.DateCreated)}\r\n");
                };

                channel.BasicConsume("lab-mensageria", true, consumer);
                Console.ReadLine();
            }
            catch(RabbitMQ.Client.Exceptions.BrokerUnreachableException ex)
            {
                Console.WriteLine("falha!\r\n");
                Console.WriteLine("Não foi possível conectar ao servidor!\r\n\r\n");
                Console.WriteLine(ex.Message);
                throw ex;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }
    }
}
