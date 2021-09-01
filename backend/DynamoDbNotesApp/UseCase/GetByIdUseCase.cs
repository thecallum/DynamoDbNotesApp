using DynamoDbNotesApp.Boundary.Response;
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

        public Task<NoteResponseObject> Execute(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
