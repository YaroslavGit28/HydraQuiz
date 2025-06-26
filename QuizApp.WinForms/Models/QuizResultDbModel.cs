using System;

namespace QuizApp.Models
{
    public class QuizResultDbModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Theme { get; set; } = string.Empty;
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public double Percentage { get; set; }
        public DateTime CompletedDate { get; set; }
    }
} 