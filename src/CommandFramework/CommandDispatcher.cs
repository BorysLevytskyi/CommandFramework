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
		void SetUserInputCommandParser(ICommandParser parser);
	}

	public class CommandDispatcher : ICommandDispatcher
	{
		private readonly CommandsCatalog _commandsCatalog;
		private ICommandParser _commandParser;

		private bool _isRunning;

		private bool _enableTrace;

		public CommandDispatcher(CommandsCatalog commandsCatalog)
		{
			_commandsCatalog = commandsCatalog ?? new CommandsCatalog();
			
			Initialize();

			DefaultCommand = _commandsCatalog.GetDefaultCommand();
		}

		public ICommand DefaultCommand { get; set; }

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

		public CommandsCatalog CommandsCatalog
		{
			get { return _commandsCatalog; }
		}

		public bool DebugMode { get; set; }

		public Action<Exception> CommandErrorHandler { get; set; }

		private void Initialize()
		{
			SetUserInputCommandParser(new CommandLineCommandParser());
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
			
			if ((cmd = _commandsCatalog.FindByName(commandInput.Name)) == null && DefaultCommand == null)
			{
				Console.WriteLine("'{0}' command not found", commandInput.Name);
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

		public void SetUserInputCommandParser(ICommandParser parser)
		{
			_commandParser = parser;
		}

		private void DispatchCommandWithoutErrorHandling(ICommandInput commandInput, ICommand cmd)
		{
			if (EnableTrace)
			{
				Trace.Indent();
				Tracer.WriteCommandExecution(commandInput);
			}

			cmd.Execute(commandInput.ParameterInputs);

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