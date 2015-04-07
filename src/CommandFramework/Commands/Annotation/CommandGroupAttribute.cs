using System;

namespace CommandFramework.Commands.Annotation
{
	[AttributeUsage(AttributeTargets.Class)]
	public class CommandGroupAttribute : Attribute
	{
		public CommandGroupAttribute()
		{
			
		}

		public CommandGroupAttribute(string groupName)
		{
			GroupName = groupName;
		}

		public string GroupName { get; set; }
	}
}