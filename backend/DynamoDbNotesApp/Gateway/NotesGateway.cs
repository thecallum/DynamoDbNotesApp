using DynamoDbNotesApp.Domain;
using DynamoDbNotesApp.Gateway.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDbNotesApp.Gateway
{
    public class NotesGateway : INotesGateway
    {
        public Task CreateNote(Note note)
        {
            throw new NotImplementedException();
        }

        public Task DeleteNote(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Note>> GetAllNotes()
        {
            throw new NotImplementedException();
        }

        public Task<Note> GetNoteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateNote(Note note)
        {
            throw new NotImplementedException();
        }
    }
}
