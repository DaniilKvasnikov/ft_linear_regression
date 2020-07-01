namespace ft_linear_regression_form
{
    public class Data
    {
        public Data(double km, double price)
        {
            this.km = km;
            this.price = price;
        }

        public Data()
        {
            km = 0;
            price = 0;
        }

        public double km { get; set; }
        public double price { get; set; }
    }
}