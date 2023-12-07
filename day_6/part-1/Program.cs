var file = File.ReadAllLines("input");

var times = file[0]
    .Replace("Time: ", string.Empty)
    .Trim()
    .Split(' ')
    .Where(x => x != string.Empty)
    .Select(int.Parse).ToList();
var distances = file[1]
    .Replace("Distance: ", string.Empty)
    .Trim()
    .Split(' ')
    .Where(x => x != string.Empty)
    .Select(int.Parse).ToList();

var margin = 1L;
for (int i = 0; i < times.Count; i++)
{

    var max_time = times[i];
    var dist_to_beat = distances[i];
    var min_time_to_beat_dist = 0;
    var max_time_to_beat_dist = 0;
    // Search from small to big
    for (int j = 0; j < max_time; j++)
    {
        var time_for_travelling = max_time - j;
        var dis_travelled = j * time_for_travelling;

        if (dis_travelled > dist_to_beat)
        {
            min_time_to_beat_dist = j;
            break;
        }
        //Console.WriteLine(dis_travelled);
    }

    for (int j = max_time; j > 0; j--)
    {
        var time_for_travelling = max_time - j;
        var dis_travelled = j * time_for_travelling;

        if (dis_travelled > dist_to_beat)
        {
            max_time_to_beat_dist = j;
            break;
        }

    }
    Console.WriteLine($"Min: {min_time_to_beat_dist}" + 
    $"Max: {max_time_to_beat_dist}" +  
    $"Races to Win: {max_time_to_beat_dist - min_time_to_beat_dist + 1}");
    Console.WriteLine();
    
    margin *= max_time_to_beat_dist - min_time_to_beat_dist + 1;
        //Console.WriteLine(string.Join(",", times));

}

Console.WriteLine(margin);