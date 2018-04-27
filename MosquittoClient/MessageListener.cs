using Microsoft.Extensions.Configuration;
using MQTTnet;
using MQTTnet.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MosquittoClient
{
    class MessageListener : IDisposable
    {
        private readonly IConfiguration configuration;
        private IMqttClient client;

        public event EventHandler<MqttApplicationMessage> MessageReceived;

        public MessageListener(IConfiguration configuration)
        {
            this.configuration = configuration;
            var factory = new MqttFactory();
            client = factory.CreateMqttClient();
        }

        public async Task StartAsync()
        {
            var clientId = configuration["mqttClientId"];
            var host = configuration["mqttBrokerHost"];
            var port = configuration["mqttBrokerPort"];
            var credentials = configuration.GetSection("mqttCredentials").Get<MqttClientCredentials>();
            var options = new MqttClientOptionsBuilder()
                .WithClientId(clientId)
                .WithTcpServer(host, int.Parse(port))
                .WithCredentials(credentials.Username, credentials.Password)
                .WithCleanSession()
                .Build();

            await client.ConnectAsync(options);

            client.ApplicationMessageReceived += OnApplicationMessageReceived;
        }

        private void OnApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            MessageReceived?.Invoke(sender, e.ApplicationMessage);
        }

        public void Dispose()
        {
            client.ApplicationMessageReceived -= OnApplicationMessageReceived;
        }
    }
}
