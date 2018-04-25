using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MosquittoClient
{
    class MessageListener
    {
        private readonly IConfiguration configuration;

        public event EventHandler<string> MessageReceived;

        public MessageListener(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Start()
        {
            var host = configuration["mqttBrokerHost"];

        }
    }
}
