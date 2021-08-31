using DynamoDbNotesApp.Boundary.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDbNotesApp.UseCase.Interfaces
{
    public interface IUpdateNoteUseCase
    {
        Task Execute(Guid id, UpdateNoteRequest request);
    }
}
