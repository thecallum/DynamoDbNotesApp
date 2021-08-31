using DynamoDbNotesApp.Boundary.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDbNotesApp.UseCase.Interfaces
{
    public interface IGetAllUseCase
    {
        Task<List<NoteResponseObject>> Execute();
    }
}
