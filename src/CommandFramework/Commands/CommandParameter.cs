using System;
using System.Collections.Generic;
using CommandFramework.Commands.Input;

namespace CommandFramework.Commands
{
	public abstract class Parameter : IParameter
	{
		private readonly string _name;
		private readonly int _positionIndex;
		private readonly ICollection<string> _synonyms;

		protected Parameter(IParameterDescription description)
		{
			_name = description.Name;
			_positionIndex = description.PositionIndex;

			_synonyms = new HashSet<string>(description.Synonyms, StringComparer.OrdinalIgnoreCase);

			Description = description.Description;
			AllowsDefaultValue = description.AllowsDefaultValue;
			DefaultValue = description.DefaultValue;
			IsCollection = description.IsCollection;
		}

		public string Name => _name;

	    public int PositionIndex => _positionIndex;

	    public string Description { get; set; }

		public bool AllowsDefaultValue { get; protected set; }

		public object DefaultValue { get; protected set; }

		public virtual bool SupportsAssignmentByPositionIndex => _positionIndex >= 0;

	    public ICollection<string> Synonyms => _synonyms;

	    public bool IsCollection { get; private set; }

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