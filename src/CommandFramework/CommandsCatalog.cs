using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandFramework.Catalog;
using CommandFramework.Commands;
using CommandFramework.Commands.Class;
using CommandFramework.Commands.Method;
using CommandFramework.Container;
using CommandFramework.Dispatcher;
using CommandFramework.Utils;

namespace CommandFramework
{
	public class CommandsCatalog : IReadOnlyCollection<ICommand>
	{
		private readonly List<ICommand> _commands;

		public CommandsCatalog()
		{
			_commands = new List<ICommand>();
		}

		public ICommand AddCommand<T>(Action<T> action)
		{
			return AddCommand(MethodCommandFactory.Create(action.Method, action.Target));
		}

		public ICommand AddCommand<T1, T2>(Action<T1, T2> action)
		{
			return AddCommand(MethodCommandFactory.Create(action.Method, action.Target));
		}

		public ICommand AddCommand<T1, T2, T3>(Action<T1, T2, T3> action)
		{
			return AddCommand(MethodCommandFactory.Create(action.Method, action.Target));
		}

		public ICommand AddCommand<TInstanceCmd>(Func<ICommandContext, TInstanceCmd> factory = null) where TInstanceCmd : ICommandInstance
		{
			return AddCommand(ClassCommandFactory.Create(factory));
		}

		public ICommand AddCommand(Action action)
		{
			return AddCommand(MethodCommandFactory.Create(action.Method, action.Target));
		}

		public ICommand AddCommand(ICommand command)
		{
			if (FindByName(command.Name) != null)
			{
				throw new Exception($"Commands with name '{command}' is already added");
			}

			_commands.Add(command);

			return command;
		}

		public ICommand AddHelpCommand()
		{
			var helper = new HelpCommand(this);
			return AddCommand<string, string>(helper.DisplayHelp);
		}

		public ICommand AddExitCommand()
		{
			return AddCommand(() => Environment.Exit(0)).WithName("exit").WithGroup("general");
		}

		public ICommand FindByName(string name)
		{
			return this.SingleOrDefault(c => c.Name.EqualsIgnoreCase(name));
		}

		public IEnumerator<ICommand> GetEnumerator()
		{
			return _commands.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public int Count => _commands.Count;

	    public void AddCommandsFrom(object inst)
		{
			_commands.AddRange(TypeCommandScanner.FindMethodCommandsOnInstance(inst));
		}

		public void AddCommandsFrom<T>() where T : class
		{
			AddCommandsFrom(typeof(T));
		}

		public void AddCommandsFrom(Type type)
		{
			_commands.AddRange(TypeCommandScanner.FindStaticMethodCommands(type));
		}

		public void AddCommandsFrom(params Assembly[] assemblies)
		{
		    foreach (var assembly in assemblies)
		    {
                _commands.AddRange(TypeCommandScanner.FindStaticMethodCommands(assembly));
                _commands.AddRange(TypeCommandScanner.FindClassCommands(assembly));
            }
        }

		public ICommand GetDefaultCommand()
		{
			return _commands.FirstOrDefault(c => c.IsDefault);
		}
	}
}