using System.Linq;

namespace ft_linear_regression_form
{
    public class TrainerCalculator : ITrainer
    {
        (double, double)[] thetasHistory;
        public (double, double) GetResult(Data[] dataArray)
        {
            var xMid = dataArray.Sum(e => e.km) / dataArray.Length;
            var yMid = dataArray.Sum(e => e.price) / dataArray.Length;
            var sxx = dataArray.Sum(e => (e.km - xMid) * (e.km - xMid));
            var syy = dataArray.Sum(e => (e.price - yMid) * (e.price - yMid));
            var sxy = dataArray.Sum(e => (e.km - xMid) * (e.price - yMid));
            var B = sxy / sxx;
            var A = yMid - B * xMid;
            thetasHistory = new (double, double)[] { (A, B) };
            return (A, B);
        }

        public (double, double)[] GetThetasHistory()
        {
            return thetasHistory;
        }
    }
}