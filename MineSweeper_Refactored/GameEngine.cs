namespace MineSweeper_Refactored;

internal class GameEngine
{
    const int GAME_END_SLEEP_TIME = 2000;

    static (int x, int y) cell;
    static GameBoard? gameBoard;
    static GameLevel gameLevel;

    bool lose;
    bool win;
    bool validInput;
   
    static GameDisplay? gameDisplay;
    static MainMenuOption menuOption;

    public GameEngine()
    {
        lose = false;
        win = false;
        validInput = false;
       
        gameLevel = GameLevel.Normal; // set normal as default
        gameDisplay = new GameDisplay();
        Console.Clear();
        Console.ResetColor();
    }
    public void Run()
    {
        bool continueGame = true;

        while (continueGame)
        {
            gameDisplay.ShowTitle();
            menuOption = gameDisplay.ShowMainMenu();

            switch (menuOption)
            {
                case MainMenuOption.StartGame:
                    PlayGame();
                    break;
                case MainMenuOption.SelectLevel:
                    SetGameLevel();
                    break;
                case MainMenuOption.EndGame:
                    continueGame = false;
                    break;
            }    
        }

        gameDisplay.ShowGameOver();
        Environment.Exit(0);
    }

    private void PlayGame()
    {
        bool continueGame = true;
        gameBoard = new GameBoard(gameLevel);
        gameBoard.Initialise();

        Thread.Sleep(100);

        while (continueGame)
        {
            validInput = true;
            
            //Console.Clear();
            gameDisplay.ShowTitle();
            gameBoard.PrintInGameBoard(lose); // change parameter to game status for readabiity --> will change this altogether because using Spectre

            try
            {
                cell = GetCellCoordinates();
                bool cellOutOfBounds = gameBoard.IsCellOutOfBounds(cell);

                if (cellOutOfBounds)
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
                bool gameOver = gameBoard.IsCellBomb(cell);

                if (gameOver)
                { // add code to give reset option
                    ExitGameAfterPlaying(ref gameBoard, GameStatus.Loss);
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
   
    private static void SetGameLevel()
    {
        gameDisplay.ShowTitle();
        gameLevel = gameDisplay.ShowLevelMenu();
        
    }

    private static void ExitGameAfterPlaying(ref GameBoard gameBoard ,GameStatus gameStatus)
    {
        PrintGameResultBannerAndBoard(gameBoard,gameStatus);
        Thread.Sleep(GAME_END_SLEEP_TIME);
        PrintGameOverBanner(gameStatus);
        Environment.Exit(0);
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
