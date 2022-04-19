using committed;
using test.application;

var committed = new Committed();
var boxIdx = 0;
Dictionary<int, BoxElement> boxElements = new() 
{
    {
        boxIdx, new BoxElement(committed, new Box(new Transform2D(1, 1)))
    }
};

PrintBoxData();

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

    PrintBoxData();
}

string BoxInfos(IReadOnlyDictionary<int, BoxElement> boxes, int selected)
{
    int i = 0;
    var data = "{\n";
    foreach ((var key, var box) in boxes)
    {
        var prepend = "  ";
        if (i == selected)
        {
            prepend = "* ";
        }

        var line = $"{prepend}{{{key} => {box.Meta}}},";
        var leftOver = Console.WindowWidth - line.Length;
        line += new string (' ', leftOver-1);
        line += '\n';
        data += line;
        ++i;
    }
    data += "}";
    return data;
}

void PrintBoxData()
{
    Console.CursorVisible = false;
    Console.SetCursorPosition(0, 0);
    Console.WriteLine(BoxInfos(boxElements, boxIdx));
    int currentPosition = Console.CursorTop;

    Console.WriteLine(new string(' ', Console.WindowWidth));
    Console.WriteLine(new string(' ', Console.WindowWidth));
    Console.WriteLine(new string(' ', Console.WindowWidth));
    Console.CursorTop = currentPosition;
    Console.CursorVisible = true;
}