using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using EventSourcingSample.UserCommandConsumer.Abstractions;
using EventSourcingSample.UserCommandConsumer.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventSourcingSample.UserCommandConsumer.Implementations
{
    class UserCreateCommandConsumer : IConsumer
    {
        private readonly IDictionary<string, object> consumerConfig;
        private const string kafkaTopic = "testtopic"; // TODO: set meaningful topic name

        public UserCreateCommandConsumer(IConfiguration configuration) {
            var kafkaEndpoint = configuration.GetValue<string>("KafkaEndpoint");
            consumerConfig = new Dictionary<string, object>
            {
                { "group.id", "myconsumer" },
                { "bootstrap.servers", kafkaEndpoint },
            };
        }

        public void Consume() {
            using (var consumer = new Consumer<Null, byte[]>(consumerConfig, null, new ByteArrayDeserializer())) {
                // Subscribe to the OnMessage event
                consumer.OnMessage += (obj, msg) => {
                    var message = Encoding.UTF8.GetString(msg.Value);
                    var command = JsonConvert.DeserializeObject<CreateUserCommand>(message);
                    Console.WriteLine($"Received: Id = {command.Id}, UserName: {command.UserName}");
                };

                consumer.Subscribe(new List<string>() { kafkaTopic });

                while (true) {
                    consumer.Poll(TimeSpan.FromMilliseconds(100));
                }
            }
        }
    }
}
