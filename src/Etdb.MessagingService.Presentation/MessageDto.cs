using System;

namespace Etdb.MessagingService.Presentation
{
    public class MessageDto : GuidDto
    {
        public Guid SenderId { get; }

        public Guid ReceiverId { get; }
        
        public string Content { get; }

        public MessageDto(Guid guid, Guid senderId, Guid receiverId, string content) : base(guid)
        {
            this.SenderId = senderId;
            this.ReceiverId = receiverId;
            this.Content = content;
        }
    }
}