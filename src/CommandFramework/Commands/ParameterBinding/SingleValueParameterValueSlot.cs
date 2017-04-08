using System;

namespace CommandFramework.Commands.ParameterBinding
{
    public class SingleValueParameterValueSlot<TParameter> : IParameterValueSlot<TParameter> where TParameter : IParameter
    {
        private readonly TParameter _parameter;
        private object _value = null;
        private bool _valueSet = false;

        public SingleValueParameterValueSlot(TParameter parameter)
        {
            _parameter = parameter;
        }

        public TParameter Parameter => _parameter;

        public void SetValue(object value)
        {
            if (_valueSet)
            {
                throw new InvalidOperationException("{0} parameter is already specified");
            }

            _valueSet = true;
            _value = value;
        }

        public object Value => _value;
    }
}