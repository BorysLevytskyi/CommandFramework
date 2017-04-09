using System.Linq;
using CommandFramework.Annotation;
using CommandFramework.Dispatcher;
using FluentAssertions;
using NUnit.Framework;

namespace CommandFramework.Tests.MethodCommand
{
	[TestFixture]
	public class TypeCommandScannerTests
	{
		[Test]
		public void ShouldFindStaticCommands()
		{
			var staticCommands = TypeCommandScanner.FindStaticMethodCommands(typeof (Specimen));
			staticCommands.Select(c => c.Name).ShouldAllBeEquivalentTo(new [] { "static1", "static2" });
		}

		[Test]
		public void ShouldFindInstanceCommands()
		{
			var staticCommands = TypeCommandScanner.FindMethodCommandsOnInstance(new Specimen());
			staticCommands.Select(c => c.Name).ShouldAllBeEquivalentTo(new[] { "inst1", "inst2" });
		}

		public class Specimen
		{
			[Command]
			public static void Static1()
			{
			}

			[Command]
			public static void Static2()
			{
			}

			[Command]
			public void Inst1()
			{
			}

			[Command]
			public void Inst2()
			{
			}
		}
	}
}