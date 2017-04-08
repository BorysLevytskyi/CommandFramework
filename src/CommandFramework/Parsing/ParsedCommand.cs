using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using CommandFramework.Commands.Input;

namespace CommandFramework.Parsing
{
	[DebuggerDisplay("{CommandName}")]
	public class ParsedCommand : ICommandInput
	{
		public ParsedCommand(string name, IEnumerable<ParsedParameter> arguments)
		{
			CommandName = name;
			Parameters = new ReadOnlyCollection<ParsedParameter>(arguments.ToList());
		}

		public string CommandName { get; }

		IReadOnlyCollection<IParameterInput> ICommandInput.InputParameters => Parameters;

	    public IReadOnlyCollection<ParsedParameter> Parameters { get; }
	}
}