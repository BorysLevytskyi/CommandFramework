using System;
using System.Collections.Generic;
using System.Linq;
using CommandFramework.Commands.Input;

namespace CommandFramework.Commands.ParameterBinding
{
	internal class ParameterValuesList<TParameter> where TParameter : IParameter
	{
		private readonly IEnumerable<TParameter> _parameters;
		private readonly IDictionary<TParameter, IParameterValueSlot<TParameter>> _slots;
		private readonly TParameter _defaultCollectionParameter;

		public ParameterValuesList(IEnumerable<TParameter> parameters)
		{
			_parameters = parameters;
			_slots = new Dictionary<TParameter, IParameterValueSlot<TParameter>>();
			_defaultCollectionParameter = _parameters.SingleOrDefault(p => p.IsCollection && p.PositionIndex == 0);
		}

		public void SetParameterValue(IParameterInput parameter)
		{
			var slot = EnsureSlot(FindParameter(parameter));
			slot.SetValue(InputValueConverter.Convert(parameter.Value, slot.Parameter.ValueType));

		}

		private TParameter FindParameter(IParameterInput parameter)
		{
			if (parameter.Name == null && _defaultCollectionParameter != null)
			{
				return _defaultCollectionParameter;
			}

			var prm = _parameters.SingleOrDefault(s => s.Matches(parameter));

			if (prm == null)
			{
				throw new InvalidOperationException(string.Format("Parameter wasn't found: {0}", parameter));
			}

			return prm;
		}

		private IParameterValueSlot<TParameter> EnsureSlot(TParameter parameter)
		{
			IParameterValueSlot<TParameter> slot;
			if (!_slots.TryGetValue(parameter, out slot))
			{
				_slots.Add(parameter, slot = CreateSlot(parameter)); 
			}

			return slot;
		}

		public void SetParameterValue(TParameter parameter, object value)
		{
			EnsureSlot(parameter).SetValue(value);
		}

		public Dictionary<TParameter, object> GetParameterValues()
		{
			return _slots.Values.ToDictionary(s => s.Parameter, s => s.Value);
		}

		private IParameterValueSlot<TParameter> CreateSlot(TParameter parameter)
		{
			if (parameter.IsCollection)
			{
				return new CollectionParameterValueSlot<TParameter>(parameter);
			}

			return new SingleValueParameterValueSlot<TParameter>(parameter);
		}
	}
}