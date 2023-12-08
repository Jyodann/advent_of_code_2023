var file = File.ReadAllLines("input");

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

Console.WriteLine(LocationFind());
int LocationFind()
{
    var steps = 0;
    var stop = false;
    while (!stop)
    {
        foreach (var instruction in instructions)
        {
            var test = true;
            for (int i = 0; i < locations.Count; i++)
            {
                if (instruction == 'L') {
                    locations[i] = map[locations[i]].LeftNode;
                }   else {
                    locations[i] = map[locations[i]].RightNode;
                }
            } 
            steps++;
            for (int i = 0; i < locations.Count; i++)
            {
                var loc = locations[i];
                if (loc.IndexOf('Z') != 2) {
                    test = false;
                }
            }

            if (test) {
                return steps;
            }
        }
    }
    return -1;
}


struct Path(string LeftNode, string RightNode)
{
    public string LeftNode = LeftNode;

    public string RightNode = RightNode;
}