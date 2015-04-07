using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CommandFramework.Commands.Class;
using CommandFramework.Commands.Method;
using CommandFramework.Parsing;
using CommandFramework.Utils;

namespace CommandFramework.Commands
{
	public class CommandsCatalog : IReadOnlyCollection<ICommand>
	{
		private readonly List<ICommand> _commands;

		public CommandsCatalog()
		{
			_commands = new List<ICommand>();
		}

		public void AddCommand<T>(string name, Action<T> action)
		{
			AddCommand(MethodCommandFactory.Create(action.Method, name: name));
		}

		public void AddCommand<TClassCommand>(string name, Func<TClassCommand> factory = null) where TClassCommand : ICommandInstance
		{
			AddCommand(ClassCommandFactory.Create(factory, name));
		}

		public void AddCommand<TInstanceCmd>(Func<TInstanceCmd> factory = null) where TInstanceCmd : ICommandInstance
		{
			AddCommand(ClassCommandFactory.Create(factory));
		}

		public void AddCommand(string name, Action action)
		{
			AddCommand(MethodCommandFactory.Create(action.Method, name: name));
		}

		public void AddCommand(ICommand command)
		{
			_commands.Add(command);
		}

		public ICommand FindCommand(string name)
		{
			var overloads = this.Where(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
			return overloads.FirstOrDefault(); // TODO: Allow only 1 cmd with the same name
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

		public int Count
		{
			get { return _commands.Count; }
		}

		public ICommand GetDefaultCommand()
		{
			return _commands.FirstOrDefault(c => c.IsDefault);
		}
	}
}