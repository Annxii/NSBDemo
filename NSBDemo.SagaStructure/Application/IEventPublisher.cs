using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NSBDemo.SagaStructure.Application
{
    public interface IEventPublisher
    {
        Task PreparationCompleted(Guid itemId, Guid preparationId);
        Task ProcessingCompleted(Guid itemId, Guid resultId);
    }
}
