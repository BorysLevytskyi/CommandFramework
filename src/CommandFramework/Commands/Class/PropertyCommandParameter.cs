using System;
using System.Reflection;
using CommandFramework.Reflection;

namespace CommandFramework.Commands.Class
{
	internal class PropertyParameter : Parameter
	{
	    public PropertyParameter(PropertyInfo property, CommandParameterDescriptor description) : 
			base(description)
		{
			Property = property;

			ValueType = IsCollection
				? CollectionConstructor.GetElementType(property.PropertyType)
				: property.PropertyType;
		}

		public PropertyInfo Property { get; }

	    public override Type ValueType { get; }
	}
}