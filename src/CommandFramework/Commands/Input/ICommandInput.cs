using System.Collections.Generic;
using System.Linq;
using CommandFramework.Parsing;

namespace CommandFramework.Commands.Input
{
	public interface ICommandInput
	{
		string Name { get; }

		IReadOnlyCollection<IParameterInput> ParameterInputs { get; }
	}

	public static class InputExtensions
	{
		public static ICommandInput ToDefaultCommand(this ICommandInput cmdInput, string name)
		{
			var parameters = new List<ParsedParameter>(cmdInput.ParameterInputs.Count + 1)
			{
				new ParsedParameter(0, null, cmdInput.Name)
			};

			parameters.AddRange(cmdInput.ParameterInputs.Select(p => new ParsedParameter(p.PositionIndex + 1, p.Name, p.Value)));
			return new ParsedCommand(name, parameters);
		}
	}
}