using DynamoDbNotesApp.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDbNotesApp.UseCase
{
    public class DeleteNoteUseCase : IDeleteNoteUseCase
    {
        public Task Execute(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
