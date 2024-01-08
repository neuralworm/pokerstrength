namespace Data{
    interface HelpSection{
        public static void output() => Console.WriteLine("");
    }
    class ChenFormula: HelpSection{
        static string title = "Chen Formula";
        static string description = "The Chen formula is a system for scoring different starting hands in Texas Hold’em. It was created by Bill Chen for use in the book Hold’em Excellence by Lou Krieger. Bill Chen is also the guy that wrote The Mathematics of Poker.\n";

        public static void output(){
            Console.WriteLine(title);
            Console.WriteLine(description);
        }
    }
}