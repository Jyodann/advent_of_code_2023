using System;

var games = File.ReadAllLines("input");

string[] game_types = ["red", "green", "blue"];

var num_red = 12;
var num_green = 13;
var num_blue = 14;
var possible_matches = new List<int>();

for (int i = 0; i < games.Length; i++)
{
    var game_str = games[i];
    var game_info = game_str.Substring(game_str.IndexOf(':') + 2);

    var combinations = game_info.Split(";");
    //Console.WriteLine(game_info);

    int max_red = 0;
    int max_green = 0;
    int max_blue = 0;
    foreach (var item in combinations)
    {
        var cubes = item.Split(",");

        foreach (var cube in cubes)
        {
            //Console.WriteLine(cube);
            var num = returnNumber(cube);
            //Console.WriteLine(num);
            if (cube.Contains("red", StringComparison.CurrentCulture))
            {
                // Red:
            
                max_red = int.Max(max_red, num);
            }
            else if (cube.Contains("green"))
            {
                // Green:
                max_green = int.Max(max_green, num);
            }
            else
            {
                // Blue:
                max_blue = int.Max(max_blue, num);
            }

        }
        
    }

  // Console.WriteLine($"Max Red: {max_red} Max Green: {max_green} Max Blue: {max_blue}");
    possible_matches.Add(max_blue * max_green * max_red);
   
}

Console.WriteLine(possible_matches.Sum());

int returnNumber(string input) {
    input = input.Replace("red", "");
    input = input.Replace("green", "");
    input = input.Replace("blue", "");

    return int.Parse(input);
}
