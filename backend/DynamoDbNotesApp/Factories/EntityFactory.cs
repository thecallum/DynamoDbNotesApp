using DynamoDbNotesApp.Boundary.Request;
using DynamoDbNotesApp.Domain;
using DynamoDbNotesApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDbNotesApp.Factories
{
    public static class EntityFactory
    {
        public static Note ToDomain(this NotesDb databaseEntity)
        {
            return new Note
            {
                Id = databaseEntity.Id,
                Title = databaseEntity.Title,
                AuthorName = databaseEntity.AuthorName,
                Contents = databaseEntity.Contents,
                Created = databaseEntity.Created,
                Modified = databaseEntity.Modified
            };
        }

        public static Note ToDomain(this CreateNoteRequest request)
        {
            return new Note
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                AuthorName = request.AuthorName,
                Contents = request.Contents,
            };
        }

        public static Note ToDomain(this UpdateNoteRequest request, Guid id)
        {
            return new Note
            {
                Id = id,
                Title = request.Title,
                AuthorName = request.AuthorName,
                Contents = request.Contents,
            };
        }

        public static NotesDb ToDatabase(this Note entity)
        {
            return new NotesDb
            {
                Id = entity.Id,
                Title = entity.Title,
                AuthorName = entity.AuthorName,
                Contents = entity.Contents,
                Created = (entity.Created != null) ? (DateTime)entity.Created : DateTime.UtcNow,
                Modified = (entity.Modified != null) ? (DateTime)entity.Modified : DateTime.UtcNow
            };
        }
    }
}
