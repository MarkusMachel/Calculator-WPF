using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber, result;
        SelectedOperator selectedOperator;

        public MainWindow()
        {
            InitializeComponent();

            acButton.Click += AcButton_Click;
            negativeButton.Click += NegativeButton_Click;
            percentageButton.Click += PercentageButton_Click;
            equalButton.Click += EqualButton_Click;
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;

            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = SimpleMath.Add(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Minus:
                        result = SimpleMath.Substract(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMath.Multiply(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Division:
                        result = SimpleMath.Divide(lastNumber, newNumber);
                        break;
                }

                resultLabel.Content = result.ToString();
            }

        }

        private void PercentageButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber / 100;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
        }

        private void NegativeButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber *= -1;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber)) resultLabel.Content = "0";

            if (sender == multiplicationButton) selectedOperator = SelectedOperator.Multiplication;
            if (sender == divisionButton) selectedOperator = SelectedOperator.Division;
            if (sender == additionButton) selectedOperator = SelectedOperator.Addition;
            if (sender == minusButton) selectedOperator = SelectedOperator.Minus;

        }

        private void pointButton_Click(object sender, RoutedEventArgs e)
        {
            if (!resultLabel.Content.ToString().Contains("."))
                resultLabel.Content = $"{resultLabel.Content}.";
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = int.Parse((sender as Button).Content.ToString());

            if (resultLabel.Content.ToString() == "0")
            {
                resultLabel.Content = $"{selectedValue}";
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}{selectedValue}";
            }
        }
    }

    public enum SelectedOperator
    {
        Addition,
        Minus,
        Multiplication,
        Division
    } 

    public class SimpleMath
    {
        public static double Add(double a, double b) => a + b;
        public static double Substract(double a, double b) => a - b;
        public static double Multiply(double a, double b) => a * b;
        public static double Divide(double a, double b)
        {
            if (b == 0)
            {
                MessageBox.Show("Divsion by zero is not supported", "Wrong operation", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }

            return a / b;
        }
    }
}
