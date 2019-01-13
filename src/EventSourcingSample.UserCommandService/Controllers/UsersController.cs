using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventSourcingSample.UserCommandService.Commands;
using EventSourcingSample.UserCommandService.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventSourcingSample.UserCommandService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator) {
            this.mediator = mediator;
        }
        
        [HttpPost("{userName}")]
        public async Task<ActionResult<CommandResult>> Post(string userName) {
            var result = await mediator.Send(new CreateUserCommand(userName));
            return result;
        }
    }
}
