using NSBDemo.SagaStructure.Application;
using NSBDemo.SagaStructure.Sagas.Messages;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NSBDemo.SagaStructure.Sagas.Handlers
{
    internal class ExecuteProcessingHandler : IHandleMessages<ExecuteProcessing>
    {
        private readonly IProcessingApplicationService applicationService;

        public ExecuteProcessingHandler(IProcessingApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }

        public async Task Handle(ExecuteProcessing message, IMessageHandlerContext context)
        {
            try
            {
                var resultId = await applicationService.Process(message.PreparationId).ConfigureAwait(false);
                await context.Reply(message.CreateSuccess(resultId)).ConfigureAwait(false);
            }
            catch (ApplicationException ex)
            {
                await context.Reply(message.CreateFailure(ex.Message)).ConfigureAwait(false);
            }
        }
    }
}
