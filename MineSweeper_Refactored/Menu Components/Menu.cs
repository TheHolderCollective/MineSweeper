using Spectre.Console;
namespace MineSweeper.Menu_Components;

public class Menu
{
    private List<MenuOption> optionsList;
    public string  MenuPrompt{ get; set; } 

    public Menu(MenuOption[] options)
    {
        optionsList = new List<MenuOption>();

        foreach (MenuOption option in options)
        {
            optionsList.Add(option);
        }
    }

    public int ShowMenu()
    {
        var menuOptions = GetNamesArray();
        var menu = AnsiConsole.Prompt(new SelectionPrompt<string>()
                           .Title(MenuPrompt)
                           .PageSize(5)
                           .AddChoices(menuOptions));

        return GetOptionValue(menu);
    }

    public int GetOptionValue(string selectedOption)
    {
        int optionValue = 0;

        foreach (MenuOption option in optionsList)
        {
            if (option.OptionName == selectedOption)
            {
               optionValue = option.OptionValue;
            }
        }

        return optionValue;
    }

    private string[] GetNamesArray()
    {
        string[] optionNames = new string[optionsList.Count];

        for (int i = 0; i < optionsList.Count; i++)
        {
            optionNames[i] = optionsList[i].OptionName;
        }

        return optionNames;
       
    }
}
