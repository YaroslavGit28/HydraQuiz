namespace QuizApp.Models
{
    public class LeaderBoardUser
    {
        public string UserName { get; set; } = string.Empty;
        public double AveragePercentage { get; set; }
        public int Attempts { get; set; }
        public string DisplayName => $"{UserName} ({Attempts} п.)";
    }
} 