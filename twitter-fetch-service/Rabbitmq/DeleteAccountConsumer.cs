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
    public class DeleteAccountConsumer : BackgroundService
    {
        //public ICoordinateCollector _collector;

        RabbitMQ.Client.IConnectionFactory connectionFactory;
        RabbitMQ.Client.IModel channel;
        RabbitMQ.Client.IConnection connection;

        public IPostRepo _postRepo;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        string queue;
        public DeleteAccountConsumer(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            connectionFactory = new ConnectionFactory
            {
                //HostName = "localhost",
                //HostName = "rabbitmq-clusterip-srv",
                HostName = "twitter-rabbit-87dtk",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
            connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();

            channel.ExchangeDeclare("AccountDeleteExchange", ExchangeType.Fanout, true, false, null);
            queue = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue, "AccountDeleteExchange", string.Empty);

            Console.WriteLine("----> queue created: " + queue);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += Consumer_Received;
                channel.BasicConsume(queue, true, consumer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return Task.CompletedTask;
        }
        private void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            byte[] body = e.Body.ToArray();
            string Username = Encoding.UTF8.GetString(body);
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                _postRepo = scope.ServiceProvider.GetRequiredService<IPostRepo>();
                //update account
                _postRepo.DeleteUser(Username);
                Console.WriteLine("Account deleted: " + Username);
            }
        }
    }
}
