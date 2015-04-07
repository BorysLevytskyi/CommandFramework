using System.Collections.Generic;
using System.Linq;

namespace CommandFramework.Commands.ParameterBinding
{
	internal static class InputParameterBinder
	{
		internal static IParameterInput GetInputForCommandParameter(
			IEnumerable<IParameterInput> inputParameters,
			Parameter parameter)
		{
			return inputParameters.SingleOrDefault(parameter.Matches);
		}

		internal static ICollection<IParameterInput> GetAntonymousInputParameters(
			IEnumerable<IParameterInput> inputParameters)
		{
			return inputParameters.Where(p => string.IsNullOrEmpty(p.Name)).ToList();
		}
	}
}