using rna.Artifacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rna.TrainningTools
{
    internal class HebbTest : TrainningAlgorithm
    {
        public override int TrainNeuron(ref Neuron neuron, bool[][] m_inputs, bool[] targets, int iterationsLimit, int alph = 1)
        {
            int[] weights = neuron.Weights, cachWeights;
            int iterationsCount = 0;
            bool target;

            int t_count = targets.Length,
                i_count = neuron.InputCount,
                w_count = weights.Length;
            int t, i, w;

            cachWeights = weights;
            for (t = 0; t < t_count; t++)
            {
                target = targets[t];
                for (i = 0; i < i_count; i++)
                    weights[i] += alph * (target ? 1 : -1) * (m_inputs[t][i] ? 1 : -1);
                weights[i_count] += alph * (target ? 1 : -1);
                    
                Console.WriteLine($"|{neuron.Label} [{iterationsCount}] l:({t}) w:({this.ToStringWeights(weights)})");
            }

            neuron.Weights = weights;
            this.DebugTrainCicles(neuron, iterationsCount);
            Console.WriteLine(TestAlgorithm.TestNeuron(neuron, m_inputs, targets));
            return 0;
        }
    }
}
