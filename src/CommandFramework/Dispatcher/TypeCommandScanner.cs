using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandFramework.Annotation;
using CommandFramework.Commands;
using CommandFramework.Commands.Class;
using CommandFramework.Commands.Method;

namespace CommandFramework.Dispatcher
{
	internal static class TypeCommandScanner
	{
	    private static readonly Type CommmandInstanceType = typeof(ICommandInstance);

		public static IEnumerable<ICommand> FindMethodCommandsOnInstance(object inst)
		{
			return FindMethodCommandsOnInstanceInternal(inst).ToList();
		}

		public static IEnumerable<ICommand> FindStaticMethodCommands(Type type)
		{
			return FindStaticMethodCommandsInternal(type).ToList();
		}

	    public static IEnumerable<ICommand> FindClassCommands(Assembly assembly)
	    {
	        return (from t in assembly.DefinedTypes
	            where !t.IsAbstract && CommmandInstanceType.IsAssignableFrom(t)
	            select ClassCommandFactory.CreateFromType(t)).ToArray();
	    }

		public static IEnumerable<ICommand> FindStaticMethodCommands(Assembly assembly)
		{
			return (from type in assembly.DefinedTypes
                where IsStatic(type)
				let attr = type.GetCustomAttribute<CommandGroupAttribute>()
				where attr != null
				from cmd in FindStaticMethodCommandsInternal(type)
				select cmd).ToList();
		}

	    public static bool IsStatic(TypeInfo type)
	    {
	        return type.IsAbstract && type.IsSealed;
	    }

	    private static IEnumerable<ICommand> FindStaticMethodCommandsInternal(Type type)
		{
			foreach (MethodInfo method in type.GetMethods(
				BindingFlags.NonPublic |
				BindingFlags.Public |
				BindingFlags.Static).Where(m => (m.IsStatic)))
			{
				var attr = method.GetCustomAttribute<CommandAttribute>();
				if (attr == null)
				{
					continue;
				}

				yield return MethodCommandFactory.Create(method);
			}
		}

		private static IEnumerable<ICommand> FindMethodCommandsOnInstanceInternal(object inst)
		{
			Type type = inst.GetType();

			return (from method in type.GetMethods().Where(m => !m.IsStatic)
				let attr = method.GetCustomAttribute<CommandAttribute>()
				where attr != null
				select MethodCommandFactory.Create(method, inst));
		}
	}
}