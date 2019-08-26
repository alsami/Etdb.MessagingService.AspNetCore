using Etdb.ServiceBase.DocumentRepository;
using Etdb.ServiceBase.DocumentRepository.Abstractions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Etdb.MessagingService.Repositories
{
    public class MessagingServiceDocumentDbContext : DocumentDbContext
    {
        public MessagingServiceDocumentDbContext(IOptions<DocumentDbContextOptions> options) : base(options)
        {
        }

        public MessagingServiceDocumentDbContext(MongoClientSettings mongoClientSettings, string databaseName) : base(mongoClientSettings, databaseName)
        {
        }

        public sealed override void Configure()
        {
            throw new System.NotImplementedException();
        }
    }
}