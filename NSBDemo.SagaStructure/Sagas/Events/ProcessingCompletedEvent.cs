using System;
using System.Collections.Generic;
using System.Text;

namespace NSBDemo.SagaStructure.Sagas.Events
{
    public class ProcessingCompletedEvent
    {
        public Guid ItemId { get; set; }
        public Guid ResultId { get; set; }
    }
}
