using CinemaProjectLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourCheckingClassTests
{
    [TestClass]
    public class GenerateRandomPasswordTests
    {
        [TestMethod]
        public void TestGenerateRandomPassword_NegativeLength()
        {
            // Arrange
            int length = -5;

            // Act & Assert
            string login = LoginAndPasswordGeneratorClass.GenerateRandomLogin(length);

            // Assert
            Assert.AreEqual("", login); // Ожидается пустая строка, так как длина равна 0
        }
    


        [TestMethod]
        public void TestGenerateRandomPassword_ZeroLength()
        {
            // Arrange
            int length = 0;

            // Act
            string password = LoginAndPasswordGeneratorClass.GenerateRandomPassword(length);

            // Assert
            Assert.AreEqual("", password); // Ожидается пустая строка, так как длина равна 0
        }

        [TestMethod]
        public void TestGenerateRandomPassword_LengthCheck()
        {
            // Arrange
            int length = 10;

            // Act
            string password = LoginAndPasswordGeneratorClass.GenerateRandomPassword(length);

            // Assert
            Assert.AreEqual(length, password.Length); // Ожидается, что длина сгенерированного пароля равна заданной длине
        }
    }
}
