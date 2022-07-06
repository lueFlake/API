using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
#nullable enable

namespace MessageService {
    /// <summary>
    /// Класс взаимодействия с псевдо-БД.
    /// </summary>
    internal class DatabaseIOManager {
        private string _path;

        /// <summary>
        /// Инициализация класса взаимодействия с БД.
        /// </summary>
        /// <param name="path">Путь к БД.</param>
        public DatabaseIOManager(string path) {
            _path = path;
        }

        /// <summary>
        /// Вывести данные в базу.
        /// </summary>
        /// <param name="obj">Выводимый объект.</param>
        /// <returns>Результат запроса, в зависимости от наличия исключения.</returns>
        public IActionResult TryWrite<T>(T? obj) {
            try {
                string jsonString = JsonSerializer.Serialize(obj);
                System.IO.File.WriteAllText(_path, jsonString);
                return new OkObjectResult(obj);
            }
            catch (System.Exception ex) {
                return new BadRequestObjectResult(ex);
            }
        }

        /// <summary>
        /// Ввести данные в базу.
        /// </summary>
        /// <returns>Результат запроса, в зависимости от наличия исключения.</returns>
        public (IActionResult, T?) TryRead<T>() {
            try {
                string jsonString = System.IO.File.ReadAllText(_path);
                return (new OkResult(), JsonSerializer.Deserialize<T>(jsonString));
            }
            catch (System.Exception ex) { 
                return (new BadRequestObjectResult(ex), default(T));
            }
        }
    }
}
