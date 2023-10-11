using rna.Artifacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rna.TrainningTools
{
    internal class Perceptron : TrainningAlgorithm
    {
        public override int TrainNeuron(ref Neuron neuron, bool[][] m_inputs, bool[] targets, int iterationsLimit, int alph = 1)
        {
            int[] weights = neuron.Weights, cachWeights;
            int iterationsCount = 0;
            bool theSame = false;
            bool target;

            int t_count = targets.Length,
                i_count = neuron.InputCount,
                w_count = weights.Length;
            int t, i, w;
            do
            {
                cachWeights = (int[])weights.Clone();
                for (t = 0; t < t_count; t++)
                {
                    target = targets[t];
                    if (!TestAlgorithm.TestNeuron(neuron, m_inputs[t], target))
                    {
                        for (i = 0; i < i_count; i++)
                            weights[i] += alph * (target ? 1 : -1) * (m_inputs[t][i] ? 1 : -1);
                        weights[i_count] += alph * (target ? 1 : -1);
                    }
                    Console.WriteLine($"|{neuron.Label} [{iterationsCount}] l:({t}) w:({this.ToStringWeights(weights)})");
                }

                neuron.Weights = weights;
                this.DebugTrainCicles(neuron, iterationsCount);

                for (w = 0; w < w_count; w++)
                    if (weights[w] != cachWeights[w])
                        break;

                if (w >= w_count)
                    if (theSame)
                        return iterationsCount;
                    else
                        theSame = true;
                else
                    theSame = false;

            } while (iterationsCount++ <= iterationsLimit);
            return iterationsCount;
        }
    }
}
