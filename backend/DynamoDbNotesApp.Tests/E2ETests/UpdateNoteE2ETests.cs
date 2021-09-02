using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using AutoFixture;
using DynamoDbNotesApp.Boundary.Request;
using DynamoDbNotesApp.Domain;
using DynamoDbNotesApp.Factories;
using DynamoDbNotesApp.Infrastructure;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DynamoDbNotesApp.Tests.E2ETests
{
    [Collection("Database collection")]
    public class UpdateNoteE2ETests : IDisposable
    {
        private readonly Fixture _fixture = new Fixture();

        private readonly IAmazonDynamoDB _client;
        private readonly IDynamoDBContext _context;

        private readonly Random _random = new Random();

        private readonly HttpClient _httpClient;

        private readonly DatabaseFixture<Startup> _testFixture;

        public UpdateNoteE2ETests(DatabaseFixture<Startup> testFixture)
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
        public async Task UpdateNote_WhenInvalidId_Returns400BadRequest()
        {
            // create invalid id
            var invalidId = "9uiasjdd9uasjd9uasjd";

            // create mock updateNoteRequest
            var mockUpdateNoteRequest = _fixture.Create<UpdateNoteRequest>();

            // call controller method
            var response = await UpdateNoteRequest(invalidId, mockUpdateNoteRequest);

            // assert response is 400 bad request
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateNote_WhenInvalidRequest_Returns400BadRequest()
        {
            // create random id
            var randomId = Guid.NewGuid();

            // create null UpdateNoteRequest
            var invalidUpdateNoteRequest = (UpdateNoteRequest)null;

            // call controller method
            var response = await UpdateNoteRequest(randomId.ToString(), invalidUpdateNoteRequest);

            // assert respose is 400 bad request
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateNote_WhenNoteDoesntExist_Returns404NotFound()
        {
            // create random id
            var randomId = Guid.NewGuid();

            // create mock UpdateNoteRequest
            var mockUpdateNoteRequest = _fixture.Create<UpdateNoteRequest>();

            // call controller method
            var response = await UpdateNoteRequest(randomId.ToString(), mockUpdateNoteRequest);

            // assert respose is 404 not found
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateNote_WhenValidRequest_Returns200OkResponse()
        {
            // create mock note
            var mockNote = _fixture.Create<NotesDb>();

            // insert into database
            await SetupTestData(mockNote);

            // update mock note with changes
            var updatedNoteRequest = _fixture.Create<UpdateNoteRequest>();

            // call controller method
            var response = await UpdateNoteRequest(mockNote.Id.ToString(), updatedNoteRequest);

            // assert response is 200
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // get mock note from database
            var databaseResponse = await _context.LoadAsync<NotesDb>(mockNote.Id);

            // check values match updated note
            databaseResponse.Title.Should().Be(updatedNoteRequest.Title);
            databaseResponse.AuthorName.Should().Be(updatedNoteRequest.AuthorName);
            databaseResponse.Contents.Should().Be(updatedNoteRequest.Contents);
        }

        private async Task<HttpResponseMessage> UpdateNoteRequest(string id, UpdateNoteRequest request)
        {
            var uri = new Uri($"/api/notes/{id}", UriKind.Relative);

            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PatchAsync(uri, data).ConfigureAwait(false);

            return response;
        }
    }
}
