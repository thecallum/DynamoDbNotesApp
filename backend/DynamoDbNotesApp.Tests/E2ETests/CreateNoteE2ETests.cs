using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using AutoFixture;
using DynamoDbNotesApp.Boundary.Request;
using DynamoDbNotesApp.Boundary.Response;
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
    public class CreateNoteE2ETests : IDisposable
    {
        private readonly Fixture _fixture = new Fixture();

        private readonly IAmazonDynamoDB _client;
        private readonly IDynamoDBContext _context;

        private readonly Random _random = new Random();

        private readonly HttpClient _httpClient;

        private readonly DatabaseFixture<Startup> _testFixture;

        public CreateNoteE2ETests(DatabaseFixture<Startup> testFixture)
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
        public async Task CreateNote_WhenInvalidRequest_Returns400BadRequest()
        {
            // create null CreateNoteRequest
            var invalidRequest = (CreateNoteRequest) null;

            // call controller method
            var response = await CreateNoteRequest(invalidRequest);

            // assert response is 400 bad request
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateNote_WhenValidRequest_ReturnsNoteCreatedResponseObject()
        {
            // create mock CreateNoteRequest
            var mockNoteRequest = _fixture.Create<CreateNoteRequest>();

            // call controller method
            var response = await CreateNoteRequest(mockNoteRequest);

            // assert response is 200
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            // get id of created note
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<NoteCreatedResponseObject>(responseContent);

            var noteId = apiEntity.Id;

            // find note in database
            var databaseResponse = await _context.LoadAsync<NotesDb>(noteId);

            // assert note exists in database
            databaseResponse.Should().NotBeNull();

            databaseResponse.Title.Should().Be(mockNoteRequest.Title);
            databaseResponse.AuthorName.Should().Be(mockNoteRequest.AuthorName);
            databaseResponse.Contents.Should().Be(mockNoteRequest.Contents);
        }

        private async Task<HttpResponseMessage> CreateNoteRequest(CreateNoteRequest request)
        {
            var uri = new Uri($"/api/notes/", UriKind.Relative);

            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, data).ConfigureAwait(false);

            return response;
        }
    }
}
