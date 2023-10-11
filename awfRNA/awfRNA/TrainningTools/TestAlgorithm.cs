using System;

using rna.Artifacts;

namespace rna.TrainningTools
{
    internal class TestAlgorithm
    {

        /// <param name="m_inputs">Matriz de entradas.</param>
        public static bool TestNeuron(Neuron neuron, double[][] m_inputs, double[] targets, out double acertiveRate)
        {
            bool activation = false;
            acertiveRate = 0;

            int count = m_inputs.Length;
            for (int i = 0; i < m_inputs.Length; i++)
                TestNeuron(neuron, m_inputs[i], targets[i], out activation);
                if (activation)
                    acertiveRate += 1f;

            acertiveRate *= 100 / count;
            return activation;
        }

        public static double TestNeuron(Neuron neuron, double[] inputs, double target, out bool activation)
        {
            return neuron.Calc(inputs, out activation);
        }
    }
}
