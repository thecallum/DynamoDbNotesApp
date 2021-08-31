using DynamoDbNotesApp.Boundary.Request;
using DynamoDbNotesApp.Boundary.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDbNotesApp.UseCase.Interfaces
{
    public interface ICreateNoteUseCase
    {
        Task<NoteCreatedResponseObject> Execute(CreateNoteRequest request);
    }
}
