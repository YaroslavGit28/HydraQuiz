using QuizApp.Models;
using QuizApp.WinForms.Theming;

namespace QuizApp.WinForms;

public partial class LeadersForm : Form
{
    private readonly QuizResultsDbService _dbService = new QuizResultsDbService();

    public LeadersForm()
    {
        InitializeComponent();
        this.Load += LeadersForm_Load!;
    }

    private void LeadersForm_Load(object? sender, EventArgs e)
    {
        ThemeManager.ApplyTheme(this);

        try
        {
            var leaders = _dbService.GetTopLeaders(10);
            if (leaders == null) return;

            _dataGridView.DataSource = leaders;

            // Настройка столбцов
            if (_dataGridView.Columns["DisplayName"] != null)
                _dataGridView.Columns["DisplayName"]!.HeaderText = "Игрок";
            
            if (_dataGridView.Columns["AveragePercentage"] != null)
            {
                _dataGridView.Columns["AveragePercentage"]!.HeaderText = "Средний %";
                _dataGridView.Columns["AveragePercentage"]!.DefaultCellStyle.Format = "F2";
            }
                
            // Скрываем ненужные столбцы
            if (_dataGridView.Columns["UserName"] != null)
                _dataGridView.Columns["UserName"]!.Visible = false;
            if (_dataGridView.Columns["Attempts"] != null)
                _dataGridView.Columns["Attempts"]!.Visible = false;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
} 