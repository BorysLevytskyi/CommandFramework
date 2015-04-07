using System;

namespace CommandFramework.Commands.Input
{
	public static class InputValueConverter
	{
		public static object Convert(string value, Type type)
		{
			var valueType = Nullable.GetUnderlyingType(type);
			if (valueType != null)
			{
				return value == null
					? Activator.CreateInstance(type)
					: ChangeNonNullableType(value, valueType);
			}

			return ChangeNonNullableType(value, type);
		}

		private static object ChangeNonNullableType(string value, Type type)
		{
			if (type == typeof (Boolean) && string.IsNullOrEmpty(value))
			{
				return true;
			}

			return System.Convert.ChangeType(value, type);
		}
	}
}