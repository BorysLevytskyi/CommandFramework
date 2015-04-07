using System;

namespace CommandFramework.Commands
{
	public interface IParameter : IParameterDescription
	{
		bool SupportsAssignmentByPositionIndex { get; }

		Type ValueType { get; }

		bool Matches(IParameterInput inputParameter);
	}
}