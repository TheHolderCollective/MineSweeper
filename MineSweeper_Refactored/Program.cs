namespace MineSweeper_Refactored;

internal class Program
{
    static void Main(string[] args)
    {
        GameEngine minesweeperEngine = new GameEngine();

        minesweeperEngine.RunGame();
    }

    //static int bombCount;
    //static int x_choice;
    //static int y_choice;
    //static int boardSize;
    //static int level;

    //static void Main(string[] args)
    //{
    //    bool true_info;
    //    bool lose = false;
    //    bool win = false;
    //    bool continue_game = true;

    //    StartGame(ref bombCount, ref boardSize);
    //    Thread.Sleep(100);

    //    bool[,] choice_board = new bool[boardSize, boardSize];
    //    char[,] game_board = new char[boardSize, boardSize];

    //    SetBombs(ref game_board, bombCount, ref choice_board);

    //    while (continue_game)
    //    {
    //        true_info = true;
    //        Console.Clear();
    //        PrintBoard(game_board, lose, choice_board);
    //        Console.WriteLine();

    //        try
    //        {
    //            Console.Write("insert x:");
    //            x_choice = int.Parse(Console.ReadLine());
    //            Console.Write("insert y:");
    //            y_choice = int.Parse(Console.ReadLine());
    //        }
    //        catch
    //        {
    //            true_info = false;
    //        }

    //        if (x_choice < 1 || x_choice > boardSize || y_choice < 1 || y_choice > boardSize)
    //        {
    //            true_info = false;
    //        }

    //        CheckWin(ref win, choice_board);

    //        if (true_info)
    //        {
    //            CheckGame(ref game_board, x_choice - 1, y_choice - 1, ref lose, ref win, ref choice_board, ref continue_game);
    //        }

    //        CheckWin(ref win, choice_board);

    //    }
    //    Console.ReadKey();
    //}

    //public static void FindEmpty(ref char[,] char_board, int x, int y, ref bool[,] bool_board)
    //{
    //    if (bool_board[x, y] == false)
    //    {
    //        if (char_board[x, y] != 'B')
    //        {
    //            bool_board[x, y] = true;
    //        }
    //        if (x > 0)
    //        {
    //            if (char_board[x - 1, y] != 'B')
    //            {
    //                bool_board[x - 1, y] = true;

    //            }
    //        }

    //        if (x > 0 && y < boardSize - 1)
    //        {
    //            if (char_board[x - 1, y + 1] != 'B')
    //            {
    //                bool_board[x - 1, y + 1] = true;

    //            }
    //        }

    //        if (x > 0 && y > 0)
    //        {
    //            if (char_board[x - 1, y - 1] != 'B')
    //            {
    //                bool_board[x - 1, y - 1] = true;

    //            }
    //        }

    //        if (y > 0)
    //        {
    //            if (char_board[x, y - 1] != 'B')
    //            {
    //                bool_board[x, y - 1] = true;

    //            }
    //        }

    //        if (y < boardSize - 1)
    //        {
    //            if (char_board[x, y + 1] != 'B')
    //            {
    //                bool_board[x, y + 1] = true;

    //            }
    //        }

    //        if (x < boardSize - 1 && y < boardSize - 1)
    //        {
    //            if (char_board[x + 1, y + 1] != 'B')
    //            {
    //                bool_board[x + 1, y + 1] = true;

    //            }
    //        }

    //        if (x < boardSize - 1 && y > 0)
    //        {
    //            if (char_board[x + 1, y - 1] != 'B')
    //            {
    //                bool_board[x + 1, y - 1] = true;

    //            }
    //        }


    //    }
    //}

    //public static void CheckGame(ref char[,] char_board, int x, int y, ref bool lose_status, ref bool win_status, ref bool[,] bool_board, ref bool continue_status)
    //{
    //    if (char_board[x, y] == 'B')
    //    {
    //        continue_status = false;

    //        lose_status = true;
    //        win_status = false;
    //        Console.Clear();
    //        Console.ResetColor();
    //        Console.ForegroundColor = ConsoleColor.Red;

    //        Console.WriteLine("=================\\ !!! You Lose !!! /=================");

    //        for (int i = 0; i < boardSize; i++)
    //        {

    //            for (int j = 0; j < boardSize; j++)
    //            {
    //                Console.Write($"|{char_board[i, j]}|");
    //            }
    //            Console.WriteLine();
    //        }
    //        Console.WriteLine();
    //        Console.Write("Reset Game(Y or N):");
    //        string reset = Console.ReadLine();
    //        if (reset == "Y" || reset == "y")
    //        {
    //            continue_status = true;
    //            Console.ResetColor();
    //            ResetGame(ref lose_status, ref win_status, ref char_board, ref bool_board);
    //            Console.ResetColor();
    //        }
    //        else
    //        {
    //            Console.Clear();
    //            Console.WriteLine("\n\n\n\t\t Game Over");
    //            Thread.Sleep(3999);
    //            Console.ResetColor();
    //            Environment.Exit(0);

