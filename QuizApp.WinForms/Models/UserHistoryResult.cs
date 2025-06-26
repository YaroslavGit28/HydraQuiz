using System;

namespace QuizApp.Models
{
    public class UserHistoryResult
    {
        public string Theme { get; set; } = string.Empty;
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public double Percentage { get; set; }
        public DateTime CompletedDate { get; set; }
    }
} 