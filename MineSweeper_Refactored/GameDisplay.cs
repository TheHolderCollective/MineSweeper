using Spectre.Console;
namespace MineSweeper_Refactored;


public class GameDisplay
{
   
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
                                                        "\t\t\t     Select Mode", 
                                                        "\t\t\t     Exit Game" };


    private readonly string[] gameLevelOptions = new[] { "\t\t      (5*5)   - Beginner",
                                                         "\t\t      (8*8)   - Normal",
                                                         "\t\t     (10*10)  - Hard" };


    private readonly string gameLevelPrompt = "  Please select game difficulty:";

    static int titlePanelWidth;
    static int titlePanelHeight;
    public GameDisplay()
    {
        titlePanelWidth = 72;
        titlePanelHeight = 10;
    }

    public void ShowTitle()
    {
        var titleWithMarkup = new Markup(gameTitle).Centered();
        var titlePanel = new Panel(titleWithMarkup).Border(BoxBorder.Double);

        AnsiConsole.Clear();
        AnsiConsole.Write(titlePanel);
        AnsiConsole.WriteLine();
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
           choice = MainMenuOption.EndGame;
        }

        return choice;
    }

    public GameLevel ShowLevelMenu()
    {
        GameLevel choice = GameLevel.Normal;

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
}
