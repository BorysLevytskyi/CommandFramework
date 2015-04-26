using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CommandFramework.Commands.Method
{
	internal static class MethodCompiler
	{
		internal static Action<object, object[]> CompileInstanceMethodInvocation(MethodInfo methodInfo)
		{
			var arrArg = Expression.Parameter(typeof(object[]));
			var instanceType = methodInfo.DeclaringType;
			var instanceArg = Expression.Parameter(typeof(object));
			var arguments = CreateArguments(arrArg, methodInfo.GetParameters().Select(p => p.ParameterType)).ToArray();
			var body = Expression.Call(Expression.Convert(instanceArg, instanceType), methodInfo, arguments);
			var lambda = Expression.Lambda<Action<object, object[]>>(body, instanceArg, arrArg);
			return lambda.Compile();
		}

		internal static Action<object, object[]> CompileStaticMethodInvocation(MethodInfo methodInfo)
		{
			var arrArg = Expression.Parameter(typeof (object[]));
			var instanceArg = Expression.Parameter(typeof(object));
			var arguments = CreateArguments(arrArg, methodInfo.GetParameters().Select(p => p.ParameterType)).ToArray();
			var body = Expression.Call(methodInfo, arguments);
			var lambda = Expression.Lambda<Action<object, object[]>>(body, instanceArg, arrArg);
			return lambda.Compile();
		}

		private static IEnumerable<Expression> CreateArguments(Expression argExpr, IEnumerable<Type> dataTypes)
		{
			int index = 0;
			foreach (var type in  dataTypes)
			{
				int i = index++;
				yield return Expression.Convert(Expression.ArrayIndex(argExpr, Expression.Constant(i)), type);
			}
		}
	}
}