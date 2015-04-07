using System;
using System.Collections.Generic;
using System.Linq;
using CommandFramework;
using CommandFramework.Catalog;
using CommandFramework.Annotation;
using CommandFramework.Utils;

namespace ToDoApp
{
	public class Program
	{
		private static void Main(string[] args)
		{
			var manager = new TaskManager();
			Run(manager);
		}

		private static void Run(TaskManager manager)
		{
			var catalog = new CommandsCatalog();
			catalog.AddCommandsFrom(manager);

			catalog.AddHelpCommand().WithGroup("todo");
			catalog.AddExitCommand().WithGroup("todo");

			catalog.AddCommandsFrom<Debugging>();

			var dipstacher = new CommandDispatcher(catalog);

			dipstacher.DispatchCommand("task \"check example app\" -c -t CommandFramework -t Example");

			dipstacher.StartDispatchingFromUserInput();
		}
	}

	[CommandGroup("todo")]
	public class TaskManager
	{
		private readonly List<Task> _tasks = new List<Task>();

		[Command("List created all tasks", Name = "list")]
		public void ListTasks([Parameter(Synonyms = "c")] bool completed = false)
		{
			if (!_tasks.Any())
			{
				Console.WriteLine("No tasks");
			}

			foreach (var task in _tasks)
			{
				var color = task.Completed ? ConsoleColor.Green : ConsoleColor.Gray;

				ConsoleEx.Write(color, task.Completed ? "[x]" : "[ ]");
				ConsoleEx.Write(color, " {0}", task.Title);
				ConsoleEx.WriteLine(color, " {0}", string.Join(" ", task.Tags.Select(t => string.Format("[{0}]", t))));
			}
		}

		[Command(Name = "task")]
		public void CreateTask(
			[Parameter] string title,
			[Parameter(Synonyms = "c")] bool completed = false,
			[Parameter(Synonyms = "t")] IEnumerable<string> tags = null)
		{
			var task = new Task
			{
				Title = title,
				Completed = completed,
				Tags = new List<string>(tags ?? Enumerable.Empty<string>())
			};

			_tasks.Add(task);

			Console.WriteLine("Task created");

			ListTasks();
		}
	}

	public class Task
	{
		public string Title { get; set; }

		public bool Completed { get; set; }

		public List<string> Tags { get; set; }
	}

}