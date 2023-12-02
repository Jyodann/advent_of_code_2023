// See https://aka.ms/new-console-template for more information
string[] tokens = [

    "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
    "one", "two", "three", "four", "five", "six", "seven", "eight", "nine",
];

Dictionary<string, int> conv_table = new() {

    { "one" , 1 },
    { "two" , 2 },
    { "three" , 3 },
    { "four" , 4 },
    { "five" , 5 },
    { "six" , 6 },
    { "seven" , 7 },
    { "eight" , 8 },
    { "nine" , 9 },
};

var lines = File.ReadAllLines("input");
var total = 0;
foreach (var item in lines)
{
    //Console.WriteLine(string.Join(",", returnNext(item)));
    var number = int.Parse(returnNext(item));
    total += number;
}

Console.WriteLine(total);

string returnNext(string input)
{
    var ls = new List<(string, int)>();
    Console.WriteLine(input);
    foreach (var item in tokens)
    {
        
        var idx = input.IndexOf(item);
        
        while (idx != -1)
        {
            var sub_str = input.Substring(idx, item.Length);
            
             
            if (conv_table.ContainsKey(sub_str))
            {
                ls.Add((conv_table[sub_str].ToString(), idx));
            }
            else
            {

                ls.Add((sub_str, idx));
            }

            idx = input.IndexOf(item, idx + item.Length);  
        }
    }
    var max = -1;
    var lo = 10000;

    var max_str = "";
    var lo_str = "";
    foreach (var item in ls)
    {
        var idx = item.Item2;

        if (idx > max)
        {
            max = idx;
            max_str = item.Item1;
        }

        if (idx < lo)
        {
            lo = idx;
            lo_str = item.Item1;
        }
    }
    Console.WriteLine($"{lo_str}{max_str}");
    return $"{lo_str}{max_str}";
}