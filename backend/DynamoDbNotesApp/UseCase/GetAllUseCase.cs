using DynamoDbNotesApp.Boundary.Response;
using DynamoDbNotesApp.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDbNotesApp.UseCase
{
    public class GetAllUseCase : IGetAllUseCase
    {
        public Task<List<NoteResponseObject>> Execute()
        {
            throw new NotImplementedException();
        }
    }
}
