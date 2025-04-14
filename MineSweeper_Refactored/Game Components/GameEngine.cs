﻿namespace MineSweeper_Refactored;

public class GameEngine
{
    static (int x, int y) cell;
    static GameBoard? gameBoard;
    static GameLevel gameLevel;

    static GameDisplay? gameDisplay;
    static MainMenuOption menuOption;
    static RestartMenuOption restartOption;

    public GameEngine()
    {
        gameLevel = GameLevel.Normal; // set normal as default
        gameDisplay = new GameDisplay();
        Console.Clear();
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
                case MainMenuOption.ExitGame:
                    continueGame = false;
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
        
        gameBoard = new GameBoard(gameLevel);
        gameBoard.Initialise();


        while (gameStatus == GameStatus.InProgress)
        {
            gameDisplay.ShowTitle();
            gameDisplay.ShowGameInformation(gameBoard.BombCount, gameLevel);
            gameDisplay.ShowGameBoard(gameBoard);

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
                    gameDisplay.ShowTitle();
                    gameDisplay.ShowGameInformation(gameBoard.BombCount, gameLevel);
                    gameDisplay.ShowGameBoard(gameBoard); // replace this with show result gameboard
                    gameDisplay.ShowGameResult(gameStatus);

                    Console.ReadKey();
                    gameDisplay.ShowTitle();
                    restartOption = gameDisplay.ShowRestartMenu();

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
        gameLevel = gameDisplay.ShowLevelMenu();

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
