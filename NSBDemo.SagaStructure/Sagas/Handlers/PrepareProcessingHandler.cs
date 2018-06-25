using NSBDemo.SagaStructure.Application;
using NSBDemo.SagaStructure.Sagas.Messages;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NSBDemo.SagaStructure.Sagas.Handlers
{
    internal class PrepareProcessingHandler : IHandleMessages<PrepareProcessing>
    {
        private readonly IProcessingApplicationService applicationService;

        public PrepareProcessingHandler(IProcessingApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }

        public async Task Handle(PrepareProcessing message, IMessageHandlerContext context)
        {
            try
            {
                var preparationId = await applicationService.Prepare(message.ItemId).ConfigureAwait(false);
                await context.Reply(message.CreateSuccess(preparationId)).ConfigureAwait(false);
            }
            catch (ApplicationException ex)
            {
                await context.Reply(message.CreateFailure(ex.Message)).ConfigureAwait(false);
            }
        }
    }
}
