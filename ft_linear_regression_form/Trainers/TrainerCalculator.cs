using System;
using System.Linq;

namespace ft_linear_regression_form
{
    public class TrainerCalculator : ITrainer
    {
        public (double, double) GetResult(Data[] dataArray)
        {
            double xMid = dataArray.Sum(e => e.km) / dataArray.Length;
            double yMid = dataArray.Sum(e => e.price) / dataArray.Length;
            double sxx = dataArray.Sum(e => (e.km - xMid) * (e.km - xMid));
            double syy = dataArray.Sum(e => (e.price - yMid) * (e.price - yMid));
            double sxy = dataArray.Sum(e => (e.km - xMid) * (e.price - yMid));
            var B = sxy / sxx;
            var A = yMid - B * xMid;
            return (A, B);
        }
    }
}