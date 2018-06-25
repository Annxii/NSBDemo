using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NSBDemo.SagaStructure.Application
{
    public class DefaultProcessingApplicationService : IProcessingApplicationService
    {
        public Task<Guid> Prepare(Guid itemId)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> Process(Guid preparationId)
        {
            throw new NotImplementedException();
        }
    }
}
