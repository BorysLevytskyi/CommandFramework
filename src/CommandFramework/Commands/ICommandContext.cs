using System;
using System.Collections.Generic;
using CommandFramework.Commands.Input;

namespace CommandFramework.Commands
{
    public interface ICommandContext : IDisposable
    {
        T CreateInstance<T>();

        ICommandInput CommandInput { get; }
    }
}