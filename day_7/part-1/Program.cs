var files = File.ReadAllLines("input");

char[] strengths = [
    '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A'
];

var card_strengths = new List<double>();

foreach (var item in files)
{
    var raw_str_arr = item.Split(' ');
    var cards = raw_str_arr[0];
    var strength = CalculateStrength(cards);
    //Console.WriteLine(strength);
    card_strengths.Add(strength);
}

Console.WriteLine(string.Join(',', card_strengths));

var ordered = card_strengths.Order().ToList();
Console.WriteLine(string.Join(',', ordered));
var total_bet = 0;
for (int i = 0; i < ordered.Count; i++)
{
    var curr_strength = ordered[i];
    var idx = card_strengths.IndexOf(curr_strength);

    var bet = int.Parse(files[idx].Split(' ')[1]) * (i + 1);

    //Console.WriteLine(bet);
    total_bet += bet;
}

Console.WriteLine(total_bet);
/*
Console.WriteLine(CalculateStrength("22222"));
Console.WriteLine(CalculateStrength("KAAAA"));
Console.WriteLine(CalculateStrength("33322"));
Console.WriteLine(CalculateStrength("33392"));
Console.WriteLine(CalculateStrength("23424"));
Console.WriteLine(CalculateStrength("A23A4"));
Console.WriteLine(CalculateStrength("12345"));
Console.WriteLine(CalculateStrength("KKKKK"));
*/
double CalculateStrength(string cards) {

    var kind = ReturnKind(cards);
    var curr_strength = 0.0;
    for (int i = 0; i < cards.Length; i++)
    {
        var curr_char = cards[i];
        double idx_of = (Array.IndexOf(strengths, curr_char) + 1) / (double)strengths.Length;
        var strength = idx_of * Math.Pow(10, 6 - i);
        curr_strength += strength;
    }
    return curr_strength + ((int)kind * 1_000_000_0);
}

Kinds ReturnKind(string cards)
{
    var hand = cards;
    var curr_kind = Kinds.HighCard;
    var current_value = 0;

    for (int i = 0; i < 5; i++)
    {
        var curr_char = hand[0];

        var same_char = 0;
        for (int j = 1; j < hand.Length; j++)
        {
            //Console.WriteLine(hand[j]);
            if (curr_char == hand[j])
            {
                same_char++;
            }
        }
        //Console.WriteLine($"Current Char: {curr_char} Same Chars: {same_char}");
        if (same_char == 4)
        {
            curr_kind = Kinds.Five;
            break;
        }

        if (same_char == 3)
        {
            curr_kind = Kinds.Four;
            break;
        }

        if (same_char == 1 && curr_kind == Kinds.ThreeOfKind)
        {
            curr_kind = Kinds.FullHouse;
            break;
        }

        if (same_char == 2 && curr_kind != Kinds.OnePair)
        {
            curr_kind = Kinds.ThreeOfKind;
        }

        if (same_char == 1 && curr_kind == Kinds.OnePair)
        {
            curr_kind = Kinds.TwoPair;
            break;
        }

        if (same_char == 1)
        {
            curr_kind = Kinds.OnePair;
        }

        hand = hand.Replace(curr_char.ToString(), string.Empty);

        //Console.WriteLine(hand);
        if (hand == string.Empty)
        {
            break;
        }
    }
    return curr_kind;
}

enum Kinds
{
    Five = 7,
    Four = 6,
    FullHouse = 5,
    ThreeOfKind = 4,
    TwoPair = 3,
    OnePair = 2,
    HighCard = 1
}



