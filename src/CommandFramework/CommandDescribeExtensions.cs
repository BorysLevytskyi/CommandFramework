using CommandFramework.Commands;

namespace CommandFramework
{
	public static class CommandDescribeExtensions
	{
		public static ICommand WithName(this ICommand cmd, string name)
		{
			cmd.Name = name;
			return cmd;
		}

		public static ICommand WithGroup(this ICommand cmd, string group)
		{
			cmd.Group = group;
			return cmd;
		}

		public static ICommand WithDescription(this ICommand cmd, string description)
		{
			cmd.Description = description;
			return cmd;
		}
	}
}