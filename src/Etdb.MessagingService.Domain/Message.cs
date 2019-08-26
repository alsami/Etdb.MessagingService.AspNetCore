using System;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace Etdb.MessagingService.Domain
{
    public class Message : GuidDocument
    {
        public Guid SenderId { get; private set; }

        public Guid ReceiverId { get; private set; }

        public byte[] Salt { get; private set; }

        public string Content { get; private set; }

        public Message(Guid id, Guid senderId, Guid receiverId, byte[] salt, string content) : base(id)
        {
            this.SenderId = senderId;
            this.ReceiverId = receiverId;
            this.Salt = salt;
            this.Content = content;
        }
    }
}