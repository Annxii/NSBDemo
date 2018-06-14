using System;
using System.Collections.Generic;
using System.Text;

namespace NSBDemo.SagaStructure.Sagas.Messages
{
    internal class PrepareProcessing
    {
        public class Response
        {
            public Guid PreparationId { get; set; }
            public bool Success { get; set; }
            public string Message { get; set; }
        }

        public Guid ItemId { get; set; }

        public Response CreateSuccess(Guid preparationId) => new Response { Success = true, PreparationId = preparationId };
        public Response CreateFailure(string message) => new Response { Message = message };
    }
}
