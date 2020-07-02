namespace ft_linear_regression_form
{
    public interface ITrainer
    {
        (double, double) GetResult(Data[] dataArray);
        (double, double)[] GetThetasHistory();
    }
}