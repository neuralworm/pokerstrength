// See https://aka.ms/new-console-template for more information
using Figgle;
Console.WriteLine(FiggleFonts.Standard.Render("POKER STRENGTH"));
Console.WriteLine("Poker hand strength index");
while (true)
{
    Console.WriteLine("Enter starting hand in format `C1 C2 S/O` (press ctrl+c or type 'exit' to exit).");
    string input = Console.ReadLine();


    if (input == "exit") break;
    Console.WriteLine(getStrengthOutput(input));
}




// UTIL
bool isValidInput(string inputString){
    return false;
}
string getStrengthOutput(string inputString){

    return "88";
}

string[] cards = ["2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"];

Dictionary<string, string[]> cardMap = new Dictionary<string, string[]>{["2"] = ["2", "two"]};

// Chen formula
Dictionary<string,float> chenDictionary = new Dictionary<string, float>{
    ["2"] = 1,
    ["3"] = 1.5f,
    ["4"] = 2,
    ["5"] = 2.5f,
    ["6"] = 3,
    ["7"] = 3.5f,
    ["8"] = 4,
    ["9"] = 4.5f,
    ["10"] = 5,
    ["J"] = 6,
    ["Q"] = 7,
    ["K"] = 8,
    ["A"] = 10
};
float getChenStrength(string c1, string c2, bool suited){
    float total = 0;
    total += chenDictionary[c1];
    total += chenDictionary[c2];


    if(c1 == c2){
        if(c1 == "2" || c1 == "3" || c1 == "4") total += 5;
        else if (c1 == "5") total += 6;
        else total *= 2;
    }
    if(suited) total += 2;
    return total;
}
