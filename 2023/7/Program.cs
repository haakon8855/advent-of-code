using Utils;

bool test = false;
Chronograph chronograph = new Chronograph();
string path = test ? "test.txt" : "input.txt";
string[] lines = File.ReadAllLines(path);

// ======================= 
//        Part 1
// ======================= 
Console.WriteLine("Part 1: ");
chronograph.Toggle();
// ======================= 

// Card in order of rank (desc)
List<char> cards = new List<char> { 'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2' };

var hands = lines.Select(x => x.Split(" ")).ToList();

var players = new List<Player>();

foreach (var hand in hands)
{
    // Calculate number of occurences of each card in each hand (32T3J => 311 (Three of a kind + two unique))
    var dict = new Dictionary<char, int>();
    for (int i = 0; i < hand[0].Length; i++)
    {
        char c = hand[0][i];
        if (!dict.ContainsKey(c))
            dict[c] = 1;
        else
            dict[c]++;
    }

    var handString = hand[0];
    var type = dict.Values.OrderDescending().ToList();
    var bet = int.Parse(hand[1]);

    // Create a Player object with the hand (string), the type (list of numbers we calculated) and the bet (int)
    players.Add(new Player(handString, type, bet));
}

Comparer<Player> comparer = Comparer<Player>.Create((x, y) =>
{
    // First sort by length of type list (e.g. 311 > 2111, etc.)
    if (x.Type.Count != y.Type.Count)
        return x.Type.Count - y.Type.Count;
    // Then sort by values in type list (e.g. 311 > 221, etc.)
    for (int i = 0; i < x.Type.Count; i++)
    {
        if (x.Type[i] != y.Type[i])
            return y.Type[i] - x.Type[i];
    }
    // Then sort by card values left to right
    for (int i = 0; i < x.Hand.Length; i++)
    {
        if (cards.IndexOf(x.Hand[i]) != cards.IndexOf(y.Hand[i]))
            return cards.IndexOf(x.Hand[i]) - cards.IndexOf(y.Hand[i]);
    }
    // Should never get here with the input given, if we do, two hands are somehow identical
    return 0;
});

// Sort the list of players/hands
players = players.OrderByDescending(x => x, comparer).ToList();

int sum = 0;
for (int i = 0; i < players.Count; i++)
{
    sum += players[i].Bet * (i + 1);
}

Console.WriteLine(sum);

chronograph.Toggle();

// =======================
//        Part 2
// ======================= 
Console.WriteLine("\nPart 2: ");
chronograph.Toggle();
// ======================= 

// New ordered list with jack in the back (xdd)
cards = new List<char> { 'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J' };

players = new List<Player>();

foreach (var hand in hands)
{
    // Calculate number of occurences of each card in each hand (32T3J => 311 (Three of a kind + two unique))
    var dict = new Dictionary<char, int>();
    for (int i = 0; i < hand[0].Length; i++)
    {
        char c = hand[0][i];
        if (!dict.ContainsKey(c))
            dict[c] = 1;
        else
            dict[c]++;
    }
    var handString = hand[0];
    var type = dict.Values.OrderDescending().ToList();

    // Calculate the new type based on the number of jacks in the hand.
    int js = 0;
    if (dict.ContainsKey('J'))
    {
        // Count number of jacks
        js = dict['J'];

        // Remove the jacks from the type list
        type.Remove(js);
        if (type.Count == 0)
            type.Add(0);
        // Add the number of jacks to the highest number in the list
        type[0] += js;
    }

    var bet = int.Parse(hand[1]);

    // Create a Player object with the hand (string), the type (list of numbers we calculated) and the bet (int)
    players.Add(new Player(handString, type, bet));
}

// Sort the list of players/hands
players = players.OrderByDescending(x => x, comparer).ToList();

sum = 0;
for (int i = 0; i < players.Count; i++)
{
    sum += players[i].Bet * (i + 1);
}

Console.WriteLine(sum);

chronograph.Toggle();


// Nothing to see here, move along, move along
public class Player(string hand, List<int> type, int bet)
{
    public string Hand { get; set; } = hand;
    public List<int> Type { get; set; } = type;
    public int Bet { get; set; } = bet;

}

