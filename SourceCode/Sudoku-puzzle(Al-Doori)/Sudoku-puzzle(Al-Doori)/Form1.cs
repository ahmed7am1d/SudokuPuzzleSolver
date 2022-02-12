using Sudoku_puzzle_Al_Doori_.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku_puzzle_Al_Doori_
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        #region Proprties and Variables


        List<List<int>> GridPuzzle = new List<List<int>>();
        OpenFileDialog LoadedFile = new OpenFileDialog();
        PuzzleSolverServices s = new PuzzleSolverServices();
        internal static Form1 form1;
        int TextAreaCounter = 0;

        #endregion

        #region Methods
        //Print Method that will be called by solver
        internal void PrintSolution(int cell)
        {
            pnlPuzzel.Controls[TextAreaCounter].Text = cell.ToString();
            TextAreaCounter--;
        }


        #endregion

        #region Button Load Puzzle (Event Click)
        private void btnLoadPuzzle_Click(object sender, EventArgs e)
        {
            //Reset values every load of new puzzle
            GridPuzzle = new List<List<int>>();
            TextAreaCounter = 80;
            //1- upload file and save it
            LoadedFile = fileDialogPuzzle;
            DialogResult d = LoadedFile.ShowDialog();

            //2- Read data of the file and insert into the pnlPuzzle controls and save it in the list gridPuzzle
            int counter = 0;
            int textAreaCounter = 80;
            if (d == DialogResult.OK)
            {
                foreach (string line in File.ReadLines(LoadedFile.FileName))
                {
                    GridPuzzle.Add(new List<int>());
                    for (int i = 0; i < line.Length; i++)
                    {
                        GridPuzzle[counter].Add(int.Parse(line[i].ToString()));
                        if (line[i].ToString() == "0")
                            pnlPuzzel.Controls[textAreaCounter].Text = "";
                        else
                            pnlPuzzel.Controls[textAreaCounter].Text = line[i].ToString();

                        textAreaCounter--;
                    }
                    counter++;
                }
            }

        }




        #endregion

        #region Button Solve (Event Click)
        private void btnSolvePuzzle_Click(object sender, EventArgs e)
        {
            if(GridPuzzle.Count== 0)
                MessageBox.Show("Please Load The Puzzle File", "Empty Data !",MessageBoxButtons.OK, MessageBoxIcon.Warning);  
            else
            s.SolvePuzzle(ref GridPuzzle);
        }
        #endregion

        #region Form Constructor
        public Form1()
        {
            InitializeComponent();
            form1 = this;
        }

        #endregion

        #region OnForm Load Event
        private void Form1_Load(object sender, EventArgs e)
        {
            lbltime.Text = DateTime.Now.ToLongTimeString();
            lblDate.Text = DateTime.Now.ToLongDateString();
        }
        #endregion

        #region Timer for  Time
        private void DateTimeTimer_tick(object sender, EventArgs e)
        {
            lbltime.Text = DateTime.Now.ToLongTimeString();
            DateTimeTimer.Start();
        }
        #endregion



    }
}
