namespace MineSweeper;

public class Board
{
    
    const int ColumnLabelRow = 0;
    const int RowLabelColumn = 0;
   
    int bombCount;
    int boardSize;

    bool[,] choice_board;
    char[,] game_board;

    BoardElement boardElement;
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

    public Board(GameLevel gameLevel)
    {
        SetBoardParameters(gameLevel);
        choice_board = new bool[boardSize, boardSize];
        game_board = new char[boardSize, boardSize];
        boardElement = new BoardElement();
        Initialise();
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

    public string[,] ExportGameBoard(GameStatus gameStatus)
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
                exportGameBoard[ColumnLabelRow, ColumnLabelRow] = String.Format($"{boardElement.EmptyCell}{boardElement.EmptyCell}");
            }
            else if (boardColumn <= 9)
            {
                exportGameBoard[ColumnLabelRow, boardColumn] = String.Format($"  {boardColumn}");

                if (boardColumn == 9) // extra space after 9 ensures correct positioning of remaining numbers
                {
                    exportGameBoard[ColumnLabelRow, boardColumn] = exportGameBoard[ColumnLabelRow, boardColumn] + " ";
                }
            }
            else if (boardColumn >= 9 && boardColumn < gameBoardSize)
            {
                exportGameBoard[ColumnLabelRow, boardColumn] = String.Format($" {boardColumn}");
            }

            if (boardColumn == gameBoardSize - 1) // new line needed on last number
            {
                exportGameBoard[ColumnLabelRow, boardColumn] = exportGameBoard[ColumnLabelRow, boardColumn] + "\n";
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
                    if (gameStatus == GameStatus.InProgress)
                    {
                        // prints actual cells from gameboard based on choiceboard
                        if (choiceBoard[boardRow - 1, boardColumn - 1] == false)
                        {
                            exportGameBoard[boardRow, boardColumn] = String.Format($"{boardElement.HiddenCell}");

                        }
                        else if ((choiceBoard[boardRow - 1, boardColumn - 1] == true) && (gameBoard[boardRow - 1, boardColumn - 1] != boardElement.CellBomb))
                        {
                            exportGameBoard[boardRow, boardColumn] = String.Format($"|{gameBoard[boardRow - 1, boardColumn - 1]}|");
                        }

                        if (boardColumn == gameBoardSize - 1)
                        {
                            exportGameBoard[boardRow, boardColumn] = exportGameBoard[boardRow, boardColumn] + "\n";
                        }
                    }
                    else if ((gameStatus == GameStatus.Won) || (gameStatus == GameStatus.Loss))
                    {
                        exportGameBoard[boardRow, boardColumn] = String.Format($"|{game_board[boardRow - 1, boardColumn - 1]}|");

                        if (boardColumn == gameBoardSize - 1)
                        {
                            exportGameBoard[boardRow, boardColumn] = exportGameBoard[boardRow, boardColumn] + "\n";
                        }

                    }
                    
                }

            }
        }

        return exportGameBoard;
    }
    
    public bool IsCellBomb((int x,int y) inputCell)
    {
        return (game_board[inputCell.x - 1, inputCell.y - 1] == boardElement.CellBomb) ? true : false;
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
            if (game_board[x, y] != boardElement.CellBomb)
            {
                choice_board[x, y] = true;
            }
            if (x > 0)
            {
                if (game_board[x - 1, y] != boardElement.CellBomb)
                {
                    choice_board[x - 1, y] = true;

                }
            }

            if (x > 0 && y < boardSize - 1)
            {
                if (game_board[x - 1, y + 1] != boardElement.CellBomb)
                {
                    choice_board[x - 1, y + 1] = true;

                }
            }

            if (x > 0 && y > 0)
            {
                if (game_board[x - 1, y - 1] != boardElement.CellBomb)
                {
                    choice_board[x - 1, y - 1] = true;

                }
            }

            if (y > 0)
            {
                if (game_board[x, y - 1] != boardElement.CellBomb)
                {
                    choice_board[x, y - 1] = true;

                }
            }

            if (y < boardSize - 1)
            {
                if (game_board[x, y + 1] != boardElement.CellBomb)
                {
                    choice_board[x, y + 1] = true;

                }
            }

            if (x < boardSize - 1 && y < boardSize - 1)
            {
                if (game_board[x + 1, y + 1] != boardElement.CellBomb)
                {
                    choice_board[x + 1, y + 1] = true;

                }
            }

            if (x < boardSize - 1 && y > 0)
            {
                if (game_board[x + 1, y - 1] != boardElement.CellBomb)
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
               
            } while (game_board[x, y] == boardElement.CellBomb);
            game_board[x, y] = boardElement.CellBomb;
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
                if (game_board[i, j] != boardElement.CellBomb)
                {
                    game_board[i, j] = boardElement.EmptyCell;
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
                if (game_board[i, j] != boardElement.CellBomb)
                {
                    if (i > 0 && game_board[i - 1, j] == boardElement.CellBomb) // North neighbor
                    {
                        num_bomb++;
                    }
                    if (i > 0 && j < boardSize - 1 && game_board[i - 1, j + 1] == boardElement.CellBomb) // East North neighbor
                    {
                        num_bomb++;
                    }

                    if (i > 0 && j > 0 && game_board[i - 1, j - 1] == boardElement.CellBomb) // West North neighbor
                    {
                        num_bomb++;
                    }

                    if (j > 0 && game_board[i, j - 1] == boardElement.CellBomb) // West neighbor
                    {
                        num_bomb++;
                    }

                    if (j < boardSize - 1 && game_board[i, j + 1] == boardElement.CellBomb) // East neighbor
                    {
                        num_bomb++;
                    }

                    if (i < boardSize - 1 && j < boardSize - 1 && game_board[i + 1, j + 1] == boardElement.CellBomb) // South neighbor
                    {
                        num_bomb++;
                    }

                    if (i < boardSize - 1 && j > 0 && game_board[i + 1, j - 1] == boardElement.CellBomb) // West South neighbor
                    {
                        num_bomb++;
                    }
                    if (i < boardSize - 1 && game_board[i + 1, j] == boardElement.CellBomb) // South
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
                bombCount = (int) GameLevelBombs.Beginner;
                boardSize = (int) BoardSize.Beginner;
                break;

            case GameLevel.Normal:
                bombCount = (int) GameLevelBombs.Normal;
                boardSize = (int) BoardSize.Normal;
                break;

            case GameLevel.Difficult :
                bombCount = (int) GameLevelBombs.Difficult;
                boardSize = (int) BoardSize.Difficult;
                break;

            default:
                bombCount = (int) GameLevelBombs.Normal;
                boardSize = (int) BoardSize.Normal;
                break;
        }
    }
}
