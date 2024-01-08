// See https://aka.ms/new-console-template for more information
using Figgle;
using Data;
// LIFECYCLE
Console.CancelKeyPress += delegate
{
    Environment.Exit(-1);
};
// Chen formula
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


float getChenStrength(string c1, string c2, bool suited)
{
    float total = 0;
    total += chenDictionary[getHighestCard(c1, c2)];
    // IF PAIR
    if (c1 == c2)
    {
        if (c1 == "2" || c1 == "3" || c1 == "4") total += 5;
        else if (c1 == "5") total += 6;
        else total *= 2;
    }
    // ELSE GAPPERS/CLOSENESS
    else
    {
        // Subtract points if their is a gap between the two cards.
        int gap = Math.Abs(Array.FindIndex<string>(cards, x => x.Contains(c1)) - Array.FindIndex<string>(cards, x => x.Contains(c2)));
        total -= gap < 3 ? gap : (gap < 4 ? 4 : 5);
        // Add 1 point if there is a 0 or 1 card gap and both cards are lower than a Q. (e.g. JT, 75, 32 etc, this bonus point does not apply to pocket pairs)

        if(total < 0) total = 0;
        if (suited) total += 2;
    }
    return total;
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
        string[] arguments = input.Split(" ");
        if (arguments.Length == 1) arguments = input.Split("");

        string c1 = arguments[0].ToUpper();
        string c2 = arguments[1].ToUpper();
        if (!isValidCard(c1) || !isValidCard(c2))
        {
            errorMessage();
            continue;
        }
        bool suited;
        if (arguments[2].ToUpper() == "O") suited = false;
        else if (arguments[2].ToUpper() == "S") suited = true;
        else
        {
            errorMessage();
            continue;
        }


        Console.WriteLine(getStrengthOutput(c1, c2, suited));
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
string getStrengthOutput(string c1, string c2, bool suited)
{
    float chenStrength = getChenStrength(c1, c2, suited);
    return $"{c1} {c2} {(c1 == c2 ? "" : ((suited ? "Suited" : "Offsuit")))}\nChen Formula Strength: {chenStrength} / 22";
}
void helpMenu(){
    Console.WriteLine(FiggleFonts.Standard.Render("HELP MENU"));
    Data.ChenFormula.output();
}

