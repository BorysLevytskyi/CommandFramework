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
			var catalog = new CommandsCatalog();
			catalog.AddCommandsFrom(manager);

			catalog.AddHelpCommand().WithGroup("todo");
			catalog.AddExitCommand().WithGroup("todo");

			var dispatcher = new CommandDispatcher(catalog);
			catalog.AddCommandsFrom(new Debugging(dispatcher));

			dispatcher.DispatchCommand("\"check example app\" -c -t CommandFramework -t Example");

			dispatcher.StartDispatchingFromUserInput();
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
				return;
			}

			Console.WriteLine("Tasks");

			int n = 1;
			foreach (var task in _tasks)
			{
				if (completed && !task.Completed)
				{
					continue;
				}

				var color = task.Completed ? ConsoleColor.Green : ConsoleColor.Gray;

				ConsoleEx.Write(color, "{0}. ", n++);
				ConsoleEx.Write(color, task.Completed ? "[x]" : "[ ]");
				ConsoleEx.Write(color, " {0}", task.Title);
				ConsoleEx.WriteLine(color, " {0}", string.Join(" ", task.Tags.Select(t => string.Format("[{0}]", t))));
			}
		}

		[Command(Name = "task", IsDefault = true)]
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
			Console.WriteLine();

			ListTasks();
		}

		[Command]
		public void Done(int taskNum = 1)
		{
			if (taskNum < 1 || taskNum > _tasks.Count)
			{
				ConsoleEx.WriteLine(ConsoleColor.Red, "No task with number: {0}", taskNum);
			}

			var task = _tasks[taskNum-1];
			task.Completed = true;

			ConsoleEx.WriteLine(ConsoleColor.Green, "Completed: {0}", task.Title);
			Console.WriteLine();

			ListTasks();
		}
	}
}