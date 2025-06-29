namespace MineSweeper;

public class Engine
{
    private (int x, int y) cell;
    private Board? gameBoard;
    private GameLevel gameLevel;

    private Display? gameDisplay;
    private MainMenuOption menuOption;
    private RestartMenuOption restartOption;

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
            menuOption = gameDisplay.ShowMainMenuWithTitle();

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
            gameDisplay.ShowGameDisplay(gameBoard, gameLevel, gameStatus, false);

            try
            {
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
                    gameDisplay.ShowGameDisplay(gameBoard, gameLevel, gameStatus, true);

                    Console.ReadKey();
                    restartOption = gameDisplay.ShowRestartMenuWithTitle();

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
    private void SetGameLevel()
    {
        gameLevel = gameDisplay.ShowLevelMenuWithTitle();
    }
    private (int, int) GetCellCoordinates()
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
