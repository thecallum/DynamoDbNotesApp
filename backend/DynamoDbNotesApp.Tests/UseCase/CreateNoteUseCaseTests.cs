using AutoFixture;
using DynamoDbNotesApp.Boundary.Request;
using DynamoDbNotesApp.Boundary.Response;
using DynamoDbNotesApp.Domain;
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
    public class CreateNoteUseCaseTests
    {
        private readonly Mock<INotesGateway> _mockNotesGateway;
        private readonly CreateNoteUseCase _createNoteUseCase;

        private readonly Fixture _fixture = new Fixture();

        public CreateNoteUseCaseTests()
        {
            _mockNotesGateway = new Mock<INotesGateway>();

            _createNoteUseCase = new CreateNoteUseCase(_mockNotesGateway.Object);
        }

        [Fact]
        public async Task CreateNoteUseCase_WhenCalled_ReturnsId()
        {
            // create mock CreateNoteRequest
            var mockRequest = _fixture.Create<CreateNoteRequest>();

            // create mock Id
            var mockId = Guid.NewGuid();

            // setup usecase
            _mockNotesGateway.Setup(x => x.CreateNote(It.IsAny<Note>())).ReturnsAsync(mockId);

            // call method
            var response = await _createNoteUseCase.Execute(mockRequest).ConfigureAwait(false);

            // assert response 
            response.Should().BeSameAs(mockId);
        }
    }
}
