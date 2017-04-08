using System;
using System.Diagnostics;
using CommandFramework.Commands;
using CommandFramework.Commands.Input;

namespace CommandFramework.Dispatcher
{
	internal class Tracer
	{
	    private static ConsoleTraceListener _listener;

	    public static void WriteCommandExecution(ICommandInput cmd)
		{
			Console.WriteLine("Executing command: {0}", cmd.Name);

			foreach (var arg in cmd.ParameterInputs)
			{
				Trace.Indent();
				Trace.WriteLine($"#{arg.PositionIndex} {arg.Name ?? "(anonymous)"}: {arg.Value}");
				Trace.Unindent();
			}
		}

		public static void Start()
		{
			Debug.Listeners.Add(_listener = new ConsoleTraceListener());
		}

	    public static void Stop()
	    {
	        Debug.Listeners.Remove(_listener);
	        _listener = null;
	    }
	}
}