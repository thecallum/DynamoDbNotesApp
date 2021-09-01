using AutoFixture;
using DynamoDbNotesApp.Boundary.Request;
using DynamoDbNotesApp.Boundary.Response;
using DynamoDbNotesApp.Domain;
using DynamoDbNotesApp.Factories;
using DynamoDbNotesApp.Gateway.Interfaces;
using DynamoDbNotesApp.UseCase;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DynamoDbNotesApp.Tests.UseCase
{
    public class GetByIdUseCaseTests
    {
        private readonly Mock<INotesGateway> _mockNotesGateway;
        private readonly GetByIdUseCase _getByIdUseCase;

        private readonly Fixture _fixture = new Fixture();

        public GetByIdUseCaseTests()
        {
            _mockNotesGateway = new Mock<INotesGateway>();

            _getByIdUseCase = new GetByIdUseCase(_mockNotesGateway.Object);
        }

        [Fact]
        public async Task GetByIdUseCase_WhenNoteDoesntExist_ReturnsNull()
        {
            // create mock Id
            var mockId = Guid.NewGuid();

            // setup usecase
            _mockNotesGateway.Setup(x => x.GetNoteById(It.IsAny<Guid>())).ReturnsAsync((Note)null);

            // call method
            var response = await _getByIdUseCase.Execute(mockId).ConfigureAwait(false);

            // assert response is false
            response.Should().BeNull();
        }

        [Fact]
        public async Task GetByIdUseCase_WhenNoteExists_ReturnsNoteResponseObject()
        {
            // create mock Id
            var mockId = Guid.NewGuid();

            // create mock response object
            var mockNote = _fixture.Create<Note>();

            // setup usecase
            _mockNotesGateway.Setup(x => x.GetNoteById(It.IsAny<Guid>())).ReturnsAsync(mockNote);

            // call method
            var response = await _getByIdUseCase.Execute(mockId).ConfigureAwait(false);

            // assert response is false
            response.Should().BeOfType(typeof(NoteResponseObject));
        }
    }
}
