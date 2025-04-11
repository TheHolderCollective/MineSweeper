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

public enum BombCount
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