using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Profiles
{
    // Profile is a class that inherits from AutoMapper.Profile
    // Profile is used to define the mapping between the source and target
    // Source is the model class, Target is the DTO class
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // Source -> Target
            CreateMap<Command, CommandReadDto>(); // This is converting from Command to CommandReadDto for purposes of reading data (GET)
            CreateMap<CommandCreateDto, Command>(); // This is converting from CommandCreateDto to Command for purposes of creating data (POST)
            CreateMap<CommandUpdateDto, Command>(); // This is converting from CommandUpdateDto to Command for purposes of updating data (PUT)
            CreateMap<Command, CommandUpdateDto>(); // This is converting from Command to CommandUpdateDto for purposes of updating data (PATCH)
        }
    }
}
