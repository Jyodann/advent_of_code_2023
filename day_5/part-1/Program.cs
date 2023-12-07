﻿// See https://aka.ms/new-console-template for more information
using System.Numerics;
using System.Text;

Console.WriteLine("Hello, World!");

var lines = File.ReadAllLines("input");

var seeds_str = lines[0];

var seeds_arr = seeds_str[(seeds_str.IndexOf(':') + 1)..]
    .Trim()
    .Split(' ')
    .Select(BigInteger.Parse);

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
var location_nums = new List<BigInteger>();
foreach (var seed in seeds_arr)
{
    BigInteger curr_seed = seed;
    Console.Write($"Seed: {curr_seed}");
    for (int i = 0; i < transformer.Count; i++)
    {
        var curr_transformer = transformer[i];
        curr_seed = curr_transformer.ReturnDestinationNumber(curr_seed);

    }
    Console.Write($" Location: {curr_seed}\n");

    location_nums.Add(curr_seed);
}

Console.WriteLine(location_nums.Min());
struct Map
{

    public Map(string rawStr)
    {
        Console.WriteLine("rawStr: " + rawStr);
        var raw_mappings = rawStr.Split('\n');
        for (int i = 0; i < raw_mappings.Length; i++)
        {
            var mapping = raw_mappings[i];
            if (mapping == string.Empty) continue;
            var maps = mapping.Split(' ').Select(BigInteger.Parse).ToList();
            destRange.Add(maps[0]);
            sourceRange.Add(maps[1]);
            range.Add(maps[2]);
        }

    }

    public List<BigInteger> sourceRange = [];
    public List<BigInteger> destRange = [];

    public List<BigInteger> range = [];

    public BigInteger ReturnDestinationNumber(BigInteger sourceNum)
    {
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