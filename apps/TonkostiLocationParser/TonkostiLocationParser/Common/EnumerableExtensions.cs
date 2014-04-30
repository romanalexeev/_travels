using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<T> InsertRange<T>(this IEnumerable<T> list, params T[] items)
		{
			if (list == null)
				throw new ArgumentNullException("list");

			List<T> result = new List<T>();
			result.AddRange(items);
			result.AddRange(list);

			return result;
		}

		public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
		{
			if (enumerable == null)
				throw new ArgumentNullException("enumerable");

			ObservableCollection<T> result = new ObservableCollection<T>();

			lock (enumerable)
			{
				foreach (T item in enumerable)
				{
					result.Add(item);
				}
			}

			return result;
		}

		public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> enumerable)
		{
			if (collection == null)
				throw new ArgumentNullException("collection");

			if (enumerable == null)
				throw new ArgumentNullException("enumerable");

			lock (collection)
			{
				foreach (T item in enumerable)
				{
					if (item == null)
						throw new ArgumentException("enumerable contains nulls");

					collection.Add(item);
				}
			}
		}

		public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
		{
			if (enumerable == null)
				throw new ArgumentNullException("enumerable");

			lock (enumerable)
			{
				foreach (T item in enumerable)
				{
					action(item);
				}
			}

			return enumerable;
		}

		public static async Task<IEnumerable<T>> ForEachAsync<T>(this IEnumerable<T> enumerable, Func<T, Task> action)
		{
			if (enumerable == null)
				throw new ArgumentNullException("enumerable");

			foreach (T item in enumerable)
			{
				await action(item);
			}

			return enumerable;
		}

		public static void Reload<T>(this ICollection<T> collection, IEnumerable<T> enumerable)
		{
			if (collection == null)
				throw new ArgumentNullException("collection");

			if (enumerable == null)
				throw new ArgumentNullException("enumerable");

			collection.Clear();
			collection.AddRange(enumerable);
		}
	}
}
