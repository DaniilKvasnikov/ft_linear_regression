using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ft_linear_regression_form
{
    public partial class MainForm : Form
    {
        public string dataPath = "./InuptFiles/data.csv";

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
            var form = new TrainForm(dataPath);
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = ".";
                openFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 0;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    dataPath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }
            MessageBox.Show(fileContent, "File Content at path: " + dataPath, MessageBoxButtons.OK);
        }
    }
}