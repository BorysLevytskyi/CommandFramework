using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CommandFramework.Reflection
{
	internal static class CollectionConstructor
	{
		public static object CreateInstance(Type requestedType, object enumerable)
		{
			if (!IsSupportedCollectionType(requestedType))
			{
				throw new InvalidOperationException(string.Format("Given type doesn't support collection assignment: {0}", requestedType));
			}

			var cast = enumerable as IEnumerable;

			if (cast == null)
			{
				throw new InvalidOperationException("Given values is not an enumerable");
			}

			return requestedType.IsArray ? CreateArray(requestedType.GetElementType(), cast) : CreateList(requestedType.GenericTypeArguments.First(), cast);
		}

		public static bool IsSupportedCollectionType(Type requestedType)
		{
			if (requestedType == typeof(string))
			{
				return false;
			}

			return requestedType.IsArray || IsKnownGenericType(requestedType);
		}

		public static Type GetElementType(Type requestedType)
		{
			if (!typeof(IEnumerable).IsAssignableFrom(requestedType))
			{
				throw new InvalidOperationException(string.Format("Given type is not enumerable type: {0}", requestedType));
			}

			if (requestedType.IsArray)
			{
				return requestedType.GetElementType();
			}

			if (!requestedType.IsGenericType)
			{
				return typeof (object);
			}

			return requestedType.GetGenericArguments().First();
		}

		private static bool IsKnownGenericType(Type requestedType)
		{
			if (!requestedType.IsGenericType)
			{
				return false;
			}

			var genericTypeDefinition = requestedType.GetGenericTypeDefinition();
			var isKnownGenericType = KnownListTypes.Any(listType => listType == genericTypeDefinition);
			return isKnownGenericType;
		}

		private static object CreateList(Type requestedType, IEnumerable enumerable)
		{
			var listType = typeof (List<>).MakeGenericType(requestedType);
			var array = CreateArray(requestedType, enumerable);

			object[] args = {array};
			return Activator.CreateInstance(listType, args);
		}

		private static object CreateArray(Type requestedType, IEnumerable enumerable)
		{
			var list = enumerable.Cast<object>().ToList();

			Array arr = Array.CreateInstance(requestedType, list.Count);

			for (int i = 0; i < list.Count; i++)
			{
				arr.SetValue(list[i], i);
			}

			return arr;
		}

		private static readonly List<Type> KnownListTypes = new List<Type>
		{
			typeof (List<>),
			typeof (IList<>),
			typeof (IEnumerable<>),
			typeof (ICollection<>)
		};
	}
}
