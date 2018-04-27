using System;
using System.Threading.Tasks;

namespace MosquittoClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Mosquitto client started");

            try
            {
                Worker worker = new Worker();
                await worker.StartAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("Listener is active");
            Console.ReadKey();
        }
    }
}
