// See https://aka.ms/new-console-template for more information

using Microsoft.VisualBasic;

var lines = File.ReadAllLines("input");

var first_int = "";
var second_int = "";
var total = 0;
var list_of_char = new List<string>();
foreach (var item in lines)
{
    Console.WriteLine(item);   

    foreach (var ch in item) {
        var char_as_str = ch.ToString();
        if (int.TryParse(char_as_str, out int _)) {    
            list_of_char.Add(char_as_str);
        }


    }

    Console.WriteLine(string.Join(",", list_of_char));
    var first = list_of_char[0];
    var second = list_of_char.Last();

    Console.WriteLine($"First: {first} Second: {second}");
    int.TryParse($"{first}{second}", out int prased);
    total += prased;

    list_of_char.Clear();
}

Console.WriteLine(total);