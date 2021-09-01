using Amazon.DynamoDBv2.DataModel;
using DynamoDbNotesApp.Boundary.Response;
using DynamoDbNotesApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDbNotesApp.Gateway.Interfaces
{
    public interface INotesGateway
    {
        Task<Note> GetNoteById(Guid id);
        Task<List<Note>> GetAllNotes();
        Task<Guid> CreateNote(Note note);
        Task<bool> UpdateNote(Note note);
        Task<bool> DeleteNote(Guid id);
    }
}
