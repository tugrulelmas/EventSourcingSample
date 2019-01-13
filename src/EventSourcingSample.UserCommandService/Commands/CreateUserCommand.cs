using EventSourcingSample.UserCommandService.Entities;
using MediatR;
using System;

namespace EventSourcingSample.UserCommandService.Commands
{
    public class CreateUserCommand : IRequest<CommandResult>
    {
        public CreateUserCommand(string userName) {
            Id = Guid.NewGuid();
            UserName = userName;
        }

        public Guid Id { get; }

        public string UserName { get; }
    }
}
