using System;

namespace CommandFramework.Parsing
{
	public class ParseException : Exception
	{
		public ParseException(string input, int index, string message) : base(message)
		{
		}

		public ParseException(string message): this (null, -1, message)
		{
		}
	}
}