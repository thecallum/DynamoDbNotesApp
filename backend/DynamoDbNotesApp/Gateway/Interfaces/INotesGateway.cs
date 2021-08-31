using Amazon.DynamoDBv2.DataModel;
using DynamoDbNotesApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDbNotesApp.Gateway.Interfaces
{
    interface INotesGateway
    {
        Task<Note> GetNoteById(Guid id);
        Task<List<Note>> GetAllNotes();
        Task CreateNote(Note note);
        Task UpdateNote(Note note);
        Task DeleteNote(Guid id);
    }
}
