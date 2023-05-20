using Commander.Models;
using Microsoft.EntityFrameworkCore;

namespace Commander.Data
{
    public class CommanderContext : DbContext
    {
        public CommanderContext(DbContextOptions<CommanderContext> opt) : base(opt)
        {
            
        }

        // DbSet represents a collection of entities from the database
        // DbSet is a property of DbContext, Command is the model class, get and set are accessors
        public DbSet<Command> Commands { get; set; }
    }
}
