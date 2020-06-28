using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CsvHelper;

namespace ft_linear_regression_form
{
    public class Data
    {
        public int km { get; set; }
        public int price { get; set; }
    }

    public partial class Form1 : Form
    {
        private readonly Data[] array;
        private readonly Point[] arrayPoints;
        private double xMid;
        private double yMid;
        private double sxx;
        private double sxy;
        private double syy;
        private double A;
        private double B;
        private double r;
        private Graphics graphicsObj;
        private readonly Pen graphPen;
        private readonly Pen linePen;
        private Font myFont;
        private Brush myBrush;
        private readonly int maxKm;
        private readonly int maxPrice;
        private double widthDelta;
        private double heightDelta;

        public Form1()
        {
            InitializeComponent();
            array = ReadFile("./InuptFiles/data.csv");
            arrayPoints = new Point[array.Length];
            CalcAB();
            maxKm = array.Max(elem => elem.km);
            maxPrice = array.Max(elem => elem.price);
            Init_Form();
            graphicsObj = CreateGraphics();
            
            graphPen = new Pen(Color.Red, 3);
            linePen = new Pen(Color.Black, 3);
            myBrush = new SolidBrush(System.Drawing.Color.Red);
            
            Paint += Form1_Paint;
            Resize += ResizeHandler;
        }

        private void CalcAB()
        {
            foreach (var elem in array)
            {
                xMid += elem.km;
                yMid += elem.price;
            }
            xMid /= array.Length;
            yMid /= array.Length;
            foreach (var elem in array)
            {
                sxx += (elem.km - xMid) * (elem.km - xMid);
                syy += (elem.price - yMid) * (elem.price - yMid);
                sxy += (elem.km -xMid) * (elem.price - yMid);
            }
            B = sxy / sxx;
            A = yMid - B * xMid;
            r = sxy / Math.Sqrt(sxx * syy);
        }

        private void Init_Form()
        {
            graphicsObj = CreateGraphics();
            widthDelta = .9 * Width / maxKm;
            heightDelta = .9 * Height / maxPrice;
            for (var i = 0; i < array.Length; i++)
            {
                arrayPoints[i].X = (int) (array[i].km * widthDelta);
                arrayPoints[i].Y = (int) (array[i].price * heightDelta);
            }
        }

        private void ResizeHandler(object sender, EventArgs e)
        {
            Init_Form();
            Form1_Paint(sender, null);
        }

        private Data[] ReadFile(string pathCsvFile)
        {
            using (var reader = new StreamReader(pathCsvFile))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<Data>().OrderBy(e => e.km).ToArray();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            myFont = new Font("Helvetica", (int)(Width / 40.0), FontStyle.Italic);
            graphicsObj.Clear(Color.White);
            graphicsObj.DrawLines(graphPen, arrayPoints);
            graphicsObj.DrawLine(linePen, 0, (int)(A * heightDelta), (int) (array[array.Length - 1].km * widthDelta), (int)
                (Fun(array[array.Length - 1].km) * heightDelta));
            graphicsObj.DrawString($"y = {A} + {B} * x", myFont, myBrush, 30, 30);
            string strong;
            if (0.7 <= r && r <= 1) strong = "strong correlation";
            else if (0.4 < r && r <= 0.7) strong = "moderate correlation";
            else if (0.2 < r && r <= 0.4) strong = "weak correlation";
            else strong = "no correlation";
            graphicsObj.DrawString($"r = {r} {strong}", myFont, myBrush, 30, 30 + (int)(Width / 40.0));
        }

        double Fun(int x)
        {
            return A + B * x;
        }
    }
}