using MQTTnet;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MosquittoClient
{
    class Worker
    {
        private readonly MessageListener messageListener;
        private readonly MessageRedirector messageRedirector;

        public Worker()
        {
            var configurationFactory = new ConfigurationFactory();
            var configuration = configurationFactory.Create();
            var messageToXmlParser = new MessageToXmlParser(configuration);

            messageListener = new MessageListener(configuration);
            messageRedirector = new MessageRedirector(configuration, messageToXmlParser);
        }

        public async Task StartAsync()
        {
            messageListener.MessageReceived += OnMessageReceived;

            await messageListener.StartAsync();
        }

        private async void OnMessageReceived(object sender, MqttApplicationMessage e)
        {
            try
            {
                await messageRedirector.RedirectAsync(e);
                Console.WriteLine($"Error while redirecting message. {e.Topic}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while redirecting message. {e.Topic}, {e.Payload?.Length ?? 0}, {ex}");
            }
        }
    }
}
