using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
#nullable enable

namespace MessageService {
    /// <summary>
    /// Оператор для добавления/получения пользователей.
    /// </summary>
    [ApiController]
    [Route("api")]
    public class UsersController : ControllerBase {
        private static readonly DatabaseIOManager s_usersIO = new("Database/users.json");
        /// <summary>
        /// Запрос на получение пользователей в подмассиве.
        /// </summary>
        /// <param name="limit">Количество пользователей, которое необходимо вернуть.</param>
        /// <param name="offset">Порядковый номер пользователя, начиная с которого необходимо получать информацию.</param>
        /// <returns>Результат запроса.</returns>

        [Produces("application/json")]
        [HttpGet("users")]
        public IActionResult Get([FromQuery(Name = "limit")] int limit = 1, [FromQuery(Name = "offset")] int offset = 0) {
            (IActionResult result, User[]? users) = s_usersIO.TryRead<User[]>();
            if (result is not OkResult) {
                return result;
            }

            if (users is null) {
                return new OkObjectResult(Enumerable.Empty<User>());
            }

            if (offset >= users.Count() || limit > users.Count() - offset || offset < 0 || limit <= 0) {
                return BadRequest();
            }

            return Ok(users[offset..(limit + offset)]);
        }


        /// <summary>
        /// Запрос на получение всех пользователей.
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [HttpGet("allusers")]
        public IActionResult Get() {
            (IActionResult result, User[]? users) = s_usersIO.TryRead<User[]>();
            if (result is not OkResult) {
                return result;
            }

            if (users is null) {
                return NotFound();
            }

            return Ok(users);
        }

        /// <summary>
        /// Запрос на получение пользователя по почте.
        /// </summary>
        /// <param name="email">Почта.</param>
        /// <returns>Результат запроса.</returns>
        [Produces("application/json")]
        [HttpGet("users/{email}")]
        public IActionResult Get(string email) {
            (IActionResult result, User[]? users) = s_usersIO.TryRead<User[]>();
            if (result is not OkResult) {
                return result;
            }

            if (users is null) {
                return NotFound();
            }


            User? requestedUser = users.FirstOrDefault((item) => item.Email == email);
            if (requestedUser is null) {
                return NotFound();
            }

            return Ok(requestedUser);
        }

        /// <summary>
        /// Запрос на добавление нового пользователя.
        /// </summary>
        /// <param name="newUser">Новый пользователь.</param>
        /// <returns>Результат запроса.</returns>
        [Produces("application/json")]
        [HttpPost("users")]
        public IActionResult Post([FromBody] User newUser) {
            (IActionResult result, User[]? users) = s_usersIO.TryRead<User[]>();
            if (result is not OkResult) {
                return result;
            }

            if (!Validator.ValidateUser(newUser)) {
                return BadRequest();
            }

            if (users is null) {
                users = new User[0];
            }

            if (users.FirstOrDefault((item) => item.Email == newUser.Email) is not null) {
                return BadRequest();
            }

            Array.Resize(ref users, users.Length + 1);
            users[^1] = newUser;

            Array.Sort(users, (item1, item2) => item1.Email.CompareTo(item2.Email));

            return s_usersIO.TryWrite(users);
        }
    }
}