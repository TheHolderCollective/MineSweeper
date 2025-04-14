namespace MineSweeper_Refactored;

public class GameBoard
{
    private readonly char cellBomb = '*';
    private readonly char cellEmpty = ' ';
    private readonly char cellUnknown = '?';

    const int ColumnLabelRow = 0;
    const int RowLabelColumn = 0;
    
    string visibleCellBomb;
    string hiddenCell;

    int bombCount;
    int boardSize;

    bool[,] choice_board;
    char[,] game_board;

    public int BombCount
    {
        get { return bombCount; }
    }

    public int BoardDimensions
    {
        get { return boardSize; }
    }
    
    public bool[,] ChoiceBoard
    {
        get { return choice_board; }
    }

    public char[,] PlayerBoard
    {
        get { return game_board; }
    }
    public GameBoard(GameLevel gameLevel)
    {
        SetBoardParameters(gameLevel);
        choice_board = new bool[boardSize, boardSize];
        game_board = new char[boardSize, boardSize];

        visibleCellBomb = "|" + cellBomb + "|";
        hiddenCell = "|" + cellUnknown + "|";

    }

    public void Initialise()
    {
        SetBombs();
        SetEmptyCells();
        SetBombCounts();
    }

    public bool IsGameWon() 
    {
        int cnt = 0;
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                if (choice_board[i, j] == true)
                {
                    cnt++;
                }
            }
        }
        if (cnt + bombCount == (boardSize * boardSize))
        {
           return true;
        }
        else
        {
            return false;
        }
    }

    public string[,] ExportInPlayGameBoard()
    {
        var gameBoard = game_board;
        var choiceBoard = choice_board;

        int gameBoardSize = boardSize + 1;

        string[,] exportGameBoard = new string[gameBoardSize, gameBoardSize];


        // write headers
        for (int boardColumn = 0; boardColumn < gameBoardSize; boardColumn++)
        {
            if (boardColumn == 0)
            {
                exportGameBoard[ColumnLabelRow, ColumnLabelRow] = String.Format($"{cellEmpty}{cellEmpty}");
            }
            else if (boardColumn == gameBoardSize - 1)
            {
                exportGameBoard[ColumnLabelRow, boardColumn] = String.Format($"  {boardColumn}\n");
            }
            else
            {
                exportGameBoard[ColumnLabelRow, boardColumn] = String.Format($"  {boardColumn}");
            }

        }

        // write rows
        for (int boardRow = 1; boardRow < gameBoardSize; boardRow++)
        {
            for (int boardColumn = 0; boardColumn < gameBoardSize; boardColumn++)
            {
                // prints row labels
                if (boardColumn == RowLabelColumn)
                {
                    if (boardRow <= 9)
                    {
                        exportGameBoard[boardRow, RowLabelColumn] = String.Format($" {boardRow} ");
                    }
                    else
                    {
                        exportGameBoard[boardRow, RowLabelColumn] = String.Format($"{boardRow} ");
                    }

                }
                else
                {
                    // prints actual cells from gameboard based on choiceboard
                    if (choiceBoard[boardRow - 1, boardColumn - 1] == false)
                    {
                        exportGameBoard[boardRow, boardColumn] = String.Format($"{hiddenCell}");

                    }
                    else if ((choiceBoard[boardRow - 1, boardColumn - 1] == true) && (gameBoard[boardRow - 1, boardColumn - 1] != cellBomb))
                    {
                        exportGameBoard[boardRow, boardColumn] = String.Format($"|{gameBoard[boardRow - 1, boardColumn - 1]}|");
                    }

                    if (boardColumn == gameBoardSize - 1)
                    {
                        exportGameBoard[boardRow, boardColumn] = exportGameBoard[boardRow, boardColumn] + "\n";
                    }
                }

            }
        }

        return exportGameBoard;
    }
    public void PrintInGameBoard(bool loss_status)
    {
        // print numbered headers
        for (int k = 0; k < boardSize; k++)
        {
            if (k == 0)
                Console.Write($"    {k + 1}");
            else if (k != 9)
                Console.Write($"  {k + 1}");
            else if (k == 8)
                Console.Write($" {k + 1} ");
            else
                Console.Write($"  {k + 1}");
        }
        Console.WriteLine();

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                if (j == 0 && i != 9) // this bit of code takes into account the change for the double digit
                    Console.Write($" {i + 1} ");
                else if (i == 9 && j == 0)
                    Console.Write($"{i + 1} ");

                //print bomb if lose
                if (game_board[i, j] == cellBomb && loss_status)
                {
                    Console.Write($"{visibleCellBomb}");
                }
                // print "?" 
                else if ((game_board[i, j] == cellBomb && !loss_status) || (game_board[i, j] == cellEmpty && choice_board[i, j] == false) || (game_board[i, j] != cellBomb && game_board[i, j] != cellEmpty && choice_board[i, j] == false))
                {
                    Console.Write($"{hiddenCell}");
                }
                // print adjacent bomb count if selected
                else if (game_board[i, j] != cellBomb && choice_board[i, j] == true)
                {
                    if (game_board[i, j] != cellEmpty)
                    {
                        int bombCount = Int32.Parse(game_board[i, j].ToString());

                        switch (bombCount)
                        {
                            case 1:
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                break;
                            case 2:
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                break;
                            case 3:
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                break;
                            case 4:
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                break;
                            case 5:
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                break;
                            case 6:
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                break;
                            default:
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                break;
                        }
                    }
                    Console.Write($"|{game_board[i, j]}|");
                    Console.ResetColor();
                }
            }
            Console.WriteLine();
        }
    }

    public void PrintFinalResultsGameBoard()
    {
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                Console.Write($"|{game_board[i, j]}|");
            }
            Console.WriteLine();
        }
    }

    public bool IsCellBomb((int x,int y) inputCell)
    {
        return (game_board[inputCell.x - 1, inputCell.y - 1] == cellBomb) ? true : false;
    }

    public bool IsCellOutOfBounds((int x, int y) inputCell)
    {
        return (inputCell.x < 1 || inputCell.x > boardSize || inputCell.y < 1 || inputCell.y > boardSize);
    }
    public void UpdateBoard((int x, int y) inputCell)
    {
        int x = inputCell.x - 1;
        int y = inputCell.y - 1;

        if (choice_board[x, y] == false)
        {
            if (game_board[x, y] != cellBomb)
            {
                choice_board[x, y] = true;
            }
            if (x > 0)
            {
                if (game_board[x - 1, y] != cellBomb)
                {
                    choice_board[x - 1, y] = true;

                }
            }

            if (x > 0 && y < boardSize - 1)
            {
                if (game_board[x - 1, y + 1] != cellBomb)
                {
                    choice_board[x - 1, y + 1] = true;

                }
            }

            if (x > 0 && y > 0)
            {
                if (game_board[x - 1, y - 1] != cellBomb)
                {
                    choice_board[x - 1, y - 1] = true;

                }
            }

            if (y > 0)
            {
                if (game_board[x, y - 1] != cellBomb)
                {
                    choice_board[x, y - 1] = true;

                }
            }

            if (y < boardSize - 1)
            {
                if (game_board[x, y + 1] != cellBomb)
                {
                    choice_board[x, y + 1] = true;

                }
            }

            if (x < boardSize - 1 && y < boardSize - 1)
            {
                if (game_board[x + 1, y + 1] != cellBomb)
                {
                    choice_board[x + 1, y + 1] = true;

                }
            }

            if (x < boardSize - 1 && y > 0)
            {
                if (game_board[x + 1, y - 1] != cellBomb)
                {
                    choice_board[x + 1, y - 1] = true;

                }
            }
        }
    }

    public void ResetBoard(GameLevel gameLevel)
    {
        SetBoardParameters(gameLevel);
        choice_board = new bool[boardSize, boardSize];
        game_board = new char[boardSize, boardSize];
        Initialise();
    }

    private void SetBombs()
    {
        Random rand = new Random();
        int numBombs = bombCount;
        int x, y;

        // place bombs on boards
        while (numBombs > 0)
        {
            do
            {
                x = rand.Next(0, boardSize);
                y = rand.Next(0, boardSize);
                //Console.WriteLine($"{x},{y}");
            } while (game_board[x, y] == cellBomb);
            game_board[x, y] = cellBomb;
            numBombs--;
        }
    }

    private void SetEmptyCells()
    {
        // place spaces which represent cells
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                if (game_board[i, j] != cellBomb)
                {
                    game_board[i, j] = cellEmpty;
                }
                choice_board[i, j] = false;
            }
        }
    }

    private void SetBombCounts()
    {
        int num_bomb = 0;
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                // count the number of bombs adjacent to every square
                if (game_board[i, j] != cellBomb)
                {
                    if (i > 0 && game_board[i - 1, j] == cellBomb) // North neighbor
                    {
                        num_bomb++;
                    }
                    if (i > 0 && j < boardSize - 1 && game_board[i - 1, j + 1] == cellBomb) // East North neighbor
                    {
                        num_bomb++;
                    }

                    if (i > 0 && j > 0 && game_board[i - 1, j - 1] == cellBomb) // West North neighbor
                    {
                        num_bomb++;
                    }

                    if (j > 0 && game_board[i, j - 1] == cellBomb) // West neighbor
                    {
                        num_bomb++;
                    }

                    if (j < boardSize - 1 && game_board[i, j + 1] == cellBomb) // East neighbor
                    {
                        num_bomb++;
                    }

                    if (i < boardSize - 1 && j < boardSize - 1 && game_board[i + 1, j + 1] == cellBomb) // South neighbor
                    {
                        num_bomb++;
                    }

                    if (i < boardSize - 1 && j > 0 && game_board[i + 1, j - 1] == cellBomb) // West South neighbor
                    {
                        num_bomb++;
                    }
                    if (i < boardSize - 1 && game_board[i + 1, j] == cellBomb) // South
                    {
                        num_bomb++;
                    }

                }
                // write number of adjacent bombs to cell
                if (num_bomb > 0)
                {
                    string str = num_bomb.ToString();
                    char[] temp = str.ToCharArray();
                    game_board[i, j] = temp[0];
                }
                num_bomb = 0;
            }
        }
    }
    private void SetBoardParameters(GameLevel gameLevel)
    {

        switch (gameLevel)
        {
            case GameLevel.Beginner:
                bombCount = (int) GameLevelBombCount.Beginner;
                boardSize = (int) BoardSize.Beginner;
                break;

            case GameLevel.Normal:
                bombCount = (int) GameLevelBombCount.Normal;
                boardSize = (int) BoardSize.Normal;
                break;

            case GameLevel.Difficult :
                bombCount = (int) GameLevelBombCount.Difficult;
                boardSize = (int) BoardSize.Difficult;
                break;

            default:
                bombCount = (int) GameLevelBombCount.Normal;
                boardSize = (int) BoardSize.Normal;
                break;
        }
    }


}
