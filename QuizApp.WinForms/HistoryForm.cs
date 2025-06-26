using QuizApp.Models;
using QuizApp.WinForms.Theming;
using System;
using System.Windows.Forms;

namespace QuizApp.WinForms
{
    public partial class HistoryForm : Form
    {
        private readonly QuizResultsDbService _dbService = new QuizResultsDbService();
        private readonly string _playerName;

        public HistoryForm(string playerName)
        {
            InitializeComponent();
            _playerName = playerName;
            this.Text = $"История игр: {playerName}";
            this.Load += HistoryForm_Load!;
        }

        private void HistoryForm_Load(object? sender, EventArgs e)
        {
            ThemeManager.ApplyTheme(this);
            try
            {
                var history = _dbService.GetUserHistory(_playerName);
                _historyDataGridView.DataSource = history;

                // Настройка столбцов
                if (_historyDataGridView.Columns["Theme"] != null)
                    _historyDataGridView.Columns["Theme"]!.HeaderText = "Тема";
                if (_historyDataGridView.Columns["Score"] != null)
                    _historyDataGridView.Columns["Score"]!.HeaderText = "Счет";
                if (_historyDataGridView.Columns["TotalQuestions"] != null)
                    _historyDataGridView.Columns["TotalQuestions"]!.HeaderText = "Всего вопросов";
                if (_historyDataGridView.Columns["Percentage"] != null)
                {
                    _historyDataGridView.Columns["Percentage"]!.HeaderText = "Процент";
                    _historyDataGridView.Columns["Percentage"]!.DefaultCellStyle.Format = "F2";
                }
                if (_historyDataGridView.Columns["CompletedDate"] != null)
                    _historyDataGridView.Columns["CompletedDate"]!.HeaderText = "Дата";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки истории: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
} 