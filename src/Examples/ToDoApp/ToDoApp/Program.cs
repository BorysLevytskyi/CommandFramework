using CommandFramework;
using CommandFramework.Catalog;

namespace ToDoApp
{
	public class Program
	{
		private static void Main(string[] args)
		{
			var manager = new TaskManager();
			var catalog = new CommandsCatalog();
			catalog.AddCommandsFrom(manager);

			catalog.AddHelpCommand().WithGroup("todo");
			catalog.AddExitCommand().WithGroup("todo");

			var dispatcher = new CommandDispatcher(catalog);
			catalog.AddCommandsFrom(new Debugging(dispatcher));

			dispatcher.DispatchCommand("\"check example app\" -c -t CommandFramework -t Example");

			dispatcher.StartDispatchingFromUserInput();
		}
	}
}