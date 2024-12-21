using RabbitMQ.Client;
using System.Text;

namespace MessageSend
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                MessageSend(); // SendMessage
            }
        }

        public static void MessageSend() // SendMessage
        {
            // Connection to RabbitMQ
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://ulelssbx:Xe5xxzNaKMqlJv1pM4-rWhzQbrOAyJZP@rat.rmq2.cloudamqp.com/ulelssbx"); // RabbitMQ URL

            // The connection is starting.
            using var connection = factory.CreateConnection();

            // A channel needs to be created.
            var channel = connection.CreateModel();

            // Channel settings and queue creation.
            channel.QueueDeclare("Message", true, false, false);
            // queue name (routing key) - Do you want it to be stored somewhere other than memory? - Can this connection be accessed from other places? - Should auto/deleted be used?

            // The sent message has been created.
            var mesaj = "Test message";

            // The message needs to be converted to binary format to send - message converted to bytes.
            var body = Encoding.UTF8.GetBytes(mesaj);

            // Since there are no exchanges, empty routing key, properties are null by default, message
            channel.BasicPublish(string.Empty, "Message", null, body);

            Console.WriteLine("Message sent.");
            Console.ReadLine();
        }
    }
}
