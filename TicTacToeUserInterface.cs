using System;
using Ex02.ConsoleUtils;

namespace C17_Ex02
{
    class TicTacToeUserInterface
    {
        private static readonly char[] m_Symbols = {'X', 'O'};
        private static int m_NumOfTurns;
        private static bool m_GameRunning = true;

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
        }

        //The method gets player's input, creates a matrix accordingly and returns it
        public static char[,] tableSizeInput()
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

        //The method intialize the table's values
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

        //The method puts Player input into PlayerDetails object
        public static void inputName(ref PlayerDetails io_player)
        {
            Console.Write("Enter your name: ");
            io_player.m_Name = Console.ReadLine();
        }

        //The method Draws the game board
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

        public static void optionSelect()
        {
            bool loopRunner = true;
            do
            {
                Console.Write("Please Choose an option: ");
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
                    else if (optionChosen < 1 || optionChosen > 4)
                    {
                        throw new InvalidOperationException("Please enter number between 1 to 4!");
                    }
                    Screen.Clear();
                    Options answer = (Options)optionChosen;
                    switch (answer)
                    {
                        case Options.SinglePlayer:
                        {
                            gameTable = tableSizeInput();
                            singlePlayer(ref gameTable);
                            break;
                        }
                        case Options.MultiPlayer:
                        {
                            gameTable = tableSizeInput();
                            multiPlayer(ref gameTable);
                            break;
                        }
                        case Options.Instractions:
                            break;
                        case Options.Exit:
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
        
        //The method runs the single-player game
        private static void singlePlayer(ref char[,] io_singleGameBoard)
        {
            PlayerDetails player = new PlayerDetails();
            PlayerDetails computer = new PlayerDetails();
            m_NumOfTurns = 0;

            player.m_PlayerSymbol = m_Symbols[0];
            computer.m_PlayerSymbol = m_Symbols[1];

            Console.WriteLine();
            inputName(ref player);
            computer.m_Name = "Computer";

            Console.WriteLine("\nYou Begin, follow the orders in every level\n" +
                              "to quit at any point press 'Q', " +
                              "press 'Enter to start the game!'\n");
            Console.ReadKey();
            Screen.Clear();

            while (m_GameRunning && m_NumOfTurns != io_singleGameBoard.Length)
            {
                drawTable(io_singleGameBoard); //draws the table

                if (m_Symbols[m_NumOfTurns % 2] == player.m_PlayerSymbol)
                {
                    m_GameRunning = TicTacToeLogic.playerInput(player.m_PlayerSymbol, io_singleGameBoard, player);
                    if (!m_GameRunning)
                    {
                        Screen.Clear();
                        //TODO Change to call for method - chooseOption
                        Console.WriteLine("You left the game, see you soon");
                        return;
                    }
                }
                else if (m_Symbols[m_NumOfTurns % 2] == computer.m_PlayerSymbol)
                {
                    TicTacToeLogic.computerMove(ref io_singleGameBoard, computer);
                }

                bool isPlayerLost = TicTacToeLogic.checkforStraight(io_singleGameBoard, m_Symbols[m_NumOfTurns % 2], m_NumOfTurns);
                m_NumOfTurns++;

                if (m_NumOfTurns == io_singleGameBoard.Length)
                {
                    Console.WriteLine("You got 'Draw', Game Over!");
                    continue;
                }
                if (isPlayerLost)
                {
                    m_GameRunning = false;
                    continue;
                }
                Screen.Clear();
            }
            Screen.Clear();
            drawTable(io_singleGameBoard);
            TicTacToeLogic.findWinner(player, computer, m_Symbols[(m_NumOfTurns + 1) % 2]);
        }
        
        //The method runs the multi-player game
        private static void multiPlayer(ref char[,] io_multiGameBoard)
        {
            PlayerDetails player1 = new PlayerDetails();
            PlayerDetails player2 = new PlayerDetails();
            m_NumOfTurns = 0;

            player1.m_PlayerSymbol = m_Symbols[0];
            player2.m_PlayerSymbol = m_Symbols[1];

            Console.Write("\nFirst player - ");
            inputName(ref player1);
            Console.Write("Second player -");
            inputName(ref player2);

            Console.WriteLine("\n{0} begins, follow the orders in every level\n" +
                              "to quit at any point press 'Q', " +
                              "press 'Enter to start the game!'\n", player1.m_Name);
            Console.ReadKey();
            Screen.Clear();

            m_GameRunning = true;

            while (m_GameRunning && m_NumOfTurns != io_multiGameBoard.Length)
            {
                drawTable(io_multiGameBoard); //draws the table
                m_GameRunning = TicTacToeLogic.playerInput(m_Symbols[m_NumOfTurns % 2], io_multiGameBoard,
                    (m_NumOfTurns % 2 == 0) ? player1 : player2);

                if (!m_GameRunning)
                {
                    Screen.Clear();
                    Console.WriteLine("You left the game, see you soon");
                    return;
                }

                bool isPlayerLost =
                    TicTacToeLogic.checkforStraight(io_multiGameBoard, m_Symbols[m_NumOfTurns % 2], m_NumOfTurns);
                m_NumOfTurns++;

                //if table is full
                if (m_NumOfTurns == io_multiGameBoard.Length)
                {
                    Console.WriteLine("You got 'Draw', Game Over!");
                    continue;
                }
                //if player lost
                if (isPlayerLost)
                {
                    m_GameRunning = false;
                    continue;
                }
                Screen.Clear();
            }
            Screen.Clear();
            drawTable(io_multiGameBoard);
            TicTacToeLogic.findWinner(player1, player2, m_Symbols[(m_NumOfTurns + 1) % 2]);
        }
        /*
        private static void presentInstractions()
        {
            
        }
        */
    }
}


