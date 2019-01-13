using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using EventSourcingSample.UserCommandService.Commands;
using EventSourcingSample.UserCommandService.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventSourcingSample.UserCommandService.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, CommandResult>
    {
        private readonly IDictionary<string, object> producerConfig;
        private const string kafkaTopic = "testtopic"; // TODO: set meaningful topic name

        public CreateUserHandler(IConfiguration configuration) {
            var kafkaEndpoint = configuration.GetValue<string>("KafkaEndpoint");
            producerConfig = new Dictionary<string, object> { { "bootstrap.servers", kafkaEndpoint } };
        }

        public async Task<CommandResult> Handle(CreateUserCommand request, CancellationToken cancellationToken) {
            using (var producer = new Producer<Null, byte[]>(producerConfig, null, new ByteArraySerializer())) {
                var bytes = GetBytes(request);
                var result = await producer.ProduceAsync(kafkaTopic, null, bytes);
            }

            return new CommandResult(request.Id);
        }

        private byte[] GetBytes(object value) {
            var message = JsonConvert.SerializeObject(value);
            return Encoding.UTF8.GetBytes(message);
        }
    }
}
