using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandFramework.Parsing
{
	/// <summary>
	/// Parses command with arguments separated by spaces.
	/// </summary>
	public class CommandLineCommandParser : ICommandParser
	{
		public ParsedCommand Parse(string input)
		{
		    string[] parts = CommandLineParser.SplitCommandLine(input);
			return Parse(parts);
		}

		public ParsedCommand Parse(IList<string> parts)
		{
			if (parts.Count == 0)
			{
				throw new ParseException("Arguments expected");
			}

			string cmdName = parts.First();
			int counter = 0;
			var args = new List<ParsedParameter>(parts.Count - 1);

			string argName = null;
			foreach (var part in parts.Skip(1))
			{
				if (part.StartsWith("-"))
				{
					if (argName != null)
					{
						args.Add(new ParsedParameter(counter++, argName, null));
					}

					argName = part.TrimStart('-');
					continue;
				}

				args.Add(new ParsedParameter(counter++, argName, part));
				argName = null;
			}

			if (argName != null)
			{
				args.Add(new ParsedParameter(counter++, argName, null));
			}

			return new ParsedCommand(cmdName, args);
		}

		public static IEnumerable<string> SplitArguments(string input)
		{
			var sb = new StringBuilder();
			bool inQuotes = false;

			foreach (char ch in input)
			{
				if (ch == '"')
				{
					inQuotes = !inQuotes;
					continue;
				}

				if (ch == ' ' && !inQuotes)
				{
					yield return sb.ToString();
					sb.Clear();
					continue;
				}

				sb.Append(ch);
			}

			if (sb.Length > 0)
			{
				yield return sb.ToString();
			}
		}

		
	}
}