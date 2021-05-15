using ESimonMensageria.Model.Wrappers;
using RabbitMQ.Client;
using System;
using System.Text;

namespace LabMensageria.Client
{
    public class Publish
    {
        public void PublishMessage(ClientWrapper wrapper)
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            channel.ConfirmSelect();
            channel.BasicAcks += ConfirmatedEvent;
            channel.BasicNacks += NotConfirmatedEvent;
            channel.QueueDeclare(queue: "lab-mensageria",
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            String json = Newtonsoft.Json.JsonConvert.SerializeObject(wrapper);
            byte[] body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: "lab-mensageria", basicProperties: null, body: body);

            Console.WriteLine($"Mensagem enviada! - [{wrapper.Name}]");
        }

        private void NotConfirmatedEvent(object sender, RabbitMQ.Client.Events.BasicNackEventArgs e)
        {
            //Console.WriteLine("BasicNacks");
        }

        private void ConfirmatedEvent(object sender, RabbitMQ.Client.Events.BasicAckEventArgs e)
        {
            //Console.WriteLine("BasicAcks");
        }
    }
}
