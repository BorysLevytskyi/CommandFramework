using System;
using System.Diagnostics;
using System.IO;

using CommandFramework.Commands.Annotation;

namespace CommandFramework.Commands.Catalog
{
	[CommandGroup("debug")]
	public class Debugging
	{
		private readonly CommandDispatcher _dispatcher;

		public Debugging(CommandDispatcher dispatcher)
		{
			_dispatcher = dispatcher;
		}

		[Command(Description = "Exits application and launches build command")]
		public void Build()
		{
			string buildFilePath = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\build.bat");
			Process.Start(buildFilePath);
			Environment.Exit(0);
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