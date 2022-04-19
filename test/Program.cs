using test.application;

using committed;

var committed = new Committed();
ObjectWrapper<Box>? boxWrapper = null;

while (true)
{
    var key = Console.ReadKey();
    Console.WriteLine();
    switch (key.Key)
    {
        case ConsoleKey.C:
            if (boxWrapper == null || boxWrapper.Object.Deleted)
            {
                var action = new CreateBoxAction(1, 1);
                committed.Commit(action);
                boxWrapper = action.BoxWrapper;
            }
            else
            {
                Console.WriteLine("Box is already created, move it with m or delete it with d");
            }
            break;
        case ConsoleKey.D:
            if (boxWrapper != null && !boxWrapper.Object.Deleted)
            {
                committed.Commit(new DeleteBoxAction(boxWrapper));
            }
            else
            {
                Console.WriteLine("Box is not yet created, create it first by pressing c");
            }
            break;
        case ConsoleKey.M:
            if (boxWrapper != null && !boxWrapper.Object.Deleted)
            {
                Console.Write("Enter x,y: ");
                var input = "";
                while (input.Count(c => c == ',') != 1)
                {
                    input = Console.ReadLine() ?? "";
                }
                var tokens = input.Split(",");
                float x = float.Parse(tokens[0]);
                float y = float.Parse(tokens[1]);
                committed.Commit(new MoveBoxAction(boxWrapper, x, y));
            }
            else
            {
                Console.WriteLine("Box is not yet created, create it first by pressing c");
            }
            break;
        case ConsoleKey.Z:
            committed.Undo();
            break;
        case ConsoleKey.Y:
            committed.Redo();
            break;
    }

    if (boxWrapper != null)
    {
        Console.WriteLine($"Box is at position {boxWrapper.Object.Transform2D.X}:{boxWrapper.Object.Transform2D.Y}");
    }
}
