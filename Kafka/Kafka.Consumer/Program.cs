using System;
using System.Threading;

using Confluent.Kafka;

namespace Kafka.Consumer;

public class Program
{
    public static void Main(string[] args)
    {
        Thread t1 = new Thread(() =>
                               {
                                   Thread.Sleep(5000);
                                   string topic = "plantHealth";
                                   ProducerConfig producerConfig = new ProducerConfig
                                                                   {
                                                                       BootstrapServers = "localhost:9092",
                                                                       Acks = Acks.All,
                                                                       QueueBufferingMaxMessages = 100_000
                                                                   };

                                   using IProducer<Null, string>? producer = new ProducerBuilder<Null, string>(producerConfig).Build();
                                   while (true)
                                       producer.Produce(topic, new Message<Null, string> { Value = "test" });
                               });

        Console.WriteLine("Hello World!");

        Thread t2 = new Thread(() =>
                               {
                                   string topic = "plantHealth";

                                   ConsumerConfig consumerConfig = new ConsumerConfig
                                                                   {
                                                                       BootstrapServers = "localhost:9092",
                                                                       GroupId = "someId",
                                                                       AutoOffsetReset = AutoOffsetReset.Earliest
                                                                   };
                                   using (IConsumer<Null, string>? consumer = new ConsumerBuilder<Null, string>(consumerConfig).Build())
                                   {
                                       consumer.Subscribe(topic);

                                       while (true)
                                       {
                                           ConsumeResult<Null, string>? consumeResult = consumer.Consume();

                                           // handle consumed message.
                                           int i = 0;
                                           i = i + 5;
                                       }

                                       consumer.Close();
                                   }
                               });

        t1.Start();
        t2.Start();

        t2.Join();
        t1.Join();
    }
}