using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandFramework.Annotation;
using CommandFramework.Container;

namespace CommandFramework.Commands.Class
{
	internal static class ClassCommandFactory
	{
		internal static ClassCommand<TInstanceCmd> Create<TInstanceCmd>() where TInstanceCmd : ICommandInstance
		{
			return Create(DefaultInstanceFactory<TInstanceCmd>);
		}

		internal static ClassCommand<TInstanceCmd> Create<TInstanceCmd>(Func<ICommandContext, TInstanceCmd> factory, string name = null) where TInstanceCmd : ICommandInstance
		{
			var descriptor = ClassCommandDescriptor<TInstanceCmd>.Build();
			var parameters = ReadParameters<TInstanceCmd>();
			var cmd = new ClassCommand<TInstanceCmd>(name ?? descriptor.Name, factory ?? DefaultInstanceFactory<TInstanceCmd>, parameters)
			{
				Group = descriptor.GroupName
			};
			return cmd;
		}

		private static TInstanceCmd DefaultInstanceFactory<TInstanceCmd>(ICommandContext context) where TInstanceCmd: ICommandInstance
		{
			return context.CreateInstance<TInstanceCmd>();
		}

		public static IEnumerable<PropertyParameter> ReadParameters<T>()
		{
			return from p in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public)
				let attr = p.GetCustomAttribute<ParameterAttribute>()
				where attr != null
				select new PropertyParameter(p, CommandParameterDescriptor.CreateFor(p));
		}

	}
}