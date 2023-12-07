// See https://aka.ms/new-console-template for more information
// using System.Numerics;
using System.Text;

Console.WriteLine("Hello, World!");

var lines = File.ReadAllLines("input");

var seeds_str = lines[0];

var seeds_arr = seeds_str[(seeds_str.IndexOf(':') + 1)..]
    .Trim()
    .Split(' ')
    .Select(long.Parse).ToList();

var new_seed_numbers = new List<long>();


//Console.WriteLine(string.Join(',', seeds_arr));


var str = new StringBuilder();
var transformer = new List<Map>();

foreach (var line in lines.Skip(2))
{

    if (!line.Contains(':'))
    {
        if (line == string.Empty)
        {
            transformer.Add(new Map(str.ToString()));
            str.Clear();
        }
        str.AppendLine(line);
        //Console.WriteLine(line);
    }
}

transformer.Add(new Map(str.ToString()));

var smallest_location = long.MaxValue;
Parallel.For(0, seeds_arr.Count / 2, (int i) => {
    var new_i = i * 2;
    var seed_start_range = seeds_arr[new_i];
    var seed_range = seeds_arr[new_i + 1];

    Console.WriteLine($"{seed_start_range} {seed_range}");
    for (long j = seed_start_range; j < seed_start_range + seed_range; j++)
    {
        long curr_seed = j;
        //    Console.Write($"Seed: {curr_seed}");
        for (int k = 0; k < transformer.Count; k++)
        {
            var curr_transformer = transformer[k];
            curr_seed = curr_transformer.ReturnDestinationNumber(curr_seed);
        }

        if (curr_seed < smallest_location) {
            smallest_location = curr_seed;
        }
    }

} );

Console.WriteLine(smallest_location);
struct Map
{

    public Map(string rawStr)
    {
        //Console.WriteLine("rawStr: " + rawStr);
        var raw_mappings = rawStr.Split('\n');
        for (int i = 0; i < raw_mappings.Length; i++)
        {
            var mapping = raw_mappings[i];
            if (mapping == string.Empty) continue;
            var maps = mapping.Split(' ').Select(long.Parse).ToList();
            destRange.Add(maps[0]);
            sourceRange.Add(maps[1]);
            range.Add(maps[2]);
        }
        var largest_range = range.IndexOf(range.Max());
        Largest_Source = sourceRange[largest_range] + range.Max();
        Smallest_Source = range.Min();

    }

    public List<long> sourceRange = [];
    public List<long> destRange = [];

    public List<long> range = [];

    public long Largest_Source = 0;
    public long Smallest_Source = 0;
    public long ReturnDestinationNumber(long sourceNum)
    {
        
        if (Largest_Source < sourceNum) {
            return sourceNum;
        }

        if (Smallest_Source > sourceNum) {
            return sourceNum;
        }
        for (int i = 0; i < sourceRange.Count; i++)
        {
            var range_num = range[i];
            var source_range_num = sourceRange[i];
            var max_source_num = source_range_num + range_num - 1;
            var destRange_num = destRange[i];
            if (sourceNum >= source_range_num && sourceNum <= max_source_num)
            {
                var diff = sourceNum - source_range_num;
                return destRange_num + diff;
            }
        }

        return sourceNum;
    }
}