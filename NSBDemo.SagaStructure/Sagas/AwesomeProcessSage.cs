using NSBDemo.SagaStructure.Sagas.Commands;
using NSBDemo.SagaStructure.Sagas.Messages;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NSBDemo.SagaStructure.Sagas
{
    internal class AwesomeProcessSage : Saga<AwesomeProcessSagaData>,
        IAmStartedByMessages<StartAwesomeProcessCommand>,
        IHandleMessages<PrepareProcessing.Response>,
        IHandleMessages<ExecuteProcessing.Response>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<AwesomeProcessSagaData> mapper)
        {
            mapper.ConfigureMapping<StartAwesomeProcessCommand>(x => x.ItemId).ToSaga(x => x.ItemId);
        }

        // STEP 1
        public async Task Handle(StartAwesomeProcessCommand message, IMessageHandlerContext context)
        {
            Data.Initialize(message);
            switch (Data.State)
            {
                case AwesomeProcessDataStates.Preparing:
                    await context.SendLocal(Data.CreatePreparation()).ConfigureAwait(false);
                    break;
            }

            await TryFinalizeSaga().ConfigureAwait(false);
        }

        // STEP 2
        public async Task Handle(PrepareProcessing.Response message, IMessageHandlerContext context)
        {
            Data.ApplyPreparationResponse(message);
            switch (Data.State)
            {
                case AwesomeProcessDataStates.Processing:
                    // Event notification?
                    await context.Publish(Data.CreatePreparationEvent()).ConfigureAwait(false);
                    await context.SendLocal(Data.CreateExecution()).ConfigureAwait(false);
                    break;
            }

            await TryFinalizeSaga().ConfigureAwait(false);
        }

        // STEP 3
        public async Task Handle(ExecuteProcessing.Response message, IMessageHandlerContext context)
        {
            Data.ApplyExecutionResponse(message);
            switch (Data.State)
            {
                case AwesomeProcessDataStates.Failed:
                    // DO SOMETHING!
                    break;
                case AwesomeProcessDataStates.Done:
                    // Event notification?
                    await context.Publish(Data.CreateProcessingEvent()).ConfigureAwait(false);
                    break;
            }

            await TryFinalizeSaga().ConfigureAwait(false);
        }

        private Task TryFinalizeSaga()
        {
            switch (Data.State)
            {
                case AwesomeProcessDataStates.Failed:
                case AwesomeProcessDataStates.Done:
                    MarkAsComplete();
                    break;
                default:
                    break;
            }

            return Task.CompletedTask;
        }
    }
}
