using System;
using System.Text;
using CommandFramework.Commands.Input;

namespace CommandFramework.Commands
{
	public class ParameterValueAssignException : Exception
	{
	    public ParameterValueAssignException(IParameter parameter, IParameterInput parameterParameterInput, Exception innerException) : 
			base(BuildMessage(parameter, parameterParameterInput, innerException), innerException)
		{
			Parameter = parameter;
			ParameterInput = parameterParameterInput;
		}

		public IParameter Parameter { get; }

	    public IParameterInput ParameterInput { get; }

	    public static string BuildMessage(IParameter parameter, IParameterInput input, Exception innerException)
		{
			var sb = new StringBuilder();
			sb.AppendFormat("Failed to set value for a parameter.");
			sb.AppendLine();

			sb.AppendFormat("Parameter: {0}", parameter.Name);
			sb.AppendLine();

			sb.AppendFormat("Input Value: {0}", input.Value);

			if (innerException != null)
			{
				sb.AppendLine();
				sb.AppendFormat("\t{0}", innerException.Message);
			}

			return sb.ToString();
		}
	}
}