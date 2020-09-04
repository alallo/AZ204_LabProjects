using Azure;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System;
using System.Threading.Tasks;

namespace MessageProcessor
{
  	public class Program
    {
        private const string storageConnectionString = "myConnectionString";
        private const string queueName = "messagequeue";

        public static async Task Main(string[] args)
        {
             	QueueClient client = new QueueClient(storageConnectionString, queueName);        
                await client.CreateAsync();
                Console.WriteLine($"---Account Metadata---");
                Console.WriteLine($"Account Uri:\t{client.Uri}");

                Console.WriteLine($"---New Messages---");
                string greeting = "Hi, Developer!";
                await client.SendMessageAsync(greeting);
                Console.WriteLine($"Sent Message:\t{greeting}");

                Console.WriteLine($"---Existing Messages---");
                var batchSize = 10;
                TimeSpan visibilityTimeout = TimeSpan.FromSeconds(2.5d);

                Response<QueueMessage[]> messages = await client.ReceiveMessagesAsync(batchSize, visibilityTimeout);
                foreach(QueueMessage message in messages?.Value)
                {
                    Console.WriteLine($"[{message.MessageId}]\t{message.MessageText}");
                }

                foreach(QueueMessage message in messages?.Value)
                {
                    Console.WriteLine($"Deleting [{message.MessageId}]\t{message.MessageText}");
                    await client.DeleteMessageAsync(message.MessageId, message.PopReceipt);
                }


        }
    }

}
