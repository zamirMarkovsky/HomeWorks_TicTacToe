using System;
using Ex02.ConsoleUtils;

namespace C17_Ex02
{
    class TicTacToeUserInterface
    {
        protected enum Options
        {
            SinglePlayer = 1,
            MultiPlayer,
            Instractions,
            Exit
        };

        public static void displayWelcome()
        {
            Console.Write(@"
                        _________________
                       | Welcome to the  |
                       |     fliped      |
                       |   Tic Tac Toe   |
                       |_________________|
                       
                       ===================
                       |  X  |  O  |     |
                       -------------------
                       |  O  |  X  |     |
                       -------------------
                       |  O  |     |  X  |
                       ===================");
            Console.WriteLine("\n");
        }

        public static void displayMenu()
        {
            Console.WriteLine("Main Menu");
            Console.WriteLine("\n1. Single Player");
            Console.WriteLine("2. Multi Player");
            Console.WriteLine("3. Instractions");
            Console.WriteLine("4. Exit");

            bool loopRunner = true;
            do
            {
                Console.Write("Please Choose an option: ");
                try
                {
                    string userDecision = Console.ReadLine();
                    int optionChosen;
                    bool convertionSucceded = Int32.TryParse(userDecision, out optionChosen);

                    if (!convertionSucceded)
                    {
                        throw new InvalidOperationException("Please enter chosen option number!");
                    }
                    else if (optionChosen < 1 || optionChosen > 4)
                    {
                        throw new InvalidOperationException("Please enter number between 1 to 4!");
                    }
                    Screen.Clear();

                    Options answer = (Options) optionChosen;
                    switch (answer)
                    {
                        case Options.SinglePlayer:
                        {
                            char[,] gameTable = tableSizeInput();
                            //TicTacToeLogic.singlePlayer(gameTable);
                            break;
                        }
                        case Options.MultiPlayer:
                        {
                            char[,] gameTable = tableSizeInput();
                            TicTacToeLogic.multiPlayer(gameTable);
                            break;
                        }
                        case Options.Instractions:
                        case Options.Exit:
                        {
                            Console.WriteLine("Hope to see you again soon!");
                            break;
                        }
                    }
                    Console.ReadLine();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                loopRunner = false;
            } while (loopRunner);
        }

        public static char[,] tableSizeInput()
        {
            bool loopRunner = true;
            char[,] io_gameTable = null;

            Console.WriteLine("Please set the size of the table.");
            do
            {
                Console.Write("Enter a size between 3 to 9: ");
                try
                {
                    string userSize = Console.ReadLine();
                    int tableSize;
                    bool convertionSucceded = Int32.TryParse(userSize, out tableSize);

                    if (!convertionSucceded)
                    {
                        throw new InvalidOperationException("Please enter numbers only");
                    }
                    else if (tableSize < 3 || tableSize > 9)
                    {
                        throw new InvalidOperationException("You inserted an invalid number, try again!");
                    }
                    io_gameTable = new char[tableSize, tableSize]; //A matrix for the game values
                    initTable(io_gameTable); //initiate matrix with white spaces
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                loopRunner = false;
            } while (loopRunner);

            return io_gameTable;
        }

        public static void initTable(char[,] io_table)
        {
            int length = io_table.GetLength(0);
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    io_table[i, j] = ' ';
                }
            }
        }

        public static void inputName(ref PlayerDetails io_player)
        {
            Console.Write("Enter your name: ");
            io_player.m_Name = Console.ReadLine();
        }

        public static void drawTable(char[,] io_mat)
        {
            int edgeSize = io_mat.GetLength(0);

            //Upper area creation
            for (int i = 0; i < edgeSize; i++)
            {
                Console.Write("   {0}", i + 1);
            }
            Console.WriteLine();
            //Table Creation
            for (int i = 0; i < edgeSize; i++)
            {
                Console.Write(i + 1);
                for (int j = 0; j < edgeSize; j++)
                {
                    Console.Write("| {0} ", io_mat[i, j]);
                }
                Console.WriteLine("|");
                Console.Write(" ");
                for (int j = 0; j < edgeSize; j++)
                {
                    Console.Write("====");
                }
                Console.WriteLine("=");
            }
        }


    }
}


