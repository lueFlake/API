using System;
using System.Text.RegularExpressions;

namespace MessageService
{
    /// <summary>
    /// 
    /// </summary>
    internal class RandomGenerator
    {
        private static Random s_rng = new Random();

        private static string s_latinChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private static string s_digitChars = "0123456789";

        private int _maxLength;

        /// <summary>
        /// Создание генератора случайных строк.
        /// </summary>
        /// <param name="maxLength">Максимальная длина строки.</param>
        public RandomGenerator(int maxLength)
        {
            _maxLength = maxLength;
        }

        /// <summary>
        /// Генерация строки с цифрами/буквами.
        /// </summary>
        /// <param name="length">Длина строки.</param>
        /// <returns>Случайную строку с цифрами/буквами определенной длины.</returns>
        private string RandomLetterOrDigitString(int length)
        {
            string digitsAndLetters = s_latinChars + s_digitChars;
            string result = "";
            for (int i = 0; i < length; i++)
            {
                result += digitsAndLetters[s_rng.Next(digitsAndLetters.Length)];
            }
            return result;
        }

        /// <summary>
        /// Генерация строки с буквами.
        /// </summary>
        /// <returns>Случайную строку с буквами.</returns>
        public string RandomLetterString()
        {
            string result = "";
            int length = s_rng.Next(1, _maxLength + 1);
            for (int i = 0; i < length; i++)
            {
                result += s_latinChars[s_rng.Next(s_latinChars.Length)];
            }
            return result;
        }

        /// <summary>
        /// Генерация строки с цифрами/буквами.
        /// </summary>
        /// <returns>Случайную строку с цифрами/буквами.</returns>
        public string RandomLetterOrDigitString()
        {
            return RandomLetterOrDigitString(s_rng.Next(_maxLength + 1));
        }

        /// <summary>
        /// Генерация строки с электронной почтой.
        /// </summary>
        /// <returns>Случайную строку с электронной почтой.</returns>
        public string RandomEmail()
        {
            int max = _maxLength;
            var parts = new string[3];
            for (int i = 0; i < 3; i++)
            {
                int length = s_rng.Next(1, Math.Min(max, _maxLength - 2) + 1);
                parts[i] = RandomLetterOrDigitString(length);
                max -= length;
            }
            return parts[0] + "@" + parts[1] + "." + parts[2];
        }
    }
}