using System;
using System.Reflection;
using CommandFramework.Reflection;

namespace CommandFramework.Commands.Class
{
	public class PropertyParameter : Parameter
	{
		private readonly PropertyInfo _property;
		private readonly Type _valueType;

		public PropertyParameter(PropertyInfo property, CommandParameterDescriptor description) : 
			base(description)
		{
			_property = property;

			_valueType = IsCollection
				? CollectionConstructor.GetElementType(property.PropertyType)
				: property.PropertyType;
		}

		public PropertyInfo Property
		{
			get { return _property; }
		}

		public override Type ValueType
		{
			get { return _valueType; }
		}
	}
}