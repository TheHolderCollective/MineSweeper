using Spectre.Console;
namespace MineSweeper_Refactored;

public class GameDisplay
{
    private readonly char cellBomb = '*';
    private readonly char cellEmpty = ' ';
    private readonly char cellUnknown = '?';


    private readonly string gameTitle = "[blue]\n" +
                           "   /\\/\\ (_)_ __   ___  _____      _____  ___ _ __   ___ _ __ \n" +
                           "  /    \\| | '_ \\ / _ \\/ __\\ \\ /\\ / / _ \\/ _ \\ '_ \\ / _ \\ '__|\n" +
                           "/ /\\/\\ \\ | | | |  __/\\__ \\\\ V  V /  __/  __/ |_) |  __/ |  \n" +
                           "\\/    \\/_|_| |_|\\___||___/ \\_/\\_/ \\___|\\___| .__/ \\___|_|  \n" +
                           "                                            |_|               \n" +
                           "[/]";

     private readonly string gameTitle2 = "[blue]" +
                                  "         _____ _                                             \n" +
                                  "        |     |_|___ ___ ___ _ _ _ ___ ___ ___ ___ ___       \n" +
                                  "        | | | | |   | -_|_ -| | | | -_| -_| . | -_|  _|      \n" +
                                  "        |_|_|_|_|_|_|___|___|_____|___|___|  _|___|_|        \n" +
                                  "                                          |_|                \n" +
                                  "[/]";

    private readonly string gameOver = "[blue]" + 
        @"   ___                         ___                
  / _ \__ _ _ __ ___   ___    /___\_   _____ _ __ 
 / /_\/ _` | '_ ` _ \ / _ \  //  /| \ / / _ \ '__|
/ /_\\ (_| | | | | | |  __/ / \_// \ V /  __/ |   
\____/\__,_|_| |_| |_|\___| \___/   \_/ \___|_|   " +
                           "\n[/]";

    private readonly string[] mainMenuOptions = new[] { "\t\t\t     Start Game", 
                                                        "\t\t\t     Select Level", 
                                                        "\t\t\t     Exit Game" };


    private readonly string[] gameLevelOptions = new[] { "\t\t      (5*5)   - Beginner",
                                                         "\t\t      (8*8)   - Normal",
                                                         "\t\t     (10*10)  - Hard" };


    private readonly string[] restartGameOptions = new[]  { "\t\t\t     Continue Playing",
                                                            "\t\t\t     Return to Main Menu",                            
                                                            "\t\t\t     Exit Game" };

    private readonly string gameLevelPrompt = "  Please select game difficulty:";

    //private readonly string restartMenuPrompt = "Do you wish to: ";

    private readonly string gameOverWin = "=================\\ !!! You Win !!! /=================";
    private readonly string gameOverLoss = "=================\\ !!! You Lose !!! /=================";

    const int titlePanelWidth = 68;
   

    public GameDisplay()
    {
      
    }

    public void ShowTitle()
    {
        var titleWithMarkup = new Markup(gameTitle).Centered();
        var titlePanel = new Panel(titleWithMarkup).Border(BoxBorder.Double);
        titlePanel.Width = titlePanelWidth;

        AnsiConsole.Clear();
        AnsiConsole.Write(titlePanel);

    }

    public void ShowGameOver()
    {
        var gameOverWithMarkup = new Markup(gameOver).Centered();
        var gameOverPanel = new Panel(gameOverWithMarkup).Border(BoxBorder.Double);

        AnsiConsole.Clear();
        AnsiConsole.Write(gameOverPanel);
        AnsiConsole.WriteLine();
    }

    public MainMenuOption ShowMainMenu()
    {
       
        MainMenuOption choice = MainMenuOption.StartGame;

        AnsiConsole.WriteLine();

        var gameMenu = AnsiConsole.Prompt(new SelectionPrompt<string>()
                            .Title("")
                            .PageSize(5)
                            .AddChoices(mainMenuOptions));

        if (gameMenu == mainMenuOptions[0])
        {
           choice = MainMenuOption.StartGame;
        }
        else if (gameMenu == mainMenuOptions[1])
        {
           choice = MainMenuOption.SelectLevel;
        }
        else if (gameMenu == mainMenuOptions[2])
        {
           choice = MainMenuOption.ExitGame;
        }

        return choice;
    }

    public GameLevel ShowLevelMenu()
    {
        GameLevel choice = GameLevel.Normal;

        AnsiConsole.WriteLine();

        var levelMenu = AnsiConsole.Prompt(new SelectionPrompt<string>()
                            .Title(gameLevelPrompt)
                            .PageSize(5)
                            .MoreChoicesText("[grey](Move up and down to select)[/]")
                            .AddChoices(gameLevelOptions));

        if (levelMenu == gameLevelOptions[0])
        {
            choice = GameLevel.Beginner;
        }
        else if (levelMenu == gameLevelOptions[1])
        {
            choice = GameLevel.Normal;
        }
        else if (levelMenu == gameLevelOptions[2])
        {
            choice = GameLevel.Difficult;
        }
        return choice;
    }

    public void ShowGameBoard(GameBoard board)
    {
        var gameBoard = board.ExportInPlayGameBoard();

        // display actual board
      
        for (int i = 0; i <= gameBoard.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= gameBoard.GetUpperBound(1); j++)
            {
                if (i != 0 && j != 0)
                {
                    // all squares will have a default background colour of grey
                    string cellContents = ExtractCellContents(gameBoard[i, j]);

                    if ((cellContents[0] == cellUnknown) || (cellContents[0] == cellEmpty))
                    {
                        AnsiConsole.Background = Color.FromInt32((int)SquareColors.Grey);
                        AnsiConsole.Foreground = Color.FromInt32((int)SquareColors.Black);
                    }
                    else
                    {
                        int bombCount = Int32.Parse(cellContents);

                        AnsiConsole.Background = Color.FromInt32((int)SquareColors.Grey);

                        switch (bombCount)
                        {
                            case 1:
                                AnsiConsole.Foreground = Color.FromInt32((int)SquareColors.DarkBlue);
                                break;
                            case 2:
                               AnsiConsole.Foreground = Color.FromInt32((int)SquareColors.DarkGreen);
                                break;
                            case 3:
                               AnsiConsole.Foreground = Color.FromInt32((int)SquareColors.DarkYellow);
                                break;
                            case 4:
                               AnsiConsole.Foreground = Color.FromInt32((int)SquareColors.DarkMagenta);
                                break;
                            case 5:
                               AnsiConsole.Foreground = Color.FromInt32((int)SquareColors.DarkRed);
                                break;
                            case 6:
                               AnsiConsole.Foreground = Color.FromInt32((int)SquareColors.DarkCyan);
                                break;
                            default:
                               AnsiConsole.Foreground = Color.FromInt32((int)SquareColors.Black);
                                break;
                           
                        }

                    }

                    AnsiConsole.Write(gameBoard[i, j]);
                    AnsiConsole.ResetColors();

                }
                else
                { // write label
                    string shift;
                    if (j == 0)
                    {
                        // calculate padding needed to center gameboard
                        int padLength = (titlePanelWidth - (gameBoard.GetUpperBound(0) + 1) * 3) / 2; 
                        shift = "".PadLeft(padLength);
                    }
                    else
                    {
                        shift = String.Empty; 
                    }    
                     
                    AnsiConsole.Write(shift + gameBoard[i, j]);
                }

            }
        }
      
    }

