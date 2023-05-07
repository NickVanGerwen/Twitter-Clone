using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Text;
using twitter_post_service.Models;

namespace twitter_fetch_service.Rabbitmq
{
    public class RabbitMQConsumer : BackgroundService
    {
        //public ICoordinateCollector _collector;

        RabbitMQ.Client.IConnectionFactory connectionFactory;
        RabbitMQ.Client.IModel channel;
        RabbitMQ.Client.IConnection connection;

        private readonly IServiceScopeFactory _serviceScopeFactory;
        public RabbitMQConsumer(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;

            connectionFactory = new ConnectionFactory
            {
                HostName = "twitterbutcooler.com",
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

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            channel.BasicConsume("PostCreationQueue", true, consumer);

            return Task.CompletedTask;
        }
        private void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            //convert payload to CoordinateDTO
            byte[] body = e.Body.ToArray();
            string payload = Encoding.UTF8.GetString(body);
            JObject postJson = JObject.Parse(payload);
            string time = postJson["Date"].ToString();
            try
            {
                PostCreateModel post = new PostCreateModel()
                {
                    Message = (string)postJson["Message"],
                    Author = (string)postJson["Author"],
                    Date = DateTime.Parse(time),
                    Likes = (int)postJson["Likes"]
                };
                Console.WriteLine(post);
            }
            catch (Exception ex) { Console.Write(ex.ToString()); }
        }
    }
}
