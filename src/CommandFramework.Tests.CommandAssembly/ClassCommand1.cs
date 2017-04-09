using CommandFramework.Annotation;
using CommandFramework.Commands.Class;

namespace CommandFramework.Tests.CommandAssembly
{
    [Command(Name = "class_command_1", Description = "Class command 1", GroupName = "ClassCommands")]
    public class ClassCommand1 : ICommandInstance
    {
        public void Run()
        {
        }
    }
}