using System;

namespace CommandFramework.Annotation
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
	public class CommandAttribute : Attribute
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public bool IsDefault { get; set; }

        public string GroupName { get; set; }

		public CommandAttribute()
		{
		}

		public CommandAttribute(string description) : this()
		{
			Description = description;
		}

		public CommandAttribute(string name, string description)
			: this(description)
		{
			Name = name;
		}
	}
}