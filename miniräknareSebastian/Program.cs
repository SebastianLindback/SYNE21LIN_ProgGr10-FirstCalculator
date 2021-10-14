using System;
using System.Text.RegularExpressions;
using System.Linq;

namespace miniräknareSebastian
{
    class Program
    {

        static void printOut(double n1,string s1, double n2, string[,] arr)
        {
            Console.ForegroundColor = ConsoleColor.White;
            // print som välkommnar user
            if (s1.Length < 1) { s1 = "/ - * +"; }
            Console.WriteLine("Välkommen till en fantastisk miniräknare. \nSenaste fem input:");

            // Print av history
            for (int i = 0; i < 5; i++)
            {
                string prevCalc = i +1 + ": ";
                for (int y = 0; y < 4; y++)
                {
                    if (y == 3) { prevCalc += " = "; }
                    prevCalc += arr[i, y] + " ";
                }
                Console.WriteLine(prevCalc);
            }

            //print av text för akuell uträkning
            Console.Write("\n \nAnge ekvationen i tre steg. \n 1. Tal 1 : "); Console.ForegroundColor = ConsoleColor.Red; Console.Write(n1); Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" \n 2. Räknesätt: "); Console.ForegroundColor = ConsoleColor.Red; Console.Write(s1); Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" \n 3. Tal2: "); Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine(n2); Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("______________________");
                
        }
        static string charCheck(string input, bool sign)
        {
            input = input.Trim();
            // kolla efter Marcus, om han dyker upp avslutas programmet
            if (input.ToLower() == "marcus") { Console.WriteLine("Hej!"); Console.ReadKey(); Environment.Exit(0); }
            
            // Se till att användaren endast skriver in ett tecken med räknesätt.
            if (!sign)
            {
                while (!(input.Length == 1  && (input.Contains("/") || input.Contains("-") || input.Contains("+") || input.Contains("*"))))
                {
                    
                    Console.WriteLine("Fel angivet format. Ange ett av följande tecken: / - * +");
                    input = Console.ReadLine();
                    input = input.Trim();
                }
            }
            // OUTPUT
            return input;

        }

        static string Calc(double n1, string s1, double n2)
        {
            // Kollar vilket räknesätt som är inmatat och utför lämplig ekvation. Vid dividering med noll ges felmeddelande
            string res = "";
            if (s1 == "/") { if (n2 == 0) { res = "Error! Dividerat med 0"; } else { res= Convert.ToString(n1 / n2); } }
            if (s1 == "-") { res= Convert.ToString(n1 - n2); }
            if (s1 == "*") { res= Convert.ToString(n1 * n2); }
            if (s1 == "+") { res= Convert.ToString(n1 + n2); }
            return res;
        }
        static double numInput()
        {
            // tag ett Trimmat input. Försök kovertera enhet från string till double och fråga efter nytt input efter varje misslyckad konvertering.
            double res;
            string temp = Console.ReadLine();
            temp = temp.Trim();
            while (!double.TryParse(charCheck(temp, true), out res))
            {
                Console.WriteLine("Error! Felaktigt format. Skriv in igen");
                temp = charCheck(Console.ReadLine(), true);
                temp = temp.Trim();

            }

            // OUTPUT
            return res;
        }
        static void Main(string[] args)
        {
            double t1 = 0;
            string sign = "";
            double t2 = 0;
            string temp = "";
            string[,] array2Da = new string[5, 4] { { "0", "-", "0", "0" }, { "0", "-", "0", "0" }, { "0", "-", "0", "0" }, { "0", "-", "0", "0" }, { "0", "-", "0", "0" }, };
            int sel = 0;
            while(sel >= 0)
            {
                // Gör en basic printout
                printOut(0,"",0,array2Da);
                
                

                // tag inmatning av tal 1. därefter rensa consolen och skriv ut den igen med nyinmatade tal 1
                Console.WriteLine("Ange tal 1: ");
                t1 = numInput();
                Console.Clear();
                    printOut(t1, "", 0, array2Da);
                

                // tag inmatning av räknesätt. därefter rensa consolen och skriv ut den igen med räknesättet samt tal 1
                Console.WriteLine("Ange räknesätt(/ - * +): ");
                temp = Console.ReadLine();
                sign = charCheck(temp, false);
                    Console.Clear();
                    printOut(t1, sign, 0, array2Da);
                

                // tag inmatning av tal 2. därefter rensa consolen och skriv ut den igen med räknesättet + tal 1 och tal 2
                Console.WriteLine("Ange tal 2: ");
                t2 = numInput();
                    Console.Clear();
                    printOut(t1, sign, t2, array2Da);
                
                // Spara resultatet i array.
                    if (sel >= 5) { sel = 0; }
                    //- nolla selektorn i array när den ligger i slutet av array
                    for (int i = 0; i < 4; i++)
                    {
                        //- Varje input användaren ger sparas i en egen cell i array
                        if (i == 0) { array2Da[sel, i] = Convert.ToString(t1); }
                        if (i == 1) { array2Da[sel, i] = sign; }
                        if (i == 2) { array2Da[sel, i] = Convert.ToString(t2); }
                        if (i == 3) { array2Da[sel, i] = Calc(t1, sign, t2); }
                    }
                    
                sel++;



                // skriv ut restultat
                Console.WriteLine("Resultat: "); Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine(Calc(t1, sign, t2)); Console.ForegroundColor = ConsoleColor.White; 
                Console.WriteLine("Tryck på valfri knapp för att återgå");
                Console.ReadKey();
                Console.Clear();


            }
            
        }
    }
}
