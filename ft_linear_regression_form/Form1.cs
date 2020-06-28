using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CsvHelper;

namespace ft_linear_regression_form
{
    public class ProgrammingLanguage
    {
        public int km { get; set; }
        public int price { get; set; }
    }

    public partial class Form1 : Form
    {
        private readonly ProgrammingLanguage[] array;
        private readonly Point[] arrayPoints;
        private Graphics graphicsObj;
        private readonly Pen graphPen;
        private readonly int maxKm;
        private readonly int maxPrice;
        private double widthDelta;
        private double heightDelta;

        public Form1()
        {
            InitializeComponent();
            array = ReadFile("./InuptFiles/data.csv");
            arrayPoints = new Point[array.Length];
            maxKm = array.Max(elem => elem.km);
            maxPrice = array.Max(elem => elem.price);
            Init_Form();
            graphicsObj = CreateGraphics();
            graphPen = new Pen(Color.Red, 5);
            Paint += Form1_Paint;
            Resize += ResizeHandler;
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

        private ProgrammingLanguage[] ReadFile(string pathCsvFile)
        {
            using (var reader = new StreamReader(pathCsvFile))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<ProgrammingLanguage>().OrderBy(e => e.km).ToArray();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            graphicsObj.Clear(Color.White);
            graphicsObj.DrawLines(graphPen, arrayPoints);
        }
    }
}