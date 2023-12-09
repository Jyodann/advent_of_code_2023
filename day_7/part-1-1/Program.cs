
//Console.WriteLine("Hello, World!");

var file = File.ReadAllLines("input");
Dictionary<char, int> tokens = new()
{
    {'A' , 14},
    {'K', 13 },
    {'Q', 12 },
    {'J', 11 },
    {'T', 10 },
    {'9', 9 },
    {'8', 8},
    {'7', 7},
    {'6', 6},
    {'5', 5},
    {'4', 4},
    {'3', 3},
    {'2', 2},
};

var hands = new List<Hand>();

foreach (var item in file)
{
    var split = item.Split(' ');

    var hand = split[0].Trim();
    var bet = int.Parse(split[1].Trim());

    hands.Add(new Hand(bet,
                       hand,
                       ref tokens));
}

var pairs = hands.GroupBy(x => x.kind).OrderBy(x => x.Key);
var current_rank = 1;
var current_winning = 0;
foreach (var item in pairs)
{
    Console.WriteLine(item.Key);

    foreach (var ordered in item.Order())
    {
        current_winning += ordered.bet * current_rank;
        current_rank++;        
    } 
}

Console.WriteLine(current_winning);

class Hand : IComparable<Hand>
{
    public int bet;

    public string HandCards;
    public Kind kind;

    public Dictionary<char, int> tokens;

    public Hand(int bet, string cards, ref Dictionary<char,int> tokens) {
        HandCards = cards;
        this.bet = bet;
        this.tokens = tokens;
        kind = ProcessKind(cards);
    }

    public int CompareTo(Hand? other)
    {
        for (int i = 0; i < HandCards.Length; i++)
        {
            var current_char = tokens[ HandCards[i] ];
            var other_char = tokens [ other!.HandCards[i] ]; 
            
            if (current_char > other_char) {
                return 1;
            }

            if (current_char < other_char) {
                return -1;
            }
        }

        return 0;
    }

    Kind ProcessKind(string card)
    {
        var groups = card.GroupBy(x => x).Select(x => new { Letter = x.Key, Count = x.Count() });

        if (groups.Any(x => x.Count == 5))
        {
            return Kind.FiveOfKind;
        }

        if (groups.Any(x => x.Count == 4))
        {
            return Kind.FourOfKind;
        }

        if (groups.Any(x => x.Count == 3) && groups.Any(x => x.Count == 2))
        {
            return Kind.FullHouse;
        }

        if (groups.Any(x => x.Count == 3))
        {
            return Kind.ThreeOfKind;
        }

        if (groups.Count(x => x.Count == 2) == 2)
        {
            return Kind.TwoPair;
        }

        if (groups.Count(x => x.Count == 2) == 1)
        {
            return Kind.OnePair;
        }
        return Kind.HighCard;
    }

}


enum Kind
{
    FiveOfKind = 6,
    FourOfKind = 5,
    FullHouse = 4,
    ThreeOfKind = 3,
    TwoPair = 2,
    OnePair = 1,
    HighCard = 0
}