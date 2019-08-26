using System;
using Etdb.ServiceBase.Domain.Abstractions.Documents;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace Etdb.MessagingService.Domain
{
    public abstract class GuidDocument : IDocument<Guid>
    {
        public Guid Id { get; private set; }

        protected GuidDocument(Guid id)
        {
            this.Id = id;
        }
    }
}