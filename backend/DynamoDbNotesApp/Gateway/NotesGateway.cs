using Amazon.DynamoDBv2.DataModel;
using DynamoDbNotesApp.Boundary.Response;
using DynamoDbNotesApp.Domain;
using DynamoDbNotesApp.Factories;
using DynamoDbNotesApp.Gateway.Interfaces;
using DynamoDbNotesApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DynamoDbNotesApp.Gateway
{
    public class NotesGateway : INotesGateway
    {
        private readonly IDynamoDBContext _context;

        public NotesGateway(IDynamoDBContext databaseContext)
        {
            _context = databaseContext;
        }

        public async Task<Guid> CreateNote(Note note)
        {
            var noteDb = note.ToDatabase();

            await _context.SaveAsync(noteDb);

            return noteDb.Id;
        }

        public async Task<bool> DeleteNote(Guid id)
        {
            var noteExists = await NoteExists(id);
            if (noteExists == false) return false;

            await _context.DeleteAsync<NotesDb>(id);

            return true;
        }

        public async Task<List<Note>> GetAllNotes()
        {
            var conditions = new List<ScanCondition>();

            var response = await _context.ScanAsync<NotesDb>(conditions).GetRemainingAsync();

            return response.Select(x => x.ToDomain()).ToList();

        }

        public async Task<Note> GetNoteById(Guid id)
        {
            var response = await _context.LoadAsync<NotesDb>(id).ConfigureAwait(false);

            return response?.ToDomain();
        }

        public async Task<bool> UpdateNote(Note note)
        {
            var existingNote = await GetNoteById(note.Id);
            if (existingNote == null) return false; // note doesnt exist

            var newNote = note.ToDatabase();
            newNote.Created = (DateTime)existingNote.Created;
            newNote.Modified = DateTime.UtcNow;

            await _context.SaveAsync(newNote);

            return true;
        }

        private async Task<bool> NoteExists(Guid id)
        {
            var response = await _context.LoadAsync<NotesDb>(id).ConfigureAwait(false);

            return response != null;
        }
    }
}
