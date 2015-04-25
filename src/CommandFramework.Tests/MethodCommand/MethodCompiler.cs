using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandFramework.Commands.Method;
using FluentAssertions;
using NUnit.Framework;

namespace CommandFramework.Tests.MethodCommand
{
	[TestFixture]
	public class MethodCompilerTest
	{
		[Test]
		public void ShouldCompileStaticMethod()
		{
			MethodInfo method = typeof (MethodCompilerTest).GetMethod("TestStatic");
			var action = MethodCompiler.CompileStaticMethodInvocation(method);
			action(null, new object[] { 1, DateTime.MaxValue, "test"});
		}

		[Test]
		public void ShouldCompileInstanceMethod()
		{
			MethodInfo method = typeof(MethodCompilerTest).GetMethod("TestInstance");
			var inst = new MethodCompilerTest();
			var action = MethodCompiler.CompileInstanceMethodInvocation(method);
			action(inst, new object[] { 2, DateTime.MaxValue.AddDays(-1), "test2" });
		}

		public static void TestStatic(int a1, DateTime a2, string a3)
		{
			a1.Should().Be(1);
			a2.Should().Be(DateTime.MaxValue);
			a3.Should().Be("test");
		}

		public void TestInstance(int a1, DateTime a2, string a3)
		{
			a1.Should().Be(2);
			a2.Should().Be(DateTime.MaxValue.AddDays(-1));
			a3.Should().Be("test2");
		}
	}
}
