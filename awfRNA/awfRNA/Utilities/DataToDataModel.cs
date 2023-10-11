using System;
using System.Collections.Generic;
using System.IO;

namespace awfRNA.Utilities
{
    public static class DataToDataModel
    {
        public static string[] SplitCSV(string data)
        {
            return data.Split(',', ';');
        }

        /// <summary>
        /// extrai linhas / lista
        /// </summary>
        public static void StractData<T>(string path, out T[] data)
        {
            data = File.ReadAllLines(path).Cast<T>().ToArray();
        }

        /// <summary>
        /// Extrai listas de linhas / lista de listas.
        /// </summary>
        public static void StractData<T>(string path, out T[][] data)
        {
            string[] lines = File.ReadAllLines(path);
            data = lines.Select(n => DataToDataModel.StractData<T>(n)).ToArray();
        }

        /// <summary>
        /// Separa elementos do texto.
        /// </summary>
        public static T[] StractData<T>(string text)
        {
            return SplitCSV(text).Select(n => (T)Convert.ChangeType(n, typeof(T))).ToArray();
        }

        /// <summary>
        /// Extrai e converte texto da linha.
        /// </summary>
        /// <param name="line">[0..n]</param>
        public static void StractData<T>(string path, int line, out T data)
        {
            data = (T)Convert.ChangeType(File.ReadAllLines(path)[line], typeof(T));
        }

        /// <summary>
        /// Extrai lista de elementos da linha.
        /// </summary>
        /// <param name="line">[0..n]</param>
        public static void StractData<T>(string path, int line, out T[] data)
        {
            data = SplitCSV(File.ReadAllLines(path)[line]).Cast<T>().ToArray();
        }

        public static void InsertLine(string path, string data)
        {
            File.AppendAllText(path, data + "\n");
        }

        public static void UpdateLine(string path, int line, string data)
        {
            try
            {
                string[] lines = File.ReadAllLines(path);
                lines[line] += data;
                File.WriteAllLines(path, lines);
            }
            catch (IndexOutOfRangeException)
            {
                InsertLine(path, data);
            }
        }

        public static void ReplaceLine(string path, int line, string data)
        {
            try
            {
                string[] lines = File.ReadAllLines(path);
                lines[line] = data;
                File.WriteAllLines(path, lines);
            }
            catch (IndexOutOfRangeException)
            {
                    InsertLine(path, data);
            }
        }


        
    }
}
