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
		private IntegrationTest inst;
		private CommandDispatcher dispatcher;

		[TestFixtureSetUp]
		public void FixtureSetup()
		{
			inst = new IntegrationTest();

			var catalog = new CommandsCatalog();
			catalog.AddCommandsFrom<IntegrationTest>();
			catalog.AddCommandsFrom(inst);

			dispatcher = new CommandDispatcher(catalog);
		}

		[Test]
		public void GenericTestCommands()
		{
			dispatcher.DispatchCommand("static -intP 10 -stringP test -dateP 2015-01-01 -boolP -colP 2 -colP 3 -colP 4");
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