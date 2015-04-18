using System;
using CommandFramework.Commands.Input;

namespace CommandFramework.Commands
{
	public class ParameterNotFoundException : Exception
	{
		private readonly IParameterInput _parameterInput;

		public ParameterNotFoundException(IParameterInput parameterInput, string message) : base(message)
		{
			_parameterInput = parameterInput;
		}

		public IParameterInput ParameterInput
		{
			get { return _parameterInput; }
		}
	}
}