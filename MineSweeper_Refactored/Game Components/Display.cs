using Spectre.Console;
using MineSweeper.Menu_Components;
namespace MineSweeper;

public class Display
{
    private readonly string gameTitle = "[blue]\n" +
                           "   /\\/\\ (_)_ __   ___  _____      _____  ___ _ __   ___ _ __ \n" +
                           "  /    \\| | '_ \\ / _ \\/ __\\ \\ /\\ / / _ \\/ _ \\ '_ \\ / _ \\ '__|\n" +
                           "/ /\\/\\ \\ | | | |  __/\\__ \\\\ V  V /  __/  __/ |_) |  __/ |  \n" +
                           "\\/    \\/_|_| |_|\\___||___/ \\_/\\_/ \\___|\\___| .__/ \\___|_|  \n" +
                           "                                            |_|               \n" +
                           "[/]";

    private readonly string gameOver = "[blue]" + "Thanks for playing!" + "[/]";


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

    private readonly string gameOverWin = "=================\\ !!! You Win !!! /=================";

    private readonly string gameOverLoss = "=================\\ !!! You Lose !!! /=================";
  
    private const int gamePanelWidth = 68;

   
    Menu mainMenu;
    Menu gameLevelMenu;
    Menu restartMenu;
    BoardElement boardElement;

    public Display()
    {
        boardElement = new BoardElement();
        BuildMenus();
    }

    public void ShowGameDisplay(Board gameBoard,GameLevel gameLevel, GameStatus gameStatus, bool showGameResult)
    {
        ShowTitle();
        ShowGameInformation(gameBoard.BombCount, gameLevel);
        ShowGameBoard(gameBoard, gameStatus);

        if (showGameResult)
        {
            ShowGameResult(gameStatus);
        }
    }

    public void ShowTitle()
    {
        var titleWithMarkup = new Markup(gameTitle).Centered();
        var titlePanel = new Panel(titleWithMarkup).Border(BoxBorder.Double);
        titlePanel.Width = gamePanelWidth;

        AnsiConsole.Clear();
        AnsiConsole.Write(titlePanel);

    }

    public void ShowGameOver()
    {
        var gameOverWithMarkup = new Markup(gameOver).Centered();
        var gameOverPanel = new Panel(gameOverWithMarkup).Border(BoxBorder.Double);
        gameOverPanel.Width = gamePanelWidth;

        AnsiConsole.Write(gameOverPanel);
        AnsiConsole.WriteLine();
    }

    public MainMenuOption ShowMainMenu()
    {
        return (MainMenuOption) ShowMenu(MenuType.MainMenu);
    }
    
    public MainMenuOption ShowMainMenuWithTitle()
    {
        ShowTitle();
        return ShowMainMenu();
    }
    
    public RestartMenuOption ShowRestartMenu()
    {
        return (RestartMenuOption) ShowMenu(MenuType.RestartMenu);
    }
    
    public RestartMenuOption ShowRestartMenuWithTitle()
    {
        ShowTitle();
        return ShowRestartMenu();
    }
    
    public GameLevel ShowLevelMenu()
    {
        return (GameLevel) ShowMenu(MenuType.GameLevelMenu);
    }
    
    public GameLevel ShowLevelMenuWithTitle()
    {
        ShowTitle();
        return ShowLevelMenu();
    }
    
    private int ShowMenu(MenuType menuType)
    {
        int choice = 0;

        AnsiConsole.WriteLine();

        switch (menuType)
        {
            case MenuType.MainMenu:
                choice = mainMenu.ShowMenu();
                break;
            case MenuType.GameLevelMenu:
                choice = gameLevelMenu.ShowMenu();
                break;
            case MenuType.RestartMenu:
                choice = restartMenu.ShowMenu();
                break;
            default:
                break;
        }

        return choice;
    }
    
    public void ShowGameBoard(Board board, GameStatus gameStatus)
    {
        var gameBoard = board.ExportGameBoard(gameStatus);

        // display actual board
        for (int i = 0; i <= gameBoard.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= gameBoard.GetUpperBound(1); j++)
            {
                if (i != 0 && j != 0)
                {
                    // all squares will have a default background colour of grey
                    string cellContents = ExtractCellContents(gameBoard[i, j]);

                    if ((cellContents[0] == boardElement.UnknownCell) || (cellContents[0] == boardElement.EmptyCell))
                    {
                        AnsiConsole.Background = Color.FromInt32((int)SquareColors.Grey);
                        AnsiConsole.Foreground = Color.FromInt32((int)SquareColors.Black);
                    }
                    else if (cellContents[0] == boardElement.CellBomb)
                    {
                        switch (gameStatus)
                        {
                            case GameStatus.Won:
                                AnsiConsole.Background = Color.FromInt32((int)SquareColors.DarkGreen);
                                AnsiConsole.Foreground = Color.FromInt32((int)SquareColors.Black);
                                break;
                            case GameStatus.Loss:
                                AnsiConsole.Background = Color.FromInt32((int)SquareColors.DarkRed);
                                AnsiConsole.Foreground = Color.FromInt32((int)SquareColors.Black);
                                break;
                            default:
                                break;
                        }
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
                        int padLength = (gamePanelWidth - (gameBoard.GetUpperBound(0) + 1) * 3) / 2;
                        if (padLength < 0) padLength = 0;
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

        infoPanel.Width= gamePanelWidth;
    
        AnsiConsole.Write(infoPanel);
        AnsiConsole.WriteLine();

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
            default:
                break;
        }

        var resultInfoWithMarkup = new Markup(resultInfo).Centered();
        var gameResultPanel = new Panel(resultInfoWithMarkup).Border(BoxBorder.Double);

        gameResultPanel.Width = gamePanelWidth;

        AnsiConsole.Write(gameResultPanel);
        AnsiConsole.WriteLine();
    }

    // Helper function: get value stored in cell 
    private string ExtractCellContents(string boardCell)
    {
        return boardCell.Trim().Substring(1, 1);
    }

    private void BuildMenus()
    {
        // build main menu
        MenuOption[] mainMenuItems =
        {
            new MenuOption(mainMenuOptions[0], (int) MainMenuOption.StartGame),
            new MenuOption(mainMenuOptions[1], (int) MainMenuOption.SelectLevel),
            new MenuOption(mainMenuOptions[2], (int) MainMenuOption.ExitGame),
        };

        mainMenu = new Menu(mainMenuItems);

        // build game level menu
        MenuOption[] gameLevelItems =
        {
            new MenuOption(gameLevelOptions[0], (int) GameLevel.Beginner),
            new MenuOption(gameLevelOptions[1], (int) GameLevel.Normal),
            new MenuOption(gameLevelOptions[2], (int) GameLevel.Difficult),

        };

        gameLevelMenu = new Menu(gameLevelItems);
        gameLevelMenu.MenuPrompt = gameLevelPrompt;

        // build restart menu
        MenuOption[] restartItems =
        {
            new MenuOption(restartGameOptions[0], (int) RestartMenuOption.Continue),
            new MenuOption(restartGameOptions[1], (int) RestartMenuOption.GoToMainMenu),
            new MenuOption(restartGameOptions[2], (int) RestartMenuOption.ExitGame),
        };

        restartMenu = new Menu(restartItems);
    }
}
