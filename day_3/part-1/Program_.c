// See https://aka.ms/new-console-template for more information
var lines = File.ReadAllLines("example");

var length_of_one_line = lines[0].Length;

Console.WriteLine(length_of_one_line);

char[] tokens = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '0'];

var token_cords = new List<Cords>();
var symbol_cords = new List<Cords>();

for (int y = 0; y < lines.Length; y++)
{
    var line = lines[y];

    for (int x = 0; x < line.Length; x++)
    {
        var ch = line[x];
        if (tokens.Contains(ch))
        {
            token_cords.Add(new Cords(x, y, ch));
            continue;
        }

        if (ch != '.')
        {
            symbol_cords.Add(new Cords(x, y, ch));
            Console.WriteLine(ch);
            continue;

        }
    }

}

//Console.WriteLine(string.Join(',', symbol_cords));
var head = token_cords[0];
var tmp_part_cords = new List<Cords>
{
    head
};

var part_numbers = new List<PartNumber>();
for (int i = 1; i < token_cords.Count; i++)
{
    var curr = token_cords[i];
    if (head.x + 1 == curr.x) {
        head = curr;
        tmp_part_cords.Add(curr);
    } else {
        Console.WriteLine();
        part_numbers.Add(new PartNumber());
        tmp_part_cords = [];
    }
}

foreach (var symbol in symbol_cords)
{
    Console.WriteLine(string.Join(",", symbol.ReturnNeighbours()));

    foreach (var item in symbol.ReturnNeighbours())
    {
        if (token_cords.Contains(item))
        {

            // Discover All Neighbours:
        }

    }
    break;
}

/*
foreach (var symbol_idx in symbol_cords)
{
    var top_idx = symbol_idx - length_of_one_line;
    var bottom_idx = symbol_idx + length_of_one_line;
    var left_idx = -1;
    var right_idx = -1;


    // This means the symbol is NOT at the right edge:
    if ((symbol_idx + 1) % length_of_one_line != 0) 
    {
        right_idx = symbol_idx + 1;
    }

    // This means the symbol is NOT at the left edge
    if ((symbol_idx + 1) % length_of_one_line != 1) {
        left_idx = symbol_idx - 1;
    }

    Console.WriteLine($"Symbol at {symbol_idx} has neighbours"
     + $" of: ({top_idx}, {bottom_idx}, {right_idx}, {left_idx}) ");
    break;
}
*/

struct PartNumber(int number, List<Cords> cords)
{
    private int number = number;
    private List<Cords> cords = cords;

    public override string ToString()
    {
        return $"{number}: {string.Join(",", cords)}";
    }
}

struct Cords(int x, int y, char character)
{
    public int x = x;
    public int y = y;
    public char character = character;

    public readonly Cords[] ReturnNeighbours()
    {
        var self = new Cords(x, y, character);
        var right_neighbour = new Cords(x + 1, y, character);
        var left_neighbour = new Cords(x - 1, y, character);
        var top_neighbour = new Cords(x, y - 1, character);
        var bottom_neighbour = new Cords(x, y + 1, character);

        var top_left_corner = new Cords(x - 1, y - 1, character);
        var top_right_corner = new Cords(x + 1, y - 1, character);
        var bottom_left_corner = new Cords(x - 1, y + 1, character);
        var bottom_right_corner = new Cords(x + 1, y + 1, character);

        return
        [
            top_left_corner, top_neighbour, top_right_corner,
            left_neighbour, self, right_neighbour,
            bottom_left_corner, bottom_neighbour, bottom_right_corner
        ];
    }

    public override string ToString()
    {
        return $"({x}, {y})";
    }
};

struct Token(string token_str, List<Cords> cords)
{
    public string token_str = token_str;
    public List<Cords> cords = cords;
}