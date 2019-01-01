using CommandFramework.Commands;
using CommandFramework.Commands.Class;
using CommandFramework.Tests.CommandAssembly;
using FluentAssertions;
using NUnit.Framework;

namespace CommandFramework.Tests
{
    [TestFixture]
    public class ClassCommandFactoryTests
    {
        [Test]
        public void Should_create_class_command_from_type()
        {
            ClassCommandFactory.CreateFromType(typeof(ClassCommand1))
                .Should().BeEquivalentTo(new
                {
                    Name = "class_command_1",
                    IsDefault = false,
                    Group = "ClassCommands",
                    Description = "Class command 1"
                });
        }

        [Test]
        public void Should_create_class_command_from_generic_type()
        {
            ClassCommandFactory.Create<ClassCommand1>()
                .Should().BeEquivalentTo(new
                {
                    Name = "class_command_1",
                    IsDefault = false,
                    Group = "ClassCommands",
                    Description = "Class command 1"
                });
        }
    }
}