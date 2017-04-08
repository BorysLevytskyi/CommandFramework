using System;
using System.Collections.Generic;
using CommandFramework.Commands.Input;

namespace CommandFramework.Commands
{
    public class SimpleCommandContext : ICommandContext
    {
        public SimpleCommandContext(ICommandInput commandInput)
        {
            CommandInput = commandInput;
        }

        public T CreateInstance<T>()
        {
            return Activator.CreateInstance<T>();
        }

        public ICommandInput CommandInput { get; }

        public void Dispose()
        {
        }
    }
}