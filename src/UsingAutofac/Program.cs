using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Autofac;
using CommandFramework;
using CommandFramework.Commands;
using CommandFramework.Annotation;
using CommandFramework.Commands.Class;
using CommandFramework.Commands.Input;

namespace UsingAutofac
{
    class Program
    {
        static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<Service>().SingleInstance();
            containerBuilder.RegisterType<AddValue>();

            var catalog = new CommandsCatalog();
            catalog.AddCommand<AddValue>();
            catalog.AddExitCommand();
            catalog.AddHelpCommand();

            var dispatcher = new CommandDispatcher(catalog);
            dispatcher.UseCommandContextFactory(new AutofacCommandContextFactory(containerBuilder.Build()));
            dispatcher.StartDispatchingFromUserInput();
        }
    }

    public class Service
    {
        public List<string> Values = new List<string>();
    }

    [Command(Name = "add")]
    public class AddValue : ICommandInstance
    {
        private readonly Service _service;

        public AddValue(Service service)
        {
            _service = service;
        }

        [Parameter(DefaultValue = "DefaultValue", PositionIndex = 0)]
        public string Value { get; set; }

        public void Run()
        {
            _service.Values.Add(Value);
            Console.WriteLine($"Value '{Value}' added. Now list of values is:\r\n{string.Join(Environment.NewLine, _service.Values)}");
        }
    }

    public class AutofacCommandContextFactory : ICommandContextFactory
    {
        private readonly IContainer _container;

        public AutofacCommandContextFactory(IContainer container)
        {
            _container = container;
        }

        public ICommandContext CreateContext(ICommandInput commandInput)
        {
            return new AutofacCommandContext(commandInput, _container.BeginLifetimeScope());
        }
    }

    public class AutofacCommandContext : ICommandContext
    {
        private readonly ILifetimeScope _lifetimeScope;

        public AutofacCommandContext(ICommandInput commandInput, ILifetimeScope lifetimeScope)
        {
            CommandInput = commandInput;
            _lifetimeScope = lifetimeScope;
        }

        public ICommandInput CommandInput { get; }

        public void Dispose()
        {
            _lifetimeScope.Dispose();
        }

        public T CreateInstance<T>()
        {
            return _lifetimeScope.Resolve<T>();
        }
    }
}
