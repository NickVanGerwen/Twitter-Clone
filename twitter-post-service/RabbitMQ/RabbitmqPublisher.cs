using CarTrackerProducer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using System.Text;
using twitter_post_service.DTOs;
using twitter_post_service.Models;

namespace twitter_post_service.RabbitMQ
{
    public class RabbitmqPublisher
    {
        RabbitServer rabbitServer;

        public RabbitmqPublisher()
        {
            using (StreamReader reader = new StreamReader("Properties/rabbitMQSettings.json"))
            {
                string json = reader.ReadToEnd();
                JObject configuration = JObject.Parse(json);
                rabbitServer = new RabbitServer(
                    (string)configuration.SelectToken("RabbitMQ.Username"),
                    (string)configuration.SelectToken("RabbitMQ.Password"),
                    (string)configuration.SelectToken("RabbitMQ.Hostname"),
                    (string)configuration.SelectToken("RabbitMQ.Port"));
            }
        }


        public void Publish(PostCreateDTO post)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(rabbitServer.getConnectionString())
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare("PostCreationExchange", ExchangeType.Topic, true, false, null);
            channel.QueueDeclare("PostCreationQueue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            channel.QueueBind("PostCreationQueue", "PostCreationExchange", "PostCreation.Created.*");

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(post));
            channel.BasicPublish(
                                exchange: "PostCreationExchange",
                                 routingKey: "PostCreation.Created.PostCreationNewPost",
                                 basicProperties: null,
                                 body: body);
        }
    }
}

