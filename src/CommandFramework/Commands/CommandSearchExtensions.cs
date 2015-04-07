using System.Collections.Generic;
using System.Linq;
using CommandFramework.Parsing;

namespace CommandFramework.Commands
{
	public static class CommandSearchExtensions
	{
		public static bool Matches(this ICommand cmd, ParsedCommand parsedCommand)
		{
			return !(from prm in cmd.GetParameters()
				let inputPrm = parsedCommand.Parameters.FindForCommandArgument(prm)
				where inputPrm == null && !prm.AllowsDefaultValue
				select prm).Any();
		}

		public static ParsedParameter FindForCommandArgument(this IEnumerable<ParsedParameter> arguments, IParameter parameter)
		{
			ParsedParameter byIndex = null;

			foreach (var arg in arguments)
			{
				if (!string.IsNullOrEmpty(arg.Name) && arg.Name.Equals(parameter.Name))
				{
					return arg;
				}

				if (arg.PositionIndex == parameter.PositionIndex)
				{
					byIndex = arg;
				}
			}

			return byIndex;
		}
	}
}