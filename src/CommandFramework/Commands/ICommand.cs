using System.Collections.Generic;
using CommandFramework.Commands.Input;

namespace CommandFramework.Commands
{
	public interface ICommand
	{
		string Group { get; set; }

        string Description { get; set; }

		string Name { get; set; }

		bool IsDefault { get; set; }

		void Execute(ICommandContext context);

		IReadOnlyCollection<IParameter> GetParameters();
	}
}