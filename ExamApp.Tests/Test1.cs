using ExamApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection.Emit;

namespace ExamApp.Tests
{
    [TestClass]
    public class ExamCalculatorTests
    {
        // ========== КОРРЕКТНЫЕ ДАННЫЕ ==========
        [TestMethod]
        public void Calculate_BU_ValidSums_ReturnsCorrectSumAndGrade()
        {
            // Arrange
            int m1 = 5, m2 = 10, m3 = 15, m4 = 0, m5 = 0;
            string level = "BU";
            
            // Act
            var result = ExamCalculator.Calculate(m1, m2, m3, m4, m5, level);
            // Assert
            Assert.AreEqual(30, result.sum);
            Assert.AreEqual(4, result.grade); // 30/50 = 60% -> 4? Нет, 30/50=60% -> 4, но по шкале 60 - 79 % это 4
        }
        [TestMethod]
        public void Calculate_PU_ValidSums_ReturnsCorrectSumAndGrade()
        {
            // Arrange
            int m1 = 10, m2 = 15, m3 = 25, m4 = 20, m5 = 0;
            string level = "PU";
            // Act
            var result = ExamCalculator.Calculate(m1, m2, m3, m4, m5, level);
            // Assert
            Assert.AreEqual(70, result.sum); // 10+15+25+20 = 70
            Assert.AreEqual(5, result.grade); // 70/75 = 93% -> 5? Нет, 70/75=93% -> 5
        }
        [TestMethod]
        public void Calculate_PUPlus_ValidSums_ReturnsCorrectSumAndGrade()
        {
            // Arrange
            int m1 = 10, m2 = 15, m3 = 25, m4 = 25, m5 = 25;
            string level = "PU+";
            // Act
            var result = ExamCalculator.Calculate(m1, m2, m3, m4, m5, level);
            // Assert
            Assert.AreEqual(100, result.sum);
            Assert.AreEqual(5, result.grade);
        }
        // ========== ГРАНИЧНЫЕ ЗНАЧЕНИЯ ==========
        [TestMethod]
        public void Calculate_BU_MinValues_ReturnsZeroAndGrade2()
        {
            // Arrange
            int m1 = 0, m2 = 0, m3 = 0, m4 = 0, m5 = 0;
            string level = "BU";
            // Act
            var result = ExamCalculator.Calculate(m1, m2, m3, m4, m5, level);
            // Assert
            Assert.AreEqual(0, result.sum);
            Assert.AreEqual(2, result.grade);
        }
        [TestMethod]
        public void Calculate_BU_MaxValues_Returns50AndGrade5()
        {
            // Arrange
            int m1 = 10, m2 = 15, m3 = 25, m4 = 0, m5 = 0;
            string level = "BU";
            // Act
            var result = ExamCalculator.Calculate(m1, m2, m3, m4, m5, level);
            // Assert
            Assert.AreEqual(50, result.sum);
            Assert.AreEqual(5, result.grade); // 50/50 = 100%
        }
        [TestMethod]
        public void Calculate_PU_MinValues_Returns0AndGrade2()
        {
            // Arrange
            int m1 = 0, m2 = 0, m3 = 0, m4 = 0, m5 = 0;
            string level = "PU";
            // Act
            var result = ExamCalculator.Calculate(m1, m2, m3, m4, m5, level);
            // Assert
            Assert.AreEqual(0, result.sum);
            Assert.AreEqual(2, result.grade);
        }
        [TestMethod]
        public void Calculate_PU_MaxValues_Returns75AndGrade5()
        {
            // Arrange
            int m1 = 10, m2 = 15, m3 = 25, m4 = 25, m5 = 0;
            string level = "PU";
            // Act
            var result = ExamCalculator.Calculate(m1, m2, m3, m4, m5, level);
            // Assert
            Assert.AreEqual(75, result.sum);
            Assert.AreEqual(5, result.grade);
        }
        [TestMethod]
        public void Calculate_PUPlus_MinValues_Returns0AndGrade2()
        {
            // Arrange
            int m1 = 0, m2 = 0, m3 = 0, m4 = 0, m5 = 0;
            string level = "PU+";
            // Act
            var result = ExamCalculator.Calculate(m1, m2, m3, m4, m5, level);
            // Assert
            Assert.AreEqual(0, result.sum);
            Assert.AreEqual(2, result.grade);
        }
        [TestMethod]
        public void Calculate_PUPlus_MaxValues_Returns100AndGrade5()
        {
            // Arrange
            int m1 = 10, m2 = 15, m3 = 25, m4 = 25, m5 = 25;
            string level = "PU+";
            // Act
            var result = ExamCalculator.Calculate(m1, m2, m3, m4, m5, level);
            // Assert
            Assert.AreEqual(100, result.sum);
            Assert.AreEqual(5, result.grade);
        }
        // Границы оценок (проценты)
        [TestMethod]
        public void Calculate_GradeBoundary_39Percent_Returns2()
        {
            // 39% от 50 (БУ) = 19.5 -> 19 баллов
            int m1 = 5, m2 = 7, m3 = 7, m4 = 0, m5 = 0;
            string level = "BU"; // 5+7+7=19 из 50 = 38%
            var result = ExamCalculator.Calculate(m1, m2, m3, m4, m5, level);
            Assert.AreEqual(2, result.grade);
        }
        [TestMethod]
        public void Calculate_GradeBoundary_40Percent_Returns3()
        {
            // 40% от 50 = 20 баллов
            int m1 = 5, m2 = 7, m3 = 8, m4 = 0, m5 = 0;
            string level = "BU";
            var result = ExamCalculator.Calculate(m1, m2, m3, m4, m5, level);
            Assert.AreEqual(3, result.grade);
        }
        [TestMethod]
        public void Calculate_GradeBoundary_59Percent_Returns3()
        {
            // 59% от 75 (ПУ) = 44.25 -> 44 балла
            int m1 = 10, m2 = 15, m3 = 10, m4 = 9, m5 = 0;
            string level = "PU"; // 10+15+10+9 = 44 из 75 = 58.6%
            var result = ExamCalculator.Calculate(m1, m2, m3, m4, m5, level);
            Assert.AreEqual(3, result.grade);
        }
        [TestMethod]
        public void Calculate_GradeBoundary_60Percent_Returns4()
        {
            // 60% от 75 = 45 баллов
            int m1 = 10, m2 = 15, m3 = 10, m4 = 10, m5 = 0;
            string level = "PU";
            var result = ExamCalculator.Calculate(m1, m2, m3, m4, m5, level);
            Assert.AreEqual(4, result.grade);
        }
        [TestMethod]
        public void Calculate_GradeBoundary_79Percent_Returns4()
        {
            // 79% от 100 (ПУ+) = 79 баллов
            int m1 = 10, m2 = 15, m3 = 20, m4 = 20, m5 = 14;
            string level = "PU+"; // 10+15+20+20+14 = 79
            var result = ExamCalculator.Calculate(m1, m2, m3, m4, m5, level);
            Assert.AreEqual(4, result.grade);
        }
        [TestMethod]
        public void Calculate_GradeBoundary_80Percent_Returns5()
        {
            // 80% от 100 = 80 баллов
            int m1 = 10, m2 = 15, m3 = 20, m4 = 20, m5 = 15;
            string level = "PU+";
            var result = ExamCalculator.Calculate(m1, m2, m3, m4, m5, level);
            Assert.AreEqual(5, result.grade);
        }
        // ========== НЕКОРРЕКТНЫЕ ДАННЫЕ (ИСКЛЮЧЕНИЯ) ==========
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_InvalidLevel_ThrowsArgumentException()
        {
            ExamCalculator.Calculate(0, 0, 0, 0, 0, "INVALID");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_Module1Negative_ThrowsArgumentException()
        {
            ExamCalculator.Calculate(-1, 0, 0, 0, 0, "BU");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_Module1ExceedsMax_ThrowsArgumentException()
        {
            ExamCalculator.Calculate(11, 0, 0, 0, 0, "BU");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_Module2Negative_ThrowsArgumentException()
        {
            ExamCalculator.Calculate(0, -1, 0, 0, 0, "BU");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_Module2ExceedsMax_ThrowsArgumentException()
        {
            ExamCalculator.Calculate(0, 16, 0, 0, 0, "BU");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_Module3Negative_ThrowsArgumentException()
        {
            ExamCalculator.Calculate(0, 0, -1, 0, 0, "BU");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_Module3ExceedsMax_ThrowsArgumentException()
        {
            ExamCalculator.Calculate(0, 0, 26, 0, 0, "BU");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_PU_Module4Negative_ThrowsArgumentException()
        {
            ExamCalculator.Calculate(0, 0, 0, -1, 0, "PU");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_PU_Module4ExceedsMax_ThrowsArgumentException()
        {
            ExamCalculator.Calculate(0, 0, 0, 26, 0, "PU");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_PUPlus_Module4Negative_ThrowsArgumentException()
        {
            ExamCalculator.Calculate(0, 0, 0, -1, 0, "PU+");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_PUPlus_Module4ExceedsMax_ThrowsArgumentException()
        {
            ExamCalculator.Calculate(0, 0, 0, 26, 0, "PU+");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_PUPlus_Module5Negative_ThrowsArgumentException()
        {
            ExamCalculator.Calculate(0, 0, 0, 0, -1, "PU+");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_PUPlus_Module5ExceedsMax_ThrowsArgumentException()
        {
            ExamCalculator.Calculate(0, 0, 0, 0, 26, "PU+");
        }
        // BU игнорирует модули 4 и 5, даже если они некорректны — не проверяет
        [TestMethod]
        public void Calculate_BU_IgnoresInvalidModule4()
        {
            // BU не проверяет module4, даже если он отрицательный
            var result = ExamCalculator.Calculate(0, 0, 0, -999, 0, "BU");
            Assert.AreEqual(0, result.sum);
        }
    }
}