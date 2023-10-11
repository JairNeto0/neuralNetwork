using System;
using System.Text;
using OxyPlot;
using OxyPlot.WindowsForms;
using rna.Artifacts;

namespace rna.TrainningTools
{
    internal abstract class TrainningAlgorithm
    {
        public abstract double[] TrainNeuron(ref Neuron neuron, double[][] m_inputs, double[] targets, int iterationsLimit, PlotModel plot, double alph = 1);

        private bool MatchMinLengthRequisit(int minLength, int argLength)
        {
            return argLength >= minLength;
        }

        protected string ToStringWeights(int[] weights)
        {
            StringBuilder sb = new StringBuilder();
            foreach (int weight in weights)
            {
                sb.Append(weight + ",");
            }
            return sb.ToString();
        }

        protected void DebugTrainCicles(Neuron neuron, int iterationsCount)
        {
            //Console.Write($"\n=>{neuron.Label} [{iterationsCount}] w:({ToStringWeights(neuron.Weights)})");
        }
    }
}
