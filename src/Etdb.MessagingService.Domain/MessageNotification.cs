using System;

namespace Etdb.MessagingService.Domain
{
    public class MessageNotification : GuidDocument
    {
        public Guid MessageId { get; private set; }

        public Guid ReceiverId { get; private set; }

        public MessageNotification(Guid id, Guid messageId, Guid receiverId) : base(id)
        {
            this.MessageId = messageId;
            this.ReceiverId = receiverId;
        }
    }
}