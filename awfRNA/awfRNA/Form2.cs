using awfRNA.Controls;
using awfRNA.Utilities;
using OxyPlot;
using rna.Artifacts;
using rna.TrainningTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace awfRNA
{
    public partial class Form2 : Form
    {
        private DataManager _dManager;
        private NeuralNet _nNet;

        private TrainningControl _tControl;

        int inputCount = 1;
        int limIterations = 10000;


        PlotModel _errorPlotModel;

        public Form2()
        {
            InitializeComponent();

            this._dManager = new DataManager();
            this._nNet = new NeuralNet();
            this._tControl = TrainningControl.Instance;

            this._nNet.AddNeuron(new Neuron(0, "n1", 1));

            Plot.Create2DPlotModel("Error", out this._errorPlotModel);
            Plot.AddressPlotModel(this._errorPlotModel, this.pvError);
            //pvError.Invalidated += this.UpdatePlot;

            this.SetupListView();
        }

        public void SetupListView()
        {
            this.lvClasses.Columns.Add(new ColumnHeader("Class").Name = "Classes");
            for (int i = 0; i < this.inputCount; i++)
            {
                this.lvClasses.Columns.Add(new ColumnHeader($"w[{i + 1}]").Name = $"w[{i + 1}]");
            }
            this.lvClasses.Columns.Add(new ColumnHeader("b").Name = "b");
            this.lvClasses.Columns.Add(new ColumnHeader("b").Name = "E");
            this.lvClasses.Columns.Add(new ColumnHeader("b").Name = "Acertive Rate");
            this.lvClasses.View = View.Details;
        }

        private void AdjustLargeColumnListView(ListView listView)
        {
            // Ajustar automaticamente as colunas ao maior conteúdo entre o conteúdo da coluna e o cabeçalho da coluna
            foreach (ColumnHeader column in listView.Columns)
            {
                column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                int headerWidth = TextRenderer.MeasureText(column.Text, listView.Font).Width + 26; // Adicione algum espaço para a largura do cabeçalho.
                column.Width = Math.Max(column.Width, headerWidth);
            }
        }

        public void AddItemToListView(string classe, double[] pesos, double error, double acertiveRate)
        {
            int i, count = 4 + this.inputCount;
            string[] lvItems = new string[count];

            lvItems[0] = classe;
            for (i = 1; i < this.inputCount + 1; i++)
                lvItems[i] += pesos[i - 1].ToString();

            lvItems[i++] = pesos[this.inputCount].ToString();
            lvItems[i++] = error.ToString();
            lvItems[i] = acertiveRate.ToString() + "%";

            this.lvClasses.Items.Add(new ListViewItem(lvItems));
            this.AdjustLargeColumnListView(this.lvClasses);
        }

        public void UpdateListViewStyle()
        {
            this.lvClasses.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void UpdatePlot(object sender, EventArgs e)
        {
            this.pvError.Update();
        }

        private void ExtractNewDataBase_Click(object sender, EventArgs e)
        {
            double[][] data;
            int count;

            Utilities.DataToDataModel.StractData(this.tbDataPath.Text, out data);
            count = data.Length;

            double[] xtrain, ytrain, xtest, ytest;
            xtrain = data.Select(d => d[0]).ToArray();
            ytrain = data.Select(d => d[1]).ToArray();
            xtest = data.Select(d => d[0]).ToArray();
            ytest = data.Select(d => d[1]).ToArray();

            this._dManager.SaveXTrain(xtrain);
            this._dManager.SaveYTrain(ytrain);
            this._dManager.SaveXTest(xtest);
            this._dManager.SaveYTest(ytest);

            double[][] weights;
            weights = new double[][] { new double[] { double.Parse(this.tbW1.Text), double.Parse(this.tbB.Text) } };

            this._dManager.SaveWeights(weights);
        }

        private void CheckTrain_CheckedChanged(object sender, EventArgs e)
        {
            this.tbX1.Enabled = false;
            this.tbLmr.Enabled = false;

            this.tbB.Enabled = true;
            this.tbW1.Enabled = true;

            this.tbIterationsLim.Enabled = true;
            this.tbAlpha.Enabled = true;
        }

        private void CheckTest_CheckedChanged(object sender, EventArgs e)
        {
            this.tbX1.Enabled = true;
            this.tbLmr.Enabled = true;

            this.tbB.Enabled = false;
            this.tbW1.Enabled = false;

            this.tbIterationsLim.Enabled = false;
            this.tbAlpha.Enabled = false;
        }

        private void ResetValues_Click(object sender, EventArgs e)
        {
            this.tbX1.Text = "0.0";
            this.tbLmr.Text = "1";

            this.tbB.Text = "0.5";
            this.tbW1.Text = "0.5";

            this.tbIterationsLim.Text = "1000";
            this.tbAlpha.Text = "0.01";
        }

        private double[] RandWeights(int count)
        {
            double[] weights = new double[count];
            for (int i = 0; i < count; i++)
            {
                weights[i] = Math.Round(DoubleTool.SortValue(10, -5, 0.1f),7,0);
            }
            return weights;
        }

        private void Train()
        {
            Neuron neuron = this._nNet.Neurons[0];
            double[] weights = this.RandWeights(this.inputCount + 1);
            neuron.Weights = weights;

            // !!! escopo reduzido
            int sets_count = this._dManager.XTrain.Length;
            double[][] xtrain = new double[sets_count][];
            for (int i = 0; i < sets_count; i++)
            {
                xtrain[i] = new double[] { this._dManager.XTrain[i] };
            }

            double[] analyzeData;
            analyzeData = this._tControl.TrainNeuron(neuron, xtrain, this._dManager.YTrain, this.limIterations, 
                this._errorPlotModel, double.Parse(this.tbAlpha.Text,CultureInfo.InvariantCulture));
            this._dManager.SaveWeights(new double[][] { neuron.Weights });

            this.lvClasses.Items.Clear();
            this.AddItemToListView(neuron.Label, neuron.Weights, analyzeData[1], analyzeData[2]);
        }

        private void Test()
        {

        }

        private void Computate_Click(object sender, EventArgs e)
        {
            if (this.rbTrain.Checked)
            {
                this.Train();
            }
            else
            {
                this.Test();
                this.tbB.Text = this._nNet.Neurons[0].Weights[1].ToString();
                this.tbW1.Text = this._nNet.Neurons[0].Weights[0].ToString();
            }
        }

        private void btTestSort_Click(object sender, EventArgs e)
        {
            this.lbSortedValue.Text = Math.Round(DoubleTool.SortValue(10, -5, 0.1f), 7).ToString();
        }
    }
}
