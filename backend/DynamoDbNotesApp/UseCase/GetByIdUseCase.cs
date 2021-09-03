using Amazon.Lambda.Core;
using DynamoDbNotesApp.Boundary.Response;
using DynamoDbNotesApp.Factories;
using DynamoDbNotesApp.Gateway.Interfaces;
using DynamoDbNotesApp.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDbNotesApp.UseCase
{
    public class GetByIdUseCase : IGetByIdUseCase
    {
        private INotesGateway _notesGateway;

        public GetByIdUseCase(INotesGateway notesGateway)
        {
            _notesGateway = notesGateway;
        }

        public async Task<NoteResponseObject> Execute(Guid id)
        {
            LambdaLogger.Log("Calling GetByIdUseCase");

            var response = await _notesGateway.GetNoteById(id).ConfigureAwait(false);

            return response?.ToResponse();
        }
    }
}
