using System.Collections.Generic;
using System.Linq;
using CommandFramework.Parsing;

namespace CommandFramework.Commands.Input
{
	public interface ICommandInput
	{
		string CommandName { get; }

		IReadOnlyCollection<IParameterInput> InputParameters { get; }
	}

	public static class CommandInputExtensions
	{
		public static ICommandInput ToDefaultCommand(this ICommandInput cmdInput, string name)
		{
			var parameters = new List<ParsedParameter>(cmdInput.InputParameters.Count + 1)
			{
				new ParsedParameter(0, null, cmdInput.CommandName)
			};

			parameters.AddRange(cmdInput.InputParameters.Select(p => new ParsedParameter(p.PositionIndex + 1, p.Name, p.Value)));
			return new ParsedCommand(name, parameters);
		}
	}
}