using System;
using System.Threading;

using Confluent.Kafka;

namespace Kafka.Producer;

public class Program
{
    public static void Main(string[] args)
    {
        var t1 = new Thread(() =>
                            {
                                Thread.Sleep(5000);
                                string topic = "plantHealth";
                                var producerConfig = new ProducerConfig
                                                     {
                                                         BootstrapServers = "localhost:9092",
                                                         Acks = Acks.All,
                                                         QueueBufferingMaxMessages = 100_000
                                                     };

                                using var producer = new ProducerBuilder<Null, string>(producerConfig).Build();
                                while(true)
                                    producer.Produce(topic, new Message<Null, string> { Value= "test" });
                            });
          
            
        Console.WriteLine("Hello World!");


        var t2 = new Thread(() =>
                            {
                                string topic = "plantHealth";

                                var consumerConfig = new ConsumerConfig
                                                     {
                                                         BootstrapServers = "localhost:9092",
                                                         GroupId = "someId",
                                                         AutoOffsetReset = AutoOffsetReset.Earliest
                                                     };
                                using (var consumer = new ConsumerBuilder<Null, string>(consumerConfig).Build())
                                {
                                    consumer.Subscribe(topic);

                                    while (true)
                                    {
                                        var consumeResult = consumer.Consume();

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