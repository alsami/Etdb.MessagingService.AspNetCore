using System;

namespace Etdb.MessagingService.Presentation
{
    public abstract class GuidDto
    {
        protected Guid Guid { get; }

        public GuidDto(Guid guid)
        {
            this.Guid = guid;
        }
    }
}