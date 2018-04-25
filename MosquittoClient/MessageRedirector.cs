using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MosquittoClient
{
    class MessageRedirector
    {
        private readonly IConfiguration configuration;
        private readonly MessageToXmlParser messageToXmlParser;

        public MessageRedirector(IConfiguration configuration, MessageToXmlParser messageToXmlParser)
        {
            this.configuration = configuration;
            this.messageToXmlParser = messageToXmlParser;
        }

        public void Redirect(string message)
        {
            var receiverHost = configuration["xmlDocumentsReceiver"];
            var document = messageToXmlParser.Parse(message);
            //redirect todo
        }
    }
}
