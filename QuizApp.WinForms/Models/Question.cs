using System.Collections.Generic;

namespace QuizApp.Models
{
    public class Question
    {
        public string Text { get; set; } = string.Empty;
        public List<string> Answers { get; set; } = new List<string>();
        public int CorrectAnswerIndex { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
    }
} 