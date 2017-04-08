using System.Collections.Generic;
using CommandFramework.Commands.Input;

namespace CommandFramework.Commands
{
    public class SimpleCommandContextFactory : ICommandContextFactory
    {
        public ICommandContext CreateContext(ICommandInput commandInput)
        {
            return new SimpleCommandContext(commandInput);
        }
    }
}