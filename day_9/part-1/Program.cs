
var lines = File.ReadAllLines("input");
var number = 0;
foreach (var line in lines)
{
    var numbers = line.Split(' ').Select(int.Parse);

    Console.WriteLine(string.Join(',', numbers));

    number +=    ReturnNextNum(numbers);

}

Console.WriteLine(number);

int ReturnNextNum(IEnumerable<int> numbers) {
    
    var remainders = numbers.ToList();
    var list_of_remainder = new List<List<int>>
    {
        remainders
    };
    do {
        remainders = ReturnRemainders(remainders);

        list_of_remainder.Add(remainders);
    }
    while (!remainders.All(x => x == 0)); 
    
    //list_of_remainder.Add(remainders);
    var number = 0;
    for (int i = list_of_remainder.Count - 1; i >= 0 ; i--)
    {
        var li = list_of_remainder[i];

        number += li[^1];
    }
    return number;
}

List<int> ReturnRemainders(List<int> numbers) {
    var remainders = new List<int>();
    for (int i = 1; i < numbers.Count; i++)
    {
        var first_number = numbers[i - 1];
        var second_number = numbers[i];

        remainders.Add(second_number - first_number);
    }

    return remainders;
}