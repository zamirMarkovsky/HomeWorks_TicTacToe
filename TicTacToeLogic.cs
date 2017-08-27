using System;
using C17_Ex02;
using Ex02.ConsoleUtils;

namespace C17_Ex02
{
    class TicTacToeLogic
    {
        private static readonly char[] symbols = {'X', 'O'};
        private static int numOfTurns = 0;

        // TODO: singleplayer game
        public static void singlePlayer(char[,] i_table)
        {
            throw new NotImplementedException();
        }

        // multiplayer game
        public static void multiPlayer(char[,] i_table)
        {
            PlayerDetails player1 = new PlayerDetails();
            PlayerDetails player2 = new PlayerDetails();

            player1.PlayerSymbol = symbols[0];
            player2.PlayerSymbol = symbols[1];

            Console.Write("\nFirst player - ");
            TicTacToeUserInterface.inputName(ref player1);
            Console.Write("Second player -");
            TicTacToeUserInterface.inputName(ref player2);

            Console.WriteLine("\n{0} begins, follow the orders in every level\n" +
                              "to quit at any point press 'Q', " +
                              "press 'Enter to start the game!'\n", player1.m_Name);
            Console.ReadKey();
            Screen.Clear();

            bool gameRunning = true;

            while (gameRunning)
            {
                TicTacToeUserInterface.drawTable(i_table);//draws the table
                gameRunning = playerInput(symbols[numOfTurns % 2], i_table, 
                              (numOfTurns % 2 == 0) ? player1 : player2);
                bool isPlayerLost = checkforStraight(i_table, symbols[numOfTurns % 2], numOfTurns);
                
                if (isPlayerLost)
                {
                    break;
                }
                numOfTurns++;
                Screen.Clear();
            }
            if (player1.m_PlayerSymbol == symbols[numOfTurns % 2])
            {
                player2.m_Points++;
            }
            else if (player2.m_PlayerSymbol == symbols[numOfTurns % 2])
            {
                player1.m_Points++;
            }
        }

        private static bool playerInput(char i_symbol, char[,] io_table, PlayerDetails player)
        {
            bool isInteger = false;
            bool isInsertable = false;

            Console.WriteLine("{0}, Please make your move", player.m_Name);
            do
            {              
                try
                {
                    Console.Write("please enter row selection: ");
                    string input1 = Console.ReadLine();
                    if (string.IsNullOrEmpty(input1))
                    {
                        Console.WriteLine("You didn't enter a value!");
                        continue;
                    }
                    char rowInput = char.Parse(input1);

                    Console.Write("Please enter collumn selection: ");
                    string input2 = Console.ReadLine();
                    if (string.IsNullOrEmpty(input2))
                    {
                        Console.WriteLine("You didn't enter a value!");
                        continue;
                    }
                    char colInput = char.Parse(input2);

                    // Checks if player wants to end match 
                    if (colInput == 'Q' || rowInput == 'Q' || colInput == 'q' || rowInput == 'q')
                    {
                        return false;
                    }

                    int indexRow = 0, indexCol = 0;
                    isInteger = int.TryParse(rowInput.ToString(), out indexRow) && 
                                int.TryParse(colInput.ToString(), out indexCol);

                    if (!isInteger)
                    {
                        throw new InvalidOperationException("please enter numbers of rows and cols");
                    }
                    else
                    {
                        isInsertable = TryInsert(i_symbol, ref io_table, indexRow - 1, indexCol - 1);
                        if (isInsertable == false)
                        {
                            throw new InvalidOperationException("The is no space in this spot, please try again");
                        }
                    }

                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Your input is invalid, please try again");
                }
            } while (!isInteger || !isInsertable);

            return true;
        }

        private static bool TryInsert(char symbol, ref char[,] io_table, int rowInput, int colInput)
        {
            if (io_table[rowInput, colInput] == ' ')
            {
                io_table[rowInput, colInput] = symbol;
            }

            return io_table[rowInput, colInput] == symbol;
        }

        private static bool checkforStraight(char[,] io_table, char symbol, int numOfSteps)
        {
            int edgeSize = io_table.GetLength(0);
            bool isStraight = false;

            if (numOfSteps >= (edgeSize * 2 - 1))
            {
                //Check rows
                for (int rowIndex = 0; rowIndex < edgeSize && isStraight == false; rowIndex++)
                {
                    for (int colIndex = 0; colIndex < edgeSize - 1; colIndex++)
                    {
                        if (io_table[rowIndex, colIndex] != io_table[rowIndex, colIndex + 1] ||
                            io_table[rowIndex, colIndex] != symbol)
                        {
                            break;
                        }
                        else if (colIndex == edgeSize - 2)
                        {
                            isStraight = true;
                        }
                    }
                }
                //check columns
                for (int rowIndex = 0; rowIndex < edgeSize; rowIndex++)
                {
                    for (int colIndex = 0; colIndex < edgeSize - 1; colIndex++)
                    {
                        if (io_table[colIndex, rowIndex] != io_table[colIndex, rowIndex + 1] ||
                            io_table[colIndex, rowIndex] != symbol)
                        {
                            break;
                        }
                        else if (rowIndex == edgeSize - 2)
                        {
                            isStraight = true;
                        }
                    }
                }
                //Check first diagonal
                for (int rowAndColIndex = 0; rowAndColIndex < edgeSize - 1; rowAndColIndex++)
                {
                    if (io_table[rowAndColIndex, rowAndColIndex] != io_table[rowAndColIndex, rowAndColIndex + 1]
                        || io_table[rowAndColIndex, rowAndColIndex] != symbol)
                    {
                        break;
                    }
                    else if (rowAndColIndex == edgeSize - 2)
                    {
                        isStraight = true;
                    }
                }
                //Check second diagonal
                int collumnIndex = edgeSize - 1;
                for (int rowIndex = 0; rowIndex < edgeSize - 1; rowIndex++)
                {
                    if (io_table[rowIndex, collumnIndex] != io_table[rowIndex + 1, collumnIndex - 1] ||
                        io_table[rowIndex, collumnIndex] != symbol)
                    {
                        break;
                    }
                    else if (collumnIndex == 1)
                    {
                        isStraight = true;
                    }
                    collumnIndex--;
                }
            }
            return isStraight;
        }
    }
}
