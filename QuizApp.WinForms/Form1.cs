using QuizApp.Models;
using QuizApp.WinForms.Theming;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace QuizApp.WinForms;

public partial class Form1 : Form
{
    private List<Question> _allQuestions = new List<Question>();
    private List<Question> _currentQuizQuestions = new List<Question>();
    private int _currentQuestionIndex;
    private int _score;
    private readonly QuizResultsDbService _dbService = new QuizResultsDbService();
    private CancellationTokenSource? _animationCts;
    private List<int> _userAnswers = new List<int>();

    public Form1()
    {
        InitializeComponent();
        Load += Form1_Load;
    }

    private void Form1_Load(object? sender, EventArgs e)
    {
        ThemeManager.ApplyTheme(this);

        _allQuestions = QuizDataService.LoadQuestions();
        
        var groups = _allQuestions.Select(q => q.Group).Distinct().ToList();
        
        // Отладочная информация
        if (_allQuestions.Count == 0)
        {
            MessageBox.Show("Внимание: Вопросы не загружены! Проверьте файл questions.json", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        else
        {
            var totalQuestions = _allQuestions.Count;
            var totalGroups = groups.Count;
            
            // Показываем информацию о загруженных данных (можно убрать позже)
            this.Text = $"Викторина - Загружено {totalQuestions} вопросов в {totalGroups} группах";
        }
        
        _groupComboBox.DataSource = groups;

        _groupComboBox.SelectedIndexChanged += GroupComboBox_SelectedIndexChanged;
        _startButton.Click += StartButton_Click;
        _nextButton.Click += NextButton_Click;
        _restartButton.Click += RestartButton_Click;
        _leadersButton.Click += LeadersButton_Click;
        _themeSwitchCheckBox.CheckedChanged += ThemeSwitchCheckBox_CheckedChanged;
        _historyButton.Click += HistoryButton_Click;
        _statsButton.Click += StatsButton_Click;
        _exitQuizButton.Click += ExitQuizButton_Click;
        _reviewButton.Click += ReviewButton_Click;

        // Initial theme update
        GroupComboBox_SelectedIndexChanged(this, EventArgs.Empty);
        ShowPanel(_startPanel);
    }

    private void GroupComboBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_groupComboBox.SelectedItem == null) return;
        var selectedGroup = _groupComboBox.SelectedItem.ToString();
        var themes = _allQuestions
            .Where(q => q.Group == selectedGroup)
            .Select(q => q.Category)
            .Distinct()
            .ToList();
        _themeComboBox.DataSource = themes;
    }

