using ExampleService;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading;

namespace Pitstop.ExampleService
{
    class Program
    {
        private static string _env;
        public static IConfigurationRoot Config { get; private set; }

        static Program()
        {
            _env = Environment.GetEnvironmentVariable("PITSTOP_ENVIRONMENT") ?? "Production";

            Console.WriteLine($"Environment: {_env}");

            Config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{_env}.json", optional: false)
                .Build();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // get configuration
            var configSection = Config.GetSection("RabbitMQ");
            string host = configSection["Host"];
            string userName = configSection["UserName"];
            string password = configSection["Password"];

            // start example manager
            RabbitMQMessagePublisher messagePublisher = new RabbitMQMessagePublisher(host, userName, password, "Pitstop");
            RabbitMQMessageHandler messageHandler = new RabbitMQMessageHandler(host, userName, password, "Pitstop", "Example", "");
            ExampleManager manager = new ExampleManager(messagePublisher, messageHandler);
            manager.Start();

            if (_env == "Development")
            {
                Console.WriteLine("Example service started. Press any key to stop...");
                Console.ReadKey(true);
                //manager.Stop();
            }
            else
            {
                Console.WriteLine("Example service started.");
                while (true)
                {
                    Thread.Sleep(10000);
                }
            }
        }
    }
}
