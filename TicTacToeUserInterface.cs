using System;
using Ex02.ConsoleUtils;

namespace C17_Ex02
{

    class TicTacToeUserInterface
    {
        private static readonly char[] m_Symbols = {'X', 'O'};
        private static int m_NumOfTurns;
        private static bool m_GameRunning = true;
        private static int m_PointsSum = 0;

        private enum StartGameOptions
        {
            SinglePlayer = 1,
            MultiPlayer,
            Instractions,
            Exit
        };

        private enum EndGameOptions
        {
            PlayAgain = 1,
            MainMenu,
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
        }

        //The method gets player's input, creates a matrix accordingly and returns it
        private static char[,] tableSizeInput()
        {
            bool loopRunner = true;
            char[,] io_gameTable = null;

            Screen.Clear();
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
                    if (tableSize < 3 || tableSize > 9)
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

        //The method intialize the table's values
        private static void initTable(char[,] io_table)
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

        //The method puts Player input into PlayerDetails object
        private static void inputName(ref PlayerDetails io_player)
        {
            Console.Write("Enter your name: ");
            io_player.m_Name = Console.ReadLine();
        }

        //The method Draws the game board
        private static void drawTable(char[,] io_mat)
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

        public static void optionSelect()
        {
            bool loopRunner = true;

            do
            {
                Console.Write("\nPlease Choose an option: ");
                try
                {
                    string userDecision = Console.ReadLine();
                    int optionChosen;
                    bool convertionSucceded = Int32.TryParse(userDecision, out optionChosen);
                    char[,] gameTable;

                    if (!convertionSucceded)
                    {
                        throw new InvalidOperationException("Please enter chosen option number!");
                    }
                    if (optionChosen < 1 || optionChosen > 4)
                    {
                        throw new InvalidOperationException("Please enter number between 1 to 4!");
                    }
                    Screen.Clear();
                    StartGameOptions startAnswer = (StartGameOptions) optionChosen;
                    switch (startAnswer)
                    {
                        case StartGameOptions.SinglePlayer:
                        {
                            PlayerDetails player = new PlayerDetails();
                            PlayerDetails computer = new PlayerDetails();

                            EndGameOptions endAnswer;

                            player.m_PlayerSymbol = m_Symbols[0];
                            computer.m_PlayerSymbol = m_Symbols[1];

                            inputName(ref player);
                            computer.m_Name = "Computer";

                            do
                            {
                                gameTable = tableSizeInput();
                                singlePlayer(ref gameTable, ref player, ref computer);

                                endAnswer = (EndGameOptions)afterGameOptions();

                                if (endAnswer == EndGameOptions.MainMenu)
                                {
                                    break;
                                }
                                if (endAnswer == EndGameOptions.Exit)
                                {
                                    Environment.Exit(0);
                                }
                            } while (endAnswer == EndGameOptions.PlayAgain);

                            if (endAnswer == EndGameOptions.MainMenu)
                            {
                                Screen.Clear();
                                displayMenu();
                                continue;
                            }
                            break;
                        }
                        case StartGameOptions.MultiPlayer:
                        {
                            PlayerDetails player1 = new PlayerDetails();
                            PlayerDetails player2 = new PlayerDetails();

                            EndGameOptions endAnswer;

                            player1.m_PlayerSymbol = m_Symbols[0];
                            player2.m_PlayerSymbol = m_Symbols[1];

                            Console.Write("First player - ");
                            inputName(ref player1);
                            Console.Write("Second player -");
                            inputName(ref player2);

                            do
                            {
                                gameTable = tableSizeInput();
                                multiPlayer(ref gameTable, ref player1, ref player2);

                                endAnswer = (EndGameOptions) afterGameOptions();

                                if (endAnswer == EndGameOptions.MainMenu)
                                {
                                    break;
                                }
                                if (endAnswer == EndGameOptions.Exit)
                                {
                                    Environment.Exit(0);
                                }
                            } while (endAnswer == EndGameOptions.PlayAgain);

                            if (endAnswer == EndGameOptions.MainMenu)
                            {
                                Screen.Clear();
                                displayMenu();
                                continue;
                            }
                            break;
                        }
                        case StartGameOptions.Instractions:
                            presentInstractions();
                            displayMenu();
                            continue;
                        case StartGameOptions.Exit:
                        {
                            Environment.Exit(0);
                            break;
                        }
                    }
                    //anounceWinner(gameTable);
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

        //Displays Game Instractions
        private static void presentInstractions()
        {
            Console.WriteLine("Game Instractions: ");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("You can choose to play against another player or the computer. ");
            Console.WriteLine("\nWhen you start the game you coose game board size, between 3*3 to 9*9.");
            Console.WriteLine("The game starts with an empty board, which can contain\n" +
                              "two symbols - 'X' or 'O', so each player uses his symbol.");
            Console.WriteLine("\nIn each turn the player puts a symbol into a cell in the game board,\n" +
                              "in order to make the opponent create a sequence of his type\n" +
                              "as the length of the board.");
            Console.WriteLine("Meaning, if the board length is 5*5,\n" +
                              "The first player who reaches a sequence of 5 symbols loses!");

            Console.WriteLine("\nPress 'Enter' to return to main munu!");
            Console.ReadKey();

            Screen.Clear();
        }

        private static void displayResults(PlayerDetails p1Details, PlayerDetails p2Details) 
        {
            Console.Write("Match Result - ");
            Console.WriteLine("{0} : {1}", p1Details.m_Points, p2Details.m_Points);

            if (m_PointsSum == p1Details.m_Points + p2Details.m_Points) //Draw
            {
                Console.WriteLine("\n" +
                                  "==========================\n" +
                                  "      You got draw!!\n" +
                                  "==========================");
            }
            else //Checks who won and prints his name
            {
                Console.WriteLine("\n" +
                                  "==========================\n" +
                                  "    {0} won the game,\n" +
                                  "    Congratulations!!\n" +
                                  "==========================",
                    p1Details.m_Points > p2Details.m_Points ? p1Details.m_Name : p2Details.m_Name);
            }

            m_PointsSum = p1Details.m_Points + p2Details.m_Points;
        }

        private static int afterGameOptions()
        {
            bool loopRunner = true;
            int optionChosen = 2;

            Console.WriteLine("\n1. Play again");
            Console.WriteLine("2. Return to main menu");
            Console.WriteLine("3. Quit the game\n");

            do
            {
                Console.Write("Please Choose an option: ");
                try
                {
                    string userDecision = Console.ReadLine();
                    
                    bool convertionSucceded = Int32.TryParse(userDecision, out optionChosen);
                    
                    if (!convertionSucceded)
                    {
                        throw new InvalidOperationException("Please enter chosen option number!");
                    }
                    if (optionChosen < 1 || optionChosen > 3)
                    {
                        throw new InvalidOperationException("Please enter number between 1 to 3!");
                    }
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

            return optionChosen;
        }

        //The method runs the single-player game
        private static void singlePlayer(ref char[,] io_singleGameBoard, ref PlayerDetails human, ref PlayerDetails comp)
        {
            m_NumOfTurns = 0;
            
            Console.WriteLine("\nYou Begin, follow the orders in every level\n" +
                              "to quit at any point press 'Q', " +
                              "press 'Enter to start the game!'\n");
            Console.ReadKey();
            Screen.Clear();

            while (m_GameRunning && m_NumOfTurns != io_singleGameBoard.Length)
            {
                drawTable(io_singleGameBoard); //draws the table

                if (m_Symbols[m_NumOfTurns % 2] == human.m_PlayerSymbol)
                {
                    m_GameRunning = playerInput(human.m_PlayerSymbol, io_singleGameBoard, human);
                    if (!m_GameRunning)
                    {
                        Screen.Clear();
                        //TODO Change to call for method - chooseOption
                        Console.WriteLine("You left the game, see you soon");
                        return;
                    }
                }
                else if (m_Symbols[m_NumOfTurns % 2] == comp.m_PlayerSymbol)
                {
                    TicTacToeLogic.computerMove(ref io_singleGameBoard, comp);
                }

                bool isPlayerLost =
                    TicTacToeLogic.checkforStraight(io_singleGameBoard, m_Symbols[m_NumOfTurns % 2], m_NumOfTurns);
                if (isPlayerLost)
                {
                    TicTacToeLogic.findWinner(ref human, ref comp, m_Symbols[m_NumOfTurns % 2]);
                    m_GameRunning = false;
                    continue;
                }

                m_NumOfTurns++;
                Screen.Clear();
            }

            Screen.Clear();
            drawTable(io_singleGameBoard);
            displayResults(human, comp);
        }

        //The method runs the multi-player game
        private static void multiPlayer(ref char[,] io_multiGameBoard, ref PlayerDetails p1, ref PlayerDetails p2)
        {
            m_NumOfTurns = 0;

            Console.WriteLine("\n{0} begins, follow the orders in every level\n" +
                              "to quit at any point press 'Q', " +
                              "press 'Enter to start the game!'\n", p1.m_Name);
            Console.ReadKey();
            Screen.Clear();

            m_GameRunning = true;

            while (m_GameRunning && m_NumOfTurns != io_multiGameBoard.Length)
            {
                drawTable(io_multiGameBoard); //draws the table
                m_GameRunning = playerInput(m_Symbols[m_NumOfTurns % 2], io_multiGameBoard,
                    (m_NumOfTurns % 2 == 0) ? p1 : p2);

                if (!m_GameRunning)
                {
                    Screen.Clear();
                    Console.WriteLine("You left the game, see you soon");
                    return;
                }
                bool isPlayerLost = TicTacToeLogic.checkforStraight(io_multiGameBoard, m_Symbols[m_NumOfTurns % 2], m_NumOfTurns);
                
                //if player lost
                if (isPlayerLost)
                {
                    TicTacToeLogic.findWinner(ref p1, ref p2, m_Symbols[m_NumOfTurns % 2]);
                    m_GameRunning = false;
                    continue;
                }

                m_NumOfTurns++;
                Screen.Clear();
            }
            
            Screen.Clear();
            drawTable(io_multiGameBoard);
            displayResults(p1, p2);
        }

        //The method inserts player's move and returns true when move accepted, false if he choose to end match
        private static bool playerInput(char i_symbol, char[,] io_table, PlayerDetails player)
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

                    bool isInsertable = TicTacToeLogic.TryInsert(i_symbol, ref io_table, indexRow - 1, indexCol - 1);
                    if (!isInsertable)
                    {
                        throw new InvalidOperationException("There is no space in this spot, please try again");
                    }
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception)
                {
                    Console.WriteLine("Your input is invalid, please try again");
                }
            } while (!isInteger && !isInteger);

            return true; //Returns that player made his move
        }
    }
}