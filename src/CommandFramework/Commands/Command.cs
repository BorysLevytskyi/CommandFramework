using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommandFramework.Annotation;
using CommandFramework.Commands.Input;

namespace CommandFramework.Commands
{
	internal abstract class Command : ICommand
	{
		protected Command(CommandDescriptor descriptor)
		{
			Name = descriptor.Name;
			Description = descriptor.Description;
			IsDefault = descriptor.IsDefault;
			Group = descriptor.GroupName;
		}

		public bool IsDefault { get; set; }

		public string Group { get; set; }

		public string Description { get; set; }

		public string Name { get; set; }

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.Append(Name);

			var parameters = GetParameters();
			if (parameters.Count > 0)
			{
				sb.AppendFormat(" ({0})", string.Join(", ", parameters.Select(a => a.Name)));
			}

			if (!string.IsNullOrEmpty(Description))
			{
				sb.AppendFormat(": {0} ", Description);
			}

			return sb.ToString();
		}

		public abstract IReadOnlyCollection<IParameter> GetParameters();

		public abstract void Execute(ICommandContext context);
	}
}