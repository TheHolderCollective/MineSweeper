namespace MineSweeper.Menu_Components;


public class MenuOption
{
    public string OptionName { get; set; }
    public int OptionValue { get; set; }
    public MenuOption(string name, int value)
    {
        OptionName = name;
        OptionValue = value;
    }
}
