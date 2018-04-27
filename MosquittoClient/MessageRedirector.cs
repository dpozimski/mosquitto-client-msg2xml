using Microsoft.Extensions.Configuration;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

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

        public void Redirect(MqttApplicationMessage message)
        {
            var document = messageToXmlParser.Parse(message);
            httpClient.PostAsync(receiverHostEndpoint, new StringContent(document));
        }
    }
}
