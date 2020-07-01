using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using CsvHelper;

namespace ft_linear_regression_form
{
    public partial class TrainForm : Form
    {
        private readonly Data[] array;
        private string fileName;
        private readonly (double theta0, double theta1) results;

        public TrainForm(string dataPath, ITrainer trainer)
        {
            InitializeComponent();
            richTextBox1.Visible = false;

            try
            {
                array = ReadFile(dataPath);
                if (array.Length == 0)
                    throw new Exception("No elements in array");
                results = trainer.GetResult(array);
                ViewOnChart(chart1);
            }
            catch (Exception e)
            {
                richTextBox1.Visible = true;
                richTextBox1.Text = e.ToString();
                throw;
            }
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

        private void ViewOnChart(Chart chart)
        {
            chart.Series.Clear();

            var seriesArray = chart.Series.Add(fileName);
            seriesArray.ChartType = SeriesChartType.Point;
            foreach (var data in array) seriesArray.Points.AddXY(data.km, data.price);

            var seriesFunDop = chart.Series.Add("Result");
            seriesFunDop.ChartType = SeriesChartType.Line;
            seriesFunDop.Points.AddXY(0, results.theta0);
            var x = array[array.Length - 1].km;
            var y = results.theta0 + results.theta1 * x;
            seriesFunDop.Points.AddXY(x, y);
        }
    }
}