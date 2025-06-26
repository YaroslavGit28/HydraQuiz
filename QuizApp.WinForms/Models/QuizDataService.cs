using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace QuizApp.Models
{
    public static class QuizDataService
    {
        private static readonly string _questionsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "questions.json");
        private static readonly string _resultsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "results.json");

        public static List<Question> LoadQuestions()
        {
            if (!File.Exists(_questionsFilePath))
            {
                return new List<Question>();
            }
            try
            {
                var json = File.ReadAllText(_questionsFilePath);
                return JsonSerializer.Deserialize<List<Question>>(json) ?? new List<Question>();
            }
            catch
            {
                return new List<Question>();
            }
        }

        public static void SaveQuestions(List<Question> questions)
        {
            var json = JsonSerializer.Serialize(questions, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_questionsFilePath, json);
        }

        public static List<QuizResult> LoadResults()
        {
            if (!File.Exists(_resultsFilePath))
            {
                return new List<QuizResult>();
            }
            try
            {
                var json = File.ReadAllText(_resultsFilePath);
                return JsonSerializer.Deserialize<List<QuizResult>>(json) ?? new List<QuizResult>();
            }
            catch
            {
                return new List<QuizResult>();
            }
        }

        public static void SaveResults(List<QuizResult> results)
        {
            var json = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_resultsFilePath, json);
        }
    }
} 