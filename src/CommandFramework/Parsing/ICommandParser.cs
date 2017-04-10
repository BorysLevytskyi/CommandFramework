using System.Collections.Generic;

namespace CommandFramework.Parsing
{
	public interface ICommandParser
	{
		ParsedCommand Parse(string input);

		ParsedCommand Parse(IList<string> input);
	}
}
