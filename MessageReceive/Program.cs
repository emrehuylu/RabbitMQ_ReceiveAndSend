using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace MessageReceive
{
    internal class Program
    {
        /// <summary>
        /// This is where the message capture process from RabbitMQ is performed.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            MessageReceive(); // ReceiveMessage
        }

        private static void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            // To be able to read as a string.
            string body = Encoding.UTF8.GetString(e.Body.ToArray());
            Console.WriteLine(body);
        }

        private static void MessageReceive() // ReceiveMessage
        {
            var factory = new ConnectionFactory();

            factory.Uri = new Uri("amqps://ulelssbx:Xe5xxzNaKMqlJv1pM4-rWhzQbrOAyJZP@rat.rmq2.cloudamqp.com/ulelssbx");

            using var connection = factory.CreateConnection();

            // We need to create a queue again in the same way.
            var channel = connection.CreateModel();

            channel.QueueDeclare("Message", true, false, false);
            // Up to here, since the connection has been made, the same steps as writing a message to the queue are followed.

            // To capture the message in the queue, a different structure needs to be created from here.
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume("Message", true, consumer); // We are telling it to listen to the queue named 'Message'.

            consumer.Received += Consumer_Received;

            Console.ReadLine();
        }
    }
}
