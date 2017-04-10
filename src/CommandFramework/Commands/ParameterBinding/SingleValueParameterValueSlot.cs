using System;

namespace CommandFramework.Commands.ParameterBinding
{
    public class SingleValueParameterValueSlot<TParameter> : IParameterValueSlot<TParameter> where TParameter : IParameter
    {
        private bool _valueSet = false;

        public SingleValueParameterValueSlot(TParameter parameter)
        {
            Parameter = parameter;
        }

        public TParameter Parameter { get; }

        public void SetValue(object value)
        {
            if (_valueSet)
            {
                throw new InvalidOperationException("{0} parameter is already specified");
            }

            _valueSet = true;
            Value = value;
        }

        public object Value { get; private set; } = null;
    }
}