using System.Linq;

namespace ft_linear_regression_form
{
    public class TrainerStandart : ITrainer
    {
        private double learningrate = 0.1;
        
        private Data[] arrayNormalizedData;
        private double maxKm;
        private double maxPrice;

        public (double, double) GetResult(Data[] dataArray)
        {
            maxKm = dataArray.Max(e => e.km);
            maxPrice = dataArray.Max(e => e.price);
            arrayNormalizedData = normalized_data(dataArray);
            var normResult = GradientTrain(1000);
            var A = normResult.Item1 * maxPrice;
            var B = normResult.Item2 * (maxPrice / maxKm);
            return (A, B);
        }

        private (double, double) GradientTrain(int iterrations)
        {
            var dthetas = (0.0, 0.0);
            for (var i = 0; i < iterrations; i++) dthetas = NewThetas(dthetas);

            return dthetas;
        }

        private (double, double) NewThetas((double, double) theta)
        {
            var m = arrayNormalizedData.Length;
            var derivee0 = J_theta0(theta.Item1, theta.Item2);
            var derivee1 = J_theta1(theta.Item1, theta.Item2);
            return (theta.Item1 - learningrate * (1.0 / m * derivee0),
                theta.Item2 - learningrate * (1.0 / m * derivee1));
        }

        private double estimated_price(double mileage, double theta0, double theta1)
        {
            return theta0 + theta1 * mileage;
        }

        private double J_theta0(double theta0, double theta1)
        {
            return arrayNormalizedData.Sum(t => estimated_price(t.km, theta0, theta1) - t.price);
        }

        private double J_theta1(double theta0, double theta1)
        {
            return arrayNormalizedData.Sum(t => (estimated_price(t.km, theta0, theta1) - t.price) * t.km);
        }

        private Data[] normalized_data(Data[] array)
        {
            var new_arr_data = new Data[array.Length];
            for (var i = 0; i < array.Length; i++)
                new_arr_data[i] = new Data(array[i].km / maxKm, array[i].price / maxPrice);
            return new_arr_data;
        }
    }
}