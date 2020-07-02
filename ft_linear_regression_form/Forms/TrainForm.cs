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
        (double, double)[] thetasHistory;

        public TrainForm(string dataPath, ITrainer trainer, out (double theta0, double theta1) result)
        {
            InitializeComponent();
            richTextBox1.Visible = false;
            result = (0, 0);

            try
            {
                var array = ReadFile(dataPath);
                if (array.Length == 0) throw new Exception("No elements in array");
                result = trainer.GetResult(array);
                thetasHistory = trainer.GetThetasHistory();
                ViewOnChart(array, result, dataPath, chart1);
            }
            catch (Exception e)
            {
                richTextBox1.Visible = true;
                richTextBox1.Text = e.Message;
            }
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

        private void ViewOnChart(Data[] array, (double theta0, double theta1) results, string dataPath, Chart chart)
        {
            chart.Series.Clear();

            chart.ChartAreas["ChartArea1"].AxisX.Title = "Km";
            chart.ChartAreas["ChartArea1"].AxisY.Title = "Price";
            var seriesArray = chart.Series.Add(dataPath);
            seriesArray.ChartType = SeriesChartType.Point;
            foreach (var data in array) seriesArray.Points.AddXY(data.km, data.price);

            var seriesFunDop = chart.Series.Add("Result");
            seriesFunDop.ChartType = SeriesChartType.Line;
            seriesFunDop.Points.AddXY(0, results.theta0);
            seriesFunDop.Points.AddXY(array[array.Length - 1].km,
                results.theta0 + results.theta1 * array[array.Length - 1].km);
            var seriesA = chart.Series.Add($"theta0 = {results.theta0:F2}");
            var seriesB = chart.Series.Add($"theta1 = {results.theta1:F2}");

            chart2.Series.Clear();
            chart2.ChartAreas["ChartArea1"].AxisX.Title = "Theta1";
            chart2.ChartAreas["ChartArea1"].AxisY.Title = "Theta0";
            var seriesArrayTheta0 = chart2.Series.Add("Theta");
            seriesArrayTheta0.ChartType = SeriesChartType.Point;
            foreach (var data in thetasHistory)
                seriesArrayTheta0.Points.AddXY(data.Item2, data.Item1);
        }
    }
}