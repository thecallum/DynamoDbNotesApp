using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using AutoFixture;
using DynamoDbNotesApp.Boundary.Request;
using DynamoDbNotesApp.Boundary.Response;
using DynamoDbNotesApp.Controllers;
using DynamoDbNotesApp.Domain;
using DynamoDbNotesApp.Factories;
using DynamoDbNotesApp.Gateway;
using DynamoDbNotesApp.Infrastructure;
using DynamoDbNotesApp.UseCase.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DynamoDbNotesApp.Tests.Gateway
{
    [Collection("Database collection")]
    public class NotesGatewayTests : IDisposable
    {
        private readonly Fixture _fixture = new Fixture();

        private readonly IAmazonDynamoDB _client;
        private readonly IDynamoDBContext _context;

        private readonly NotesGateway _gateway;
        private readonly Random _random = new Random();

        private readonly DatabaseFixture<Startup> _testFixture;

        public NotesGatewayTests(DatabaseFixture<Startup> testFixture)
        {
            _client = testFixture.DynamoDb;
            _context = testFixture.DynamoDbContext;

            _gateway = new NotesGateway(_context);

            _testFixture = testFixture;
        }

        [Fact]
        public async Task CreateNote_WhenCalled_InsertsNoteIntoDatabase()
        {
            // create mock note
            var mockNote = _fixture.Create<Note>();

            // call method
            var response = await _gateway.CreateNote(mockNote);

            // assert response is a guid
            response.Should().Be(mockNote.Id);

            // assert note is inserted into dateabase
            var databaseResponse = await _context.LoadAsync<NotesDb>(mockNote.Id);

            databaseResponse.Id.Should().Be(mockNote.Id);
        }

        [Fact]
        public async Task DeleteNote_WhenNoteDoesntExist_ReturnsFalse()
        {
            // create random id
            var randomId = Guid.NewGuid();

            // call method
            var response = await _gateway.DeleteNote(randomId);

            // assert response is false
            response.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteNote_WhenNoteExists_DeletesNote()
        {
            // create mock note
            var mockNote = _fixture.Create<NotesDb>();

            // insert mock note into database
            await SetupTestData(mockNote);

            // call methood
            var response = await _gateway.DeleteNote(mockNote.Id);

            // assert response is true
            response.Should().BeTrue();

            // assert note is deleted from database
            var databaseResponse = await _context.LoadAsync<NotesDb>(mockNote.Id);
            databaseResponse.Should().BeNull();
        }

        [Fact]
        public async Task GetNoteById_WhenNoteDoesntExist_ReturnsNull()
        {
            // create random id
            var randomId = Guid.NewGuid();

            // call method
            var response = await _gateway.GetNoteById(randomId);

            // assert response is null
            response.Should().BeNull();
        }

        [Fact]
        public async Task GetNoteById_WhenNoteExists_ReturnsNote()
        {
            // create mock note
            var mockNote = _fixture.Create<NotesDb>();

            // insert mock note into database
            await SetupTestData(mockNote);

            // call method
            var response = await _gateway.GetNoteById(mockNote.Id);

            // assert response is inserted note
            response.Should().BeOfType(typeof(Note));
            response.Id.Should().Be(mockNote.Id);
        }

        [Fact]
        public async Task GetAllNotes_WhenNoNotesExist_ReturnsEmptyList()
        {
            // call method
            var response = await _gateway.GetAllNotes();

            // assert response is empty list
            response.Count().Should().Be(0);
            response.Should().BeOfType(typeof(List<Note>));
        }

        [Fact]
        public async Task GetAllNotes_WhenMultipleNotesExist_ReturnsMultipleNotes()
        {
            // create multiple mock notes
            var numberOfNotes = _random.Next(2, 5);
            var mockNotes = _fixture.CreateMany<NotesDb>(numberOfNotes).ToList();

            // insert mock notes into database
            for (int i=0; i<numberOfNotes; i++)
            {
                await SetupTestData(mockNotes[i]);
            }

            // call method
            var response = await _gateway.GetAllNotes();

            // assert response length
            response.Count().Should().Be(numberOfNotes);
            response.Should().BeOfType(typeof(List<Note>));
        }

        [Fact]
        public async Task UpdateNote_WhenNoteDoesntExist_ReturnsFalse()
        {
            // create mock note
            var mockNote = _fixture.Create<Note>();

            // call method
            var response = await _gateway.UpdateNote(mockNote);

            // assert response is false
            response.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateNote_WhenNoteExists_ReturnsTrue()
        {
            // create mock note
            var mockNote = _fixture.Create<Note>();

            // insert note into database
            await SetupTestData(mockNote.ToDatabase());

            // modify mock note
            var modifiedNote = _fixture.Build<Note>().With(x => x.Id, mockNote.Id).Create();

            // call method
            var response = await _gateway.UpdateNote(modifiedNote);

            // assert response is true
            response.Should().BeTrue();

            // assert note in database has updated
            var databaseResponse = await _context.LoadAsync<NotesDb>(mockNote.Id);
            databaseResponse.Title.Should().Be(modifiedNote.Title);
            databaseResponse.Contents.Should().Be(modifiedNote.Contents);
            databaseResponse.AuthorName.Should().Be(modifiedNote.AuthorName);
        }

        private async Task SetupTestData(NotesDb note)
        {
            await _context.SaveAsync(note).ConfigureAwait(false);
        }

        public void Dispose()
        {
            _testFixture.ResetDatabase().GetAwaiter().GetResult();
        }
    }
}
