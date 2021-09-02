using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using AutoFixture;
using DynamoDbNotesApp.Domain;
using DynamoDbNotesApp.Factories;
using DynamoDbNotesApp.Infrastructure;
using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace DynamoDbNotesApp.Tests.E2ETests
{
    [Collection("Database collection")]
    public class DeleteNoteE2ETests : IDisposable
    {
        private readonly Fixture _fixture = new Fixture();

        private readonly IAmazonDynamoDB _client;
        private readonly IDynamoDBContext _context;

        private readonly Random _random = new Random();

        private readonly HttpClient _httpClient;

        private readonly DatabaseFixture<Startup> _testFixture;

        public DeleteNoteE2ETests(DatabaseFixture<Startup> testFixture)
        {
            _client = testFixture.DynamoDb;
            _context = testFixture.DynamoDbContext;

            _testFixture = testFixture;

            _httpClient = testFixture.CreateClient();
        }

        private async Task SetupTestData(NotesDb note)
        {
            await _context.SaveAsync(note).ConfigureAwait(false);
        }

        public void Dispose()
        {
            _testFixture.ResetDatabase().GetAwaiter().GetResult();
        }

        [Fact]
        public async Task DeleteNote_WhenInvalidRequest_Returns400BadRequest()
        {
            // create invalid id
            var invalidId = "9uiasjdd9uasjd9uasjd";

            // call controller method
            var response = await DeleteNoteRequest(invalidId);

            // assert response is bad request
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteNote_WhenItDoesntExist_Returns404NotFound()
        {
            // create random id
            var randomId = Guid.NewGuid();

            // call controller method
            var response = await DeleteNoteRequest(randomId.ToString());

            // assert response is not found
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task DeleteNote_WhenItExists_Returns200OKResponse()
        {
            // create mock note
            var mockNote = _fixture.Create<NotesDb>();

            // insert into database
            await SetupTestData(mockNote);

            // call controller method
            var response = await DeleteNoteRequest(mockNote.Id.ToString());

            // assert response is 200
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // assert note has been deleted from database
            var databaseResponse = await _context.LoadAsync<NotesDb>(mockNote.Id);

            databaseResponse.Should().BeNull();
        }

        private async Task<HttpResponseMessage> DeleteNoteRequest(string id)
        {
            var uri = new Uri($"/api/notes/{id}", UriKind.Relative);
            var response = await _httpClient.DeleteAsync(uri).ConfigureAwait(false);

            return response;
        }
    }
}
