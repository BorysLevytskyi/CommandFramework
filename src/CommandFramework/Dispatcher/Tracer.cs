using System;
using System.Diagnostics;
using CommandFramework.Commands;
using CommandFramework.Commands.Input;

namespace CommandFramework.Dispatcher
{
	internal class Tracer
	{
	    // private static ConsoleTraceListener _listener;

	    public static void WriteCommandExecution(ICommandInput cmd)
		{
			Console.WriteLine("Executing command: {0}", cmd.CommandName);

			foreach (var arg in cmd.InputParameters)
			{
				Tracer.Indent();
				Tracer.WriteLine($"#{arg.PositionIndex} {arg.Name ?? "(anonymous)"}: {arg.Value}");
				Tracer.Unindent();
			}
		}

		public static void Start()
		{
			//Trace.Listeners.Add(new DefaultTraceListener());
		}

	    public static void Stop()
	    {
	        //Debug.Listeners.Remove(_listener);
	        //_listener = null;
	    }

	    public static void Indent()
	    {
	    }

	    public static void Unindent()
	    {
	    }

	    public static void WriteLine(string line)
	    {
	    }
	}
}