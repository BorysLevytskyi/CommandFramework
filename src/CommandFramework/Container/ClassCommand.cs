using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using CommandFramework.Commands;
using CommandFramework.Commands.Class;
using CommandFramework.Commands.Input;

namespace CommandFramework.Container
{
    internal class ClassCommand<TInstanceCmd> : Command where TInstanceCmd : ICommandInstance
    {
        private readonly Func<ICommandContext, TInstanceCmd> _factory;

        internal ClassCommand(string name, Func<ICommandContext, TInstanceCmd> factory, IEnumerable<PropertyParameter> parameters) : base(name)
        {
            _factory = factory;
            Parameters = new ReadOnlyCollection<PropertyParameter>(parameters.ToList());
        }

        public override void Execute(ICommandContext context)
        {
            var cmd = _factory(context);
            ClassParameterValueBinder.SetPropertyValues(cmd, Parameters, context.CommandInput.InputParameters);
            TraceExecution(cmd);

            cmd.Run();
        }

        public ReadOnlyCollection<PropertyParameter> Parameters { get; }

        private void TraceExecution(ICommandInstance instance)
        {
            Trace.WriteLine($"Executing class command: {Name}");
            Trace.Indent();

            foreach (var parameter in Parameters)
            {
                Trace.WriteLine($"{parameter.Name}: {parameter.Property.GetValue(instance) ?? "<null>"}");
            }

            Trace.Unindent();
        }

        public override IReadOnlyCollection<IParameter> GetParameters()
        {
            return Parameters;
        }
    }
}