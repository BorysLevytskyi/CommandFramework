using System.Collections.Generic;

namespace CommandFramework.Commands.ParameterBinding
{
	internal class CollectionParameterValueSlot<TParameter> : IParameterValueSlot<TParameter> where TParameter : IParameter
	{
	    private readonly List<object> _valuesList;

		public CollectionParameterValueSlot(TParameter parameter)
		{
			Parameter = parameter;
			_valuesList = new List<object>();
		}

		public TParameter Parameter { get; }

	    public void SetValue(object value)
		{
			_valuesList.Add(value);
		}

		public object Value => _valuesList;
	}
}