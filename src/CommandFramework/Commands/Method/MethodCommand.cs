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
	public class MethodCommand : Command
	{
		protected readonly object Instance;

		public MethodCommand(MethodCommandDescriptor descriptor, MethodInfo method, IEnumerable<MethodParameter> parameters, object instance)
			: base(descriptor)
		{
			Instance = instance;
			Method = method;
			Parameters = new ReadOnlyCollection<MethodParameter>(parameters.ToArray());
		}

		protected IReadOnlyCollection<MethodParameter> Parameters { get; private set; }

		protected MethodInfo Method { get; private set; }

		public override void Execute(IEnumerable<IParameterInput> inputParameters)
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

					throw new Exception(string.Format("{0} parameter value wasn't supplied", defPrm.Name));
				}

				parameters[defPrm.PositionIndex] = defPrm.IsCollection
					? CollectionConstructor.CreateInstance(defPrm.Type, value)
					: value;
			}

			TraceExecution(parameters);
			ExecuteInternal(parameters);
		}

		public override IReadOnlyCollection<IParameter> GetParameters()
		{
			return Parameters;
		}

		protected virtual void ExecuteInternal(object[] parameters)
		{
			Method.Invoke(Instance, parameters);
		}

		private void TraceExecution(object[] parameters)
		{
			Trace.WriteLine(string.Format("Executing method command: {0}", Method.Name));
			Trace.Indent();
			
			for (var i = 0; i < Parameters.Count; i++)
			{
				Trace.WriteLine(string.Format("{0}: {1}", Parameters.ElementAt(i), parameters[i] ?? "<null>"));
			}

			Trace.Unindent();
		}
	}
}