using System.Linq;
using CommandFramework.Utils;

namespace CommandFramework.Parsing
{
	internal static class CommandLineParser
	{
		public static string[] SplitCommandLine(string commandLine)
		{
			bool inQuotes = false;

			return commandLine.Split(c =>
			{
				if (c == '\"')
					inQuotes = !inQuotes;

				return !inQuotes && c == ' ';
			}).Select(arg => arg.Trim().TrimMatchingQuotes('\"'))
				.Where(arg => !string.IsNullOrEmpty(arg)).ToArray();
		}

		public static string TrimMatchingQuotes(this string input, char quote)
		{
			if ((input.Length >= 2) &&
				(input[0] == quote) && (input[input.Length - 1] == quote))
				return input.Substring(1, input.Length - 2);

			return input;
		}
	}
}