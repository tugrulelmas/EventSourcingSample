using System;

namespace EventSourcingSample.UserCommandService.Entities
{
    public class CommandResult
    {
        public CommandResult(Guid commandId) {
            CommandId = commandId;
        }

        public Guid CommandId { get;  }
    }
}
