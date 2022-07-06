using Microsoft.AspNetCore.Mvc;

namespace MessageService {
    /// <summary>
    /// Оператор инициализации.
    /// </summary>
    [ApiController]
    [Route("api")]
    public class InitializerController : ControllerBase {
        /// <summary>
        /// Запрос инициализации базы данных.
        /// </summary>
        /// <returns>Результат запроса.</returns>
        [Produces("application/json")]
        [HttpPost("init")]
        public IActionResult Post() {
            Initializer.InitializeDatabase(10, 10, 20);
            return Ok();
        }
    }
}