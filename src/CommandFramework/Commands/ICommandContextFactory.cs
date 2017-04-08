using System.Collections.Generic;
using CommandFramework.Commands.Input;

namespace CommandFramework.Commands
{
    public interface ICommandContextFactory
    {
        ICommandContext CreateContext(ICommandInput commandInput);
    }
}