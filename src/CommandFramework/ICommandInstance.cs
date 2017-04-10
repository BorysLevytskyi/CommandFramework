namespace CommandFramework
{
    /// <summary>
    /// Represents an executable class command
    /// </summary>
	public interface ICommandInstance
	{
        /// <summary>
        /// Exectutes command
        /// </summary>
		void Run();
	}
}