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
    public class GetAllUseCaseTests
    {
        private readonly Mock<INotesGateway> _mockNotesGateway;
        private readonly GetAllUseCase _getAllUseCase;

        private readonly Fixture _fixture = new Fixture();

        public GetAllUseCaseTests()
        {
            _mockNotesGateway = new Mock<INotesGateway>();

            _getAllUseCase = new GetAllUseCase(_mockNotesGateway.Object);
        }

        // return none
        // return list

        [Fact]
        public async Task GetAllUseCase_WhenNoNotesFound_ReturnsEmptyList()
        {
            // create empty list
            var emptyList = new List<Note>();

            // setup mock gateway - returns empty list
            _mockNotesGateway.Setup(x => x.GetAllNotes()).ReturnsAsync(emptyList);

            // call method
            var response = await _getAllUseCase.Execute().ConfigureAwait(false);

            // assert response is empty list
            response.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllUseCase_WhenMultipleNotesFound_ReturnsListOfNotes()
        {
            // create random number (1,5)
            var rnd = new Random();
            var randomNumber = rnd.Next(1, 5);

            // create list of notes, random length
            var mockNotes = _fixture.CreateMany<Note>(randomNumber).ToList();

            // setup mock gateway - returns list
            _mockNotesGateway.Setup(x => x.GetAllNotes()).ReturnsAsync(mockNotes);

            // call method
            var response = await _getAllUseCase.Execute().ConfigureAwait(false);

            // assert response is empty list
            response.Count.Should().Be(randomNumber);
        }
    }
}
