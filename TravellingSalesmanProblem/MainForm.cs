using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TravellingSalesmanProblem
{
    public partial class MainForm : Form
    {
        private Random random = new Random();

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            var travel = Travel.CreateRandomCities(7);
            var genetic = new GeneticBase(travel);
            genetic.CountGenerations = 50;
            genetic.CountChildrenInGeneration = 50;
            genetic.UseMutation = true;
            int[] genom = genetic.Run();
            if (genom != null)
            {
                int[] path = travel.FormPath(genom);
                int pathLen = travel.GetPathLen(path);
            }
            var ex = new ExhaustiveSearch(new MinArea.Permutations<int>(), travel);
            int[] exPath = ex.GetShortestPath();
            int exLen = travel.GetPathLen(exPath);
        }

        

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                nmrcCountCities.Update();
                var val = nmrcCountCities.Value;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int count = Convert.ToInt32(nmrcCountCities.Value);
                if (count > 1)
                {
                    dtGrdVwCitiesMatrix.RowCount = dtGrdVwCitiesMatrix.ColumnCount =
                        count;
                }
            }
            catch { }
        }

        private void btnRandMatr_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dtGrdVwCitiesMatrix.RowCount; ++i)
            {
                for (int j = 0; j < i; ++j)
                {
                    dtGrdVwCitiesMatrix[i, j].Value =
                        dtGrdVwCitiesMatrix[j, i].Value =
                            random.Next(1, byte.MaxValue);
                }
                dtGrdVwCitiesMatrix[i, i].Value = "-";
            }
        }

        private void dtGrdVwCitiesMatrix_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == e.ColumnIndex)
            {
                dtGrdVwCitiesMatrix[e.ColumnIndex, e.RowIndex].Value = "-";
            }
            else
            {
                dtGrdVwCitiesMatrix[e.RowIndex, e.ColumnIndex].Value =
                dtGrdVwCitiesMatrix[e.ColumnIndex, e.RowIndex].Value;
            }
                
        }

        private void dtGrdVwCitiesMatrix_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int val = Convert.ToInt32(dtGrdVwCitiesMatrix[e.ColumnIndex, e.RowIndex].Value);
                dtGrdVwCitiesMatrix[e.ColumnIndex, e.RowIndex].Value = Math.Abs(val);

            }
            catch
            {
                dtGrdVwCitiesMatrix[e.ColumnIndex, e.RowIndex].Value =
                    dtGrdVwCitiesMatrix[e.RowIndex, e.ColumnIndex].Value;
            }
        }

        private void btnFindPath_Click(object sender, EventArgs e)
        {
            int n = dtGrdVwCitiesMatrix.RowCount;
            var matr = new int[n, n];
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < i; ++j)
                {
                    matr[i, j] = matr[j, i] = (int)dtGrdVwCitiesMatrix[i, j].Value;
                }
                matr[i, i] = 0;
            }

            var travel = new Travel(matr, n);

            var genetic = new GeneticBase(travel);
            genetic.CountGenerations = Convert.ToInt32(nmrcCountGen.Value);
            genetic.CountChildrenInGeneration = Convert.ToInt32(nmrcCountChildren.Value);
            genetic.UseMutation = chBxUseMutations.Checked;
            int[] genom = genetic.Run();

            txtBxPathLenGen.Clear();
            txtBxPathLenExh.Clear();
            if (genom != null)
            {
                int[] path = travel.FormPath(genom);
                int pathLen = travel.GetPathLen(path);
                txtBxPathLenGen.Text = pathLen.ToString();
            }
            var ex = new ExhaustiveSearch(new MinArea.Permutations<int>(), travel);
            int[] exPath = ex.GetShortestPath();
            int exLen = travel.GetPathLen(exPath);
            txtBxPathLenExh.Text = exLen.ToString();
        }
    }
}
