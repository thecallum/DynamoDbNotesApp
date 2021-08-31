using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDbNotesApp.UseCase.Interfaces
{
    public interface IDeleteNoteUseCase
    {
        Task Execute(Guid id);
    }
}
