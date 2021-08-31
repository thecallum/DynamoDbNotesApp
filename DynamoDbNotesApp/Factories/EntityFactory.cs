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
            throw new NotImplementedException();
        }

        public static NotesDb ToDatabase(this Note entity)
        {
            throw new NotImplementedException();
        }
    }
}
