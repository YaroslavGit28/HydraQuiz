using System;

namespace QuizApp.Models
{
    public class QuizResult
    {
        public string PlayerName { get; set; } = string.Empty;
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public DateTime CompletedDate { get; set; }
        public TimeSpan TimeTaken { get; set; }
    }
} 