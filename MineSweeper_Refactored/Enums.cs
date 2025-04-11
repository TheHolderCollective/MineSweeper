namespace MineSweeper_Refactored;

public enum GameStatus
{
    Won,
    Loss,
    InProgress
}
public enum GameLevel
{
    Beginner,
    Normal,
    Difficult
}

public enum GameLevelBombCount
{
    Beginner = 5,
    Normal = 15,
    Difficult = 25
}

public enum BoardSize
{
    Beginner = 5,
    Normal = 8,
    Difficult = 10
}

public enum MainMenuOption
{
    StartGame,
    SelectLevel,
    EndGame
}

public enum SquareColors // tweak these for optimal results
{
    Black = 16,
    Grey = 15,
    DarkRed = 1,
    DarkBlue = 27,
    DarkGreen = 2,
    DarkYellow = 3,
    DarkMagenta = 5,
    DarkCyan = 6,
}