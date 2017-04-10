using System;
using CommandFramework.Commands.Input;

namespace CommandFramework.Commands
{
	public class ParameterNotFoundException : Exception
	{
	    public ParameterNotFoundException(IParameterInput parameterInput, string message) : base(message)
		{
			ParameterInput = parameterInput;
		}

		public IParameterInput ParameterInput { get; }
	}
}