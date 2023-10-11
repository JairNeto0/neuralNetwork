using awfRNA.Utilities;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using rna.Artifacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rna.TrainningTools
{
    internal class Adaline : TrainningAlgorithm
    {
        public override double[] TrainNeuron(ref Neuron neuron, double[][] m_inputs, double[] targets, int iterationsLimit, PlotModel plot, double alph = 0.5)
        {
            double[] weights = neuron.Weights;
            int iterationsCount = 0;
            double target;
            double yin;
            double error;
            double acertiveRate;

            ScatterSeries scatter;
            Plot.Get2DScatterPlot(plot, out scatter);

            int t_count = targets.Length,
                i_count = neuron.InputCount;
            int t, i;
            do
            {
                error = 0;
                for (t = 0; t < t_count; t++)
                {
                    target = targets[t];
                    yin = neuron.Calc(m_inputs[t]);
                    //yn = (float)Math.Round((decimal)yn, 6);
                    error += this.CalcError(target, yin);

                    for (i = 0; i < i_count; i++)
                        weights[i] += Math.Round(alph * (target - yin) * m_inputs[t][i],7);
                     weights[i_count] += Math.Round(alph * (target - yin),7);
                }

                error /= t_count;
                //Plot.Add2DPointToPlot(plot, new float[,] { { iterationsCount, error} });
                Plot.Add2DPointToScatter(scatter, iterationsCount, (double)error);
                plot.InvalidatePlot(true);
                Console.WriteLine($"[{iterationsCount}] error: {error}");

                if (weights[0].Equals(float.NaN))
                    Console.Write("");

                neuron.Weights = weights;
                this.DebugTrainCicles(neuron, iterationsCount); 
            } while (++iterationsCount < iterationsLimit);

            TestAlgorithm.TestNeuron(neuron, m_inputs, targets, out acertiveRate);
            return new double[] { iterationsCount, (double)error, (double)acertiveRate };
        }

        public double CalcError(double t, double yin)
        {
            return (double) (Math.Pow((t - yin), 2) / 2);
        }
    }
}
