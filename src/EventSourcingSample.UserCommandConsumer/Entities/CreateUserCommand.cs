using System;

namespace EventSourcingSample.UserCommandConsumer.Entities
{
    class CreateUserCommand
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }
    }
}
