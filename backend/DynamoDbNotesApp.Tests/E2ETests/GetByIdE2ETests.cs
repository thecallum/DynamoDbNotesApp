using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using AutoFixture;
using DynamoDbNotesApp.Boundary.Response;
using DynamoDbNotesApp.Domain;
using DynamoDbNotesApp.Factories;
using DynamoDbNotesApp.Infrastructure;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace DynamoDbNotesApp.Tests.E2ETests
{
    [Collection("Database collection")]
    public class GetByIdE2ETests : IDisposable
    {
        private readonly Fixture _fixture = new Fixture();

        private readonly IAmazonDynamoDB _client;
        private readonly IDynamoDBContext _context;

        private readonly Random _random = new Random();

        private readonly HttpClient _httpClient;

        private readonly DatabaseFixture<Startup> _testFixture;

        public GetByIdE2ETests(DatabaseFixture<Startup> testFixture)
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
        public async Task GetById_WhenInvalidRequest_Returns400BadRequest()
        {
            // create invalid id
            var invalidId = "9uiasjdd9uasjd9uasjd";

            // call controller method
            var response = await GetNoteByIdRequest(invalidId);

            // assert response is 400 bad request
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GetById_WhenNoteDoesntExist_Returns404NotFound()
        {
            // create random id
            var randomId = Guid.NewGuid();

            // call controller method
            var response = await GetNoteByIdRequest(randomId.ToString());

            // assert response is 404 not found
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetById_WhenNoteExists_ReturnsNoteResponseObject()
        {
            // create mock note
            var mockNote = _fixture.Create<NotesDb>();

            // insert note into database
            await SetupTestData(mockNote);

            // call controller method
            var response = await GetNoteByIdRequest(mockNote.Id.ToString());

            // assert response is 200
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // assert resonse values match mock note
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<NoteResponseObject>(responseContent);

            apiEntity.Id.Should().Be(mockNote.Id);
            apiEntity.Title.Should().Be(mockNote.Title);
            apiEntity.AuthorName.Should().Be(mockNote.AuthorName);
            apiEntity.Contents.Should().Be(mockNote.Contents);
        }

        private async Task<HttpResponseMessage> GetNoteByIdRequest(string id)
        {
            var uri = new Uri($"/api/notes/{id}", UriKind.Relative);
            var response = await _httpClient.GetAsync(uri).ConfigureAwait(false);

            return response;
        }
    }
}
