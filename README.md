# CommandFramework
Command Line Interface library

### Nuget Package
https://www.nuget.org/packages/CommandFramework/

### Documentation
[https://github.com/BorysLevytskyi/CommandFramework/wiki/Documentation]

### How it works
Little example from ToDoApp example application:
````c#
// In TaskManager.cs

[Command(Name = "task", Description = "Creates new task"
public void CreateTask(
	string title,
	[Parameter(Synonyms = "c")] bool completed = false,
	[Parameter(Synonyms = "t")] IEnumerable<string> tags = null)
{
    // Create task
}

[Command("List created all tasks", Name = "list")]
public void ListTasks([Parameter(Synonyms = "c")] bool completed = false)
{
	// List tasks...
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
Create task: `task "this task name" -c -t tag1 -t tag2`
List completed tasks: 'list -c'
Display help: 'help'
