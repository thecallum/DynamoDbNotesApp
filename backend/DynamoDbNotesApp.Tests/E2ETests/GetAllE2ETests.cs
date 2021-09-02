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
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace DynamoDbNotesApp.Tests.E2ETests
{
    [Collection("Database collection")]
    public class GetAllE2ETests : IDisposable
    {
        private readonly Fixture _fixture = new Fixture();

        private readonly IAmazonDynamoDB _client;
        private readonly IDynamoDBContext _context;

        private readonly Random _random = new Random();

        private readonly HttpClient _httpClient;

        private readonly DatabaseFixture<Startup> _testFixture;

        public GetAllE2ETests(DatabaseFixture<Startup> testFixture)
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
        public async Task GetAllNotes_WhenNoNotesExist_ReturnsEmptyList()
        {
            // call controller method
            var response = await GetAllNotesRequest();

            // assert response is 200 Ok
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // assert response length is 0
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<List<NoteResponseObject>>(responseContent);

            apiEntity.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllNotes_WhenMultipleNotesExist_ReturnsNoteResponseObjectList()
        {
            // create multiple mock notes
            var numberOfNotes = _random.Next(2, 5);
            var mockNotes = _fixture.CreateMany<NotesDb>(numberOfNotes);

            // insert notes into database
            foreach (var mockNote in mockNotes)
            {
                await SetupTestData(mockNote);
            }

            // call controller method
            var response = await GetAllNotesRequest();

            // assert response is 200 Ok
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // assert response length is same as number inserted
            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apiEntity = JsonConvert.DeserializeObject<List<NoteResponseObject>>(responseContent);

            apiEntity.Count.Should().Be(numberOfNotes);
        }

        private async Task<HttpResponseMessage> GetAllNotesRequest()
        {
            var uri = new Uri($"/api/notes/", UriKind.Relative);
            var response = await _httpClient.GetAsync(uri).ConfigureAwait(false);

            return response;
        }
    }
}
