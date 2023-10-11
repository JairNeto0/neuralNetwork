using System;
using System.Collections.Generic;

namespace rna.Artifacts
{
    internal class NeuralNet
    {
        private List<Neuron> _neuronsLayer;

        public int NeuronsCount => this._neuronsLayer.Count;
        public Neuron[] Neurons => this._neuronsLayer.ToArray();

        public NeuralNet()
        {
            this._neuronsLayer = new List<Neuron>();
        }

        public void AddNeuralLayer(Neuron[] neurons)
        {
            this.ClearNet();
            this._neuronsLayer.AddRange(neurons);
        }

        public void AddNeuron(Neuron neuron)
        {
            this._neuronsLayer.Add(neuron);
        }

        public void RemoveNeuron(Neuron neuron)
        {
            this._neuronsLayer.Remove(neuron);
        }

        public int IndexOfNeuron(string label)
        {
            int count = this.NeuronsCount;
            for(int i = 0; i < count; i++)
            {
                if (this._neuronsLayer[i].Label.Equals(label))
                    return i;
            }
            return -1;
        }

        public Neuron NeuronAt(int index)
        {
            return this._neuronsLayer[index];
        }

        public void ClearNet()
        {
            this._neuronsLayer.Clear();
        }

        public bool[] Recognize(double[] inputs)
        {
            Neuron neuron;
            int count = this.NeuronsCount;
            bool[] resultActivations = new bool[count];
            for (int i = 0; i < count; i++)
            {
                neuron = this._neuronsLayer[i];
                resultActivations[i] = neuron.Calc(inputs) > 0;
            }
            return resultActivations;
        }

        public string[] Classify(bool[] resultActivations)
        {
            List<string> classes = new List<string>();
            int count = resultActivations.Length;
            for(int i = 0; i < count; i++)
                if (resultActivations[i])
                    classes.Add(this.Neurons[i].Label);
            return classes.ToArray();
        }

    }
}
