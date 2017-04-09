using CommandFramework.Annotation;
using CommandFramework.Commands.Class;

namespace CommandFramework.Tests.CommandAssembly
{
    [Command(Name = "class_command_2", Description = "Class command 2", GroupName = "ClassCommands")]
    public class ClassCommand2 : ICommandInstance
    {
        public void Run()
        {
        }
    }
}