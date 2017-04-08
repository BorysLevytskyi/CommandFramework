using System.Diagnostics;
using CommandFramework.Commands.Input;

namespace CommandFramework.Parsing
{
	[DebuggerDisplay("{PositionIndex} {Name} {Value}")]
	public class ParsedParameter : IParameterInput
	{
	    public ParsedParameter(int positionIndex, string name, string value)
		{
			PositionIndex = positionIndex;
			Name = name;
			Value = value;
		}

		public string Name { get; }

	    public string Value { get; }

	    public int PositionIndex { get; }

	    public override string ToString()
	    {
	        return $"{nameof(Name)}: {Name}, {nameof(Value)}: {Value}, {nameof(PositionIndex)}: {PositionIndex}";
	    }
	}
}