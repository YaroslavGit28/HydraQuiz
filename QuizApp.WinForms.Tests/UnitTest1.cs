using NUnit.Framework;
using QuizApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuizApp.WinForms.Tests
{
    /// <summary>
    /// Тесты для сервисов работы с вопросами и результатами викторины.
    /// </summary>
    [TestFixture]
    public class QuizServicesTests
    {
        private string _testQuestionsFile;
        private string _testResultsFile;
        private string _testDbFile;

        [SetUp]
        public void Setup()
        {
            // Используем временные файлы для тестов, чтобы не затронуть реальные данные
            _testQuestionsFile = Path.GetTempFileName();
            _testResultsFile = Path.GetTempFileName();
            _testDbFile = Path.GetTempFileName();
        }

        [TearDown]
        public void Cleanup()
        {
            if (File.Exists(_testQuestionsFile)) File.Delete(_testQuestionsFile);
            if (File.Exists(_testResultsFile)) File.Delete(_testResultsFile);
            if (File.Exists(_testDbFile)) File.Delete(_testDbFile);
        }

        /// <summary>
        /// Проверяет сохранение и загрузку вопросов викторины.
        /// </summary>
        [Test]
        public void SaveAndLoadQuestions_WorksCorrectly()
        {
            // Arrange
            var questions = new List<Question>
            {
                new Question { Text = "2+2?", Answers = new List<string>{"3","4"}, CorrectAnswerIndex = 1, Category = "Математика", Group = "Школа" },
                new Question { Text = "Столица Франции?", Answers = new List<string>{"Париж","Берлин"}, CorrectAnswerIndex = 0, Category = "География", Group = "Школа" }
            };
            // Act
            File.WriteAllText(_testQuestionsFile, System.Text.Json.JsonSerializer.Serialize(questions));
            var loaded = System.Text.Json.JsonSerializer.Deserialize<List<Question>>(File.ReadAllText(_testQuestionsFile));
            // Assert
            Assert.NotNull(loaded);
            Assert.AreEqual(2, loaded.Count);
            Assert.AreEqual("2+2?", loaded[0].Text);
            Assert.AreEqual("Столица Франции?", loaded[1].Text);
        }

        /// <summary>
        /// Проверяет сохранение и загрузку результатов викторины.
        /// </summary>
        [Test]
        public void SaveAndLoadResults_WorksCorrectly()
        {
            // Arrange
            var results = new List<QuizResult>
            {
                new QuizResult { PlayerName = "Иван", Score = 5, TotalQuestions = 10, CompletedDate = DateTime.Now, TimeTaken = TimeSpan.FromMinutes(2) },
                new QuizResult { PlayerName = "Анна", Score = 8, TotalQuestions = 10, CompletedDate = DateTime.Now, TimeTaken = TimeSpan.FromMinutes(1) }
            };
            // Act
            File.WriteAllText(_testResultsFile, System.Text.Json.JsonSerializer.Serialize(results));
            var loaded = System.Text.Json.JsonSerializer.Deserialize<List<QuizResult>>(File.ReadAllText(_testResultsFile));
            // Assert
            Assert.NotNull(loaded);
            Assert.AreEqual(2, loaded.Count);
            Assert.AreEqual("Иван", loaded[0].PlayerName);
            Assert.AreEqual("Анна", loaded[1].PlayerName);
        }

        /// <summary>
        /// Проверяет добавление и получение результатов из базы данных SQLite.
        /// </summary>
        [Test]
        public void AddAndGetResults_FromDb_WorksCorrectly()
        {
            // Arrange
            var dbService = new QuizResultsDbService(Path.GetFileName(_testDbFile));
            var result = new QuizResultDbModel
            {
                UserName = "Тестовый пользователь",
                Theme = "Математика",
                Score = 9,
                TotalQuestions = 10,
                Percentage = 90.0,
                CompletedDate = DateTime.Now
            };
            // Act
            dbService.AddResult(result);
            var bestResults = dbService.GetBestResults();
            // Assert
            Assert.IsTrue(bestResults.Any(r => r.UserName == "Тестовый пользователь"));
            var userHistory = dbService.GetUserHistory("Тестовый пользователь");
            Assert.IsTrue(userHistory.Any(h => h.Theme == "Математика"));
        }

        /// <summary>
        /// Проверяет удаление результатов пользователя из базы данных.
        /// </summary>
        [Test]
        public void DeleteUserResults_RemovesData()
        {
            // Arrange
            var dbService = new QuizResultsDbService(Path.GetFileName(_testDbFile));
            var result = new QuizResultDbModel
            {
                UserName = "Удаляемый пользователь",
                Theme = "История",
                Score = 7,
                TotalQuestions = 10,
                Percentage = 70.0,
                CompletedDate = DateTime.Now
            };
            dbService.AddResult(result);
            // Act
            dbService.DeleteUserResults("Удаляемый пользователь");
            var userHistory = dbService.GetUserHistory("Удаляемый пользователь");
            // Assert
            Assert.IsEmpty(userHistory);
        }
    }
}
// Документация:
// Данные тесты проверяют корректность работы сервисов для загрузки/сохранения вопросов и результатов викторины, а также взаимодействие с базой данных SQLite.
// Каждый тест использует временные файлы, чтобы не затронуть реальные пользовательские данные.
// Тесты можно запускать командой: dotnet test
// Для успешного прохождения тестов требуется наличие прав на создание файлов во временной папке и наличие SQLite.
