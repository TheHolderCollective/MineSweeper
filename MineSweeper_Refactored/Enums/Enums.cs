﻿namespace MineSweeper;

public enum GameStatus
{
    Won,
    Loss,
    InProgress,
    Restart
}

public enum GameLevel
{
    Beginner,
    Normal,
    Difficult
}

public enum MainMenuOption
{
    StartGame,
    SelectLevel,
    ExitGame
}

public enum RestartMenuOption
{
    Continue,
    GoToMainMenu,
    ExitGame
}

public enum MenuType
{
    MainMenu,
    GameLevelMenu,
    RestartMenu
}
public enum GameLevelBombs
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

public enum SquareColors 
{
    DarkRed = 1,
    DarkGreen = 2,
    DarkYellow = 3,
    DarkMagenta = 5,
    DarkCyan = 6,
    Grey = 15,
    Black = 16,
    DarkBlue = 27
}