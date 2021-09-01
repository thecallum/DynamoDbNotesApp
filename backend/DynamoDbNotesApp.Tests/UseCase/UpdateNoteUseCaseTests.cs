using AutoFixture;
using DynamoDbNotesApp.Boundary.Request;
using DynamoDbNotesApp.Domain;
using DynamoDbNotesApp.Gateway;
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
    public class UpdateNoteUseCaseTests
    {
        private readonly Mock<INotesGateway> _mockNotesGateway;
        private readonly UpdateNoteUseCase _updateNoteUseCase;

        private readonly Fixture _fixture = new Fixture();

        public UpdateNoteUseCaseTests()
        {
            _mockNotesGateway = new Mock<INotesGateway>();

            _updateNoteUseCase = new UpdateNoteUseCase(_mockNotesGateway.Object);
        }

        [Fact]
        public async Task UpdateNoteUseCase_WhenNoteDoesntExist_ReturnsFalse()
        {
            // create mock UpdateNoteRequest
            var mockRequest = _fixture.Create<UpdateNoteRequest>();

            // create mock Id
            var mockId = Guid.NewGuid();

            // setup usecase
            _mockNotesGateway.Setup(x => x.UpdateNote(It.IsAny<Note>())).ReturnsAsync(false);

            // call method
            var response = await _updateNoteUseCase.Execute(mockId, mockRequest).ConfigureAwait(false);

            // assert response is false
            response.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateNoteUseCase_WhenNoteExists_ReturnsTrue()
        {
            // create mock UpdateNoteRequest
            var mockRequest = _fixture.Create<UpdateNoteRequest>();

            // create mock Id
            var mockId = Guid.NewGuid();

            // setup usecase
            _mockNotesGateway.Setup(x => x.UpdateNote(It.IsAny<Note>())).ReturnsAsync(false);

            // call method
            var response = await _updateNoteUseCase.Execute(mockId, mockRequest).ConfigureAwait(true);

            // assert response is true
            response.Should().BeTrue();
        }
    }
}
