using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Text;
using twitter_fetch_service.Data;
using twitter_fetch_service.Models;
using twitter_fetch_service.DTOs;

namespace twitter_fetch_service.Rabbitmq
{
    public class UpdateAccountConsumer : BackgroundService
    {
        //public ICoordinateCollector _collector;

        RabbitMQ.Client.IConnectionFactory connectionFactory;
        RabbitMQ.Client.IModel channel;
        RabbitMQ.Client.IConnection connection;

        public IPostRepo _postRepo;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public UpdateAccountConsumer(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
                //HostName = "rabbitmq-clusterip-srv",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
            connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();

            channel.ExchangeDeclare("AccountUpdateExchange", ExchangeType.Topic, true, false, null);
            var queue = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue, "AccountUpdateExchange", string.Empty);

            Console.WriteLine("----> " + queue);
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
            try
            {
                AccountUpdateDto account = new AccountUpdateDto
                {
                    Id = (int)postJson["Id"],
                    Name = (string)postJson["Name"],
                    Email = (string)postJson["Email"]
                };

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    _postRepo = scope.ServiceProvider.GetRequiredService<IPostRepo>();
                    //update account
                    //_postRepo.CreatePost(post);
                    //Console.WriteLine(post);
                }
                //Console.WriteLine(post);
            }
            catch (Exception ex) { Console.Write(ex.ToString()); }
        }
    }
}
