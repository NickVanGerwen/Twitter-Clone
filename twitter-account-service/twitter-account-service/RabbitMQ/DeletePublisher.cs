using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Channels;
using twitter_account_service.DTOs;

namespace twitter_account_service.Rabbitmq
{
    public class DeletePublisher
    {
        RabbitMQ.Client.IConnectionFactory connectionFactory;
        RabbitMQ.Client.IModel channel;
        RabbitMQ.Client.IConnection connection;

        public DeletePublisher()
        {
            connectionFactory = new ConnectionFactory
            {
                //HostName = "localhost",
                HostName = "rabbitmq-clusterip-srv",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
            connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();

            channel.ExchangeDeclare("AccountDeleteExchange", ExchangeType.Fanout, true, false, null);
         }

        public void Publish(int Id)
        {
            //get username from id
            string UserName = "John Doe";
            var body = Encoding.UTF8.GetBytes(UserName);
            channel.BasicPublish(
                                exchange: "AccountDeleteExchange",
                                 routingKey: string.Empty,
                                 basicProperties: null,
                                 body: body);
        }
    }
}

