using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace MosquittoClient
{
    class MessageToXmlParser
    {
        private readonly IConfigurationRoot configuration;

        public MessageToXmlParser(IConfiguration configuration)
        {
            this.configuration = configurationRoot;
        }

        public XDocument Parse(string message)
        {
            return null;
        }
    }
}
