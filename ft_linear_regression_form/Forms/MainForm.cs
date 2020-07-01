using System;
using System.ComponentModel;
using System.Windows.Forms;
using ft_linear_regression_form.Forms;

namespace ft_linear_regression_form
{
    public partial class MainForm : Form
    {
        private string dataPath = "./InuptFiles/data.csv";
        private bool openForm;
        private (double, double) result;

        public MainForm()
        {
            InitializeComponent();
            Paint += FormPaint;
        }

        private void FormPaint(object sender, PaintEventArgs e)
        {
            currentFile.Text = dataPath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenForm(new TrainForm(dataPath, new TrainerStandart(), out result));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenForm(new GetResultForm(result));
        }

        private void OpenForm(Form form)
        {
            if (openForm) return;
            openForm = true;
            form.Show();
            form.Closing += ClosingForm;
        }

        private void ClosingForm(object sender, CancelEventArgs e)
        {
            openForm = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openForm) return;
            openForm = true;
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = ".";
                openFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 0;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    dataPath = openFileDialog.FileName;
                }
            }
            openForm = false;
        }
    }
}