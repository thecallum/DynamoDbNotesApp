using DynamoDbNotesApp.Boundary.Request;
using DynamoDbNotesApp.Gateway.Interfaces;
using DynamoDbNotesApp.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDbNotesApp.UseCase
{
    public class UpdateNoteUseCase : IUpdateNoteUseCase
    {
        private INotesGateway _notesGateway;

        public UpdateNoteUseCase(INotesGateway notesGateway)
        {
            _notesGateway = notesGateway;
        }

        public Task<bool> Execute(Guid id, UpdateNoteRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
