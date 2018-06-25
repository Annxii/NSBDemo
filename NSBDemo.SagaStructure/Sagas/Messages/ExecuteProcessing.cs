using System;
using System.Collections.Generic;
using System.Text;

namespace NSBDemo.SagaStructure.Sagas.Messages
{
    internal class ExecuteProcessing
    {
        public class Response
        {
            public Guid ResultId { get; set; }
            public bool Success { get; set; }
            public string Message { get; set; }
        }

        public Guid PreparationId { get; set; }

        public Response CreateSuccess(Guid resultId) => new Response { ResultId = resultId, Success = true };

        public Response CreateFailure(string message) => new Response { Message = message };
    }
}
