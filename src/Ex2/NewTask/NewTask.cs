using System;
using RabbitMQ.Client;
using System.Text;

namespace NewTask
{
    class NewTask
    {
        static void Main(string[] args)
        {
            var message = GetMessage(args);
            var body = Encoding.UTF8.GetBytes(message);

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(exchange: "",
                                        routingKey: "task_queue",
                                        basicProperties: properties,
                                        body: body);
                }
            }
        }
    
        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }
    }
}
