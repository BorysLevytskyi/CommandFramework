# CommandFramework
Command Line Interface library

### Nuget Package
https://www.nuget.org/packages/CommandFramework/1.0.0-alpha

### How it works

To be done. Little example from ToDoApp so far:
````c#
// In TaskManager.cs

[Command(Name = "task", Description = "Creates new task"
public void CreateTask(
	string title,
	[Parameter(Synonyms = "c")] bool completed = false,
	[Parameter(Synonyms = "t")] IEnumerable<string> tags = null)
{
    // Do stuff
}
````

````c#
void Main() 
{
	var manager = new TaskManager();
	var catalog = new CommandsCatalog();
	
	catalog.AddCommandsFrom(manager);
	catalog.AddHelpCommand().WithGroup("todo");
	catalog.AddExitCommand().WithGroup("todo");
	
	var dispatcher = new CommandDispatcher(catalog);
	dispatcher.StartDispatchingFromUserInput();
}
````
Input: `task "this task name" -c -t tag1 -t tag2`
