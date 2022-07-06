namespace MessageService {
    /// <summary>
    /// Класс сообщения.
    /// </summary>
    public class Letter {
        /// <summary>
        /// Инициализацмя сообщения.
        /// </summary>
        /// <param name="subject">Тема сообщения.</param>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="senderID">Идентификатор отправителя.</param>
        /// <param name="receiverID">Идентификатор получателя.</param>
        public Letter(string subject, string message, string senderID, string receiverID) {
            Subject = subject;
            Message = message;
            SenderID = senderID;
            ReceiverID = receiverID;
        }

        /// <summary>
        /// Тема сообщения.
        /// </summary>
        public string Subject { get; }

        /// <summary>
        /// Текст сообщения.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Идентификатор отправителя.
        /// </summary>
        public string SenderID { get; }

        /// <summary>
        /// Идентификатор получателя.
        /// </summary>
        public string ReceiverID { get; }
    }
}
