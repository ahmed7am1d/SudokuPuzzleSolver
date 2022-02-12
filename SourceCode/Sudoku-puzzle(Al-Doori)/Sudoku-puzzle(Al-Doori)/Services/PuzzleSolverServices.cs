using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku_puzzle_Al_Doori_.Services
{
    public class PuzzleSolverServices
    {
        #region Proprties
       
        #endregion

        #region Methods
        //1- Method to check if it is possible to add number at postion (x,y)
        public bool CanbeAdded(List<List<int>> gridPuzzle,  int y,int x , int num)
        {
            
            //A- checking all elements of column (y) of the number (num)
            for (int i = 0; i< 9; i++)
            {
                if (gridPuzzle[y][i] == num)
                    return false;
            }

            //B- checking all elements of raw (x) of the number (num)
            for (int i = 0; i < 9;i++) 
            {
                if (gridPuzzle[i][x] == num)
                    return false;
            }

            //C- checking the square where the num is inside 
            int x1 = (x / 3) * 3;
            int y1 = (y / 3) * 3;
            for(int i = 0; i< 3;i++)
            {
                for(int j =0; j< 3;j++)
                {
                    if (gridPuzzle[y1 + i][x1 + j] == num)
                        return false;
                }
            }
            //if all condtions did not occures means element can be added 
            return true;
        }
       
        //2- method to solve the puzzle Using backtrack algorithm (checking previous steps) avoid trying all possible solutions
        // Recursion Base Case => grid is full and no empty squares is left (when reaching last empty cell)
        public void SolvePuzzle(ref List<List<int>> GridPuzzle)
        {  
          for(int y = 0; y <9;y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if(GridPuzzle[y][x] == 0)
                    {
                        for (int i = 1; i <= 9; i++)
                        {
                            if (CanbeAdded(GridPuzzle, y, x, i))
                            {
                                GridPuzzle[y][x] = i;
                                SolvePuzzle(ref GridPuzzle);
                                GridPuzzle[y][x] = 0;
                            }
                            
                        }
                        return;
                    }

                }
               
            }

            //print data by calling Form Method Every
            foreach (List<int> row in GridPuzzle)
            {
                foreach (int cell in row)
                {
                    Form1.form1.PrintSolution(cell);
                }
            }

        }


        #endregion

        #region Constructor

        public PuzzleSolverServices()
        {
                
        }
        #endregion
    }
}
