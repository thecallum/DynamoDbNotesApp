using DynamoDbNotesApp.Boundary.Request;
using DynamoDbNotesApp.Factories;
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

        public async Task<bool> Execute(Guid id, UpdateNoteRequest request)
        {
            var note = request.ToDomain(id);

            var response = await _notesGateway.UpdateNote(note);

            return response;
        }
    }
}
