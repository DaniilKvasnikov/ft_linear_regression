using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CsvHelper;

namespace ft_linear_regression_form
{
    public class TrainResult
    {
        public double A;
        public double B;
        public double r;
        public string strong;
        
        public TrainResult(double xMid, double yMid, double sxx, double sxy, double syy)
        {
            B = sxy / sxx;
            A = yMid - B * xMid;
            r = Math.Abs(sxy / (Math.Sqrt(sxx) * Math.Sqrt(syy)));
            if (0.7 <= r && r <= 1) strong = "strong correlation";
            else if (0.4 < r && r <= 0.7) strong = "moderate correlation";
            else if (0.2 < r && r <= 0.4) strong = "weak correlation";
            else strong = "no correlation";
        }

        public override string ToString()
        {
            return $"y = {A:F4} + {B:F4} * x\nr = {r:F2} {strong}";
        }

        public double Fun(double x)
        {
            return A + B * x;
        }
    }
    
    public partial class TrainForm : Form
    {
        private Data[] array;
        private Data[] arrayNormalizedData;
        private TrainResult trainResult;
        
        private Graphics graphicsObj;
        private Pen graphPen;
        private Pen linePen;
        
        private double widthDelta;
        private double heightDelta;
        private double maxKm;
        private double maxPrice;
        private readonly Point[] arrayPoints;

        private double lrate = 0.1;
        
        public TrainForm(string dataPath)
        {
            InitializeComponent();
            richTextBox1.Visible = false;

            try
            {
                graphicsObj = CreateGraphics();
                graphPen = new Pen(Color.Red, 3);
                linePen = new Pen(Color.Black, 3);
            
                array = ReadFile(dataPath);
                if (array.Length == 0)
                    throw new Exception("No elements in array");
                arrayNormalizedData = normalized_data(array);
                Train(1000);
                trainResult = TrainBonus();
                trainResultLabel.Text = trainResult.ToString();
                arrayPoints = new Point[array.Length];
            
                Init_Form();
                Paint += TrainFormPaint;
            }
            catch (Exception e)
            {
                richTextBox1.Visible = true;
                richTextBox1.Text = e.ToString();
            }
        }

        private double[] Train(int iterationsCount)
        {
            var theta0 = 0.0;
            var theta1 = 0.0;
            for (var i = 0; i < iterationsCount; i++) {
                var tmpThetas = NewThetas(theta0, theta1);
                theta0 -= tmpThetas[0];
                theta1 -= tmpThetas[1];
            }
            return new double[]{theta0, theta1};
        }

        private double[] NewThetas(double theta0, double theta1)
        {
            var c = array.Length;
            var dtheta0 = 0.0;
            var dtheta1 = 0.0;
            for(var i = 0; i < c; i++) {
                var tmp = theta0 + (theta1 * array[i].km) - array[i].price;
                dtheta0 += tmp;
                dtheta1 += tmp * array[i].km;
            }
            return new double[]{dtheta0 / c * lrate, dtheta1 / c * lrate};
        }

        private void Init_Form()
        {
            graphicsObj = CreateGraphics();
            maxKm = array.Max(elem => elem.km);
            maxPrice = array.Max(elem => elem.price);
            widthDelta = .9 * Width / maxKm;
            heightDelta = .9 * Height / maxPrice;
            for (var i = 0; i < array.Length; i++)
            {
                arrayPoints[i].X = (int) (array[i].km * widthDelta);
                arrayPoints[i].Y = Height - (int) (array[i].price * heightDelta);
            }
        }

        private TrainResult TrainBonus()
        {
            double xMid = array.Sum(e => e.km) / (double) array.Length;
            double yMid = array.Sum(e => e.price) / (double) array.Length;
            double sxx = array.Sum(e => (e.km - xMid) * (e.km - xMid));
            double syy = array.Sum(e => (e.price - yMid) * (e.price - yMid));
            double sxy = array.Sum(e => (e.km - xMid) * (e.price - yMid));
            return new TrainResult(xMid, yMid, sxx, sxy, syy);
        }

        private Data[] ReadFile(string pathCsvFile)
        {
            try
            {
                using (var reader = new StreamReader(pathCsvFile))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    return csv.GetRecords<Data>().OrderBy(e => e.km).ToArray();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        private Data[] normalized_data(Data[] array) {
            var minKm = array.Min(e => e.km);
            var maxKm = array.Max(e => e.km);
            var c = array.Length;
            var new_arr_data = new Data[array.Length];
            for (var i = 1; i < c; i++) {
                new_arr_data[i - 1] = new Data();
                new_arr_data[i - 1].km = (array[i].km - minKm) / (maxKm - minKm);
                new_arr_data[i - 1].price = array[i].price;
            }
            return new_arr_data;
        }


        private void TrainFormPaint(object sender, PaintEventArgs e)
        {
            graphicsObj.Clear(Color.White);
            graphicsObj.DrawLines(graphPen, arrayPoints);
            graphicsObj.DrawLine(linePen, 0, Height - (int)(trainResult.A * heightDelta),
                (int) (array[array.Length - 1].km * widthDelta), Height - (int)(trainResult.Fun(array[array.Length - 1].km) * heightDelta));
        }

    }
}