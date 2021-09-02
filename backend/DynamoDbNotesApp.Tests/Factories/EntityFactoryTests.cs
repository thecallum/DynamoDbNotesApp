using AutoFixture;
using DynamoDbNotesApp.Boundary.Request;
using DynamoDbNotesApp.Boundary.Response;
using DynamoDbNotesApp.Controllers;
using DynamoDbNotesApp.Domain;
using DynamoDbNotesApp.Factories;
using DynamoDbNotesApp.Infrastructure;
using DynamoDbNotesApp.UseCase.Interfaces;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DynamoDbNotesApp.Tests.Factories
{
    public class EntityFactoryTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void NotesDbToDomain_WhenCalled_ReturnsNote()
        {
            // create mock notesDb
            var mockNotesDb = _fixture.Create<NotesDb>();

            // call method
            var response = mockNotesDb.ToDomain();

            // assert values match
            response.Id.Should().Be(mockNotesDb.Id);
            response.Title.Should().Be(mockNotesDb.Title);
            response.AuthorName.Should().Be(mockNotesDb.AuthorName);
            response.Contents.Should().Be(mockNotesDb.Contents);
            response.Created.Should().Be(mockNotesDb.Created);
            response.Modified.Should().Be(mockNotesDb.Modified);
        }

        [Fact]
        public void CreateNoteRequestToDomain_WhenNull_ReturnsNull()
        {
            // create null createnoterequest
            var nullRequest = (CreateNoteRequest)null;

            // call method
            var response = nullRequest.ToDomain();

            // assert null
            response.Should().BeNull();
        }

        [Fact]
        public void CreateNoteRequestToDomain_WhenCalled_ReturnsNote()
        {
            // create mock createnoterequest
            var mockCreateNoteRequest = _fixture.Create<CreateNoteRequest>();

            // call method
            var response = mockCreateNoteRequest.ToDomain();

            // assert values match
            response.Should().NotBeNull();
            response.Title.Should().Be(mockCreateNoteRequest.Title);
            response.AuthorName.Should().Be(mockCreateNoteRequest.AuthorName);
            response.Contents.Should().Be(mockCreateNoteRequest.Contents);
        }

        [Fact]
        public void UpdateNoteRequestToDomain_WhenNull_ReturnsNull()
        {
            // create null updatenoterequest
            var nullRequest = (UpdateNoteRequest)null;

            // create random id
            var randomId = Guid.NewGuid();

            // call method
            var response = nullRequest.ToDomain(randomId);

            // assert null
            response.Should().BeNull();
        }

        [Fact]
        public void UpdateNoteRequestToDomain_WhenCalled_ReturnsNote()
        {
            // create mock udpdatenoterequest
            var mockRequest = _fixture.Create<UpdateNoteRequest>();

            // create random id
            var randomId = Guid.NewGuid();

            // call method
            var response = mockRequest.ToDomain(randomId);

            // assert values match
            response.Should().NotBeNull();
            response.Id.Should().Be(randomId);
            response.Title.Should().Be(mockRequest.Title);
            response.AuthorName.Should().Be(mockRequest.AuthorName);
            response.Contents.Should().Be(mockRequest.Contents);
        }

        [Fact]
        public void NoteToDatabase_WhenCalled_ReturnsNote()
        {
            // create mock note
            var mockNote = _fixture.Create<Note>();

            // call method
            var response = mockNote.ToDatabase();

            // asset values match
            response.Should().NotBeNull();
            response.Id.Should().Be(mockNote.Id);
            response.Title.Should().Be(mockNote.Title);
            response.AuthorName.Should().Be(mockNote.AuthorName);
            response.Contents.Should().Be(mockNote.Contents);
        }

        [Fact]
        public void NoteToDatabase_WhenCreatedAndModifiedDatesAreNull_SetsNewDate()
        {
            // create mock note with null created and modified dates
            var mockNote = _fixture.Build<Note>()
                .With(x => x.Created, (DateTime?)null)
                .With(x => x.Modified, (DateTime?)null)
                .Create();

            // call method
            var response = mockNote.ToDatabase();

            // assert created and modified dates are recent
            response.Created.Should().BeCloseTo(DateTime.UtcNow, 5.Seconds());
            response.Modified.Should().BeCloseTo(DateTime.UtcNow, 5.Seconds());
        }

        [Fact]
        public void NoteToDatabase_WhenCreatedAndModifiedDatesAreSet_KeepsDates()
        {
            // create mock date with created and modified dates
            var mockNote = _fixture.Create<Note>();

            // call method
            var response = mockNote.ToDatabase();

            // assert created and modified dates havent changed
            response.Created.Should().Be(mockNote.Created);
            response.Modified.Should().Be(mockNote.Modified);
        }
    }
}
