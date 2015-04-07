using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandFramework.Annotation;
using CommandFramework.Commands;
using CommandFramework.Commands.Method;

namespace CommandFramework.Dispatcher
{
	internal static class TypeCommandScanner
	{
		public static IEnumerable<ICommand> FindCommandsInInstance(object inst)
		{
			return FindInstanceCommandsInternal(inst).ToList();
		}

		public static IEnumerable<ICommand> FindCommands(Type type)
		{
			return FindCommandsInType(type);
		}

		public static IEnumerable<ICommand> FindCommands(Assembly assembly)
		{
			return (from type in assembly.DefinedTypes
				let attr = type.GetCustomAttribute<CommandGroupAttribute>()
				where attr != null
				from cmd in FindCommandsInType(type)
				select cmd).ToList();
		}

		private static IEnumerable<ICommand> FindCommandsInType(Type type)
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

				yield return MethodCommandFactory.Create(method, attr);
			}
		}

		private static IEnumerable<ICommand> FindInstanceCommandsInternal(object inst)
		{
		    Type type = inst.GetType();

		    return (from method in type.GetMethods().Where(m => !m.IsStatic)
		        let attr = method.GetCustomAttribute<CommandAttribute>()
		        where attr != null
		        select MethodCommandFactory.Create(method, inst));
		}
	}
}