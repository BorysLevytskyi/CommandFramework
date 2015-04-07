using System.Collections.Generic;

namespace CommandFramework.Commands
{
	public interface ICommand
	{
		string Group { get; set; }
		string Description { get; set; }
		string Name { get; set; }

		bool IsDefault { get; set; }

		void Execute(IEnumerable<IParameterInput> inputParameters);

		IReadOnlyCollection<IParameter> GetParameters();
	}
}