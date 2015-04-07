using System;
using System.Text.RegularExpressions;
using CommandFramework.Commands.Annotation;

namespace CommandFramework.Dispatcher
{
	[CommandGroup("Standard")]
	public static class StandardCommands
	{
		[Command(Description = "Exits program")]
		public static void Exit()
		{
			Environment.Exit(0);
		}

		[Command(Description = "Clears console")]
		public static void Clear()
		{
			Console.Clear();
		}
	}
}