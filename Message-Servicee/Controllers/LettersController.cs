using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System;
#nullable enable

namespace MessageService {
    /// <summary>
    /// Оператор для отправки/получения сообщений.
    /// </summary>
    [ApiController]
    [Route("api")]
    public class LettersController : ControllerBase {
        private static readonly DatabaseIOManager s_usersIO = new("Database/users.json");
        private static readonly DatabaseIOManager s_lettersIO = new("Database/letters.json");
        /// <summary>
        /// Запрос на получение всех сообщений с отправителем и получателем.
        /// </summary>
        /// <param name="sender">Получатель (Опционально).</param>
        /// <param name="receiver">Отправитель (Опционально).</param>
        /// <returns>Результат запроса.</returns>

        [Produces("application/json")]
        [HttpGet("letters")]
        public IActionResult Get([FromQuery(Name = "sender")] string? sender, [FromQuery(Name = "receiver")] string? receiver) {
            (IActionResult result, Letter[]? letters) = s_lettersIO.TryRead<Letter[]>();
            if (result is not OkResult) {
                return result;
            }

            if (letters is null) {
                return Ok(new Letter[0]);
            }

            IEnumerable<Letter> requestedLetters = letters.Where((item) =>
                (sender is null || item.SenderID == sender) &&
                (receiver is null || item.ReceiverID == receiver));

            if (!requestedLetters.Any()) {
                return NotFound("Не найдено ни одного подходящего сообщения.");
            }

            return Ok(requestedLetters);
        }

        /// <summary>
        /// Запрос на отправку сообщения.
        /// </summary>
        /// <param name="newLetter">Отправляемое сообщение.</param>
        /// <returns>Результат запроса.</returns>
        [Produces("application/json")]
        [HttpPost("letters")]
        public IActionResult Post([FromBody] Letter newLetter) {
            (IActionResult result1, Letter[]? letters) = s_lettersIO.TryRead<Letter[]>();
            if (result1 is not OkResult) {
                return result1;
            }

            (IActionResult result2, User[]? users) = s_usersIO.TryRead<User[]>();
            if (result2 is not OkResult) {
                return result2;
            }

            if (!Validator.ValidateLetter(newLetter, users)) {
                return BadRequest();
            }

            if (letters is null || users is null) {
                return BadRequest();
            }

            if (users.FirstOrDefault((item) => item.Email == newLetter.ReceiverID) is null) {
                return NotFound("Не указан существующий получатель.");
            }
            if (users.FirstOrDefault((item) => item.Email == newLetter.SenderID) is null) {
                return NotFound("Не указан существующий отправитель.");
            }

            Array.Resize(ref letters, letters.Length + 1);
            letters[^1] = newLetter;

            return s_lettersIO.TryWrite(letters);
        }
    }
}
