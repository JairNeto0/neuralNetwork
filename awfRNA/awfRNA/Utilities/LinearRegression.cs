using System;
using System.Linq;
using awfRNA.Enums;

namespace awfRNA.Utilities
{
    internal static class LinearRegression
    {
        /// <summary>
        /// Classifica correlação entre zero, positiva ou negativa.
        /// </summary>
        /// <param name="r">Coeficiente de Correlação.</param>
        /// <returns>type(r)</returns>
        public static CorrelationT CoefficientToType(double r)
        {
            return r == 0 ? CorrelationT.zero : (r > 0 ? CorrelationT.positive : CorrelationT.negative);
        }

        /// <summary>
        /// Calcula o coeficiente de determinação com base em r.
        /// </summary>
        /// <param name="r">Coeficiente de Correlação.</param>
        /// <returns>r^2</returns>
        public static double DeterminationCoefficient(double r)
        {
            return r * r;
        }

        /// <summary>
        /// Calcula o coeficiente de  correlação de person.
        /// </summary>
        /// <returns>r</returns>
        public static double CorrelationCoefficient(double[] x, double[] y, int n)
        {
            return (n * x.Zip(y, (xi, yi) => xi * yi).Sum() - x.Sum() * y.Sum()) /
                (Math.Sqrt(n * x.Select(xi => xi * xi).Sum() - Math.Pow(x.Sum(), 2)) * Math.Sqrt(n * y.Select(yi => yi * yi).Sum() - Math.Pow(y.Sum(), 2)));
        }

        /// <summary>
        /// Calcula o valor da variável a da regressão liniar.
        /// </summary>
        /// <returns>a</returns>
        public static double CalcA(double ymean, double b, double xmean)
        {
            return ymean - b * xmean;
        }

        /// <summary>
        /// Calcula o valor da variável b da regressão linear.
        /// </summary>
        /// <returns>b</returns>
        public static double CalcB(double[] x, double[] y, int n)
        {
            return (n * x.Zip(y, (xi, yi) => xi * yi).Sum() - x.Sum() * y.Sum()) /
                (n * x.Select(xi => xi * xi).Sum() - Math.Pow(x.Sum(), 2));
        }
    }
}
