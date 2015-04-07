using System;
using CommandFramework.Commands.Annotation;

namespace CommandFramework.Commands.Catalog
{
	[CommandGroup("general")]
	public class ClearConsoleCommand
	{
		[Command("Clears console")]
		public void Clear()
		{
			Console.Clear();
		}
	}
}