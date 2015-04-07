using CommandFramework.Annotation;

namespace CommandFramework.Commands.Class
{
	internal class ClassCommandDescriptor<TInstanceCmd> : CommandDescriptor where TInstanceCmd : ICommandInstance
	{
		internal static ClassCommandDescriptor<TInstanceCmd> Build()
		{
			var descr = new ClassCommandDescriptor<TInstanceCmd>();
			descr.ReadAttributes(typeof(TInstanceCmd));
			descr.SetDefaults();
			return descr;
		}

		protected void SetDefaults()
		{
			if (string.IsNullOrEmpty(Name))
			{
				Name = typeof (TInstanceCmd).Name.ToLower();
			}
		}
	}
}