    private void StartButton_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_playerNameTextBox.Text))
        {
            MessageBox.Show("Пожалуйста, введите имя игрока.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        
        if (_groupComboBox.SelectedItem == null || _themeComboBox.SelectedItem == null)
        {
            MessageBox.Show("Пожалуйста, выберите группу и тему.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var selectedGroup = _groupComboBox.SelectedItem.ToString();
        var selectedTheme = _themeComboBox.SelectedItem.ToString();

        _currentQuizQuestions = _allQuestions
            .Where(q => q.Group == selectedGroup && q.Category == selectedTheme)
            .ToList();
        
        _score = 0;
        _currentQuestionIndex = 0;
        
        _animationCts?.Cancel();
        _animationCts = new CancellationTokenSource();
        
        _userAnswers.Clear();
        
        DisplayQuestion(_animationCts.Token);
        ShowPanel(_quizPanel);
    }

    private void NextButton_Click(object? sender, EventArgs e)
    {
        var selectedRadioButton = _answersGroupBox.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
        if (selectedRadioButton == null)
        {
            MessageBox.Show("Пожалуйста, выберите ответ.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int selectedAnswerIndex = (int)(selectedRadioButton.Tag ?? 0);
        _userAnswers.Add(selectedAnswerIndex);
        if (selectedAnswerIndex == _currentQuizQuestions[_currentQuestionIndex].CorrectAnswerIndex)
        {
            _score++;
        }

        _currentQuestionIndex++;
        if (_currentQuestionIndex < _currentQuizQuestions.Count)
        {
            DisplayQuestion(_animationCts?.Token ?? CancellationToken.None);
        }
        else
        {
            ShowResults();
        }
    }

    private void RestartButton_Click(object? sender, EventArgs e)
    {
        _playerNameTextBox.Clear();
        ShowPanel(_startPanel);
    }

    private void ThemeSwitchCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        ThemeManager.ToggleTheme();
        ThemeManager.ApplyTheme(this);
    }

    private void LeadersButton_Click(object? sender, EventArgs e)
    {
        var leadersForm = new LeadersForm();
        leadersForm.ShowDialog();
    }

    private async void DisplayQuestion(CancellationToken token)
    {
        _nextButton.Enabled = false;
        var question = _currentQuizQuestions[_currentQuestionIndex];
        _questionLabel.Text = question.Text;

        _answersGroupBox.Controls.Clear();
        for (int i = 0; i < question.Answers.Count; i++)
        {
            var answer = question.Answers[i];
            var radioButton = new RadioButton
            {
                Text = answer,
                Tag = i,
                Location = new Point(20, 30 + i * 30),
                AutoSize = true
            };
            _answersGroupBox.Controls.Add(radioButton);
        }

        // Animation
        const int animationStep = 20;
        int questionLabelTargetY = 50; 
        int answersGroupBoxTargetY = 160;

        _questionLabel.Location = new Point(_questionLabel.Location.X, -_questionLabel.Height);
        _answersGroupBox.Location = new Point(_answersGroupBox.Location.X, this.Height);
        
        try
        {
            bool isMoving = true;
            while (isMoving)
            {
                isMoving = false;

                if (_questionLabel.Location.Y < questionLabelTargetY)
                {
                    _questionLabel.Location = new Point(_questionLabel.Location.X, Math.Min(_questionLabel.Location.Y + animationStep, questionLabelTargetY));
                    isMoving = true;
                }

                if (_answersGroupBox.Location.Y > answersGroupBoxTargetY)
                {
                    _answersGroupBox.Location = new Point(_answersGroupBox.Location.X, Math.Max(_answersGroupBox.Location.Y - animationStep, answersGroupBoxTargetY));
                    isMoving = true;
                }
                
                await Task.Delay(15, token);
            }
        }
        catch (TaskCanceledException)
        {
            // Reset positions if animation is cancelled
            _questionLabel.Location = new Point(_questionLabel.Location.X, questionLabelTargetY);
            _answersGroupBox.Location = new Point(_answersGroupBox.Location.X, answersGroupBoxTargetY);
            return;
        }

        _nextButton.Enabled = true;
    }

    private void ShowResults()
    {
        double percentage = (_currentQuizQuestions.Count > 0) ? (double)_score / _currentQuizQuestions.Count * 100 : 0;
        var playerName = _playerNameTextBox.Text;
        
        // Создаем более подробный текст результата
        var resultText = $"Викторина завершена!\n\n";
        resultText += $"Игрок: {playerName}\n";
        resultText += $"Тема: {_themeComboBox.SelectedItem}\n";
        resultText += $"Результат: {_score} из {_currentQuizQuestions.Count}\n";
        resultText += $"Процент: {percentage:F1}%\n\n";
        
        // Добавляем оценку результата
        string grade;
        if (percentage >= 90) grade = "Отлично! 🏆";
        else if (percentage >= 75) grade = "Хорошо! 👍";
        else if (percentage >= 60) grade = "Удовлетворительно! 🙂";
        else if (percentage >= 40) grade = "Плохо! 😕";
        else grade = "Очень плохо! 😢";
        
        resultText += $"Оценка: {grade}";
        
        _resultLabel.Text = resultText;

        if (_themeComboBox.SelectedItem != null)
        {
            _dbService.AddResult(new QuizResultDbModel
            {
                UserName = playerName,
                Theme = _themeComboBox.SelectedItem.ToString() ?? "Неизвестная тема",
                Score = _score,
                TotalQuestions = _currentQuizQuestions.Count,
                Percentage = percentage,
                CompletedDate = DateTime.Now
            });
        }
        
        ShowPanel(_resultPanel);
    }

    private void HistoryButton_Click(object? sender, EventArgs e)
    {
        var historyForm = new HistoryForm(_playerNameTextBox.Text);
        historyForm.ShowDialog();
    }

    private void StatsButton_Click(object? sender, EventArgs e)
    {
        try
        {
            var history = _dbService.GetUserHistory(_playerNameTextBox.Text);
            if (!history.Any())
            {
                MessageBox.Show("У вас пока нет результатов для отображения статистики.", "Статистика", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var totalGames = history.Count;
            var averageScore = history.Average(h => h.Percentage);
            var bestScore = history.Max(h => h.Percentage);
            var worstScore = history.Min(h => h.Percentage);
            var totalQuestions = history.Sum(h => h.TotalQuestions);
            var totalCorrect = history.Sum(h => h.Score);

            var statsText = $"Статистика игрока: {_playerNameTextBox.Text}\n\n";
            statsText += $"Всего игр: {totalGames}\n";
            statsText += $"Средний результат: {averageScore:F1}%\n";
            statsText += $"Лучший результат: {bestScore:F1}%\n";
            statsText += $"Худший результат: {worstScore:F1}%\n";
            statsText += $"Всего вопросов: {totalQuestions}\n";
            statsText += $"Правильных ответов: {totalCorrect}\n";
            statsText += $"Общая точность: {(totalQuestions > 0 ? (double)totalCorrect / totalQuestions * 100 : 0):F1}%";

            MessageBox.Show(statsText, "Статистика", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки статистики: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ExitQuizButton_Click(object? sender, EventArgs e)
    {
        _animationCts?.Cancel();
        ShowPanel(_startPanel);
    }

    private void ReviewButton_Click(object? sender, EventArgs e)
    {
        var reviewForm = new ReviewForm(_currentQuizQuestions, _userAnswers);
        reviewForm.ShowDialog();
    }

    private void ShowPanel(Panel panelToShow)
    {
        _startPanel.Visible = panelToShow == _startPanel;
        _quizPanel.Visible = panelToShow == _quizPanel;
        _resultPanel.Visible = panelToShow == _resultPanel;
    }
}
