using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace MyTestApp
{
    public partial class ResultWindow1 : Window
    {
        public delegate void RestartGameHandler();
        public event RestartGameHandler OnRestartGame;

        public ResultWindow1(string results)
        {
            InitializeComponent();
            ResultsTextBlock.Text = results;
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            OnRestartGame?.Invoke(); // Вызываем событие, чтобы уведомить MainWindow
            this.Close(); // Закрываем окно результатов
        }
    }
}

