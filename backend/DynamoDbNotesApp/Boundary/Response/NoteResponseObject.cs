using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDbNotesApp.Boundary.Response
{
    public class NoteResponseObject
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string Contents { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
