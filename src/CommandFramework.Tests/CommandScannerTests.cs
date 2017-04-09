using System.Collections.Generic;
using CommandFramework.Commands;
using CommandFramework.Dispatcher;
using CommandFramework.Tests.CommandAssembly;
using FluentAssertions;
using NUnit.Framework;

namespace CommandFramework.Tests
{
    public class TypeCommandScannerTests
    {
        [Test]
        public void Should_find_all_class_commands()
        {
            TypeCommandScanner.FindClassCommands(typeof(ClassCommand1).Assembly)
                .ShouldAllBeEquivalentTo(
                    new object[]
                    {
                        new
                        {
                            Name = "class_command_1",
                            IsDefault = false,
                            Group = "ClassCommands",
                            Description = "Class command 1"
                        },
                        new
                        {
                            Name = "class_command_2",
                            IsDefault = false,
                            Group = "ClassCommands",
                            Description = "Class command 2"
                        }
                    });
        }
    }
}