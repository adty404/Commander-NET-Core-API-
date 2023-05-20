using System.Collections;
using System.Collections.Generic;
using Commander.Data;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    // ControllerBase is a base class for MVC controller without view support
    // Controller is a base class for MVC controller with view support

    [Route("api/commands")] // Route attribute is used to specify the route template
    [ApiController] // ApiController attribute is used to indicate that a controller responds to web API requests
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;

        public CommandsController(ICommanderRepo repository)
        {
            _repository = repository;
        }

        // private readonly MockCommanderRepo _repository = new MockCommanderRepo();

        [HttpGet] // GET api/commands
        public ActionResult<IEnumerable<Command>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();

            return Ok(commandItems);
        }

        [HttpGet("{id}")] // GET api/commands/5
        public ActionResult <Command> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);

            return Ok(commandItem);
        }
    }
}
