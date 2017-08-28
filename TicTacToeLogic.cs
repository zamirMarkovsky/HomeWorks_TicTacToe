using System;

namespace C17_Ex02
{
    class TicTacToeLogic
    {
        //The method inserts player's move and returns true when move accepted, false if he choose to end match
        public static bool playerInput(char i_symbol, char[,] io_table, PlayerDetails player)
        {
            bool isInteger = false;

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

                    int indexRow;
                    int indexCol;
                    isInteger = int.TryParse(rowInput.ToString(), out indexRow);
                    isInteger = int.TryParse(colInput.ToString(), out indexCol);

                    if (!isInteger)
                    {
                        throw new InvalidOperationException("please enter numbers of rows and cols");
                    }

                    bool isInsertable = TryInsert(i_symbol, ref io_table, indexRow - 1, indexCol - 1);
                    if (!isInsertable)
                    {
                        throw new InvalidOperationException("There is no space in this spot, please try again");
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
            } while (!isInteger && !isInteger);

            return true; //Returns that player made his move
        }

        public static void computerMove(ref char[,] io_table, PlayerDetails o_compDetails)
        {
            Random randNum = new Random();
            int rowInput;
            int colInput;

            do
            {
                rowInput = randNum.Next(io_table.GetLength(0));
                colInput = randNum.Next(io_table.GetLength(0));
            } while (!TryInsert(o_compDetails.m_PlayerSymbol, ref io_table, rowInput, colInput));
        }

        //The method tries to insert to symbol to the desired place and returns if successful
        private static bool TryInsert(char symbol, ref char[,] io_table, int rowInput, int colInput)
        {
            bool isChanged = false;

            if (io_table[rowInput, colInput] == ' ')
            {
                io_table[rowInput, colInput] = symbol;
                isChanged = true;
            }
            return isChanged;
        }

        //The method checks all the possible options for a streight and returns if successful
        public static bool checkforStraight(char[,] io_table, char symbol, int numOfSteps)
        {
            int edgeSize = io_table.GetLength(0);
            bool isStraight = false;

            if (numOfSteps >= (edgeSize * 2 - 2))
            {
                int matchesCount;
                
                //Check rows
                for (int rowIndex = 0; rowIndex < edgeSize && !isStraight; rowIndex++)
                {
                    matchesCount = 0;
                    for (int colIndex = 0; colIndex < edgeSize; colIndex++)
                    {
                        if (io_table[rowIndex, colIndex] == symbol)
                        {
                            matchesCount++;
                        }
                    }
                    isStraight = edgeSize == matchesCount;
                }
                //check columns
                for (int colIndex = 0; colIndex < edgeSize && !isStraight; colIndex++)
                {
                    matchesCount = 0;
                    for (int rowIndex = 0; rowIndex < edgeSize; rowIndex++)
                    {
                        if (io_table[rowIndex, colIndex] == symbol)
                        {
                            matchesCount++;
                        }
                    }
                    isStraight = edgeSize == matchesCount;
                }
                //Check first diagonal
                matchesCount = 0;
                for (int rowAndColIndex = 0; rowAndColIndex < edgeSize && !isStraight; rowAndColIndex++)
                {
                    if (io_table[rowAndColIndex, rowAndColIndex] == symbol)
                    {
                        matchesCount++;
                    }
                    isStraight = edgeSize == matchesCount;
                }
                //Check second diagonal
                matchesCount = 0;
                for (int rowIndex = edgeSize - 1; rowIndex >= 0 && !isStraight; rowIndex--)
                {
                    int colIndex = (edgeSize - 1) - rowIndex;
                    if (io_table[rowIndex, colIndex] == symbol)
                    {
                        matchesCount++;
                    }
                    isStraight = edgeSize == matchesCount;
                }

            }
            return isStraight;
        }

        //The method declairs the winner
        public static void findWinner(PlayerDetails io_player1, PlayerDetails io_player2, char symbol)
        {
            PlayerDetails winner;
            if (io_player1.m_PlayerSymbol == symbol)
            {
                io_player1.m_Points++;
                winner = io_player1;
            }
            else
            {
                io_player2.m_Points++;
                winner = io_player2;
            }

            Console.WriteLine("\n" +
                              "==========================\n" +
                              "    {0} won the game,\n" +
                              "    Congratulations!!\n" +  
                              "==========================", winner.m_Name);
        }
        /*
        private static void playAgain()
        {
            int userChoise;
            bool validInput = true;
            do
            {
                try
                {
                    Console.WriteLine("Would you like to play again?\n1.Yes\n2.No");
                    userChoise = Console.Read();
                    if(userChoise!=2&&userChoise!=1)
                    {
                        throw new Exception("Choose only 1 or 2");
                    }
                    else
                    {
                        validInput = true;
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } while (validInput);
        } 
        */
    }
}
