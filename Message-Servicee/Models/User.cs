using System.Text.RegularExpressions;

namespace MessageService {
    /// <summary>
    /// Класс пользователя.
    /// </summary>
    public class User {
        /// <summary>
        /// Инициализация пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="email">Почта пользователя.</param>
        public User(string userName, string email) {
            UserName = userName;
            Email = email;
        }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string UserName { get; }

        /// <summary>
        /// Почта пользователя.
        /// </summary>
        public string Email { get; }
    }
}
