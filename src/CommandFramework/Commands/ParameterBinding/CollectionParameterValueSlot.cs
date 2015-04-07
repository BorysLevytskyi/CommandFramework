using System.Collections.Generic;

namespace CommandFramework.Commands.ParameterBinding
{
	internal class CollectionParameterValueSlot<TParameter> : IParameterValueSlot<TParameter> where TParameter : IParameter
	{
		private readonly TParameter _parameter;
		private readonly List<object> _valuesList;

		public CollectionParameterValueSlot(TParameter parameter)
		{
			_parameter = parameter;
			_valuesList = new List<object>();
		}

		public TParameter Parameter
		{
			get { return _parameter; }
		}

		public void SetValue(object value)
		{
			_valuesList.Add(value);
		}

		public object Value
		{
			get { return _valuesList; }
		}
	}
}