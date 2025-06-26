using QuizApp.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace QuizApp.WinForms
{
    public class ReviewForm : Form
    {
        private DataGridView _dataGridView;

        public ReviewForm(List<Question> questions, List<int> userAnswers)
        {
            this.Text = "Разбор ответов";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 11F);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Normal;
            this.Load += (s, e) => { if (this.WindowState == FormWindowState.Normal) this.CenterToScreen(); };

            _dataGridView = new DataGridView();
            _dataGridView.Dock = DockStyle.Fill;
            _dataGridView.ReadOnly = true;
            _dataGridView.AllowUserToAddRows = false;
            _dataGridView.AllowUserToDeleteRows = false;
            _dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            _dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            var table = new System.Data.DataTable();
            table.Columns.Add("Вопрос");
            table.Columns.Add("Ваш ответ");
            table.Columns.Add("Правильный ответ");
            table.Columns.Add("Результат");

            for (int i = 0; i < questions.Count; i++)
            {
                var q = questions[i];
                var userIdx = (i < userAnswers.Count) ? userAnswers[i] : -1;
                var userAns = userIdx >= 0 && userIdx < q.Answers.Count ? q.Answers[userIdx] : "-";
                var correctAns = q.Answers[q.CorrectAnswerIndex];
                var isCorrect = userIdx == q.CorrectAnswerIndex;
                table.Rows.Add(q.Text, userAns, correctAns, isCorrect ? "✔" : "✘");
            }

            _dataGridView.DataSource = table;
            this.Controls.Add(_dataGridView);
        }
    }
} 