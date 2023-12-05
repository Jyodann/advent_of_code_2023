var alltext = File.ReadAllText("input");

var lines = alltext.Split("\n");

var length_of_line = lines[0].Length;

Console.WriteLine(length_of_line);
var pos_idx_to_token = new Dictionary<int, char>();
var pos_idx_to_symbol = new Dictionary<int, char>();

char[] tokens = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9'];

for (int i = 0; i < alltext.Length; i++)
{
    var idx = i + 1;
    var ch = alltext[i];

    if (ch == '\n')
    {
        continue;
    }
    if (tokens.Contains(ch))
    {
        pos_idx_to_token[idx] = ch;
        continue;
    }
    if (ch == '*')
    {
        pos_idx_to_symbol[idx] = ch;
    }
}

foreach (var item in pos_idx_to_token)
{
    Console.WriteLine(item);
}
var explored_cords = new List<int>();

var full_token_list = new List<int>();

foreach (var symbol in pos_idx_to_symbol)
{
    var curr_key = symbol.Key - 1;

    var left_hand_key = curr_key - 1;
    var right_hand_key = curr_key + 1;
    var top_key = curr_key - length_of_line - 1;
    var bottom_key = curr_key + length_of_line + 1;

    var top_left_key = top_key - 1;
    var top_right_key = top_key + 1;

    var bottom_left_key = bottom_key - 1;
    var bottom_right_key = bottom_key + 1;

    var keys = new List<int>() {
        top_left_key, top_key, top_right_key,
        left_hand_key, curr_key, right_hand_key,
        bottom_left_key, bottom_key, bottom_right_key
    };
    Console.WriteLine($"{alltext[top_left_key]} {alltext[top_key]} {alltext[top_right_key]}");
    Console.WriteLine($"{alltext[left_hand_key]} {alltext[curr_key]} {alltext[right_hand_key]}");
    Console.WriteLine($"{alltext[bottom_left_key]} {alltext[bottom_key]} {alltext[bottom_right_key]}");

    var explored_idxs = new List<int>();
    var temp_numbers_list = new List<int>();
    foreach (var key in keys)
    {
        var actual_key = key + 1;
        var temp_token_list = new List<int>();

        if (explored_idxs.Contains(actual_key))
        {
            continue;
        }

        if (pos_idx_to_token.ContainsKey(actual_key))
        {
            // Find neighbours in negative pos first:

            var negative_pos = actual_key - 1;
            var positive_pos = actual_key + 1;

            temp_token_list.Add(actual_key);
            while (pos_idx_to_token.ContainsKey(negative_pos))
            {
                temp_token_list.Add(negative_pos);
                negative_pos--;
            }

            while (pos_idx_to_token.ContainsKey(positive_pos))
            {
                temp_token_list.Add(positive_pos);
                positive_pos++;
            }

            temp_token_list.Sort();
        }
        explored_idxs.AddRange(temp_token_list);
        var full_number = "";
        foreach (var token in temp_token_list)
        {
            full_number += pos_idx_to_token[token];
        }

        if (full_number == string.Empty)
        {
            continue;
        }

        temp_numbers_list.Add(int.Parse(full_number));
    }

    if (temp_numbers_list.Count == 2) {
        full_token_list.Add(temp_numbers_list[0] * temp_numbers_list[1]);
    }
}

Console.WriteLine(full_token_list.Sum());