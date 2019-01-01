using System.Reflection;

namespace CommandFramework.Annotation
{
	internal abstract class CommandDescriptor
	{
		public string Name { get; set; }

        public string GroupName { get; set; }

        public string Description { get; set; }

		public bool IsDefault { get; set; }

		protected virtual void ReadAttributes<TSource>(TSource attributeProvider) where TSource : MemberInfo
		{
			foreach (var attr in attributeProvider.GetCustomAttributes(true))
			{
				var commandAttribute = attr as CommandAttribute;
				var commandGroupAttribute = attr as CommandGroupAttribute;

				if (commandAttribute != null)
				{
					ProcessAttribute(commandAttribute);
				}

				if (commandGroupAttribute != null)
				{
					ProcessAttribute(commandGroupAttribute);
				}
			}
		}

		private void ProcessAttribute(CommandAttribute attribute)
		{
			if (!string.IsNullOrEmpty(attribute.Name))
			{
				Name = attribute.Name;
			}

			if (!string.IsNullOrEmpty(attribute.Description))
			{
				Description = attribute.Description;
			}

		    if (!string.IsNullOrEmpty(attribute.GroupName))
		    {
		        GroupName = attribute.GroupName;
		    }

			IsDefault = attribute.IsDefault;
		}

		private void ProcessAttribute(CommandGroupAttribute attribute)
		{
			GroupName = attribute.GroupName;
		}
	}
}