    //        }

    //    }
    //    else
    //    {
    //        FindEmpty(ref char_board, x, y, ref bool_board);
    //    }

    //    CheckWin(ref win_status, bool_board);
    //    if (win_status)
    //    {
    //        continue_status = false;

    //        lose_status = true;
    //        win_status = false;
    //        Console.Clear();
    //        Console.ResetColor();
    //        Console.ForegroundColor = ConsoleColor.DarkGreen;

    //        Console.WriteLine("=================\\ !!! You Win !!! /=================");
    //        for (int i = 0; i < boardSize; i++)
    //        {
    //            Console.ForegroundColor = ConsoleColor.DarkGreen;
    //            for (int j = 0; j < boardSize; j++)
    //            {
    //                Console.Write($"|{char_board[i, j]}|");
    //            }
    //            Console.WriteLine();
    //        }
    //        Console.WriteLine();
    //        Console.Write("Reset Game(Y or N):");
    //        string reset = Console.ReadLine();
    //        if (reset == "Y" || reset == "y")
    //        {
    //            continue_status = true;
    //            Console.ResetColor();
    //            ResetGame(ref lose_status, ref win_status, ref char_board, ref bool_board);
    //            Console.ResetColor();
    //        }
    //        else
    //        {
    //            Console.Clear();
    //            Console.WriteLine("\n\n\n\t\t Game Over");
    //            Thread.Sleep(3999);
    //            Console.ResetColor();
    //            Environment.Exit(0);

    //        }
    //    }
    //}
    //public static void PrintBoard(char[,] char_board, bool statuse, bool[,] bool_board)
    //{
    //    for (int k = 0; k < boardSize; k++)
    //    {
    //        if (k == 0)
    //            Console.Write($"    {k + 1}");
    //        else if (k != 9)
    //            Console.Write($"  {k + 1}");
    //        else if (k == 8)
    //            Console.Write($" {k + 1} ");
    //        else
    //            Console.Write($"  {k + 1}");
    //    }
    //    Console.WriteLine();

    //    for (int i = 0; i < boardSize; i++)
    //    {
    //        for (int j = 0; j < boardSize; j++)
    //        {
    //            if (j == 0 && i != 9)
    //                Console.Write($" {i + 1} ");
    //            else if (i == 9 && j == 0)
    //                Console.Write($"{i + 1} ");

    //            //print bomb if lose
    //            if (char_board[i, j] == 'B' && statuse)
    //            {
    //                Console.Write($"|B|");
    //            }
    //            // print "?" 
    //            else if ((char_board[i, j] == 'B' && !statuse) || (char_board[i, j] == ' ' && bool_board[i, j] == false) || (char_board[i, j] != 'B' && char_board[i, j] != ' ' && bool_board[i, j] == false))
    //            {
    //                Console.Write($"|?|");
    //            }
    //            // print 1,2,3 if selected
    //            else if (char_board[i, j] != 'B' && bool_board[i, j] == true)
    //            {
    //                if (char_board[i, j] != ' ')
    //                    if (char_board[i, j] == '1')
    //                        Console.ForegroundColor = ConsoleColor.DarkBlue;
    //                    else if (char_board[i, j] == '2')
    //                        Console.ForegroundColor = ConsoleColor.DarkGreen;
    //                    else if (char_board[i, j] == '3')
    //                        Console.ForegroundColor = ConsoleColor.DarkYellow;
    //                    else if (char_board[i, j] == '4')
    //                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
    //                    else if (char_board[i, j] == '5')
    //                        Console.ForegroundColor = ConsoleColor.DarkRed;
    //                    else if (char_board[i, j] == '6')
    //                        Console.ForegroundColor = ConsoleColor.DarkCyan;
    //                    else
    //                        Console.ForegroundColor = ConsoleColor.DarkGray;
    //                Console.Write($"|{char_board[i, j]}|");
    //                Console.ResetColor();

    //            }

    //        }
    //        Console.WriteLine();
    //    }
    //}
    //public static void SetBombs(ref char[,] board_char, int number, ref bool[,] board_bool)
    //{

    //    Random rand = new Random();
    //    int x, y;

    //    while (number > 0)
    //    {
    //        do
    //        {
    //            x = rand.Next(0, boardSize);
    //            y = rand.Next(0, boardSize);
    //            //Console.WriteLine($"{x},{y}");
    //        } while (board_char[x, y] == 'B');
    //        board_char[x, y] = 'B';
    //        number--;
    //    }
    //    for (int i = 0; i < boardSize; i++)
    //    {
    //        for (int j = 0; j < boardSize; j++)
    //        {
    //            if (board_char[i, j] != 'B')
    //            {
    //                board_char[i, j] = ' ';
    //            }
    //            board_bool[i, j] = false;
    //        }
    //    }
    //    int num_bomb = 0;
    //    for (int i = 0; i < boardSize; i++)
    //    {
    //        for (int j = 0; j < boardSize; j++)
    //        {
    //            if (board_char[i, j] != 'B')
    //            {
    //                if (i > 0 && board_char[i - 1, j] == 'B') // North neighbor
    //                {
    //                    num_bomb++;
    //                }
    //                if (i > 0 && j < boardSize - 1 && board_char[i - 1, j + 1] == 'B') // East North neighbor
    //                {
    //                    num_bomb++;
    //                }

