# CommandFramework
Command Line Interface library

### Nuget Package
https://www.nuget.org/packages/CommandFramework/1.0.0-alpha

### How it works

To be done. Little example from ToDoApp so far:
````c#
[Command(Name = "task", Description = "Creates new task"
public void CreateTask(
			string title,
			[Parameter(Synonyms = "c")] bool completed = false,
			[Parameter(Synonyms = "t")] IEnumerable<string> tags = null)
		{
		    // Do stuff
		}
````
