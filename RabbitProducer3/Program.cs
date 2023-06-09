﻿using System;
using System.Text;
using RabbitMQ.Client;

namespace RabbitProducer3
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
             
            channel.QueueDeclare(queue: "hello",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var message = GetMessage(args);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty,
                routingKey: "hello",
                basicProperties: null,
                body: body);
            Console.WriteLine($" [x] Sent {message}");

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
            
            static string GetMessage(string[] args)
            { 
                return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
            }
        }
        
    }
}