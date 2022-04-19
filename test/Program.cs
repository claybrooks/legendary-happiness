using test.application;

using committed;
using test.application.actions;

var committed = new Committed();
var boxIdx = 0;
Dictionary<int, BoxElement> boxElements = new() 
{
    {
        boxIdx, new BoxElement(committed, new Box(new Transform2D(1, 1)))
    }
};

while (true)
{
    var box = boxElements[boxIdx];
    var key = Console.ReadKey();
    Console.WriteLine();
    switch (key.Key)
    {
        case ConsoleKey.C:
            ++boxIdx;
            boxElements.Add(boxIdx, new BoxElement(committed, new Box(new Transform2D(1,1))));
            break;
        case ConsoleKey.D:
            box.Delete();
            break;
        case ConsoleKey.M:
            Console.Write("Enter x,y: ");
            var input = "";
            while (input.Count(c => c == ',') != 1)
            {
                input = Console.ReadLine() ?? "";
            }
            var tokens = input.Split(",");
            float x = float.Parse(tokens[0]);
            float y = float.Parse(tokens[1]);
            box.Move(new Transform2D(x, y));

            break;
        case ConsoleKey.Z:
            committed.Undo();
            break;
        case ConsoleKey.Y:
            committed.Redo();
            break;
        case ConsoleKey.H:
            box.HideInfo();
            break;
        case ConsoleKey.S:
            box.ShowInfo();
            break;
        case ConsoleKey.I:
            var idx = -1;
            while (idx < 0)
            {
                Console.Write("Choose Box Key: ");
                var boxIdxString = Console.ReadLine();
                int.TryParse(boxIdxString, out idx);
                if (!boxElements.ContainsKey(idx))
                {
                    Console.WriteLine("Invalid Box Key");
                    idx = -1;
                }
            }
            boxIdx = idx;
            break;
    }

    Console.WriteLine(BoxInfos(boxElements));
}

string BoxInfos(IReadOnlyDictionary<int, BoxElement> boxes)
{
    var data = "{\n";
    foreach ((var key, var box) in boxes)
    {
        data += $"\t{{{key}: {box.Meta}}},\n";
    }
    data += "}";
    return data;
}
