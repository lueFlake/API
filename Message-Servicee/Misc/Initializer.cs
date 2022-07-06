using System;
using System.IO;
using System.Text.Json;
#nullable enable

namespace MessageService
{
    /// <summary>
    /// Класс инициализации случайных объектов.
    /// </summary>
    internal static class Initializer
    {
        private static Random s_random = new Random();
        private static User[] s_users = new User[0];

        /// <summary>
        /// Инициализация пользователей.
        /// </summary>
        /// <param name="maxUsersCount">Максимальное количество пользователей.</param>
        /// <param name="maxStringLength">Максимальная длина стркои.</param>
        private static void InitializeUsers(int maxUsersCount, int maxStringLength)
        {
            var generator = new RandomGenerator(maxStringLength);
            int length = s_random.Next(1, maxUsersCount + 1);
            s_users = new User[length];
            for (int i = 0; i < length; i++)
            {
                s_users[i] = new User(generator.RandomLetterString(), generator.RandomEmail());
            }
            Array.Sort(s_users, (item1, item2) => item1.Email.CompareTo(item2.Email));
            string jsonString = JsonSerializer.Serialize(s_users);
            File.WriteAllText("Database/users.json", jsonString);
        }

        /// <summary>
        /// Инициализация сообщений.
        /// </summary>
        /// <param name="maxLettersCount">Максимальное количество сообщений.</param>
        /// <param name="maxStringLength">Максимальная длина стркои.</param>
        private static void InitializeLetters(int maxLettersCount, int maxStringLength)
        {
            var generator = new RandomGenerator(maxStringLength);
            int length = s_random.Next(1, maxLettersCount + 1);
            var letters = new Letter[length];
            for (int i = 0; i < length; i++)
            {
                int index = s_random.Next(s_users.Length);
                int other = (index + s_random.Next(1, s_users.Length)) % s_users.Length;
                letters[i] = new Letter(generator.RandomLetterOrDigitString(),
                    generator.RandomLetterOrDigitString(),
                    s_users[index]?.Email,
                    s_users[other]?.Email);
            }
            string jsonString = JsonSerializer.Serialize(letters);
            File.WriteAllText("Database/letters.json", jsonString);
        }

        /// <summary>
        /// Инициализация БД.
        /// </summary>
        /// <param name="maxUsersCount">Максимальное количество пользователей.</param>
        /// <param name="maxLettersCount">Максимальное количество сообщений.</param>
        /// <param name="maxStringLength">Максимальная длина стркои.</param>
        public static void InitializeDatabase(int maxUsersCount, int maxLettersCount, int maxStringLength)
        {
            InitializeUsers(maxUsersCount, maxStringLength);
            InitializeLetters(maxLettersCount, maxStringLength);
        }
    }
}