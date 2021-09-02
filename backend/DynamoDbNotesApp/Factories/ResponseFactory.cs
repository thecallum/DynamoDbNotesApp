using DynamoDbNotesApp.Boundary.Response;
using DynamoDbNotesApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDbNotesApp.Factories
{
    public static class ResponseFactory
    {
        public static NoteResponseObject ToResponse(this Note domain)
        {
            if (domain == null) return null;

            return new NoteResponseObject
            {
                Id = domain.Id,
                Title = domain.Title,
                AuthorName = domain.AuthorName,
                Contents = domain.Contents,
                Created = (DateTime)domain.Created,
                Modified = (DateTime)domain.Modified
            };
        }
    }
}
