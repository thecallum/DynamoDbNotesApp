using DynamoDbNotesApp.Boundary.Request;
using DynamoDbNotesApp.Boundary.Response;
using DynamoDbNotesApp.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDbNotesApp.UseCase
{
    public class CreateNoteUseCase : ICreateNoteUseCase
    {
        Task<NoteCreatedResponseObject> ICreateNoteUseCase.Execute(CreateNoteRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
