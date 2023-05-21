using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
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
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet] // GET api/commands
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        [HttpGet("{id}", Name = "GetCommandById")] // GET api/commands/5
        public ActionResult <CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);

            if (commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }

            return NotFound();
        }

        [HttpPost] // POST api/commands
        // public is the access modifier, ActionResult is the return type, <CommandReadDto> is the generic type (the type of the object returned), CreateCommand is the method name,
        // and CommandCreateDto is the parameter type
        public ActionResult <CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto); // Map the commandCreateDto to a Command object

            _repository.CreateCommand(commandModel); // Create the commandModel

            _repository.SaveChanges(); // Save the changes

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel); // Map the commandModel to a CommandReadDto object

            // Parameters: routeName, routeValues, value
            return CreatedAtRoute(nameof(GetCommandById), new {Id = commandReadDto.Id}, commandReadDto); // Return the commandReadDto
        }

        [HttpPut("{id}")] // PUT api/commands/5
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromRepo = _repository.GetCommandById(id); // Get the commandModel from the repository

            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(commandUpdateDto, commandModelFromRepo); // Map the commandUpdateDto to the commandModelFromRepo, updating the commandModelFromRepo with the commandUpdateDto values

            _repository.UpdateCommand(commandModelFromRepo); // Update the commandModelFromRepo

            _repository.SaveChanges(); // Save the changes

            return NoContent();
        }

        [HttpPatch("{id}")] // PATCH api/commands/5
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFromRepo = _repository.GetCommandById(id); // Get the commandModel from the repository

            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo); // Map the commandModelFromRepo to a CommandUpdateDto object

            patchDoc.ApplyTo(commandToPatch, ModelState); // Apply the patchDoc to the commandToPatch, updating the commandToPatch with the patchDoc values and adding any errors to the ModelState

            // Validate the commandToPatch and add any errors to the ModelState
            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, commandModelFromRepo); // Map the commandToPatch to the commandModelFromRepo, updating the commandModelFromRepo with the commandToPatch values

            _repository.UpdateCommand(commandModelFromRepo); // Update the commandModelFromRepo

            _repository.SaveChanges(); // Save the changes

            return NoContent();
        }
    }
}
