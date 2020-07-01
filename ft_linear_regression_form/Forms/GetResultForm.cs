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
        }
    }
}