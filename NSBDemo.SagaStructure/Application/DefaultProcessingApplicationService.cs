using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NSBDemo.SagaStructure.Application
{
    public class DefaultProcessingApplicationService : IProcessingApplicationService
    {
        private readonly IEventPublisher publisher;

        public DefaultProcessingApplicationService(IEventPublisher publisher)
        {
            this.publisher = publisher;
        }

        public async Task<Guid> Prepare(Guid itemId)
        {
            // DO SOMETHING
            // Prepare item for processing
            var preparationId = Guid.NewGuid();

            // Event notification?
            await publisher.PreparationCompleted(itemId, preparationId).ConfigureAwait(false);
            return preparationId;
        }

        public async Task<Guid> Process(Guid preparationId)
        {
            // DO SOMETHING!
            // Lookup preparation, do some processing and persist the result
            var itemId = Guid.Empty;
            var resultId = Guid.NewGuid();

            // Event notification?
            await publisher.ProcessingCompleted(itemId, resultId).ConfigureAwait(false);
            return resultId;
        }
    }
}
