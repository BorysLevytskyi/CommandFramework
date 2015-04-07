using System;
using System.Linq;
using CommandFramework.Annotation;
using CommandFramework.Utils;

namespace CommandFramework.Catalog
{
	public class Helper
	{
		private readonly CommandsCatalog _catalog;

		[Command(Name = "help")]
		public void DisplayHelp(
			[Parameter(Description = "Show help for a specific command")] string cmd = null,
			[Parameter(Description = "Show help for a specific group")] string group = null)
		{
			if (cmd != null)
			{
				var command = _catalog.FindByName(cmd);
				if (command == null)
				{
					ConsoleEx.Write(ConsoleColor.Red, "Command not found: {0}", cmd);
					return;
				}

				HelpWriter.WriteCommand(command);
				return;
			}

			HelpWriter.WriteCommands(
				_catalog.Where(c => @group == null || c.Group.Equals(@group, StringComparison.OrdinalIgnoreCase)));
		}

		public Helper(CommandsCatalog catalog)
		{
			_catalog = catalog;
		}
	}
}