using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NSBDemo.SagaStructure.Application
{
    public interface IProcessingApplicationService
    {
        Task<Guid> Prepare(Guid itemId);
        Task<Guid> Process(Guid preparationId);
    }
}
