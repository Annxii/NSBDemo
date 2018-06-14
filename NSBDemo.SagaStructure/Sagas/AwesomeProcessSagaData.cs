using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using NSBDemo.SagaStructure.Sagas.Commands;
using NSBDemo.SagaStructure.Sagas.Events;
using NSBDemo.SagaStructure.Sagas.Messages;
using NServiceBus;
using System.Runtime.CompilerServices;

namespace NSBDemo.SagaStructure.Sagas
{
    internal class AwesomeProcessSagaData : ContainSagaData
    {
        public Guid ItemId { get; set; }
        public Guid PreparationId { get; set; }
        public string Message { get; set; }

        public AwesomeProcessDataStates State { get; set; }

        public void Initialize(StartAwesomeProcessCommand command)
        {
            VerifyDataChange(AwesomeProcessDataStates.Initializing);

            ItemId = command.ItemId;
            State = AwesomeProcessDataStates.Preparing;
        }

        public void ApplyPreparationResponse(PrepareProcessing.Response response)
        {
            VerifyDataChange(AwesomeProcessDataStates.Preparing);

            PreparationId = response.PreparationId;
            if(response.Success)
            {
                State = AwesomeProcessDataStates.Processing;
            }
            else
            {
                State = AwesomeProcessDataStates.Failed;
                Message = response.Message;
            }
        }

        public void ApplyExecutionResponse(ExecuteProcessing.Response response)
        {
            VerifyDataChange(AwesomeProcessDataStates.Processing);

            if (response.Success)
            {
                State = AwesomeProcessDataStates.Done;
            }
            else
            {
                State = AwesomeProcessDataStates.Failed;
                Message = response.Message;
            }
        }

        public PrepareProcessing CreatePreparation() => new PrepareProcessing
        {
            ItemId = ItemId
        };

        public ExecuteProcessing CreateExecution() => new ExecuteProcessing
        {
            ItemId = ItemId,
            PreparationId = PreparationId
        };

        public ProcessingCompletedEvent CreateCompletionEvent() => new ProcessingCompletedEvent
        {
            ItemId = ItemId,
            Success = State == AwesomeProcessDataStates.Done,
            Message = Message
        };

        private void VerifyDataChange(AwesomeProcessDataStates allowedState, [CallerMemberName]string callerName = null)
        {
            if (allowedState != State)
                throw new ApplicationException($"You're not allowed to call {callerName} when in the state of {State.ToString()}");
        }
    }
}
