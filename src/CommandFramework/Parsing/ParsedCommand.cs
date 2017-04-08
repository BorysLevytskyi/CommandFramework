using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using CommandFramework.Commands.Input;

namespace CommandFramework.Parsing
{
	[DebuggerDisplay("{Name}")]
	public class ParsedCommand : ICommandInput
	{
		public ParsedCommand(string name, IEnumerable<ParsedParameter> arguments)
		{
			Name = name;
			Parameters = new ReadOnlyCollection<ParsedParameter>(arguments.ToList());
		}

		public string Name { get; private set; }

		IReadOnlyCollection<IParameterInput> ICommandInput.ParameterInputs => Parameters;

	    public IReadOnlyCollection<ParsedParameter> Parameters { get; private set; }
	}
}