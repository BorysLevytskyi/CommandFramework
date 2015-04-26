using System.Linq;
using CommandFramework.Parsing;
using FluentAssertions;
using NUnit.Framework;

namespace CommandFramework.Tests.Parsers
{
	[TestFixture]
	public class CommandLineCommandParserTests
	{
		[Test]
		public void ShouldSupportDoubleHyphens()
		{
			var parser = new CommandLineCommandParser();
			var cmd = parser.Parse("test --hello --test");
			cmd.Parameters.Select(p => p.Name).ShouldAllBeEquivalentTo(new[] { "hello", "test" });
		}

		[Test]
		public void ShouldSupportHyphensInMiddle()
		{
			var parser = new CommandLineCommandParser();
			var cmd = parser.Parse("test --hello-test --test");
			cmd.Parameters.Select(p => p.Name).ShouldAllBeEquivalentTo(new[] { "hello-test", "test" });
		}
	}
}