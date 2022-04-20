﻿using committed;
using test.application;

Console.CursorVisible = false;

var committed = new Committed();
var boxIdx = 0;
var boxKey = boxIdx;
Dictionary<int, BoxElement> boxElements = new() 
{
    {
        boxIdx, new BoxElement(committed, new Box(new Transform2D(1, 1)))
    }
};

var box = boxElements[boxKey];
PrintBoxData(box);

while (true)
{
    var key = Console.ReadKey();
    Console.WriteLine();
    switch (key.Key)
    {
        case ConsoleKey.A:
            box.Create();
            ++boxKey;
            if (boxKey > boxIdx) { boxKey = boxIdx; }
            break;
        case ConsoleKey.C:
            ++boxIdx;
            boxElements.Add(boxIdx, new BoxElement(committed, new Box(new Transform2D(1, 1))));
            break;
        case ConsoleKey.D:
            box.Delete();
            --boxKey;
            if (boxKey < 0) { boxKey = 0; }
            break;
        case ConsoleKey.M:
            Console.Write("Enter x,y: ");
            var input = "";
            while (input.Count(c => c == ',') != 1)
            {
                input = Console.ReadLine() ?? "";
            }
            var tokens = input.Split(",");
            int x = int.Parse(tokens[0]);
            int y = int.Parse(tokens[1]);
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
            boxKey = idx;
            break;
        case ConsoleKey.UpArrow:
            box.MoveUpOne();
            break;
        case ConsoleKey.DownArrow:
            box.MoveDownOne();
            break;
        case ConsoleKey.LeftArrow:
            box.MoveLeftOne();
            break;
        case ConsoleKey.RightArrow:
            box.MoveRightOne();
            break;
    }

    box = boxElements[boxKey];
    PrintBoxData(box);
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

void PrintBoxData(BoxElement selectedBox)
{
    int furthestPosition = Console.CursorTop;

    Console.SetCursorPosition(0, 0);
    Console.WriteLine(GetHeader());
    Console.WriteLine(BoxInfos(boxElements, boxKey));
    int currentPosition = Console.CursorTop;
    while (currentPosition < furthestPosition)
    {
        Console.WriteLine(new string(' ', Console.WindowWidth));
        --furthestPosition;
    }
    Console.CursorTop = currentPosition;
    PrintUIElements(boxElements.Values, selectedBox, currentPosition);
    Console.SetCursorPosition(0, Console.CursorTop);
}

void PrintUIElements(IEnumerable<BoxElement> boxes, BoxElement selectedBox, int offset)
{
    int furthest = offset;

    // All boxes are 4x4
    foreach (BoxElement boxElement in boxes)
    {
        if (!boxElement.Deleted)
        {
            if (Object.ReferenceEquals(boxElement, selectedBox))
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            for(int i = 0; i < 5; ++i)
            {
                for(int j = 0; j < 3; ++j)
                {
                    var y = offset + boxElement.Position.Y + j;
                    Console.SetCursorPosition(boxElement.Position.X + i, y);
                    Console.Write("*");

                    if (furthest < y)
                    {
                        furthest = y;
                    }
                }
            }
        }
    }
    Console.ForegroundColor = ConsoleColor.White;
    Console.SetCursorPosition(0, furthest);
}

string GetHeader()
{
    return 
$@"
***********************************************************
*   C: Create
*   A: Allocate
*   D: Delete
*   M: Move
*   I: Switch Index
*   H: Hide UI
*   S: Show UI
*   Z: Undo
*   Y: Redo
***********************************************************
";
}