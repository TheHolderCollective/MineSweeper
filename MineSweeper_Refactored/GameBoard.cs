namespace MineSweeper_Refactored;

internal class GameBoard
{
    int bombCount;
    int _boardSize;
    bool[,] choice_board;
    char[,] game_board;

    public int BoardDimensions
    {
        get { return _boardSize;}
    }
    
    public GameBoard(GameLevel gameLevel)
    {
        SetBoardParameters(gameLevel);
        choice_board = new bool[_boardSize, _boardSize];
        game_board = new char[_boardSize, _boardSize];
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
        for (int i = 0; i < _boardSize; i++)
        {
            for (int j = 0; j < _boardSize; j++)
            {
                if (choice_board[i, j] == true)
                {
                    cnt++;
                }
            }
        }
        if (cnt + bombCount == (_boardSize * _boardSize))
        {
           return true;
        }
        else
        {
            return false;
        }
    }
    public void PrintInGameBoard(bool loss_status)
    {
        // print numbered headers
        for (int k = 0; k < _boardSize; k++)
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

        for (int i = 0; i < _boardSize; i++)
        {
            for (int j = 0; j < _boardSize; j++)
            {
                if (j == 0 && i != 9) // this bit of code takes into account the change for the double digit
                    Console.Write($" {i + 1} ");
                else if (i == 9 && j == 0)
                    Console.Write($"{i + 1} ");

                //print bomb if lose
                if (game_board[i, j] == 'B' && loss_status)
                {
                    Console.Write($"|B|");
                }
                // print "?" 
                else if ((game_board[i, j] == 'B' && !loss_status) || (game_board[i, j] == ' ' && choice_board[i, j] == false) || (game_board[i, j] != 'B' && game_board[i, j] != ' ' && choice_board[i, j] == false))
                {
                    Console.Write($"|?|");
                }
                // print adjacent bomb count if selected
                else if (game_board[i, j] != 'B' && choice_board[i, j] == true)
                {
                    if (game_board[i, j] != ' ')
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
        for (int i = 0; i < _boardSize; i++)
        {
            for (int j = 0; j < _boardSize; j++)
            {
                Console.Write($"|{game_board[i, j]}|");
            }
            Console.WriteLine();
        }
    }
    public bool IsCellBomb((int x,int y) inputCell)
    {
        return (game_board[inputCell.x - 1, inputCell.y - 1] == 'B') ? true : false;
    }

    public bool IsCellOutOfBounds((int x, int y) inputCell)
    {
        return (inputCell.x < 1 || inputCell.x > _boardSize || inputCell.y < 1 || inputCell.y > _boardSize);
    }
    public void UpdateBoard((int x, int y) inputCell)
    {
        int x = inputCell.x - 1;
        int y = inputCell.y - 1;

        if (choice_board[x, y] == false)
        {
            if (game_board[x, y] != 'B')
            {
                choice_board[x, y] = true;
            }
            if (x > 0)
            {
                if (game_board[x - 1, y] != 'B')
                {
                    choice_board[x - 1, y] = true;

                }
            }

            if (x > 0 && y < _boardSize - 1)
            {
                if (game_board[x - 1, y + 1] != 'B')
                {
                    choice_board[x - 1, y + 1] = true;

                }
            }

            if (x > 0 && y > 0)
            {
                if (game_board[x - 1, y - 1] != 'B')
                {
                    choice_board[x - 1, y - 1] = true;

                }
            }

            if (y > 0)
            {
                if (game_board[x, y - 1] != 'B')
                {
                    choice_board[x, y - 1] = true;

                }
            }

            if (y < _boardSize - 1)
            {
                if (game_board[x, y + 1] != 'B')
                {
                    choice_board[x, y + 1] = true;

                }
            }

            if (x < _boardSize - 1 && y < _boardSize - 1)
            {
                if (game_board[x + 1, y + 1] != 'B')
                {
                    choice_board[x + 1, y + 1] = true;

                }
            }

            if (x < _boardSize - 1 && y > 0)
            {
                if (game_board[x + 1, y - 1] != 'B')
                {
                    choice_board[x + 1, y - 1] = true;

                }
            }
        }
    }

    public void ResetBoard(GameLevel gameLevel)
    {
        SetBoardParameters(gameLevel);
        choice_board = new bool[_boardSize, _boardSize];
        game_board = new char[_boardSize, _boardSize];
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
                x = rand.Next(0, _boardSize);
                y = rand.Next(0, _boardSize);
                //Console.WriteLine($"{x},{y}");
            } while (game_board[x, y] == 'B');
            game_board[x, y] = 'B';
            numBombs--;
        }
    }

    private void SetEmptyCells()
    {
        // place spaces which represent cells
        for (int i = 0; i < _boardSize; i++)
        {
            for (int j = 0; j < _boardSize; j++)
            {
                if (game_board[i, j] != 'B')
                {
                    game_board[i, j] = ' ';
                }
                choice_board[i, j] = false;
            }
        }
    }

    private void SetBombCounts()
    {
        int num_bomb = 0;
        for (int i = 0; i < _boardSize; i++)
        {
            for (int j = 0; j < _boardSize; j++)
            {
                // count the number of bombs adjacent to every square
                if (game_board[i, j] != 'B')
                {
                    if (i > 0 && game_board[i - 1, j] == 'B') // North neighbor
                    {
                        num_bomb++;
                    }
                    if (i > 0 && j < _boardSize - 1 && game_board[i - 1, j + 1] == 'B') // East North neighbor
                    {
                        num_bomb++;
                    }

                    if (i > 0 && j > 0 && game_board[i - 1, j - 1] == 'B') // West North neighbor
                    {
                        num_bomb++;
                    }

                    if (j > 0 && game_board[i, j - 1] == 'B') // West neighbor
                    {
                        num_bomb++;
                    }

                    if (j < _boardSize - 1 && game_board[i, j + 1] == 'B') // East neighbor
                    {
                        num_bomb++;
                    }

                    if (i < _boardSize - 1 && j < _boardSize - 1 && game_board[i + 1, j + 1] == 'B') // South neighbor
                    {
                        num_bomb++;
                    }

                    if (i < _boardSize - 1 && j > 0 && game_board[i + 1, j - 1] == 'B') // West South neighbor
                    {
                        num_bomb++;
                    }
                    if (i < _boardSize - 1 && game_board[i + 1, j] == 'B') // South
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
                bombCount = (int)  BombCount.Beginner;
                _boardSize = (int) BoardSize.Beginner;
                break;

            case GameLevel.Normal:
                bombCount = (int)  BombCount.Normal;
                _boardSize = (int) BoardSize.Normal;
                break;

            case GameLevel.Difficult :
                bombCount = (int)  BombCount.Difficult;
                _boardSize = (int) BoardSize.Difficult;
                break;

            default:
                bombCount = (int)  BombCount.Normal;
                _boardSize = (int) BoardSize.Normal;
                break;
        }
    }


}
