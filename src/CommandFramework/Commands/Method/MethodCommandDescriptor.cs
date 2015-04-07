using System.Reflection;
using CommandFramework.Annotation;

namespace CommandFramework.Commands.Method
{
	public class MethodCommandDescriptor : CommandDescriptor
	{
		internal static MethodCommandDescriptor Build(MethodInfo method)
		{
			var descr = new MethodCommandDescriptor {Name = method.Name.ToLower()};
			descr.ReadAttributes(method.DeclaringType); // TODO: May be some overrides and other side effects
			descr.ReadAttributes(method);
			return descr;
		}
	}
}