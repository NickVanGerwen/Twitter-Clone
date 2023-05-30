using CarTrackerProducer.Models;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Channels;
using twitter_post_service.DTOs;
using twitter_post_service.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace twitter_post_service.Rabbitmq
{
    public class RabbitmqPublisher
    {
        RabbitServer rabbitServer;
        RabbitMQ.Client.IConnectionFactory connectionFactory;
        RabbitMQ.Client.IModel channel;
        RabbitMQ.Client.IConnection connection;

        public RabbitmqPublisher()
        {
            connectionFactory = new ConnectionFactory
            {
                HostName = "twitter-rabbit-87dtk",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
            connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();

            channel.ExchangeDeclare("PostCreationExchange", ExchangeType.Topic, true, false, null);
            channel.QueueDeclare("PostCreationQueue",
            durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            channel.QueueBind("PostCreationQueue", "PostCreationExchange", "PostCreation.Created.*");
        }

        public void Publish(PostCreateDTO post)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(post));
            channel.BasicPublish(
                                exchange: "PostCreationExchange",
                                 routingKey: "PostCreation.Created.PostCreationNewPost",
                                 basicProperties: null,
                                 body: body);
        }
    }
}

