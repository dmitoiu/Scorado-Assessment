namespace server.Model
{
    public class SudokuBoard
    {
        private char[,] sudokuBoard;
        public SudokuBoard(char[,] sudokuBoard)
        {
            this.sudokuBoard = sudokuBoard;
        }

        public char[,] Board { get; set; }

    }
}
