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
    public class GenerateRando_LoginTests
    {
        [TestClass]
        public class LoginAndPasswordGeneratorClassTests
        {
            /// <summary>
            /// Тестирование метода GenerateRandomLogin с отрицательной длиной.
            /// Ожидается возникновение исключения ArgumentOutOfRangeException.
            /// </summary>
            [TestMethod]
            public void TestGenerateRandomLogin_NegativeLength()
            {
                // Arrange
                int length = -5;

                string login = LoginAndPasswordGeneratorClass.GenerateRandomLogin(length);

                // Assert
                Assert.AreEqual("", login); // Ожидается пустая строка, так как длина равна 0
            }
        }

            /// <summary>
            /// Тестирование метода GenerateRandomLogin с длиной равной 0.
            /// Ожидается пустая строка, так как длина равна 0.
            /// </summary>
            [TestMethod]
            public void TestGenerateRandomLogin_ZeroLength()
            {
                // Arrange
                int length = 0;

                // Act
                string login = LoginAndPasswordGeneratorClass.GenerateRandomLogin(length);

                // Assert
                Assert.AreEqual("", login); // Ожидается пустая строка, так как длина равна 0
            }

            /// <summary>
            /// Тестирование метода GenerateRandomLogin на проверку длины сгенерированного логина.
            /// Ожидается, что длина сгенерированного логина будет равна заданной длине.
            /// </summary>
            [TestMethod]
            public void TestGenerateRandomLogin_LengthCheck()
            {
                // Arrange
                int length = 8;

                // Act
                string login = LoginAndPasswordGeneratorClass.GenerateRandomLogin(length);

                // Assert
                Assert.AreEqual(length, login.Length); // Ожидается, что длина сгенерированного логина равна заданной длине
            }
        }
    }

