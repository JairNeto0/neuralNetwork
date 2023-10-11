using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rna.Artifacts
{
    internal class Neuron
    {
        private string _label;
        private double _tetha;
        private int _inputCount;
        private double[] _weights;

        public string Label => this._label;
        public double Tetha => this._tetha;
        public int InputCount => this._inputCount;

        public double[] Weights 
        {
            get => this._weights;
            set => this._weights = value;
        }

        public Neuron(double tetha, string label, int inputCount): 
            this(tetha, label, new double[inputCount + 1], inputCount)
        { }

        public Neuron(double tetha, string label, double[] weights) : 
            this(tetha, label, weights, weights.Length - 1)
        { }

        public Neuron(double tetha, string label, double[] weights, int inputCount)
        {
            this._label = label;
            this._tetha = tetha;
            this._weights = weights;
            this._inputCount = inputCount;
        }

        public double Calc(double[] inputs, double lmr = 1f)
        {
            double yin = 0;
            for (int i = 0; i < this.InputCount; i++)
                yin += inputs[i] * this.Weights[i];
            return yin += lmr * this.Weights[this.InputCount];
        }

        public double Calc(double[] inputs, out bool activation_status, double lmr = 1f)
        {
            double yin = this.Calc(inputs, lmr);
            activation_status = yin >= this.Tetha;
            return yin;
        }
    }
}
