using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DynamoDbNotesApp.Tests
{
    public class DatabaseFixture<TStartup> : IDisposable where TStartup : class
    {
        // https://xunit.net/docs/shared-context#collection-fixture

        public IAmazonDynamoDB DynamoDb { get; private set; }
        public IDynamoDBContext DynamoDbContext { get; private set; }

        private const string NotesTableName = "Notes";

        public DatabaseFixture()
        {
            // setup database connection
            var clientConfig = new AmazonDynamoDBConfig { ServiceURL = "http://localhost:8000" };
            DynamoDb =  new AmazonDynamoDBClient(clientConfig);

            DynamoDbContext = new DynamoDBContext(DynamoDb);

            // initialise data in the test database
            try
            {
                CreateNotesTableAsync().GetAwaiter().GetResult();
            } catch(Exception)
            {
                // table exists
            }
        }

        public void Dispose()
        {
            // cleanup database
            DynamoDb.DeleteTableAsync(NotesTableName).GetAwaiter().GetResult();

        }

        public async Task ResetDatabase()
        {
            await DynamoDb.DeleteTableAsync(NotesTableName);
            await CreateNotesTableAsync();
        }

        private async Task CreateNotesTableAsync()
        {
            var request = new CreateTableRequest
            {
                TableName = NotesTableName,
                AttributeDefinitions = new List<AttributeDefinition>()
                    {
                        new AttributeDefinition
                        {
                            AttributeName = "id",
                            AttributeType = "S"
                        }
                    },
                KeySchema = new List<KeySchemaElement>()
                  {
                    new KeySchemaElement
                    {
                      AttributeName = "id",
                      KeyType = "HASH"  //Partition key
                    }
                  },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 2,
                    WriteCapacityUnits = 2
                }
            };

            await DynamoDb.CreateTableAsync(request).ConfigureAwait(false);

        }
    }

    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture<Startup>>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
