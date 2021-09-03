using Amazon.Lambda.Core;
using DynamoDbNotesApp.Boundary.Request;
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
    public class CreateNoteUseCase : ICreateNoteUseCase
    {
        private INotesGateway _notesGateway;

        public CreateNoteUseCase(INotesGateway notesGateway)
        {
            _notesGateway = notesGateway;
        }

        public async Task<NoteCreatedResponseObject> Execute(CreateNoteRequest request)
        {
            LambdaLogger.Log("Calling CreateNoteUseCase");

            var note = request.ToDomain();

            var response = await _notesGateway.CreateNote(note).ConfigureAwait(false);

            return new NoteCreatedResponseObject
            {
                Id = response
            };
        }
    }
}