    public void ShowGameInformation(int bombCount, GameLevel gameLevel)
    {
        string? level = Enum.GetName(typeof(GameLevel), gameLevel);

        string info = String.Format("Game Level: {0}    Bomb Count: {1}", level, bombCount);
        var gameInfoWithMarkup = new Markup("[blue]" + info + "[/]").Centered();
        var infoPanel = new Panel(gameInfoWithMarkup).Border(BoxBorder.Double);

        infoPanel.Width= titlePanelWidth;
    
        AnsiConsole.Write(infoPanel);
        AnsiConsole.WriteLine();

    }
    public RestartMenuOption ShowRestartMenu()
    {
        RestartMenuOption choice = RestartMenuOption.Continue;

        AnsiConsole.WriteLine();

        var levelMenu = AnsiConsole.Prompt(new SelectionPrompt<string>()
                           .Title("")
                           .PageSize(5)
                           .MoreChoicesText("[grey](Move up and down to select)[/]")
                           .AddChoices(restartGameOptions));

        if (levelMenu == restartGameOptions[0])
        {
            choice = RestartMenuOption.Continue;
        }
        else if (levelMenu == restartGameOptions[1])
        {
            choice = RestartMenuOption.GoToMainMenu;
        }
        else if(levelMenu == restartGameOptions[2])
        {
            choice = RestartMenuOption.ExitGame;
        }

        return choice;

    }

    public void ShowGameResult(GameStatus gameStatus)
    {
        string resultInfo = string.Empty;

        AnsiConsole.WriteLine();

        
        switch (gameStatus)
        {
            case GameStatus.Won:
                resultInfo = "[green]" + gameOverWin + "[/]";
                break;
            case GameStatus.Loss:
                resultInfo = "[red]" + gameOverLoss + "[/]";
                break;
        }

        var resultInfoWithMarkup = new Markup(resultInfo).Centered();
        var gameResultPanel = new Panel(resultInfoWithMarkup).Border(BoxBorder.Double);

        gameResultPanel.Width = titlePanelWidth;

        AnsiConsole.Write(gameResultPanel);
        AnsiConsole.WriteLine();
    }

    // Helper function: get value stored in cell 
    private string ExtractCellContents(string boardCell)
    {
        return boardCell.Trim().Substring(1, 1);
    }

}
