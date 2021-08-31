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
            throw new NotImplementedException();
        }
    }
}
