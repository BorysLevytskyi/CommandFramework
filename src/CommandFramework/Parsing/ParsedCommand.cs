using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommandFramework.Commands;
using CommandFramework.Commands.Input;

namespace CommandFramework.Parsing
{
	public class ParsedCommand : ICommandInput
	{
		public ParsedCommand(string name, IEnumerable<ParsedParameter> arguments)
		{
			Name = name;
			Parameters = new ReadOnlyCollection<ParsedParameter>(arguments.ToList());
		}

		public string Name { get; private set; }

		IReadOnlyCollection<IParameterInput> ICommandInput.ParameterInputs
		{
			get { return Parameters; }
		}

		public IReadOnlyCollection<ParsedParameter> Parameters { get; private set; }
	}
}