using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
#nullable enable

namespace MessageService {
    /// <summary>
    /// Класс валидации объектов.
    /// </summary>
    internal static class Validator {
        /// <summary>
        /// Валидация пользователя.
        /// </summary>
        /// <param name="user">Проверяемый пользователь.</param>
        /// <returns>Результат валидации.</returns>
        public static bool ValidateUser(User? user) {
            return user is not null &&
                user.UserName is not null &&
                user.Email is not null &&
                Regex.IsMatch(user.UserName, "^\\w+$") && 
                Regex.IsMatch(user.Email, "^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*$");
        }

        /// <summary>
        /// Валидация сообщения.
        /// </summary>
        /// <param name="letter">Проверяемое сообщение.</param>
        /// <param name="users">Зарегистрированные пользователи.</param>
        /// <returns>Результат валидации.</returns>
        public static bool ValidateLetter(Letter? letter, IEnumerable<User>? users) {
            return users is not null &&
                letter is not null &&
                letter.Message is not null &&
                users.Any(item => item.Email == letter.ReceiverID && item.Email == letter.SenderID) &&
                letter.SenderID != letter.ReceiverID;
        }
    }
}
