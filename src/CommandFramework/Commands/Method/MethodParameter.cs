using System;
using System.Diagnostics;
using System.Reflection;
using CommandFramework.Reflection;

namespace CommandFramework.Commands.Method
{
    [DebuggerDisplay("{Type.Name} {Name}")]
    internal class MethodParameter : Parameter
    {
        private readonly ParameterInfo _parameter;

        public MethodParameter(ParameterInfo parameter)
            : base(CommandParameterDescriptor.CreateFor(parameter))
        {
            _parameter = parameter;

            ValueType = IsCollection
                ? CollectionConstructor.GetElementType(_parameter.ParameterType)
                : _parameter.ParameterType;

            Debug.Assert(!parameter.IsOut);
        }

        public Type Type => _parameter.ParameterType;

        public override bool SupportsAssignmentByPositionIndex => true;

        public override Type ValueType { get; }
    }
}