using System;
using System.Collections.Generic;
using System.Text;

namespace NSBDemo.SagaStructure.Sagas
{
    public enum AwesomeProcessDataStates
    {
        Initializing = 0,
        Preparing,
        Processing,
        Failed,
        Done
    }
}
