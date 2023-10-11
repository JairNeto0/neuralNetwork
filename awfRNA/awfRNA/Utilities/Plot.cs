using System;
using System.Collections.Generic;


using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace awfRNA.Utilities
{
    public static class Plot
    {
        public static void Create2DPlotModel(string title, out PlotModel plot, string xLabel = "x", string yLabel = "y")
        {
            plot = new PlotModel { 
                Title = title,
                Axes = {
                    new LinearAxis { Position = AxisPosition.Bottom, Title = xLabel },
                    new LinearAxis { Position = AxisPosition.Left, Title = yLabel }
                }
            };
        }

        public static void AddressPlotModel(PlotModel plot, PlotView control)
        {
            control.Model?.Series.Clear();
            control.Model = plot;
        }

        public static void TestPlot(PlotModel plot)
        {
            plot.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos (x)"));
        }

        public static void Get2DScatterPlot(PlotModel plot, out ScatterSeries scatter)
        {
            scatter = new ScatterSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 1,
                MarkerStroke = OxyColors.Blue,
                MarkerFill = OxyColors.Blue
            };
            plot.Series.Add(scatter);
        }

        public static void Add2DPointToScatter(ScatterSeries scatter, double x, double y)
        {
            scatter.Points.Add(new ScatterPoint(x, y));

        }

        public static void Add2DScatterToPlot(PlotModel plot, ScatterSeries scatter)
        {
            plot.Series.Add(scatter);
        }

        public static void Add2DPointToPlot(PlotModel plot, double[,] xy)
        {
            ScatterSeries scatter = new ScatterSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 5,
                MarkerStroke = OxyColors.Blue,
                MarkerFill = OxyColors.Blue
            };
            scatter.Points.Add(new ScatterPoint(xy[0,0], xy[0,1]));
            plot.Series.Add(scatter);
        }
    }
}
