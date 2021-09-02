using AutoFixture;
using DynamoDbNotesApp.Domain;
using DynamoDbNotesApp.Factories;
using FluentAssertions;
using Xunit;

namespace DynamoDbNotesApp.Tests.Factories
{
    public class ResponseFactoryTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public void NoteToResponse_WhenNull_ReturnsNull()
        {
            // create null note
            var nullNote = (Note)null;

            // call method
            var response = nullNote.ToResponse();

            // assert null
            response.Should().BeNull();
        }

        [Fact]
        public void NoteToResponse_WhenValud_ReturnsNoteResponseObject()
        {
            // create mock note
            var mockNote = _fixture.Create<Note>();

            // call method
            var response = mockNote.ToResponse();

            // assert matching values
            response.Should().NotBeNull();
            response.Id.Should().Be(mockNote.Id);
            response.Title.Should().Be(mockNote.Title);
            response.AuthorName.Should().Be(mockNote.AuthorName);
            response.Contents.Should().Be(mockNote.Contents);
            response.Created.Should().Be(mockNote.Created);
            response.Modified.Should().Be(mockNote.Modified);
        }
    }
}
