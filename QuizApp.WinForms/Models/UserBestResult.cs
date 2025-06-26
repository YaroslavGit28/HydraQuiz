namespace QuizApp.Models
{
    public class UserBestResult
    {
        public string UserName { get; set; } = string.Empty;
        public string Theme { get; set; } = string.Empty;
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public double Percentage { get; set; }
    }
} 