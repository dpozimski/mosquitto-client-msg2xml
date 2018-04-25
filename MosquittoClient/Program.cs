using System;

namespace MosquittoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Mosquitto client started");

            var configurationFactory = new ConfigurationFactory();
            var configuration = configurationFactory.Create();

            var messageToXmlParser = new MessageToXmlParser(configuration);
            var messageListener = new MessageListener(configuration);
            var messageRedirector = new MessageRedirector(configuration, messageToXmlParser);

            messageListener.MessageReceived += (o, e) =>
            {
                messageRedirector.Redirect(e);
            };

            messageListener.Start();

            Console.WriteLine("Listener is active");
            Console.ReadKey();
        }
    }
}
