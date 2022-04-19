using test.application;

using committed;

var committed = new Committed();
ObjectWrapper<Box>? box = null;

while (true)
{
    var key = Console.ReadKey();
    Console.WriteLine();
    switch (key.Key)
    {
        case ConsoleKey.C:
            if (box == null)
            {
                var action = new CreateBoxAction(1, 1);
                committed.Commit(action);
                box = action.BoxWrapper;
            }
            else
            {
                Console.WriteLine("Box is already created, move it with m or delete it with d");
            }
            break;
        case ConsoleKey.D:
            if (box != null)
            {
                committed.Commit(new DeleteBoxAction(box));
            }
            else
            {
                Console.WriteLine("Box is not yet created, create it first by pressing c");
            }
            break;
        case ConsoleKey.M:
            if (box != null)
            {
                Console.Write("Enter x,y: ");
                string input = Console.ReadLine() ?? "";
                var tokens = input.Split(",");
                float x = float.Parse(tokens[0]);
                float y = float.Parse(tokens[1]);
                committed.Commit(new MoveBoxAction(box, x, y));
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

    if (box != null)
    {
        Console.WriteLine($"Box is at position {box.Object.Transform2D.X}:{box.Object.Transform2D.Y}");
    }
}
