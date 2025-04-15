namespace MineSweeper
{
    public class GameBoardElement
    {
        private string cellBomb; 
        private string cellEmpty;
        private string cellUnknown;
        private string cellBorder;

        public char CellBomb
        {
            get
            {
                return cellBomb[0];   
            }
        }

        public string VisibleCellBomb
        {
            get
            {
                return String.Format(cellBorder + cellBomb + cellBorder);
            }
        }

        public string HiddenCell
        {
            get 
            {
                return String.Format(cellBorder + cellUnknown + cellBorder);
            }
        }

        public char EmptyCell
        {
            get 
            { 
                return cellEmpty[0]; 
            }
        }

        public char UnknownCell
        {
            get 
            {
                return cellUnknown[0];
            }
        }
        public GameBoardElement()
        {
            cellBomb = "*"; 
            cellEmpty = " ";
            cellUnknown = "?";
            cellBorder = "|";
        }

        public GameBoardElement(char bombChar, char cellBorderChar, char hiddenCellChar)
        {
            cellBomb = bombChar.ToString();
            cellBorder = cellBorderChar.ToString();
            cellUnknown = hiddenCellChar.ToString();
            cellEmpty = " ";
        }

    }
}
