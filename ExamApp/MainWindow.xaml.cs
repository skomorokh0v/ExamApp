using System;
using System.Windows;
namespace ExamApp
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateModulesVisibility();
            UpdateMaxInfo();
        }
        private void Level_Changed(object sender, RoutedEventArgs e)
        {
            UpdateModulesVisibility();
            UpdateMaxInfo();
        }
        private void UpdateModulesVisibility()
        {
            if (rbBU.IsChecked == true)
            {
            }
            else if (rbPU.IsChecked == true)
            {
                txtModule4.Visibility = Visibility.Visible;
                txtModule5.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtModule4.Visibility = Visibility.Visible;
                txtModule5.Visibility = Visibility.Visible;
            }
        }
        private void UpdateMaxInfo()
        {
            string maxText = rbBU.IsChecked == true ? "Максимум БУ: 50 баллов" :
             rbPU.IsChecked == true ? "Максимум ПУ: 75 баллов" :
             "Максимум ПУ+: 100 баллов";
        }
        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(txtModule1.Text, out int m1))
                    throw new FormatException("Модуль 1: введите целое число");
                if (!int.TryParse(txtModule2.Text, out int m2))
                    throw new FormatException("Модуль 2: введите целое число");
                if (!int.TryParse(txtModule3.Text, out int m3))
                    throw new FormatException("Модуль 3: введите целое число");
                int m4 = 0, m5 = 0;
                if (txtModule4.Visibility == Visibility.Visible)
                {
                    if (!int.TryParse(txtModule4.Text, out m4))
                        throw new FormatException("Модуль 4: введите целое число");
                }
                if (txtModule5.Visibility == Visibility.Visible)
                {
                    if (!int.TryParse(txtModule5.Text, out m5))
                        throw new FormatException("Модуль 5: введите целое число");
                }
                string level = rbBU.IsChecked == true ? "BU" :
                 rbPU.IsChecked == true ? "PU" : "PU+";
                var result = ExamCalculator.Calculate(m1, m2, m3, m4, m5, level);
                txtSum.Text = $"Сумма баллов: {result.sum}";
                txtGrade.Text = $"Оценка: {result.grade}";
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButton.OK,
                MessageBoxImage.Warning);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка валидации", MessageBoxButton.OK,
                MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неизвестная ошибка: {ex.Message}", "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}