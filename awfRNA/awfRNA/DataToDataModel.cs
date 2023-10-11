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
    public partial class DataToDataModel : Form
    {
        public DataToDataModel()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            float[][] data;
            int count;

            Utilities.DataToDataModel.StractData(this.tbDataPath.Text, out data);
            count = data.Length;

            Utilities.DataToDataModel.InsertLine(".\\classess.txt", "c");

            List<string> targets = new List<string>();
            float[] aux;
            for (int i = 0; i < count; i++)
            {
                aux = data[i];
                if (aux[aux.Length - 1] == 1)
                    targets.Add($"{i}");
            }
            Utilities.DataToDataModel.InsertLine(".\\targets.txt",
                string.Join(',', targets));

            float[] weights = new float[data[0].Length];
            Utilities.DataToDataModel.InsertLine(".\\weights.txt",
                string.Join(',', weights.Select(n => n.ToString(CultureInfo.InvariantCulture))));

            foreach (float[] dataLine in data)
                Utilities.DataToDataModel.InsertLine(".\\train_input_set.txt",
                    string.Join(',', dataLine.Take(dataLine.Length - 1).Select(n => n.ToString())));
        }

    }
}
