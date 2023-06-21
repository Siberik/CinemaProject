using CinemaProjectLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HourCheckingClassTests
{
    [TestClass]
    public class HourCheckingClassTests
    {
        private HourAndDateChekingClass target; // Замените на имя вашего класса

        [TestInitialize]
        public void Setup()
        {
            target = new HourAndDateChekingClass(); // Создание экземпляра класса перед каждым тестом
        }

        /// <summary>
        /// Тест проверки пустого формата времени.
        /// Ожидается false, так как пустая строка не является валидным форматом времени.
        /// </summary>
        [TestMethod]
        public void TestEmptyTimeFormat()
        {
            // Arrange
            string input = "";

            // Act & Assert
            Assert.IsFalse(target.IsValidTimeFormat(input));
        }

        /// <summary>
        /// Тест проверки недопустимых символов в формате времени.
        /// Ожидается false, так как строка содержит символы, не соответствующие формату времени.
        /// </summary>
        [TestMethod]
        public void TestInvalidCharacters()
        {
            // Arrange
            string input = "abc";

            // Act & Assert
            Assert.IsFalse(target.IsValidTimeFormat(input));
        }

        /// <summary>
        /// Тест проверки валидного формата времени.
        /// Ожидается true, так как строка соответствует формату времени "чч:мм".
        /// </summary>
        [TestMethod]
        public void TestValidTimeFormat()
        {
            // Arrange
            string input = "12:34";

            // Act & Assert
            Assert.IsTrue(target.IsValidTimeFormat(input));
        }

        /// <summary>
        /// Тест проверки недопустимых секунд в формате времени.
        /// Ожидается false, так как строка содержит недопустимое значение секунд (96).
        /// </summary>
        [TestMethod]
        public void TestInvalidSeconds()
        {
            // Arrange
            string input = "12:34:96";

            // Act & Assert
            Assert.IsFalse(target.IsValidTimeFormat(input));
        }

        /// <summary>
        /// Тест проверки недопустимого формата времени, содержащего только часы.
        /// Ожидается false, так как формат времени должен содержать и минуты.
        /// </summary>
        [TestMethod]
        public void TestInvalidHourOnly()
        {
            // Arrange
            string input = "12";

            // Act & Assert
            Assert.IsFalse(target.IsValidTimeFormat(input));
        }

        /// <summary>
        /// Тест проверки граничных значений формата времени.
        /// Ожидается false, так как формат времени не допускает значение "24:00".
        /// </summary>
        [TestMethod]
        public void TestBoundaryValues()
        {
            // Arrange
            string input = "24:00";

            // Act & Assert
            Assert.IsFalse(target.IsValidTimeFormat(input));
        }

        /// <summary>
        /// Положительный сценарий проверки валидного формата времени.
        /// Ожидается true, так как строка соответствует формату времени "чч:мм".
        /// </summary>
        [TestMethod]
        public void TestPositiveScenario()
        {
            // Arrange
            string input = "08:15";

            // Act & Assert
            Assert.IsTrue(target.IsValidTimeFormat(input));
        }
    }
}
