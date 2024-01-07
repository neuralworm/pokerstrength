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

Dictionary<string, string[]> cardMap = [];