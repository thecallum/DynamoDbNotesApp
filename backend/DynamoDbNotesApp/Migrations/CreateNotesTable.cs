using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDbNotesApp.Migrations
{
    public class CreateNotesTable
    {
        private async Task CreateNotesTableAsync()
        {

            var clientConfig = new AmazonDynamoDBConfig { ServiceURL = "http://localhost:8000" };
            var client = new AmazonDynamoDBClient(clientConfig);

            string tableName = "Notes";

            var request = new CreateTableRequest
            {
                TableName = tableName,
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

            await client.CreateTableAsync(request).ConfigureAwait(false);

        }
    }
}
