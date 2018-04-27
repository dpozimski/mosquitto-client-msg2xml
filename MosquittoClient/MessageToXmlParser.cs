using Microsoft.Extensions.Configuration;
using MQTTnet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MosquittoClient
{
    class MessageToXmlParser
    {
        private readonly IList<MessageToXmlPattern> patterns;

        public MessageToXmlParser(IConfiguration configuration)
        {
            patterns = configuration.GetSection("xmlTemplates").Get<List<MessageToXmlPattern>>();
        }

        public string Parse(MqttApplicationMessage message)
        {
            var template = GetTemplate(message.Topic);
            var xml = GetXml(template, message.Topic, message.Payload);
            return xml;
        }

        private string GetXml(string template, string topic, byte[] payload)
        {
            var topicParts = topic.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            for(int i = 1; i <= topicParts.Length; i++)
            {
                template = template.Replace($"#{i}", topicParts[i - 1]);
            }
            template = template.Replace("#Content", Encoding.UTF8.GetString(payload));
            return template;
        }

        private string GetTemplate(string topic)
        {
            //get specific pattern
            var pattern = patterns.FirstOrDefault(s => s.Topic == topic);
            //get default pattern
            if (pattern is null)
                pattern = patterns.First(s => s.Topic == "Default");
            return pattern.Template;
        }
    }
}
