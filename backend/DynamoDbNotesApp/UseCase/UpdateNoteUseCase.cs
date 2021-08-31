using DynamoDbNotesApp.Boundary.Request;
using DynamoDbNotesApp.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDbNotesApp.UseCase
{
    public class UpdateNoteUseCase : IUpdateNoteUseCase
    {
        public Task<bool> Execute(Guid id, UpdateNoteRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
