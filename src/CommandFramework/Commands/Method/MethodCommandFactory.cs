using System;
using System.Linq;
using System.Reflection;

namespace CommandFramework.Commands.Method
{
	public static class MethodCommandFactory
	{
		public static MethodCommand Create(Action @delegate, string name = null)
		{
			return Create(@delegate.Method, @delegate.Target, name);
		}

		public static MethodCommand Create<TArg1>(Action<TArg1> @delegate, string name = null)
		{
			return Create(@delegate.Method, @delegate.Target, name);
		}

		public static MethodCommand Create<TArg1, TArg2>(Action<TArg1, TArg2> @delegate, string name = null)
		{
			return Create(@delegate.Method, @delegate.Target, name);
		}

		public static MethodCommand Create<TArg1, TArg2, TArg3>(Action<TArg1, TArg2, TArg3> @delegate, string name = null)
		{
			return Create(@delegate.Method, @delegate.Target, name);
		}

		public static MethodCommand Create(MethodInfo method, object instance = null, string name = null)
		{
			var d = MethodCommandDescriptor.Build(method);
			d.Name = d.Name ?? name;
			return new MethodCommand(d, method, method.GetParameters().Select(p => new MethodParameter(p)), instance);
		}
	}
}