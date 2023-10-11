using System;
using System.Data;
using System.IO;
using System.Globalization;


namespace awfRNA.Controls
{
    internal class DataManager
    {
        private const string DATA_PATH = "data/";
        private const string XTRAIN = "data/xtrain.txt";
        private const string YTRAIN = "data/ytrain.txt";
        private const string XTEST = "data/xtest.txt";
        private const string YTEST = "data/ytest.txt";
        private const string WEIGHTS = "data/weights.txt";

        double[] _xtrain, _ytrain, _xtest, _ytest;
        double[][] _weights;

        public double[] XTrain => this._xtrain;
        public double[] YTrain => this._ytrain;
        public double[] XTest => this._xtest;
        public double[] YTest => this._ytest;
        public double[][] Weights => this._weights;


        public DataManager()
        {
            this.PrepareDefaultPaths();
            this.UpdateAllData();
        }

        private void CreatePathIfNotExists(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        private void CreateFileIfNotExists(string file)
        {
            if (!File.Exists(file))
                File.Create(file);
        }

        private void PrepareDefaultPaths()
        {
            try
            {
                CreatePathIfNotExists(Application.StartupPath + DATA_PATH);
                CreateFileIfNotExists(Application.StartupPath + XTRAIN);
                CreateFileIfNotExists(Application.StartupPath + YTRAIN);
                CreateFileIfNotExists(Application.StartupPath + XTEST);
                CreateFileIfNotExists(Application.StartupPath + YTEST);
                CreateFileIfNotExists(Application.StartupPath + Weights);
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        public void UpdateAllData()
        {
            this.LoadXTrain();
            this.LoadYTrain();
            this.LoadXTest();
            this.LoadYTest();
            this.LoadWeights();
        }

        private double[] LoadList(string path, ref double[] data_set)
        {
            try
            {
                return data_set = File.ReadLines(path).Select(x => double.Parse(x, CultureInfo.InvariantCulture)).ToArray();
            }catch(Exception e)
            {
                Console.WriteLine($"Erro ao manipular arquivo de dados [{path}]!");
                return new double[] { };
            }
        }

        private double[][] LoadMatrix(string path, ref double[][] data_set)
        {
            try
            {
                return data_set = File.ReadLines(path).Select(a => a.Split(',').Select(v => double.Parse(v)).ToArray()).ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao manipular arquivo de dados [{path}]!");
                return new double[][] { };
            }
        }

        public double[] LoadXTrain()
        {
            return this.LoadList(XTRAIN, ref this._xtrain);
        }

        public double[] LoadYTrain()
        {
            return this.LoadList(YTRAIN, ref this._ytrain);
        }

        public double[] LoadXTest()
        {
            return this.LoadList(XTEST, ref this._xtest);
        }

        public double[] LoadYTest()
        {
            return this.LoadList(YTEST, ref this._ytest);
        }

        public double[][] LoadWeights()
        {
            return this.LoadMatrix(WEIGHTS, ref this._weights);
        }

        private void Save(string path, double[] values, ref double[] data_set)
        {
            File.WriteAllLines(path, values.Select(x => x.ToString()));
            data_set = values;
        }

        private void Save(string path, double[][] values, ref double[][] data_set)
        {
            File.WriteAllLines(path, values.Select(a => string.Join(',', a.Select(v => v.ToString()))));
            data_set = values;
        }

        public void SaveXTrain(double[] values)
        {
            this.Save(XTRAIN, values, ref this._xtrain);
        }

        public void SaveYTrain(double[] values)
        {
            this.Save(YTRAIN, values, ref this._ytrain);
        }

        public void SaveXTest(double[] values)
        {
            this.Save(XTEST, values, ref this._xtest);
        }

        public void SaveYTest(double[] values)
        {
            this.Save(YTEST, values, ref this._ytest);
        }

        public void SaveWeights(double[][] values)
        {
            this.Save(WEIGHTS, values, ref this._weights);
        }
    }
}
