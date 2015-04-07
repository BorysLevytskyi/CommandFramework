namespace CommandFramework.Commands.Input
{
	public interface IParameterInput
	{
		string Name { get; }
		int PositionIndex { get; }
		string Value { get; }
	}
}