using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDbNotesApp.Boundary.Request
{
    public class CreateNoteRequest
    {
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string Contents { get; set; }
    }
}
