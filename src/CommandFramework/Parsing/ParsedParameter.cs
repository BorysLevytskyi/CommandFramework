using System.Diagnostics;
using CommandFramework.Commands.Input;

namespace CommandFramework.Parsing
{
	[DebuggerDisplay("{PositionIndex} {Name} {Value}")]
	public class ParsedParameter : IParameterInput
	{
		private readonly int _positionIndex;
		private readonly string _name;
		private readonly string _value;

		public ParsedParameter(int positionIndex, string name, string value)
		{
			_positionIndex = positionIndex;
			_name = name;
			_value = value;
		}

		public string Name
		{
			get { return _name; }
		}

		public string Value
		{
			get { return _value; }
		}

		public int PositionIndex
		{
			get { return _positionIndex; }
		}
	}
}