    //                if (i > 0 && j > 0 && board_char[i - 1, j - 1] == 'B') // West North neighbor
    //                {
    //                    num_bomb++;
    //                }

    //                if (j > 0 && board_char[i, j - 1] == 'B') // West neighbor
    //                {
    //                    num_bomb++;
    //                }

    //                if (j < boardSize - 1 && board_char[i, j + 1] == 'B') // East neighbor
    //                {
    //                    num_bomb++;
    //                }

    //                if (i < boardSize - 1 && j < boardSize - 1 && board_char[i + 1, j + 1] == 'B') // South neighbor
    //                {
    //                    num_bomb++;
    //                }

    //                if (i < boardSize - 1 && j > 0 && board_char[i + 1, j - 1] == 'B') // West South neighbor
    //                {
    //                    num_bomb++;
    //                }
    //                if (i < boardSize - 1 && board_char[i + 1, j] == 'B') // South
    //                {
    //                    num_bomb++;
    //                }

    //            }
    //            if (num_bomb > 0)
    //            {
    //                string str = num_bomb.ToString();
    //                char[] temp = str.ToCharArray();
    //                board_char[i, j] = temp[0];

    //            }
    //            num_bomb = 0;
    //        }
    //    }
    //}
    //static void CheckWin(ref bool win, bool[,] board_bool)
    //{
    //    int cnt = 0;
    //    for (int i = 0; i < boardSize; i++)
    //    {
    //        for (int j = 0; j < boardSize; j++)
    //        {
    //            if (board_bool[i, j] == true)
    //            {
    //                cnt++;
    //            }
    //        }
    //    }
    //    if (cnt + bombCount == (boardSize * boardSize))
    //    {
    //        win = true;
    //    }
    //}
    //static void StartGame(ref int BOMB, ref int SIZE)
    //{
    //    bool true_info = false;
    //    Console.Clear();
        
    //    while (!true_info)
    //    {
    //        PrintCopyRightBanner();
    //        PrintGameOptions();

    //        string? selectedOption = Console.ReadLine();
    //        true_info = true;

    //        try
    //        {
    //            level = Int32.Parse(selectedOption);
    //        }
    //        catch (FormatException e)
    //        {
    //            level = 0; // will trigger the default game board
    //        }
            
    //        switch (level)
    //        {
    //            case 1:
    //                BOMB = 5;
    //                SIZE = 5;
    //                break;

    //            case 2:
    //                BOMB = 15;
    //                SIZE = 8;
    //                break;

    //            case 3:
    //                BOMB = 25;
    //                SIZE = 10;
    //                break;

    //            default:
    //                BOMB = 15;
    //                SIZE = 10;
    //                break;
    //        }
           
    //    }
    //}
    //static void ResetGame(ref bool lose, ref bool win, ref char[,] char_board, ref bool[,] bool_board)
    //{
    //    lose = false;
    //    win = false;
    //    StartGame(ref bombCount, ref boardSize);
    //    bool[,] choice_board = new bool[boardSize, boardSize];
    //    char[,] game_board = new char[boardSize, boardSize];
    //    char_board = game_board;
    //    bool_board = choice_board;
    //    SetBombs(ref char_board, bombCount, ref bool_board);
    //}

    //#region Helper Functions --> I added this code
    //static void PrintCopyRightBanner()
    //{
    //    const int maxStars = 13;

    //    PrintRowofStars(maxStars);
    //    for (int j = 0; j < 2; j++)
    //    {
    //        if (j == 0)
    //        {
    //            Console.WriteLine("*  Farzad Foroozanfar   *");

    //        }
    //        if (j == 1)
    //        {
    //            Console.WriteLine("*                       *");
    //            Console.WriteLine("*  Copyright(c) 2022    *");
    //        }
    //    }
    //    PrintRowofStars(maxStars);
    //}
    //static void PrintRowofStars(int starCount)
    //{
    //    for (int i = 0; i < starCount; i++)
    //    {
    //        if (i < starCount - 1)
    //        {
    //            Console.Write("* ");
    //        }
    //        else
    //        {
    //            Console.WriteLine("*");
    //        }
    //    }
    //}
    //static void PrintGameOptions()
    //{
    //    Console.Write("\n\n 1-Beginner(5*5)\n 2-Normal(8*8)\n 3-Hard(10*10)\n Select the Game level :");
    //}

    //#endregion

}

