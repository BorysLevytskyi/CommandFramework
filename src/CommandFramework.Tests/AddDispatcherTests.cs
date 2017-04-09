using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace CommandFramework.Tests
{
    [TestFixture]
    public class AddDispatcherTests
    {
        public void When_two_commands_have_the_same_names_should_pick_the_first_one()
        {
            var catalog = new CommandsCatalog();
            int value = 0;
            catalog.AddCommand(() => value = 1).WithName("cmd");
            catalog.AddCommand(() => value = 2).WithName("cmd");
            
            var dispacther = new CommandDispatcher(catalog);
            dispacther.DispatchCommand("cmd");

            value.Should().Be(1);
        }
    }
}
