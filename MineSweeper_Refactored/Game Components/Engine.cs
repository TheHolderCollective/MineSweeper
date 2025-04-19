namespace MineSweeper;

public class Engine
{
    static (int x, int y) cell;
    static Board? gameBoard;
    static GameLevel gameLevel;

    static Display? gameDisplay;
    static MainMenuOption menuOption;
    static RestartMenuOption restartOption;

    public Engine()
    {
        gameLevel = GameLevel.Normal; // set normal as default
        gameDisplay = new Display();
        Console.Clear();
    }
    public void Run()
    {
        bool continueGame = true;

        while (continueGame)
        {
            gameDisplay.ShowTitle();
            menuOption = (MainMenuOption) gameDisplay.ShowMainMenu();

            switch (menuOption)
            {
                case MainMenuOption.StartGame:
                    PlayGame();
                    break;
                case MainMenuOption.SelectLevel:
                    SetGameLevel();
                    break;
                case MainMenuOption.ExitGame:
                    continueGame = false;
                    break;
                default:
                    break;
            }
        }

        gameDisplay.ShowGameOver();
        Environment.Exit(0);
    }

    private void PlayGame()
    {
        GameStatus gameStatus = GameStatus.InProgress;
        bool validInput;
        
        gameBoard = new Board(gameLevel);
      
        while (gameStatus == GameStatus.InProgress)
        {
            gameDisplay.ShowTitle();
            gameDisplay.ShowGameInformation(gameBoard.BombCount, gameLevel);
            gameDisplay.ShowGameBoard(gameBoard, gameStatus);

            try
            {
                // for testing only
                //Console.Write($"\nLast cell: {cell.x},{cell.y}");
                //
                cell = GetCellCoordinates();
                validInput = gameBoard.IsCellOutOfBounds(cell) ? false : true;
            }
            catch
            {
                validInput = false;
            }

            if (validInput)
            {
                gameStatus = gameBoard.IsCellBomb(cell) ? GameStatus.Loss : GameStatus.InProgress;

                if (gameStatus == GameStatus.InProgress)
                {
                    gameBoard.UpdateBoard(cell);
                    gameStatus = gameBoard.IsGameWon() ? GameStatus.Won : GameStatus.InProgress;
                }


                if (gameStatus != GameStatus.InProgress)
                {
                    gameDisplay.ShowTitle();
                    gameDisplay.ShowGameInformation(gameBoard.BombCount, gameLevel);
                    gameDisplay.ShowGameBoard(gameBoard, gameStatus); 
                    gameDisplay.ShowGameResult(gameStatus);

                    Console.ReadKey();
                    gameDisplay.ShowTitle();
                    restartOption = (RestartMenuOption) gameDisplay.ShowRestartMenu();

                    switch (restartOption)
                    {
                        case RestartMenuOption.Continue:
                            gameStatus = GameStatus.InProgress;
                            gameBoard.ResetBoard(gameLevel);
                            break;

                        case RestartMenuOption.GoToMainMenu:
                            gameStatus = GameStatus.Restart;
                            break;
                        case RestartMenuOption.ExitGame:
                            gameDisplay.ShowGameOver();
                            Environment.Exit(0);
                            break;
                        default:
                            break;
                    }

                }
            }
        }
    }

    private static void SetGameLevel()
    {
        gameDisplay.ShowTitle();
        gameLevel = (GameLevel) gameDisplay.ShowLevelMenu();

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

}
