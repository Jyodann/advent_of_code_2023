using System.Text.Json;
using System.Text.Json.Serialization;

var cards = File.ReadAllLines("input");
var win_counts = new List<int>(); 
foreach (var card in cards)
{
    var cleaned_data = card.Substring(card.IndexOf(':') + 1).Trim();
    var split = cleaned_data.Split('|');

    var elf_numbers = split[0].Trim().Split(' ').Select(x =>
    {
        if (x == "")
        {
            return -1;
        }
        return int.Parse(x);
    });
    var my_numbers = split[1].Trim().Split(' ').Select(x =>
    {
        if (x == "")
        {
            return -2;
        }
        return int.Parse(x);
    });

    var res = elf_numbers.Intersect(my_numbers).ToList().Count();
    
    win_counts.Add(res);
}
var count = 1;
var newCardCopy = new Dictionary<int, int>();
for (int i = 0; i < win_counts.Count; i++)
{
    ProcessCardWins(i, ref win_counts, newCardCopy);
}
 
Dictionary<int, int> ProcessCardWins(int card_no, ref List<int> win_table, Dictionary<int, int> card_to_copy) {
    //Console.WriteLine($"Processing Card: {card_no + 1}");

    var number_of_wins = win_table[card_no];

    if (!card_to_copy.ContainsKey(card_no)) {
        card_to_copy[card_no] = 1;
    } else {
        card_to_copy[card_no]++;
    }
    for (int i = 0; i < number_of_wins; i++)
    {
        ProcessCardWins(i + card_no + 1, ref win_table, card_to_copy);
    }


    return card_to_copy;
}

var sum = newCardCopy.Sum(x => x.Value);
Console.WriteLine(sum);