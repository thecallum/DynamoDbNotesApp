using DynamoDbNotesApp.Gateway.Interfaces;
using DynamoDbNotesApp.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDbNotesApp.UseCase
{
    public class DeleteNoteUseCase : IDeleteNoteUseCase
    {
        private INotesGateway _notesGateway;

        public DeleteNoteUseCase(INotesGateway notesGateway)
        {
            _notesGateway = notesGateway;
        }

        public async Task<bool> Execute(Guid id)
        {
            var response = await _notesGateway.DeleteNote(id).ConfigureAwait(false);

            return response;
        }
    }
}
