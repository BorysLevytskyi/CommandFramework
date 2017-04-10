using System;
using System.Collections.Generic;
using CommandFramework.Commands.Input;

namespace CommandFramework.Commands
{
	internal abstract class Parameter : IParameter
	{
	    protected Parameter(IParameterDescription description)
		{
			Name = description.Name;
			PositionIndex = description.PositionIndex;

			Synonyms = new HashSet<string>(description.Synonyms, StringComparer.OrdinalIgnoreCase);

			Description = description.Description;
			AllowsDefaultValue = description.AllowsDefaultValue;
			DefaultValue = description.DefaultValue;
			IsCollection = description.IsCollection;
		}

		public string Name { get; }

	    public int PositionIndex { get; }

	    public string Description { get; set; }

		public bool AllowsDefaultValue { get; protected set; }

		public object DefaultValue { get; protected set; }

		public virtual bool SupportsAssignmentByPositionIndex => PositionIndex >= 0;

	    public ICollection<string> Synonyms { get; }

	    public bool IsCollection { get; }

		public bool Matches(IParameterInput parameter)
		{
			if (parameter.Name == null)
			{
				if (parameter.PositionIndex != PositionIndex)
				{
					return false;
				}

				if (SupportsAssignmentByPositionIndex)
				{
					return true;
				}

				throw new NotSupportedException(
				    $"{Name} parameter doesn't support position assignment by index");
			}

			return Name != null &&
				   (Name.Equals(parameter.Name, StringComparison.OrdinalIgnoreCase) || Synonyms.Contains(parameter.Name));
		}

		public abstract Type ValueType { get; }

		public override string ToString()
		{
			return Name;
		}
	}
}