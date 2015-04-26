using System.Collections.Generic;

namespace ToDoApp
{
	public class Task
	{
		public string Title { get; set; }

		public bool Completed { get; set; }

		public List<string> Tags { get; set; }
	}
}