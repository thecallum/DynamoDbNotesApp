using DynamoDbNotesApp.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDbNotesApp.UseCase
{
    public class DeleteNoteUseCase : IDeleteNoteUseCase
    {
        Task<bool> IDeleteNoteUseCase.Execute(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
