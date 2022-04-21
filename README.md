# UndoRedo

```csharp

public class Counter
{
    public int Value = 0;
}

public class IncrementVariable : IUndoRedoAction
{
    private readonly Counter _counter;
    public IncrementVariable(Counter counter)
    {
        _counter = counter;
    }

    public Do()
    {
        ++_counter.Value;
    }

    public Undo()
    {
        --_counter.Value;
    }
}

Counter counter = new Counter();
IUndoRedo undoRedo = UndoRedoFactory.Create();

undoRedo.Commit(new IncrementVariable(counter));
undoRedo.Commit(new IncrementVariable(counter));
undoRedo.Commit(new IncrementVariable(counter));

Console.WriteLine($"Counter: {counter.Value}"); // Counter: 3

undoRedo.Undo();
Console.WriteLine($"Counter: {counter.Value}"); // Counter: 2

undoRedo.Undo();
Console.WriteLine($"Counter: {counter.Value}"); // Counter: 1

undoRedo.Redo();
Console.WriteLine($"Counter: {counter.Value}"); // Counter: 2

undoRedo.Redo();
Console.WriteLine($"Counter: {counter.Value}"); // Counter: 3

// You can pass in actions directly to commit to avoid making classes
undoRedo.Commit(() => {++counter.Value}, () => {--counter.Value});

Console.WriteLine($"Counter: {counter.Value}"); // Counter: 4

undoRedo.Undo();
Console.WriteLine($"Counter: {counter.Value}"); // Counter: 3

// You can pass in a group of actions at once to execute in a single undo/redo command
undoRedo.Commit(new List<IAction>() {
    new IncrementVariable(counter),
    new IncrementVariable(counter),
    new IncrementVariable(counter)
});

Console.WriteLine($"Counter: {counter.Value}"); // Counter: 6

undoRedo.Undo();
Console.WriteLine($"Counter: {counter.Value}"); // Counter: 3
```
