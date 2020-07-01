using System;
using System.Windows.Forms;

namespace ft_linear_regression_form.Forms
{
    public partial class GetResultForm : Form
    {
        private (double, double) results;
        public GetResultForm((double, double) result)
        {
            InitializeComponent();
            results = result;
            labelFunction.Text = $"y = {results.Item1:F2} + {results.Item2:F2} * x";
        }

        private void regressorInput_TextChanged(object sender, System.EventArgs e)
        {
            if (!float.TryParse(regressorInput.Text, out float regressor))
                ShowError($"Cannot convert {regressorInput.Text}");
            var result = GetResult(regressor);
            labelPrediction.Text = result.ToString();
        }

        private double GetResult(float regressor)
        {
            return results.Item1 + results.Item2 * regressor;
        }

        private void ShowError(string error)
        {

        }
    }
}