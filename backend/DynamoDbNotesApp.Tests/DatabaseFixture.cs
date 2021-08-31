using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DynamoDbNotesApp.Tests
{
    public class DatabaseFixture : IDisposable
    {
        // https://xunit.net/docs/shared-context#collection-fixture

        public IAmazonDynamoDB DynamoDb { get; private set; }
        public IDynamoDBContext DynamoDbContext { get; private set; }

        public DatabaseFixture()
        {
            // setup database connection
            //Db = new SqlConnection();

            // initialise data in the test database
        }

        public void Dispose()
        {
            // cleanup database

            throw new NotImplementedException();
        }
    }

    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
