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
    public class DeleteNoteUseCaseTests
    {
        private readonly Mock<INotesGateway> _mockNotesGateway;
        private readonly DeleteNoteUseCase _deleteNoteUseCase;

        private readonly Fixture _fixture = new Fixture();

        public DeleteNoteUseCaseTests()
        {
            _mockNotesGateway = new Mock<INotesGateway>();

            _deleteNoteUseCase = new DeleteNoteUseCase(_mockNotesGateway.Object);
        }

        // note exists
        // note doesnt exist

        [Fact]
        public async Task DeleteNoteUseCase_WhenNoteDoesntExist_ReturnsFalse()
        {
            // setup gateway - return false
            _mockNotesGateway.Setup(x => x.DeleteNote(It.IsAny<Guid>())).ReturnsAsync(false);

            // create random ID
            var randomId = Guid.NewGuid();

            // call method
            var response = await _deleteNoteUseCase.Execute(randomId).ConfigureAwait(false);

            // assert response is false
            response.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteNoteUseCase_WhenNoteExists_ReturnsTrue()
        {
            // setup gateway - return true
            _mockNotesGateway.Setup(x => x.DeleteNote(It.IsAny<Guid>())).ReturnsAsync(true);

            // create random ID
            var randomId = Guid.NewGuid();

            // call method
            var response = await _deleteNoteUseCase.Execute(randomId).ConfigureAwait(false);

            // assert response is true
            response.Should().BeTrue();
        }

    }
}
