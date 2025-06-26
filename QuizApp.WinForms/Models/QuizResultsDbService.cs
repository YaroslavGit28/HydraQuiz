using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.IO;
using QuizApp.Models;

namespace QuizApp.Models
{
    public class QuizResultsDbService
    {
        private readonly string _dbPath;
        private readonly string _connectionString;

        public QuizResultsDbService(string dbFileName = "quiz_results.db")
        {
            _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dbFileName);
            _connectionString = $"Data Source={_dbPath}";
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            string createTable = @"CREATE TABLE IF NOT EXISTS QuizResults (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                UserName TEXT NOT NULL,
                Theme TEXT NOT NULL,
                Score INTEGER NOT NULL,
                TotalQuestions INTEGER NOT NULL,
                Percentage REAL NOT NULL,
                CompletedDate TEXT NOT NULL
            );";
            using var cmd = new SqliteCommand(createTable, conn);
            cmd.ExecuteNonQuery();
        }

        public void AddResult(QuizResultDbModel result)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            string insert = @"INSERT INTO QuizResults (UserName, Theme, Score, TotalQuestions, Percentage, CompletedDate)
                              VALUES (@UserName, @Theme, @Score, @TotalQuestions, @Percentage, @CompletedDate)";
            using var cmd = new SqliteCommand(insert, conn);
            cmd.Parameters.AddWithValue("@UserName", result.UserName);
            cmd.Parameters.AddWithValue("@Theme", result.Theme);
            cmd.Parameters.AddWithValue("@Score", result.Score);
            cmd.Parameters.AddWithValue("@TotalQuestions", result.TotalQuestions);
            cmd.Parameters.AddWithValue("@Percentage", result.Percentage);
            cmd.Parameters.AddWithValue("@CompletedDate", result.CompletedDate.ToString("o"));
            cmd.ExecuteNonQuery();
        }

        // Получить лучших пользователей (по максимальному проценту или баллу среди всех тем)
        public List<UserBestResult> GetBestResults()
        {
            var results = new List<UserBestResult>();
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            string query = @"
                SELECT UserName, MAX(Percentage) as Percentage, MAX(Score) as Score, MAX(TotalQuestions) as TotalQuestions
                FROM QuizResults
                GROUP BY UserName
            ";
            using var cmd = new SqliteCommand(query, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                results.Add(new UserBestResult
                {
                    UserName = reader["UserName"].ToString(),
                    Theme = "-", // Тема неактуальна для общего пользователя
                    Score = Convert.ToInt32(reader["Score"]),
                    TotalQuestions = Convert.ToInt32(reader["TotalQuestions"]),
                    Percentage = Convert.ToDouble(reader["Percentage"])
                });
            }
            return results;
        }

        // Получить всю историю попыток пользователя
        public List<UserHistoryResult> GetUserHistory(string userName)
        {
            var results = new List<UserHistoryResult>();
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            string query = @"SELECT Theme, Score, TotalQuestions, Percentage, CompletedDate FROM QuizResults WHERE UserName = @UserName ORDER BY CompletedDate DESC";
            using var cmd = new SqliteCommand(query, conn);
            cmd.Parameters.AddWithValue("@UserName", userName);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                results.Add(new UserHistoryResult
                {
                    Theme = reader["Theme"].ToString(),
                    Score = Convert.ToInt32(reader["Score"]),
                    TotalQuestions = Convert.ToInt32(reader["TotalQuestions"]),
                    Percentage = Convert.ToDouble(reader["Percentage"]),
                    CompletedDate = DateTime.Parse(reader["CompletedDate"].ToString())
                });
            }
            return results;
        }

        // Получить топ-5 пользователей с самым высоким средним процентом
        public List<LeaderBoardUser> GetTopLeaders(int top = 5)
        {
            var leaders = new List<LeaderBoardUser>();
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            string query = @"
                SELECT UserName, AVG(Percentage) as AvgPercent, COUNT(*) as Attempts
                FROM QuizResults
                GROUP BY UserName
                ORDER BY AvgPercent DESC
                LIMIT @Top
            ";
            using var cmd = new SqliteCommand(query, conn);
            cmd.Parameters.AddWithValue("@Top", top);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                leaders.Add(new LeaderBoardUser
                {
                    UserName = reader["UserName"].ToString(),
                    AveragePercentage = System.Convert.ToDouble(reader["AvgPercent"]),
                    Attempts = System.Convert.ToInt32(reader["Attempts"])
                });
            }
            return leaders;
        }

        // Удалить всю статистику пользователя
        public void DeleteUserResults(string userName)
        {
            using var conn = new SqliteConnection(_connectionString);
            conn.Open();
            string query = "DELETE FROM QuizResults WHERE UserName = @UserName";
            using var cmd = new SqliteCommand(query, conn);
            cmd.Parameters.AddWithValue("@UserName", userName);
            cmd.ExecuteNonQuery();
        }
    }
} 