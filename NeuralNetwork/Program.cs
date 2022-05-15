using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Start reading from file");
            List<Anketa> anketas = Reader.ReadFromExcel("Ankety.csv");
            Console.WriteLine($"Read is end. Readed {anketas.Count}");
            Console.WriteLine("Start prepare data set");
            var dataset = PrepareDataSet(anketas);
            Console.WriteLine("Data set is prepared");
            Console.WriteLine("Creating Neural Network");
            NeuronTopology topology = new NeuronTopology(32, 1, 8);
            NeuralNetwork neuralNetwork = new NeuralNetwork(topology);
            Console.WriteLine("Neural Network is created");
            Console.WriteLine("Start learning");
            double error = neuralNetwork.Learn(dataset, 1000);
            Console.WriteLine("Learning is end");
            for (int i = 0; i < anketas.Count; i++)
            {
                double result = neuralNetwork.FeedForward(dataset[i].Item2).Output;
                Console.WriteLine($"{i} {anketas[i].IsSerious} - {result}");
            }

        }
        public static List<Tuple<double,double[]>> PrepareDataSet(List<Anketa> anketas)
        {
            List<Tuple<double, double[]>> dataset = new List<Tuple<double, double[]>>();

            foreach(var anket in anketas)
            {
                List<double> answers = new List<double>();
                answers.AddRange(anket.Authoritarian);
                answers.AddRange(anket.Selfish);
                dataset.Add(new Tuple<double, double[]>(anket.IsSerious, answers.ToArray()));
            }
            return dataset;
        }
    }
}
