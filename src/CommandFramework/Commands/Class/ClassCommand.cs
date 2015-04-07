using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using CommandFramework.Parsing;

namespace CommandFramework.Commands.Class
{
	public class ClassCommand<TInstanceCmd> : Command where TInstanceCmd : ICommandInstance
	{
		private readonly Func<TInstanceCmd> _factory;

		internal ClassCommand(string name, Func<TInstanceCmd> factory, IEnumerable<PropertyParameter> parameters) : base(name)
		{
			_factory = factory;
			Parameters = new ReadOnlyCollection<PropertyParameter>(parameters.ToList());
		}

		public override void Execute(IEnumerable<IParameterInput> inputParameters)
		{
			var cmd = _factory();
			ClassParameterValueBinder.SetPropertyValues(cmd, Parameters, inputParameters.ToList());
			TraceExecution(cmd);

			cmd.Run();
		}

		public ReadOnlyCollection<PropertyParameter> Parameters { get; private set; }

		private void TraceExecution(ICommandInstance instance)
		{
			Trace.WriteLine(string.Format("Executing class command: {0}", Name));
			Trace.Indent();

			foreach (var parameter in Parameters)
			{
				Trace.WriteLine(string.Format("{0}: {1}", parameter.Name, parameter.Property.GetValue(instance) ?? "<null>"));
			}

			Trace.Unindent();
		}

		public override IReadOnlyCollection<IParameter> GetParameters()
		{
			return Parameters;
		}
	}
}