using System;
using System.Diagnostics;
using System.IO;
using CommandFramework.Annotation;

namespace CommandFramework.Catalog
{
	[CommandGroup("debug")]
	public class DebugCommands
	{
		private readonly CommandDispatcher _dispatcher;

		public DebugCommands(CommandDispatcher dispatcher)
		{
			_dispatcher = dispatcher;
		}


		[Command("Attaches debugger")]
		public void Debug()
		{
			_dispatcher.DebugMode = true;

			Debugger.Launch();
		}

		[Command("Enables tracing of command execution")]
		public void Trace()
		{
			_dispatcher.EnableTrace = true;
			Console.WriteLine("Trace enabled");
		}
	}
}