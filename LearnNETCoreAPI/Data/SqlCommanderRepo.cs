using System.Collections.Generic;
using System.Linq;
using Commander.Models;

namespace Commander.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private readonly CommanderContext _context;

        public SqlCommanderRepo(CommanderContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new System.ArgumentNullException(nameof(cmd)); // ArgumentNullException is thrown when a null argument is passed to a method that doesn't accept it as a valid argument
            }

            _context.Commands.Add(cmd); // Add() adds the specified object as a new entity to the context
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return _context.Commands.ToList(); // ToList() converts the IEnumerable to a list
        }

        public Command GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(p => p.Id == id); // FirstOrDefault returns only one item based on given id or default value
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0); // SaveChanges() saves all changes made in this context to the database
        }
    }
}
