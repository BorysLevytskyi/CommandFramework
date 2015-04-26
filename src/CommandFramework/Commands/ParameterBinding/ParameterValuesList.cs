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

			// Initialize collection values for non-nullable values
			EnsureCollectionNoDefaultParametersSlots();
		}

		private void EnsureCollectionNoDefaultParametersSlots()
		{
			foreach (var parameter in _parameters.Where(p => p.IsCollection && !p.AllowsDefaultValue))
			{
				EnsureSlot(parameter);
			}
		}

		public void SetParameterValue(IParameterInput parameterInput)
		{
			var parameter = FindParameter(parameterInput);
			var slot = EnsureSlot(parameter);

			try
			{
				slot.SetValue(InputValueConverter.Convert(parameterInput.Value, slot.Parameter.ValueType));
			}
			catch (Exception ex)
			{
				throw new ParameterValueAssignException(parameter, parameterInput, ex);
			}
		}

		private TParameter FindParameter(IParameterInput parameterInput)
		{
			if (parameterInput.Name == null && _defaultCollectionParameter != null)
			{
				return _defaultCollectionParameter;
			}

			var prm = _parameters.SingleOrDefault(s => s.Matches(parameterInput));

			if (prm == null)
			{
				throw new ParameterNotFoundException(parameterInput, string.Format("Parameter not found: {0}", parameterInput.Name));
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