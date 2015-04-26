using System;
using System.Collections.Generic;
using CommandFramework.Annotation;
using FluentAssertions;
using NUnit.Framework;

namespace CommandFramework.Tests
{
	[TestFixture]
	public class IntegrationTest
	{
		private IntegrationTest _inst;
		private CommandDispatcher _dispatcher;

		[TestFixtureSetUp]
		public void FixtureSetup()
		{
			_inst = new IntegrationTest();

			var catalog = new CommandsCatalog();
			catalog.AddCommandsFrom<IntegrationTest>();
			catalog.AddCommandsFrom(_inst);

			_dispatcher = new CommandDispatcher(catalog);
		}

		[Test]
		public void TestStaticMethodInvocation()
		{
			_dispatcher.DispatchCommand("static -intP 10 -stringP test -dateP 2015-01-01 -boolP -colP 2 -colP 3 -colP 4");
		}

		[Test]
		public void TestInstanceMethodInvocation()
		{
			_dispatcher.DispatchCommand("inst -intP 10 -stringP test -dateP 2015-01-01 -boolP -colP 2 -colP 3 -colP 4");
		}

		[Command(Name = "inst")]
		public void InstanceAssert(int intP, string stringP, DateTime dateP, bool boolP, IEnumerable<int> colP)
		{
			StaticAssert(intP, stringP, dateP, boolP, colP);
		}

		[Command(Name = "static")]
		public static void StaticAssert(int intP, string stringP, DateTime dateP, bool boolP, IEnumerable<int> colP)
		{
			intP.Should().Be(10);
			stringP.Should().Be("test");
			dateP.Should().Be(new DateTime(2015, 1, 1));
			colP.ShouldAllBeEquivalentTo(new[] { 2, 3, 4});
		}
	}
}