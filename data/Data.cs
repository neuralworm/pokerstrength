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
     class SklanskyGroups: HelpSection {
        static string title = "Sklansky Groups";
        static string description = "Hands ranked into groups determining their playability.";
        static int[] ranks = [1, 2, 3, 4, 5, 6, 7, 8];
        public static Dictionary<int, string> rankStrengthMessages = new Dictionary<int, string>{
            [1] = "Strongest (Early/Middle/Late)",
            [2] = "Strong (Early/Middle/Late)",
            [3] = "Good (Early/Middle/Late)",
            [4] = "Decent (Early*/Middle/Late)",
            [5] = "Fair (Early*/Middle/Late)",
            [6] = "Weak (Middle*/Late)",
            [7] = "Very Weak (Late***)",
            [8] = "Bad",
            [9] = "Weak/Trash",

        };
        public static void output(){
            Console.WriteLine(title);
            Console.WriteLine(description);
        }
     }
}