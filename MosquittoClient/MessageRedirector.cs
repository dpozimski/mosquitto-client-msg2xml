using Microsoft.Extensions.Configuration;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MosquittoClient
{
    class MessageRedirector
    {
        private static HttpClient httpClient = new HttpClient();
        private readonly IConfiguration configuration;
        private readonly MessageToXmlParser messageToXmlParser;
        private readonly string receiverHostEndpoint;

        public MessageRedirector(IConfiguration configuration, MessageToXmlParser messageToXmlParser)
        {
            this.configuration = configuration;
            this.messageToXmlParser = messageToXmlParser;
            receiverHostEndpoint = configuration["xmlDocumentsReceiverEndpoint"];
        }

        public async Task RedirectAsync(MqttApplicationMessage message)
        {
            var document = messageToXmlParser.Parse(message);
            Console.WriteLine("Redirect from: " + message.Topic);
            Console.WriteLine("using xml doc: " + document);
            await httpClient.PostAsync(receiverHostEndpoint, new StringContent(document));
        }
    }
}
