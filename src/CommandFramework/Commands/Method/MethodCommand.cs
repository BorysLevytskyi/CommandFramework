using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using CommandFramework.Commands.Input;
using CommandFramework.Commands.ParameterBinding;
using CommandFramework.Reflection;

namespace CommandFramework.Commands.Method
{
	internal class MethodCommand : Command
	{
		protected readonly object Instance;
		private Action<object, object[]> _invocator;

		public MethodCommand(MethodCommandDescriptor descriptor, MethodInfo method, IEnumerable<MethodParameter> parameters, object instance)
			: base(descriptor)
		{
			Instance = instance;
			Method = method;
			Parameters = new ReadOnlyCollection<MethodParameter>(parameters.ToArray());

			CompileInvocator();
		}

		protected IReadOnlyCollection<MethodParameter> Parameters { get; }

		protected MethodInfo Method { get; }

		public override void Execute(ICommandContext context)
		{
			var parameters = BindParametesValues(context.CommandInput.InputParameters);

			TraceExecution(parameters);
			ExecuteInternal(parameters);
		}

		private object[] BindParametesValues(IEnumerable<IParameterInput> inputParameters)
		{
			var parameters = new object[Parameters.Count];

			var values = new ParameterValuesList<MethodParameter>(Parameters);

			foreach (var inputParameter in inputParameters)
			{
				values.SetParameterValue(inputParameter);
			}

			var allValues = values.GetParameterValues();

			foreach (var defPrm in Parameters)
			{
				object value;
				if (!allValues.TryGetValue(defPrm, out value))
				{
					if (defPrm.AllowsDefaultValue)
					{
						parameters[defPrm.PositionIndex] = defPrm.DefaultValue;
						continue;
					}

					throw new Exception($"{defPrm.Name} parameter value wasn't supplied");
				}

				parameters[defPrm.PositionIndex] = defPrm.IsCollection
					? CollectionConstructor.CreateInstance(defPrm.Type, value)
					: value;
			}
			return parameters;
		}

		public override IReadOnlyCollection<IParameter> GetParameters()
		{
			return Parameters;
		}

		protected virtual void ExecuteInternal(object[] parameters)
		{
			_invocator(Instance, parameters);
		}

		private void TraceExecution(object[] parameters)
		{
			Trace.WriteLine($"Executing method command: {Method.Name}");
			Trace.Indent();
			
			for (var i = 0; i < Parameters.Count; i++)
			{
				Trace.WriteLine($"{Parameters.ElementAt(i)}: {parameters[i] ?? "<null>"}");
			}

			Trace.Unindent();
		}

		private void CompileInvocator()
		{
			_invocator = Instance == null
				? MethodCompiler.CompileStaticMethodInvocation(Method)
				: MethodCompiler.CompileInstanceMethodInvocation(Method);
		}
	}
}