namespace MineSweeper_Refactored;

internal class GameEngine
{
    const int GAME_END_SLEEP_TIME = 2000;
    const int MAX_STARS = 13;

    static (int x, int y) cell;
    static GameBoard gameBoard;
    static int gameLevel;

    bool lose;
    bool win;
    bool validInput;
    bool continueGame;

    public GameEngine()
    {
        lose = false;
        win = false;
        validInput = false;
        continueGame = true;

        Console.Clear();
        Console.ResetColor();
    }
    public void RunGame()
    {
        PrintGameBanner();

        gameLevel = GetGameLevel();
        gameBoard = new GameBoard(gameLevel);
        gameBoard.Initialise();
      
        Thread.Sleep(100);

        while (continueGame)
        {
            validInput = true;
            
            Console.Clear();
            gameBoard.PrintInGameBoard(lose); // change parameter to game status for readabiity
          
            try
            {
                cell = GetCellCoordinates();
                if (gameBoard.IsCellOutOfBounds(cell))
                {
                    validInput = false;
                }
            }
            catch
            {
                validInput = false;
            }

            if (validInput)
            {
                bool endGame = gameBoard.IsCellBomb(cell);
               
                if(endGame)
                { // add code to give reset option
                    ExitGameAfterPlaying(ref gameBoard,GameStatus.Loss);
                }
                else
                { // add code to give reset option
                    gameBoard.UpdateBoard(cell);
                    win = gameBoard.IsGameWon();
                    if (win)
                    {
                        ExitGameAfterPlaying(ref gameBoard, GameStatus.Won);
                    }
                }
            }
        }
        Console.ReadKey();
    }

   
    private static void ExitGameAfterPlaying(ref GameBoard gameBoard ,GameStatus gameStatus)
    {
        PrintGameResultBannerAndBoard(gameBoard,gameStatus);
        Thread.Sleep(GAME_END_SLEEP_TIME);
        PrintGameOverBanner(gameStatus);
        Environment.Exit(0);
    }
    
    private static void ExitGameBeforePlaying()
    {
        Console.WriteLine("\n Exitng game...");
        Thread.Sleep(500);
        Environment.Exit(0);
    }
   
    //private static void ResetGame(ref bool lose, ref bool win, ref char[,] char_board, ref bool[,] bool_board)
    //{
    //    lose = false;
    //    win = false;

    //    PrintGameBanner();
    //    StartGame(ref bombCount, ref boardSize);

    //    bool[,] choice_board = new bool[boardSize, boardSize];
    //    char[,] game_board = new char[boardSize, boardSize];
    //    char_board = game_board;
    //    bool_board = choice_board;
    //    SetBombs(ref char_board, bombCount, ref bool_board);
    //}

    private static int GetGameLevel()
    {
        int level = 0;
        PrintGameOptions();

        ConsoleKeyInfo keyInput = Console.ReadKey(intercept: true);

        switch (keyInput.Key)
        {
            case ConsoleKey.D1:
            case ConsoleKey.NumPad1:
                level = 1;
                break;
            case ConsoleKey.D2:
            case ConsoleKey.NumPad2:
                level = 2;
                break;
            case ConsoleKey.D3:
            case ConsoleKey.NumPad3:
                level = 3;
                break;
            case ConsoleKey.Escape:
                ExitGameBeforePlaying();
                break;
            default:
                level = 0;
                break;
        }
        return level;
    }

    private static (int, int) GetCellCoordinates()
    {
        (int x, int y) cell;

        Console.Write("\nEnter cell x,y: ");

        string? input = Console.ReadLine();
        string x = input.Split(",")[0];
        string y = input.Split(",")[1];

        try
        {
            cell.x = Int32.Parse(x);
            cell.y = Int32.Parse(y);
        }
        catch (Exception ex)
        {
            throw;
        }

        return cell;
    }
    
    #region Helper Functions 
    private static void PrintGameBanner()
    {
        Console.Clear();
        PrintRowofStars(MAX_STARS);
        Console.WriteLine("*                       *");
        Console.WriteLine("*      MINESWEEPER      *");
        Console.WriteLine("*                       *");
        PrintRowofStars(MAX_STARS);
    }
   
    private static void PrintRowofStars(int starCount)
    {
        for (int i = 0; i < starCount; i++)
        {
            if (i < starCount - 1)
            {
                Console.Write("* ");
            }
            else
            {
                Console.WriteLine("*");
            }
        }
    }
    private static void PrintGameOptions()
    {
        Console.Write("\n\n 1-Beginner(5*5)\n 2-Normal(8*8)\n 3-Hard(10*10)\n\n Select the Game level: ");
    }

    private static void PrintResetOptions(GameStatus game_status)
    {
        Console.ResetColor();
        SetForegroundColorForGameStatus(game_status);
        
        Console.WriteLine();
        Console.Write("Reset Game(Y or N): ");

        Console.ResetColor();
    }



    private static void PrintGameResultBannerAndBoard(GameBoard game_board, GameStatus game_status)
    {
        string resultBanner = GetResultBanner(game_status);

        Console.Clear();
        SetForegroundColorForGameStatus(game_status);
        Console.WriteLine(resultBanner);

        bool loss_status = (game_status == GameStatus.Loss);
        game_board.PrintFinalResultsGameBoard();

    }



    private static void PrintGameOverBanner(GameStatus game_status)
    {
        Console.Clear();
        SetForegroundColorForGameStatus(game_status);

        Console.WriteLine("\n\n\n\t\t Game Over\n");
        Thread.Sleep(GAME_END_SLEEP_TIME);
        Console.ResetColor();
    }

    private static void SetForegroundColorForGameStatus(GameStatus game_status)
    {
        Console.ResetColor();

        switch (game_status)
        {
            case GameStatus.Won:
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                break;
            case GameStatus.Loss:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            default:
                break;
        }

    }

    private static string GetResultBanner(GameStatus game_status)
    {
        string resultBanner = String.Empty;

        switch (game_status)
        {
            case GameStatus.Won:
                resultBanner = "=================\\ !!! You Win !!! /=================";
                break;
            case GameStatus.Loss:
                resultBanner = "=================\\ !!! You Lose !!! /=================";
                break;
            default:
                break;
        }

        return resultBanner;
    }

    #endregion
}
