using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDbNotesApp.Infrastructure
{
    [DynamoDBTable("Notes", LowerCamelCaseProperties = true)]
    public class NotesDb
    {
        [DynamoDBHashKey]
        public Guid Id { get; set; }
        [DynamoDBProperty]
        public string Title { get; set; }
        [DynamoDBProperty]
        public string AuthorName { get; set; }
        [DynamoDBProperty]
        public string Contents { get; set; }
        [DynamoDBProperty]
        public DateTime Created { get; set; }
        [DynamoDBProperty]
        public DateTime Modified { get; set; }
    }
}
