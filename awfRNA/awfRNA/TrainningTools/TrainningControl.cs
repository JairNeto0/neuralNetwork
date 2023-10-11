using System;
using System.Text;
using OxyPlot;
using OxyPlot.WindowsForms;
using rna.Artifacts;
using rna.TrainningTools;

namespace rna.TrainningTools
{
    internal class TrainningControl
    {
        private static TrainningControl? instance;
        private int iterationsCount;

        //private TestAlgorithm _tAlgorithm;

        public static TrainningControl Instance => instance ??= new TrainningControl();

        private TrainningControl()
        {
            //_tAlgorithm = new TestAlgorithm();
        }

        /*public int TrainNeuron(Neuron neuron, bool[][] m_inputs, bool[] targets, int alph = 1)
        {
            //if (!MatchMinLengthRequisit(neuron.InputCount, inputs.Length))
            //    throw new Exception("Quantidade de entradas não é suficiente para o neurônio.");

            TrainningAlgorithm tA = new HebbTest();
            return tA.TrainNeuron(ref neuron, m_inputs, targets, 0, alph);
        }*/

        /*public int AdjustNeuron(Neuron neuron, bool[][] m_inputs, bool[] targets, int iterationsLimit, int alph = 1)
        {
            TrainningAlgorithm tA = new Perceptron();
            return tA.TrainNeuron(ref neuron, m_inputs, targets, iterationsLimit, alph);
        }

        /*
        public void TestNeuron()
        {
        }
        */

        public double[] TrainNeuron(Neuron neuron, double[][] m_inputs, double[] targets, int iterationsLimit, PlotModel plot, double alph = 1)
        {
            TrainningAlgorithm tA = new Adaline();
            return tA.TrainNeuron(ref neuron, m_inputs, targets, iterationsLimit, plot, alph);
        }
    }
}
