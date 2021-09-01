using DynamoDbNotesApp.Boundary.Response;
using DynamoDbNotesApp.Gateway.Interfaces;
using DynamoDbNotesApp.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDbNotesApp.UseCase
{
    public class GetAllUseCase : IGetAllUseCase
    {
        private INotesGateway _notesGateway;

        public GetAllUseCase(INotesGateway notesGateway)
        {
            _notesGateway = notesGateway;
        }

        public Task<List<NoteResponseObject>> Execute()
        {
            throw new NotImplementedException();
        }
    }
}
