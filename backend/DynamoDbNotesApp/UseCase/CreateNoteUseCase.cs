using DynamoDbNotesApp.Boundary.Request;
using DynamoDbNotesApp.Boundary.Response;
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

        public Task<NoteCreatedResponseObject> Execute(CreateNoteRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
