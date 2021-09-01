using DynamoDbNotesApp.Boundary.Response;
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
        public Task<Guid> CreateNote(Note note)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteNote(Guid id)
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

        public Task<bool> UpdateNote(Note note)
        {
            throw new NotImplementedException();
        }
    }
}
