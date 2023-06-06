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
                //HostName = "localhost",
                HostName = "rabbitmq-clusterip-srv",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
            connection = connectionFactory.CreateConnection();
            channel = connection.CreateModel();

            channel.ExchangeDeclare("AccountUpdateExchange", ExchangeType.Fanout, true, false, null);
        }

        public void Publish(AccountDto account)
        {
            AccountUpdateDto accountUpdate = new AccountUpdateDto();
            //get oldname using id
            accountUpdate.OldName = "me";
            accountUpdate.NewName = account.Name;
            accountUpdate.Email = account.Email;

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(accountUpdate));
            channel.BasicPublish(
                                exchange: "AccountUpdateExchange",
                                 routingKey: string.Empty,
                                 basicProperties: null,
                                 body: body);
        }
    }
}

