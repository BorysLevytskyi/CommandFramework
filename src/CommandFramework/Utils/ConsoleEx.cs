using System;

namespace CommandFramework.Utils
{
	public static class ConsoleEx
	{
		[StringFormatMethod("message")]
		public static void Write(ConsoleColor? color = null, string message = null, params object[] messageArgs)
		{
			Console.ForegroundColor = color ?? Console.ForegroundColor;
			if (messageArgs == null || messageArgs.Length == 0)
			{
				Console.Write(message);
			}
			else
			{
				Console.Write(message, messageArgs);
			}
			
			Console.ResetColor();
		}

		[StringFormatMethod("message")]
		public static void WriteLine(ConsoleColor? color = null, string message = null, params object[] messageArgs)
		{
			Console.ForegroundColor = color ?? Console.ForegroundColor;
			if (messageArgs == null || messageArgs.Length == 0)
			{
				Console.WriteLine(message);
			}
			else
			{
				Console.WriteLine(message, messageArgs);
			}
			Console.ResetColor();
		}

		[StringFormatMethod("message")]
		public static void WriteGreen(string message = null, params object[] messageArgs)
		{
			Write(ConsoleColor.Green, message, messageArgs);
		}
	}
}