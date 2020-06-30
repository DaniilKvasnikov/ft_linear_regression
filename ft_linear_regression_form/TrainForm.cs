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

        public double Fun(int x)
        {
            return A + B * x;
        }
    }
    
    public partial class TrainForm : Form
    {
        private Data[] array;
        private TrainResult trainResult;
        
        private Graphics graphicsObj;
        private Pen graphPen;
        private Pen linePen;
        
        private double widthDelta;
        private double heightDelta;
        private int maxKm;
        private int maxPrice;
        private readonly Point[] arrayPoints;
        
        public TrainForm(string dataPath)
        {
            InitializeComponent();
            
            graphicsObj = CreateGraphics();
            graphPen = new Pen(Color.Red, 3);
            linePen = new Pen(Color.Black, 3);
            
            array = ReadFile(dataPath);
            trainResult = Train();
            trainResultLabel.Text = trainResult.ToString();
            arrayPoints = new Point[array.Length];
            
            Init_Form();
            Paint += TrainFormPaint;
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

        private TrainResult Train()
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
            using (var reader = new StreamReader(pathCsvFile))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<Data>().OrderBy(e => e.km).ToArray();
            }
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