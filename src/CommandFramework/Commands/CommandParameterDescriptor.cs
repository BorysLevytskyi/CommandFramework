using System.Collections.Generic;
using System.Reflection;
using CommandFramework.Annotation;
using CommandFramework.Reflection;

namespace CommandFramework.Commands
{
	internal class CommandParameterDescriptor : IParameterDescription
	{
		public CommandParameterDescriptor()
		{
			Synonyms = new string[0];
			PositionIndex = -1;
		}

		public int PositionIndex { get; set; }

		public ICollection<string> Synonyms { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public bool IsCollection { get; set; }

		public bool AllowsDefaultValue { get; set; }

		public object DefaultValue { get; set; }

		public static CommandParameterDescriptor Empty()
		{
			return new CommandParameterDescriptor();
		}

		public static CommandParameterDescriptor CreateFor(PropertyInfo property)
		{
			var descriptor = new CommandParameterDescriptor
			{
				AllowsDefaultValue = true,
				Name = property.Name.ToLower(),
				IsCollection = CollectionConstructor.IsSupportedCollectionType(property.PropertyType),
				PositionIndex = -1
			};

			var attr = property.GetCustomAttribute<ParameterAttribute>();

			if (attr == null)
			{
				return descriptor;
			}

			attr.SetCreateDescriptorValues(descriptor);
			return descriptor;
		}

		public static CommandParameterDescriptor CreateFor(ParameterInfo parameter)
		{
			var descriptor = new CommandParameterDescriptor
			{
				Name = parameter.Name.ToLower(),
				AllowsDefaultValue = parameter.HasDefaultValue,
				DefaultValue = parameter.DefaultValue,
				IsCollection = CollectionConstructor.IsSupportedCollectionType(parameter.ParameterType),
				PositionIndex = parameter.Position
			};

			var attr = parameter.GetCustomAttribute<ParameterAttribute>();

			if (attr == null)
			{
				return descriptor;
			}

			attr.SetCreateDescriptorValues(descriptor);
			return descriptor;
		}
	}
}