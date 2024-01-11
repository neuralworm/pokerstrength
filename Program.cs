// See https://aka.ms/new-console-template for more information
using Figgle;
using Data;
using System.Text.RegularExpressions;
// LIFECYCLE
Console.CancelKeyPress += delegate
{
    Environment.Exit(-1);
};
// SKLANSKY GROUPS
Dictionary<int, string[]> sklanskyMap = new Dictionary<int, string[]>
{
    [1] = [
        "AA", "AKs", "KK", "QQ", "JJ"
    ],
    [2] = [
        "AK", "AQs", "AJs", "KQs", "TT"
    ],
    [3] = [
        "AQ", "ATs", "KJs", "QJs", "JTs", "99"
    ],
    [4] = [
        "AJ", "KQ", "KTs", "QTs", "J9s", "T9s", "98s", "88"
    ],
    [5] = [
        "A9s", "A8s", "A7s", "A6s", "A5s", "A4s", "A3s", "A2s", "KJ", "QJ", "JT", "Q9s", "T8s", "97s", "87s", "77", "76s", "66"
    ],
    [6] = [
        "AT", "KT", "QT", "J8s", "86s", "75s", "65s", "55", "54s"
    ],
    [7] = [
        "K9s", "K8s", "K7s", "K6s", "K5s", "K4s", "K3s", "K2s", "J9", "T9", "98", "64s", "53s", "44", "43s", "33", "22"
    ],
    [8] = [
        "A9", "K9", "Q9", "J8", "J7s", "T8", "96s", "87", "85s", "76", "74s", "65", "54", "42s", "32s"
    ]
};
// CHEM FORMULA
Dictionary<string, float> chenDictionary = new Dictionary<string, float>
{
    ["2"] = 1,
    ["3"] = 1.5f,
    ["4"] = 2,
    ["5"] = 2.5f,
    ["6"] = 3,
    ["7"] = 3.5f,
    ["8"] = 4,
    ["9"] = 4.5f,
    ["T"] = 5,
    ["J"] = 6,
    ["Q"] = 7,
    ["K"] = 8,
    ["A"] = 10
};
string[] cards = ["2", "3", "4", "5", "6", "7", "8", "9", "T", "J", "Q", "K", "A"];

Dictionary<string, string[]> cardMap = new Dictionary<string, string[]> { ["2"] = ["2", "two"] };


double getChenStrength(string c1, string c2, bool suited)
{
    float total = 0;
    total += chenDictionary[getHighestCard(c1, c2)];
    // IF PAIR
    if (c1 == c2)
    {
        total *= 2;
        if(total < 5) total = 5;
    }
    // ELSE GAPPERS/CLOSENESS
    else
    {
        int c1index = Array.IndexOf(cards, c1);
        int c2index = Array.IndexOf(cards, c2);
        // Subtract points if their is a gap between the two cards.
        int gap = Math.Abs(c1index - c2index) - 1;
        total -= gap < 3 ? gap : (gap < 4 ? 4 : 5);
        // Add 1 point if there is a 0 or 1 card gap and both cards are lower than a Q. (e.g. JT, 75, 32 etc, this bonus point does not apply to pocket pairs)
        int queenIndex = Array.IndexOf(cards, "Q");
        if(c1index < queenIndex && c2index < queenIndex){
            if(Math.Abs(c1index - c2index) - 1  <= 1) total += 1;
        }
        if (suited) total += 2;
    }

    return Math.Ceiling(total);
}

int getSklanskyRank(string c1, string c2, bool suited){
    string bigger, smaller;
    int indexOne = Array.IndexOf(cards, c1);
    int indexTwo = Array.IndexOf(cards, c2);
    bigger = indexOne > indexTwo ? c1 : c2;
    smaller = bigger == c1 ? c2 : c1;
    string keyString = $"{bigger}{smaller}{(bigger == smaller ? "" : (suited ? "s" : ""))}";
    foreach(KeyValuePair<int, string[]> entry in sklanskyMap){
        if(entry.Value.Contains(keyString)){
            return entry.Key;
        }
    }
    return 9;
}

// HEADER
Console.WriteLine(FiggleFonts.Standard.Render("POKER STRENGTH"));
Console.WriteLine("Poker hand strength index");
// MAIN LOOP
while (true)
{
    Console.WriteLine("Enter starting hand in format `C1 C2 S/O` (press ctrl+c or type 'exit' to exit).");
    string input = Console.ReadLine().Trim();
    if (input == "exit") Environment.Exit(-1);
    if (input == "help") helpMenu();
    if (!isValidInput(input)) continue;

    try
    {
        string noWhitespace = Regex.Replace(input, @"\s+", "");
        char[] arguments = noWhitespace.ToCharArray();

        string c1 = arguments[0].ToString().ToUpper();
        string c2 = arguments[1].ToString().ToUpper();
        if (!isValidCard(c1) || !isValidCard(c2))
        {
            errorMessage();
            continue;
        }
        bool suited;
        if(arguments.Length < 3) suited = false;
        else if (arguments[2].ToString().ToUpper() == "O") suited = false;
        else if (arguments[2].ToString().ToUpper() == "S") suited = true;
        else
        {
            errorMessage();
            continue;
        }

        // OUTPUT
        Console.WriteLine($"{c1} {c2} {(c1 == c2 ? "" : (suited ? "Suited" : "Offsuit"))}\n");
        drawTableRow("Strength System", "Rating", "Desc", "Turns");
        double chenStrength = getChenStrength(c1, c2, suited);
        drawTableRow("Chen Formula (Higher Better)", chenStrength.ToString(), "N/A", "N/A");
        int sklanskyRank = getSklanskyRank(c1, c2, suited);
        drawTableRow("Sklansky Rank  (1/best - 9/trash)", sklanskyRank.ToString(), "Desc", SklanskyGroups.rankStrengthMessages[sklanskyRank]);
        Console.WriteLine("OVERALL: " );
        Console.WriteLine("\n");

    }
    catch (Exception e)
    {
        errorMessage();
        continue;
    }

}


void errorMessage()
{
    Console.WriteLine("Invalid Input.");
}

// UTIL
bool isValidInput(string inputString)
{

    return true;
}
bool isValidCard(string card)
{
    if (cards.Contains(card.ToUpper())) return true;
    return false;
}
string getHighestCard(string c1, string c2)
{
    return chenDictionary[c1] > chenDictionary[c2] ? c1 : c2;
}
void helpMenu()
{
    Console.WriteLine(FiggleFonts.Standard.Render("HELP MENU"));
    Data.ChenFormula.output();
}
void drawTableRow(string label, string rank, string desc, string turn){
    Console.WriteLine(String.Format("|{0,36}|{1,12}|{2,12}|{3,30}|", label, rank, desc, turn));
}
void drawHighlightedText(string msg){
    Console.BackgroundColor = ConsoleColor.Blue;
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine(msg);
    Console.ResetColor();
}

