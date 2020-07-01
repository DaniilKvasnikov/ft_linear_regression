using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
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
        private TrainResult trainResultDop;
        
        private double widthDelta;
        private double heightDelta;
        private readonly Point[] arrayPoints;

        private double lrate = 0.0000001;

        private string fileName;
        private double[] dthetas;

        private double minKm;
        private double maxKm;
        private double minPrice;
        private double maxPrice;
        
        public TrainForm(string dataPath)
        {
            InitializeComponent();
            richTextBox1.Visible = false;

            try
            {
                array = ReadFile(dataPath);
                if (array.Length == 0)
                    throw new Exception("No elements in array");
                minKm = array.Min(e => e.km);
                maxKm = array.Max(e => e.km);
                minPrice = array.Min(e => e.price);
                maxPrice = array.Max(e => e.price);
                arrayNormalizedData = normalized_data(array);
                trainResult = TrainBonus();
                dthetas = GradientTrain(10000);
                trainResultLabel.Text = trainResult.ToString();
                arrayPoints = new Point[array.Length];
            
                Init_Form();
                ViewOnChart(chart1);
            }
            catch (Exception e)
            {
                richTextBox1.Visible = true;
                richTextBox1.Text = e.ToString();
                throw;
            }
        }

        private double[] GradientTrain(int iterrations)
        {
            dthetas = new double[] {0, 0};
            for (int i = 0; i < iterrations; i++)
            {
                dthetas = NewThetas(dthetas[0], dthetas[1]);
            }

            return dthetas;
        }

        double estimated_price(double mileage, double theta0, double theta1)
        {
            return theta0 + (theta1 * mileage);
        }
        
        double J_theta0(double theta0, double theta1)
        {
            return arrayNormalizedData.Sum(t => (estimated_price(t.km, theta0, theta1) - t.price));
        }

        double J_theta1(double theta0, double theta1)
        {
            return arrayNormalizedData.Sum(t => (estimated_price(t.km, theta0, theta1) - t.price) * t.km);
        }
        
        private double[] NewThetas(double theta0, double theta1)
        {
            var m = arrayNormalizedData.Length;
            var learningrate = 0.1;
            var derivee0 = J_theta0(theta0, theta1);
            var derivee1 = J_theta1(theta0, theta1);
            var tmpt0 = theta0 - (learningrate * ((1.0 / m) * derivee0));
            var tmpt1 = theta1 - (learningrate * ((1.0 / m) * derivee1));
            return new []{tmpt0, tmpt1};
        }

        private void Init_Form()
        {
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
                    fileName = Path.GetFileName(pathCsvFile);
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
            var c = array.Length;
            var new_arr_data = new Data[array.Length];
            for (var i = 0; i < c; i++) {
                new_arr_data[i] = new Data();
                new_arr_data[i].km = array[i].km / maxKm;
                new_arr_data[i].price = array[i].price / maxPrice;
            }
            return new_arr_data;
        }
        
        private void ViewOnChart(Chart chart)
        {
            chart.Series.Clear();
            
            Series seriesArray = chart.Series.Add(fileName);
            seriesArray.ChartType = SeriesChartType.Point;
            foreach (var data in array)
            {
                seriesArray.Points.AddXY(data.km, data.price);
            }
            
            Series seriesFun = chart.Series.Add("Result");
            seriesFun.ChartType = SeriesChartType.Line;
            seriesFun.Points.AddXY(0, trainResult.A);
            seriesFun.Points.AddXY(array[array.Length - 1].km, trainResult.Fun(array[array.Length - 1].km));
            
            Series seriesFunDop = chart.Series.Add("Dop");
            seriesFunDop.ChartType = SeriesChartType.Line;
            var A = dthetas[0] * maxPrice;
            var B = dthetas[1] * (maxPrice / maxKm);
            seriesFunDop.Points.AddXY(0,  A);
            var x = array[array.Length - 1].km;
            var y = (A + B * x);
            seriesFunDop.Points.AddXY(x, y);
        }
    }
}