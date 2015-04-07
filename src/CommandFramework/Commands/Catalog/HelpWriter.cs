using System;
using System.Collections.Generic;
using System.Linq;
using CommandFramework.Utils;

namespace CommandFramework.Commands.Catalog
{
	internal static class HelpWriter
	{
		internal static void WriteCommands(IEnumerable<ICommand> commands)
		{
			foreach (var g in commands.OrderBy(c => c.Group).GroupBy(c => c.Group))
			{
				Console.Write(" ");
				ConsoleEx.Write(ConsoleColor.Cyan, g.Key);
				Console.WriteLine(" commands");
				WriteCommandsList(g, 2);
			}
		}

		internal static void WriteCommand(ICommand command)
		{
			ConsoleEx.Write(ConsoleColor.Yellow, command.Name + " ");
			Console.WriteLine(command.Description);

			foreach (var param in command.GetParameters())
			{
				ConsoleEx.Write(ConsoleColor.Green, "  {0}", param.Name);

				if (param.PositionIndex == 0)
				{
					ConsoleEx.Write(ConsoleColor.Magenta, " [default]");
				}

				if (param.Synonyms.Any())
				{
					Console.Write(" (Synonyms: ");
					ConsoleEx.Write(ConsoleColor.DarkGreen, string.Join(", ", param.Synonyms));
					Console.Write(")");
				}

				if (!string.IsNullOrEmpty(param.Description))
				{
					Console.WriteLine(" - {0}", param.Description);
				}

				Console.WriteLine();
			}
		}

		private static void WriteCommandsList(IEnumerable<ICommand> commands, int padLevel = 0)
		{
			foreach (ICommand cmd in commands.OrderBy(c => c.Name).ThenBy(c => c.GetParameters().Count()))
			{
				var pad = new string(' ', padLevel);
				ConsoleEx.Write(ConsoleColor.Yellow, pad + cmd.Name);
				
				if (cmd.GetParameters().Count > 0)
				{
					ConsoleEx.Write(ConsoleColor.DarkYellow, String.Format(" ({0})", String.Join(", ", cmd.GetParameters().Select(a => a.Name))));
				}

				Console.WriteLine(pad + " - {0}", cmd.Description);
			}
		}
	}
}