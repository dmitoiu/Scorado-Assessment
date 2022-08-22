using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.WebEncoders.Testing;
using Newtonsoft.Json;

namespace server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SudokuSolverController : ControllerBase
    {
        // N is the size of the 2D matrix   N*N
        static int N = 9;
        public SudokuSolverController()
        {
        }

        public static int[,] getBoard(IEnumerable<IEnumerable<int>> board)
        {
            int[,] postBoard = new int[9, 9];
            for (int x = 0; x < board.Count(); x++)
            {
                var c = board.ElementAt(x);
                for (int y = 0; y < c.Count(); y++)
                {
                    int element = c.ElementAt(y);
                    postBoard[x, y] = element;
                }
            }

            return postBoard;
        }

        [HttpPost]
        public ActionResult<List<int>> GetSudokuSolution([FromBody] IEnumerable<IEnumerable<int>> board)
        {
            int[,] postBoard = getBoard(board);
            solveSudoku(postBoard, 0, 0);
            Print2DArray(postBoard);
            string result = "";
            List<List<int>> list = new List<List<int>>();
            for (int i = 0; i < postBoard.GetLength(0); i++)
            {
                List<int> elementsList = new List<int>();
                for (int j = 0; j < postBoard.GetLength(1); j++)
                {
                    int cell = postBoard[i, j];
                    elementsList.Add(cell);
                }
                list.Add(elementsList);
                result += "\n";
            }
            return Ok(list);
        }

        public static void Print2DArray<T>(T[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                if (i == 2)
                {
                    Console.Write("\n");
                }
                if (i == 5)
                {
                    Console.Write("\n");
                }
                Console.WriteLine();
            }
        }

        /* Takes a partially filled-in grid and attempts
          to assign values to all unassigned locations in
          such a way to meet the requirements for
          Sudoku solution (non-duplication across rows,
          columns, and boxes) */
        static bool solveSudoku(int[,] grid, int row,
                                int col)
        {

            /*if we have reached the 8th
                   row and 9th column (0
                   indexed matrix) ,
                   we are returning true to avoid further
                   backtracking       */
            if (row == N - 1 && col == N)
                return true;

            // Check if column value  becomes 9 ,
            // we move to next row
            // and column start from 0
            if (col == N)
            {
                row++;
                col = 0;
            }

            // Check if the current position
            // of the grid already
            // contains value >0, we iterate
            // for next column
            if (grid[row, col] != 0)
                return solveSudoku(grid, row, col + 1);

            for (int num = 1; num < 10; num++)
            {

                // Check if it is safe to place
                // the num (1-9)  in the
                // given row ,col ->we move to next column
                if (isSafe(grid, row, col, num))
                {

                    /*  assigning the num in the current
                            (row,col)  position of the grid and
                            assuming our assigned num in the position
                            is correct */
                    grid[row, col] = num;

                    // Checking for next
                    // possibility with next column
                    if (solveSudoku(grid, row, col + 1))
                        return true;
                }
                /* removing the assigned num , since our
                         assumption was wrong , and we go for next
                         assumption with diff num value   */
                grid[row, col] = 0;
            }
            return false;
        }

        /* A utility function to print grid */
        static void print(int[,] grid)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                    Console.Write(grid[i, j] + " ");
                Console.WriteLine();
            }
        }

        // Check whether it will be legal
        // to assign num to the
        // given row, col
        static bool isSafe(int[,] grid, int row, int col,
                           int num)
        {

            // Check if we find the same num
            // in the similar row , we
            // return false
            for (int x = 0; x <= 8; x++)
                if (grid[row, x] == num)
                    return false;

            // Check if we find the same num
            // in the similar column ,
            // we return false
            for (int x = 0; x <= 8; x++)
                if (grid[x, col] == num)
                    return false;

            // Check if we find the same num
            // in the particular 3*3
            // matrix, we return false
            int startRow = row - row % 3, startCol
              = col - col % 3;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (grid[i + startRow, j + startCol] == num)
                        return false;

            return true;
        }
    }
}
