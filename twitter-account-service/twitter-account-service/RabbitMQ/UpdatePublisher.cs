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
    public class UpdatePublisher
    {
        RabbitMQ.Client.IConnectionFactory connectionFactory;
        RabbitMQ.Client.IModel channel;
        RabbitMQ.Client.IConnection connection;

        public UpdatePublisher()
        {
            connectionFactory = new ConnectionFactory
            {
                //HostName = "rabbitmq-clusterip-srv",
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
            connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();

            //create and bind queues
            var queue = channel.QueueDeclare();
            var queue1 = channel.QueueDeclare();
            channel.QueueBind(queue, "AccountUpdateExchange", string.Empty);
            channel.QueueBind(queue1, "AccountUpdateExchange", string.Empty);

            channel.ExchangeDeclare("AccountUpdateExchange", ExchangeType.Fanout, true, false, null);
        }

        public void Publish(AccountUpdateDto accountUpdate)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(accountUpdate));
            channel.BasicPublish(
                                exchange: "AccountUpdateExchange",
                                 routingKey: string.Empty,
                                 basicProperties: null,
                                 body: body);
        }
    }
}

