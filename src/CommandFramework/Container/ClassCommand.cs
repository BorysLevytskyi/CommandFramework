using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using CommandFramework.Annotation;
using CommandFramework.Commands;
using CommandFramework.Commands.Class;
using CommandFramework.Dispatcher;

namespace CommandFramework.Container
{
    internal class ClassCommand<TInstanceCmd> : Command where TInstanceCmd : ICommandInstance
    {
        private readonly Func<ICommandContext, TInstanceCmd> _factory;

        internal ClassCommand(CommandDescriptor descriptor, Func<ICommandContext, TInstanceCmd> factory, IEnumerable<PropertyParameter> parameters) : base(descriptor)
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
            Tracer.WriteLine($"Executing class command: {Name}");
            Tracer.Indent();

            foreach (var parameter in Parameters)
            {
                Tracer.WriteLine($"{parameter.Name}: {parameter.Property.GetValue(instance) ?? "<null>"}");
            }

            Tracer.Unindent();
        }

        public override IReadOnlyCollection<IParameter> GetParameters()
        {
            return Parameters;
        }
    }
}