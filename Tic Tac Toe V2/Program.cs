using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;

namespace Tic_Tac_Toe_V2
{
    class Program
    {
        static Player p1 = new Player { Id = 1 };
        static Player p2 = new Player { Id = 2 };
        static Player winner;
        static int currentplayer = 0;
        static string currentSymbol;
        static bool anothergame = false;
        static string A1 = "1", B1 = "2", C1 = "3", A2 = "4", B2 = "5", C2 = "6", A3 = "7", B3 = "8", C3 = "9";
        /*Board
         * static string[] _board = {$"    A     B     C  ",
                                  $"                   ",
                                  $"       |     |     ",
                                  $"1   {A1}  |  {B1}  |  {C1}   ",
                                  $"  _____|_____|_____",
                                  $"       |     |     ",
                                  $"2   {A2}  |  {B2}  |  {C2}   ",
                                  $"  _____|_____|_____",
                                  $"       |     |     ",
                                  $"3   {A3}  |  {B3}  |  {C3}  ",
                                  $"       |     |     \n\n"};
                                  */
        static string choice;
        static int winFlag = 0; // 0 - still going, 1 - we have a winner, -1 - tie 
        static int howmany = 0;
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            BuildPlayers();

            do
            {
                if (howmany == 1)
                    GameWithAFriend();
                else GameWithComputer();

                if (winFlag == 1)
                {
                    Console.WriteLine($"The Winner Is {winner.Name} ");
                    ShowScore();
                }
                else Console.WriteLine($"This match finished with a draw\n");
                anothergame = AnotherGame();

            } while (anothergame);
            ShowScore();
            Console.WriteLine("Press Any Key To Exit...");
            Console.ReadKey();
        }

        private static bool AnotherGame()
        {
            do
            {
                Console.WriteLine($"\n\nDo you want to play another game? y/n");
                string more = Console.ReadLine().ToLower();
                if (more != "y" && more != "n")
                    Console.WriteLine($"\n\nDo you want to play another game? y/n only");
                else if (more == "y")
                {
                    ClearBoard();
                    return true;
                }
                else return false;
            } while (true); //?!?!?
        }

        private static void BuildPlayers()
        {
            Console.Write("Player 1 Please Enter Your Name: ");
            p1.Name = Console.ReadLine();

            do
            {
                Console.Write("\nSelect Your Symbol X or O:");
                p1.Soldier = Console.ReadLine().ToUpper();
                if (p1.Soldier != "X" && p1.Soldier != "O")
                    Console.WriteLine("Select only X or O ");
            } while (p1.Soldier != "X" && p1.Soldier != "O");

            if (p1.Soldier == "X")
                p2.Soldier = "O";
            else p2.Soldier = "X";

            do
            {
                Console.Write($"{p1.Name} do you want to play against the Computer = 0 or your friend = 1? ");
                howmany = int.Parse(Console.ReadLine());
                if (howmany != 0 && howmany != 1)
                    Console.WriteLine("\nSelect 1 to play vs your friend or 0 vs your Computer\n");
            } while (howmany != 0 && howmany != 1);

            if (howmany == 0)
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
                foreach (ManagementObject mo in mos.Get())
                {
                    p2.Name+=(mo["Name"]);
                }

                //p2.Name = "Computer";
            }

