var cards = File.ReadAllLines("input");
var results = 0.0;
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
    
    if (res == 0) {
        continue;
    }
    results += Math.Pow(2, Math.Max(0, res - 1));

}

Console.WriteLine(results);