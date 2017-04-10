using System;
using System.Collections.Generic;
using System.Diagnostics;
using CommandFramework.Commands;
using CommandFramework.Dispatcher;
using CommandFramework.Commands.Input;
using CommandFramework.Parsing;
using CommandFramework.Utils;

namespace CommandFramework
{
	public interface ICommandDispatcher
	{
		CommandsCatalog CommandsCatalog { get; }
		void StartDispatchingFromUserInput();
		void DispatchCommand(IList<string> args);
		void DispatchCommand(string input);
		void DispatchCommand(ICommandInput commandInput);
		void Stop();
		void UseInputCommandParser(ICommandParser parser);
	    void UseCommandContextFactory(ICommandContextFactory factory);
	}

	public class CommandDispatcher : ICommandDispatcher
	{
	    private ICommandParser _commandParser;

	    private ICommandContextFactory _commandContextFactory;

		private bool _isRunning;

		private bool _enableTrace;

		public CommandDispatcher(CommandsCatalog commandsCatalog)
		{
			CommandsCatalog = commandsCatalog ?? new CommandsCatalog();
			
			Initialize();

			DefaultCommand = CommandsCatalog.GetDefaultCommand();
		}

        /// <summary>
        /// Gets or sets a default command that will be execute when no other command has matched input
        /// </summary>
		public ICommand DefaultCommand { get; set; }

        /// <summary>
        /// Enables trace console output for command dispatcher
        /// </summary>
		public bool EnableTrace
		{
			get { return _enableTrace; }
			set
			{
				if (_enableTrace == value)
				{
					return;
				}

				 _enableTrace = value;

				if (_enableTrace)
				{
					Tracer.Start();
				}
				else
				{
					Tracer.Stop();
				}
			}
		}

		public CommandsCatalog CommandsCatalog { get; }

	    /// <summary>
        /// Gets or sets whether dispacther is in Debug mode. 
        /// In debug mode all exception thrown by commands are not cought by dispatcher
        /// </summary>
	    public bool DebugMode { get; set; }

		public Action<Exception> CommandErrorHandler { get; set; }

		private void Initialize()
		{
			UseInputCommandParser(new CommandLineCommandParser());
            UseCommandContextFactory(new SimpleCommandContextFactory());
			CommandErrorHandler = DefaultErrorHandler;
		}

		public void StartDispatchingFromUserInput()
		{
			_isRunning = true;

			while (_isRunning)
			{
				Console.WriteLine();
				Console.Write("> ");
				DispatchCommand(Console.ReadLine());
			}
		}

		public void DispatchCommand(IList<string> args)
		{
			if (args == null || args.Count == 0)
			{
				Console.WriteLine("Empty command");
				return;
			}

			DispatchCommand(_commandParser.Parse(args));
		}

		public void DispatchCommand(string input)
		{
			if (string.IsNullOrEmpty(input))
			{
				Console.WriteLine("Empty command");
				return;
			}

			DispatchCommand(_commandParser.Parse(input));
		}

		public void DispatchCommand(ICommandInput commandInput)
		{
			ICommand cmd;
			
			if ((cmd = CommandsCatalog.FindByName(commandInput.CommandName)) == null && DefaultCommand == null)
			{
				Console.WriteLine("'{0}' command not found", commandInput.CommandName);
				return;
			}

			if (cmd == null)
			{
				cmd = DefaultCommand;
				commandInput = commandInput.ToDefaultCommand(DefaultCommand.Name);
			}

			if (DebugMode || Debugger.IsAttached)
			{
				DispatchCommandWithoutErrorHandling(commandInput, cmd);
			}
			else
			{
				DispatchCommandWithErrorHandling(commandInput, cmd);
			}
		}

		public void Stop()
		{
			_isRunning = false;
		}

		public void UseInputCommandParser(ICommandParser parser)
		{
			_commandParser = parser;
		}

	    public void UseCommandContextFactory(ICommandContextFactory factory)
	    {
	        _commandContextFactory = factory;
	    }

		private void DispatchCommandWithoutErrorHandling(ICommandInput commandInput, ICommand cmd)
		{
			if (EnableTrace)
			{
				Trace.Indent();
				Tracer.WriteCommandExecution(commandInput);
			}

		    using (var context = _commandContextFactory.CreateContext(commandInput))
		    {
                cmd.Execute(context);
            }

			if (EnableTrace)
			{
				Trace.Unindent();
			}
		}

		private void DispatchCommandWithErrorHandling(ICommandInput commandInput, ICommand cmd)
		{
			try
			{
				DispatchCommandWithoutErrorHandling(commandInput, cmd);
			}
			catch (Exception ex)
			{
				CommandErrorHandler(ex);
			}
		}

		private void DefaultErrorHandler(Exception ex)
		{
			ConsoleEx.Write(ConsoleColor.Red, "Exception Occurred:");
			ConsoleEx.WriteLine(ConsoleColor.Red, ex.Message);
			Console.WriteLine();
		}
	}
}