            else
            {
                Console.Write("Player 2 Please Enter Your Name: ");
                p2.Name = Console.ReadLine();
            }
        }

        private static void ShowBoard()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(Console.WindowWidth / 3, 0);
            Console.WriteLine("Welcome to Tic Tac Toe");
            Console.ResetColor();
            Console.WriteLine($"\n{p1.Name} = {p1.Soldier}, {p2.Name} = {p2.Soldier}\n\n");

            //foreach (string row in _board)
            //    Console.WriteLine(row);
            Console.WriteLine($"    A     B     C  ");
            Console.WriteLine($"                   ");
            Console.WriteLine($"       |     |     ");
            Console.WriteLine($"1   {A1}  |  {B1}  |  {C1}   ");
            Console.WriteLine($"  _____|_____|_____");
            Console.WriteLine($"       |     |     ");
            Console.WriteLine($"2   {A2}  |  {B2}  |  {C2}   ");
            Console.WriteLine($"  _____|_____|_____");
            Console.WriteLine($"       |     |     ");
            Console.WriteLine($"3   {A3}  |  {B3}  |  {C3}  ");
            Console.WriteLine($"       |     |     \n\n");
        }

        private static void GameWithAFriend()
        {
            do
            {
                ++currentplayer;
                ShowBoard();
                do
                {
                    if (currentplayer % 2 == 0)
                    {
                        Console.WriteLine($"{p2.Name} Enter Your Selection: (ex. 1 or A1)");
                        //p2.Selections.Add(choice);
                        currentSymbol = p2.Soldier;
                    }
                    else
                    {
                        Console.WriteLine($"{p1.Name} Enter Your Selection: (ex. 1 or A1)");
                        //p1.Selections.Add(choice);
                        currentSymbol = p1.Soldier;
                    }
                    choice = Console.ReadLine();// TODO: לטפל מה קורה אם מזינים אותיות
                    if (int.Parse(choice) < 1 || int.Parse(choice) > 9)
                        Console.WriteLine("Select a position between 1 and 9");
                    //TODO: check user selection - prevent double selection;

                } while (int.Parse(choice) < 1 || int.Parse(choice) > 9);

                switch (choice)
                {
                    case "1":
                    case "A1":
                        A1 = currentSymbol;
                        break;
                    case "2":
                    case "B1":
                        B1 = currentSymbol;
                        break;
                    case "3":
                    case "C1":
                        C1 = currentSymbol;
                        break;
                    case "4":
                    case "A2":
                        A2 = currentSymbol;
                        break;
                    case "5":
                    case "B2":
                        B2 = currentSymbol;
                        break;
                    case "6":
                    case "C2":
                        C2 = currentSymbol;
                        break;
                    case "7":
                    case "A3":
                        A3 = currentSymbol;
                        break;
                    case "8":
                    case "B3":
                        B3 = currentSymbol;
                        break;
                    case "9":
                    case "C3":
                        C3 = currentSymbol;
                        break;
                }
                winner = checkWin();
            } while (winFlag == 0);
            ShowBoard();
        }

        private static void GameWithComputer()
        {
            do
            {
                ++currentplayer;
                ShowBoard();
                do
                {
                    if (currentplayer % 2 == 0)
                    {
                        Console.WriteLine($"{p2.Name} Enter Your Selection: (ex. 1 or A1)");
                        //p2.Selections.Add(choice);
                        currentSymbol = p2.Soldier;
                        choice = rnd.Next(1, 10).ToString();
                        Console.Write("Thinking ");
                        for (int i = 0; i < rnd.Next(1,15); i++)
                        {
                            Console.Write("|");
                            Thread.Sleep(50);
                            Console.Write("\b/");
                            Thread.Sleep(50);
                            Console.Write("\b-");
                            Thread.Sleep(50);
                            Console.Write("\b\\");
                            Thread.Sleep(50);
                            Console.Write("\b|");
                            Thread.Sleep(50);
                            Console.Write("\b/");
                            Thread.Sleep(50);
                            Console.Write("\b-");
                            Thread.Sleep(50);
                            Console.Write("\b\\");
                            Thread.Sleep(50);
                            Console.Write("\b");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{p1.Name} Enter Your Selection: (ex. 1 or A1)");
                        //p1.Selections.Add(choice);
                        currentSymbol = p1.Soldier;
                        // TODO: לטפל מה קורה אם מזינים אותיות
                        choice = Console.ReadLine();
                    }
                    if (int.Parse(choice) < 1 || int.Parse(choice) > 9)
                        Console.WriteLine("Select a position between 1 and 9");
                    //TODO: check user selection - prevent double selection;

                } while (int.Parse(choice) < 1 || int.Parse(choice) > 9);

                switch (choice)
                {
                    case "1":
                    case "A1":
                        A1 = currentSymbol;
                        break;
                    case "2":
                    case "B1":
                        B1 = currentSymbol;
                        break;
                    case "3":
                    case "C1":
                        C1 = currentSymbol;
                        break;
                    case "4":
                    case "A2":
                        A2 = currentSymbol;
                        break;
                    case "5":
                    case "B2":
                        B2 = currentSymbol;
                        break;
                    case "6":
                    case "C2":
                        C2 = currentSymbol;
                        break;
                    case "7":
                    case "A3":
                        A3 = currentSymbol;
                        break;
                    case "8":
                    case "B3":
                        B3 = currentSymbol;
                        break;
                    case "9":
                    case "C3":
                        C3 = currentSymbol;
                        break;
                }
                winner = checkWin();
            } while (winFlag == 0);
            ShowBoard();
        }

        private static Player checkWin()
        {
            //Vertical Win
            if ((A1 == A2 && A1 == A3) ||
                (B1 == B2 && B1 == B3) ||
                (C1 == C2 && C1 == C3))
            {
                winFlag = 1;
            }

            //Horizontal Win
            if ((A1 == B1 && A1 == C1) ||
                (A2 == B2 && A2 == C2) ||
                (A3 == B3 && A3 == C3))
            {
                winFlag = 1;
            }

            //Diagonal win
            if ((A1 == B2 && A1 == C3) ||
                (A1 == B2 && A1 == C3) ||
                (C1 == B2 && C1 == A3))
            {
                winFlag = 1;
            }

            if (winFlag == 1)
            {
                if (currentplayer % 2 == 0)
                {
                    p2.Score++;
                    return p2;
                }
                else
                {
                    p1.Score++;
                    return p1;
                }
            }

            if (A1 != "1" && B1 != "2" && C1 != "3" && A2 != "4" && B2 != "5" && C2 != "6" && A3 != "7" && B3 != "8" && C3 != "9")
            {
                winFlag = -1;
                return null;
            }
            return null;
        }

        private static void ShowScore()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"The Score is {p1.Name}: {p1.Score}, {p2.Name}: {p2.Score}");
            Console.ResetColor();
        }

        private static void ClearBoard()
        {
            A1 = "1";
            B1 = "2";
            C1 = "3";
            A2 = "4";
            B2 = "5";
            C2 = "6";
            A3 = "7";
            B3 = "8";
            C3 = "9";
            currentplayer = 0;
            winFlag = 0;
        }

    }
}
