namespace CommandFramework.Commands.ParameterBinding
{
    internal interface IParameterValueSlot<out TParameter> where TParameter : IParameter
    {
        TParameter Parameter { get; }

        void SetValue(object value);

        object Value { get; }
    }
}