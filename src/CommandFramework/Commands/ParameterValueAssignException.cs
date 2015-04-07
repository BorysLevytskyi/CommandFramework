﻿using System;
using System.Text;
using CommandFramework.Commands.Input;

namespace CommandFramework.Commands
{
	public class ParameterValueAssignException : Exception
	{
		private readonly IParameter _parameter;
		private readonly IParameterInput _parameterInput;

		public ParameterValueAssignException(IParameter parameter, IParameterInput parameterParameterInput, Exception innerException) : 
			base(BuildMessage(parameter, parameterParameterInput, innerException), innerException)
		{
			_parameter = parameter;
			_parameterInput = parameterParameterInput;
		}

		public IParameter Parameter
		{
			get { return _parameter; }
		}

		public IParameterInput ParameterInput
		{
			get { return _parameterInput; }
		}

		public static string BuildMessage(IParameter parameter, IParameterInput input, Exception innerException)
		{
			var sb = new StringBuilder();
			sb.AppendFormat("Failed to set value for parameter {0}. Input value was: {1}", parameter.Name);

			if (innerException != null)
			{
				sb.AppendLine();
				sb.AppendFormat("\t{0}", innerException.Message);
			}

			return sb.ToString();
		}
	}
}