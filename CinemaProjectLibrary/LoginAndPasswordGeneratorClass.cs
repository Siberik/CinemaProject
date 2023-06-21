using System;

namespace CinemaProjectLibrary
{
    public class LoginAndPasswordGeneratorClass
    {
        private const string LoginChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const string PasswordChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()-_=+[]{};:'\",.<>/?";

        // Генерация случайного логина заданной длины
        public static string GenerateRandomLogin(int length)
        {
            try
            {
                return GenerateRandomString(LoginChars, length);
            }
            catch
            {
                return string.Empty;
            }
        }

        // Генерация случайного пароля заданной длины
        public static string GenerateRandomPassword(int length)
        {
            try
            {
                return GenerateRandomString(PasswordChars, length);
            }
            catch
            {
                return string.Empty;
            }
        }

        private static string GenerateRandomString(string chars, int length)
        {
            if (length <= 0)
                throw new ArgumentOutOfRangeException(nameof(length), "Length must be greater than zero.");

            var random = new Random();
            var result = new char[length];

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                result[i] = chars[index];
            }

            return new string(result);
        }
    }
}
