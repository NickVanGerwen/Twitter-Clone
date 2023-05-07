using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTrackerProducer.Models
{
    internal class RabbitServer
    {
        private string _connectionString;
        private string username;
        private string password;
        private string hostname;
        private string port;


        public RabbitServer() {
            if (string.IsNullOrEmpty(username)) 
            { 
                username = "guest";
            }
            if (string.IsNullOrEmpty(password))
            {
                password = "guest";
            }
            if (string.IsNullOrEmpty(hostname))
            {
                hostname = "localhost";
            }
            if (string.IsNullOrEmpty(port))
            {
                port = "5672";
            }
            _connectionString = $"amqp://{username}:{password}@{hostname}:{port}";
        }

        public RabbitServer(string username, string password, string hostname, string port)
        {
            this.username = username;
            this.password = password;
            this.hostname = hostname;
            this.port = port;
            _connectionString = $"amqp://{this.username}:{this.password}@{this.hostname}:{this.port}";
        }

        public string getConnectionString()
        {
            return _connectionString;
        }
    }
}
