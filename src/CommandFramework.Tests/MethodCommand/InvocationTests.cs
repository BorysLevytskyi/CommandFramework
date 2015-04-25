using System;
using System.Linq;
using CommandFramework.Commands.Input;
using CommandFramework.Commands.Method;
using FluentAssertions;
using NUnit.Framework;

namespace CommandFramework.Tests.MethodCommand
{
	[TestFixture]
	public class InvocationTests
	{
		[Test]
		public void ShouldPreserveException()
		{
			var cmd = MethodCommandFactory.Create(Cmd);
			Action act = () => cmd.Execute(Enumerable.Empty<IParameterInput>());
			act.ShouldThrowExactly<ApplicationException>().And.Message.Should().Be("test");
		}

		public static void Cmd()
		{
			throw new ApplicationException("test");
		}
	}
}