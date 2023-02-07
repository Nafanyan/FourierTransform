using System.Collections.Generic;
using System.IO;
using System.Numerics;
using MathNet.Numerics.IntegralTransforms;

class Program
{
    static double fd;
    static void Main(string[] args)
    {
        List<double> data = Parse(@"C:\Users\01112\Documents\CShark\NeurologLab1\data\1.txt.txt");
        List<Complex> dataFT = FourierTransform(data);
        ReverseFourierTransform(dataFT);

    }

    static List<double> Parse(string pathSource)
    {
        IEnumerable<string> lines = File.ReadLines(pathSource);
        List<double> data = new List<double>();
        foreach (string line in lines)
        {

            if (line != "")
            {
                data.Add(double.Parse(line.Replace('.', ',')));
            }
        }
        fd = data[0];
        data.RemoveAt(0);
        data.RemoveAt(1);
        Console.WriteLine("Parse is good");
        return data;
    }
    static List<Complex> FourierTransform(List<double> signal)
    {
        List<Complex> result = new List<Complex>();
        Complex element = new Complex(0, 0);
        int size = signal.Count;

        for (int k = 0; k < size; k++)
        {

            for (int n = 0; n < size; n++)
            {
                element += signal[n] * new Complex(Math.Cos(-2 * Math.PI * n * k / size) ,
                    Math.Sin(-2 * Math.PI * n * k / size));
            }
            result.Add(element / fd);
            element = 0;
        }
        return result;
    }

    static List<double> ReverseFourierTransform(List<Complex> signal)
    {
        List<double> result = new List<double>();
        Complex element = new Complex(0, 0);
        int size = signal.Count;

        for (int k = 0; k < size; k++)
        {

            for (int n = 0; n < size; n++)
            {
                element += signal[n] * new Complex(Math.Cos(2 * Math.PI * n * k / size),
                    Math.Sin(2 * Math.PI * n * k / size));
            }
            result.Add(element.Real * fd / size);
            element = 0;
        }
        return result;
    }
}
