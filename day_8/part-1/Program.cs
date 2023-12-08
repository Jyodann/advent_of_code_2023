using Microsoft.VisualBasic;

Console.WriteLine("Hello, World!");

var file = File.ReadAllLines("example");

var instructions = file[0];

Console.WriteLine(instructions);

var map = new Dictionary<string, Path>();

foreach (var item in file[2..])
{
    var split = item.Split('=');

    var leftRightNodes = split[1]
        .Replace("(", string.Empty)
        .Replace(")", string.Empty)
        .Split(',');

    var leftNode = leftRightNodes[0].Trim();
    var rightNode = leftRightNodes[1].Trim();
    var location = split[0].Trim();
    map.Add(location, new Path(leftNode, rightNode));
}

var locations = new List<string>();

foreach (var key in map)
{
    if (key.Key.IndexOf('A') == 2)
    {
        locations.Add(key.Key);
    }
}

Console.WriteLine(string.Join(", ", locations));

var first_loc = "AAA";
var steps = 0;
var stop = false;
while (!stop)
{
    foreach (var instruction in instructions)
    {
        if (instruction == 'L')
        {

            first_loc = map[first_loc].LeftNode;
        }
        else
        {
            first_loc = map[first_loc].RightNode;
        }
        steps++;

        if (first_loc == "ZZZ") {
            stop = true;
        }
    }
}

Console.WriteLine(steps);

struct Path(string LeftNode, string RightNode)
{
    public string LeftNode = LeftNode;

    public string RightNode = RightNode;
}