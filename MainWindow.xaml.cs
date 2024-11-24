using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MyTestApp
{
    public partial class MainWindow : Window
    {
        private List<Riddle> riddles;
        private List<int> attempts;
        private int currentRiddleIndex;
        private int currentAttempt;
        public MainWindow()
        {
            InitializeComponent();
            StartGame();
        }

        private void StartGame()
        {
            InitializeRiddles();
            currentRiddleIndex = 0;
            currentAttempt = 0;
            attempts = new List<int>();
            DisplayCurrentRiddle();
        }

        private void ResetGame()
        {
            StartGame(); // Запускаем игру заново
        }

        private void InitializeRiddles()
        {
            riddles = new List<Riddle>
        {
            new Riddle("Стучит, Гремит, Вертится, Ходит весь свой век, А не человек. Кто это?", "Часы"),
            new Riddle("Кафтан на мне зеленый, А сердце — как кумач; На вкус, как сахар, сладок, На вид — похож на мяч. Кто это?", "Арбуз"),
            new Riddle("Пустые отдыхаем, А полные шагаем. Кто это?", "Сапоги"),
            new Riddle("Он с жадностью пьет — А не чувствует жажды. Он бел — А купается только однажды: Он смело ныряет В кипящую воду Себе на беду, Но на радость народу... Кто это?", "Сахар"),
            new Riddle("Что загадка эта значит? Ничего я не пойму: По листве зайчонок скачет И рассеивает тьму. Кто это?", "Свет"),
            new Riddle("Наш зверок С вершок. Носом шмыг, шмыг, Хвостиком дрыг, дрыг, А дело делает. Кто это?", "Иголка с ниткой")
        };

            // Перемешиваем загадки
            Random rnd = new Random();
            riddles = riddles.OrderBy(x => rnd.Next()).ToList();
        }

        private void DisplayCurrentRiddle()
        {
            if (currentRiddleIndex < riddles.Count)
            {
                var riddle = riddles[currentRiddleIndex];
                RiddleText.Text = riddle.Text;
                AnswerTextBox.Clear();
                ResultTextBlock.Text = "";
                currentAttempt = 0; // Сбросить количество попыток для текущей загадки
            }
            else
            {
                ShowResults();
            }
        }

        private void CheckAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            var userAnswer = AnswerTextBox.Text.Trim();
            var correctAnswer = riddles[currentRiddleIndex].Answer;

            currentAttempt++; // Увеличиваем количество попыток

            if (userAnswer.Equals(correctAnswer, StringComparison.OrdinalIgnoreCase))
            {
                attempts.Add(currentAttempt);
                ResultBorder.Visibility = Visibility.Collapsed; // Скрываем уведомление
                currentRiddleIndex++;
                DisplayCurrentRiddle();
            }
            else
            {
                ResultTextBlock.Text = "Неправильный ответ. Попробуйте еще раз.";
                ResultBorder.Visibility = Visibility.Visible; // Показываем уведомление
            }
        }


        private void ShowResults()
        {
            string results = "Результаты игры:\n";
            for (int i = 0; i < attempts.Count; i++)
            {
                results += $"Загадка {i + 1}: {attempts[i]} попыток\n";
            }

            ResultWindow1 resultsWindow = new ResultWindow1(results);
            resultsWindow.OnRestartGame += ResetGame;
            resultsWindow.ShowDialog(); // Открываем окно результатов
        }



        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            GameResultsTextBlock.Visibility = Visibility.Collapsed;
            RestartButton.Visibility = Visibility.Collapsed;
            StartGame();
        }
    }

    public class Riddle
    {
        public string Text { get; }
        public string Answer { get; }

        public Riddle(string text, string answer)
        {
            Text = text;
            Answer = answer;
        }
    }
}