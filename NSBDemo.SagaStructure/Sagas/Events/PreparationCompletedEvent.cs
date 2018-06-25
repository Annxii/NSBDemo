using System;
using System.Collections.Generic;
using System.Text;

namespace NSBDemo.SagaStructure.Sagas.Events
{
    public class PreparationCompletedEvent
    {
        public Guid ItemId { get; set; }
        public Guid PreparationId { get; set; }
    }
}
