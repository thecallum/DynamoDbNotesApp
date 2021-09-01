using AutoFixture;
using DynamoDbNotesApp.Boundary.Request;
using DynamoDbNotesApp.Boundary.Response;
using DynamoDbNotesApp.Controllers;
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

namespace DynamoDbNotesApp.Tests.Controllers
{
    public class NotesControllerTests
    {
        private readonly NotesController _notesController;
        private readonly Mock<IGetByIdUseCase> _mockGetByIdUseCase;
        private readonly Mock<IGetAllUseCase> _mockGetAllUseCase;
        private readonly Mock<ICreateNoteUseCase> _mockCreateNoteUseCase;
        private readonly Mock<IUpdateNoteUseCase> _mockUpdateNoteUseCase;
        private readonly Mock<IDeleteNoteUseCase> _mockDeleteNoteUseCase;

        private readonly Fixture _fixture = new Fixture();

        public NotesControllerTests()
        {

            _mockGetByIdUseCase = new Mock<IGetByIdUseCase>();
            _mockGetAllUseCase = new Mock<IGetAllUseCase>();
            _mockCreateNoteUseCase = new Mock<ICreateNoteUseCase>();
            _mockUpdateNoteUseCase = new Mock<IUpdateNoteUseCase>();
            _mockDeleteNoteUseCase = new Mock<IDeleteNoteUseCase>();

            _notesController = new NotesController(
                _mockGetByIdUseCase.Object,
                _mockGetAllUseCase.Object,
                _mockCreateNoteUseCase.Object,
                _mockUpdateNoteUseCase.Object,
                _mockDeleteNoteUseCase.Object
            );
        }

        [Fact]
        public async Task GetById_WhenItDoesntExist_ReturnsNotFound()
        {
            // Generate random guid
            var randomId = _fixture.Create<Guid>();

            // setup usecase
            _mockGetByIdUseCase.Setup(x => x.Execute(randomId)).ReturnsAsync((NoteResponseObject)null);

            // call controller method
            var response = await _notesController.GetById(randomId);

            // assert response is 404 not found
            response.Should().BeOfType(typeof(NotFoundObjectResult));
        }

        [Fact]
        public async Task GetById_WhenNoteExists_ReturnsNoteResponseObject()
        {
            // Generate new Note
            var mockNote = _fixture.Create<NoteResponseObject>();

            // setup usecase
            _mockGetByIdUseCase.Setup(x => x.Execute(mockNote.Id)).ReturnsAsync(mockNote);

            // call controller method with note id
            var response = await _notesController.GetById(mockNote.Id);

            // assert respose is 200
            response.Should().BeOfType(typeof(OkObjectResult));
            // assert response is instance of NoteResponseObject
            (response as OkObjectResult).Value.Should().BeEquivalentTo(mockNote);
        }

        [Fact]
        public async Task GetAll_WhenNoNotesFound_ReturnsEmptyNoteResponseObject()
        {
            // setup usecase to return empty list
            var emptyList = new List<NoteResponseObject>();
            _mockGetAllUseCase.Setup(x => x.Execute()).ReturnsAsync(emptyList);

            // call controller method
            var response = await _notesController.GetAll();

            // assert response is 200
            response.Should().BeOfType(typeof(OkObjectResult));
            // assert response is instance of List<NoteResponseObject>
            (response as OkObjectResult).Value.Should().BeOfType(typeof(List<NoteResponseObject>));
            // assert response value is empty list
            (response as OkObjectResult).Value.Should().BeEquivalentTo(emptyList);
        }

        [Fact]
        public async Task GetAll_WhenNoteExists_ReturnsNoteResponseObject()
        {
            Random rnd = new Random();

            // generate random number (1-5) of random notes
            var numberOfNotes = rnd.Next(1, 5);
            var mockNotes = _fixture.CreateMany<NoteResponseObject>(numberOfNotes).ToList();

            // setup usecase to return notes
            _mockGetAllUseCase.Setup(x => x.Execute()).ReturnsAsync(mockNotes);

            // call controller method
            var response = await _notesController.GetAll();

            // assert response is 200
            response.Should().BeOfType(typeof(OkObjectResult));
            // assert response is instance of List<NoteResponseObject>
            (response as OkObjectResult).Value.Should().BeOfType(typeof(List<NoteResponseObject>));
            // assert response value length is same as number inserted into mock database
            (response as OkObjectResult).Value.Should().BeEquivalentTo(mockNotes);
            ((response as OkObjectResult).Value as List<NoteResponseObject>).Count.Should().Be(numberOfNotes);
        }

        [Fact]
        public async Task CreateNote_WhenValidRequest_Returns201CreatedResponse()
        {
            // create mock note
            var mockNote = _fixture.Create<NoteCreatedResponseObject>();

            // setup useCase
            _mockCreateNoteUseCase.Setup(x => x.Execute(It.IsAny<CreateNoteRequest>())).ReturnsAsync(mockNote);

            // create requestObject
            var requestObject = _fixture.Create<CreateNoteRequest>();

            // call controller method
            var response = await _notesController.CreateNote(requestObject);

            // assert response is 201 created
            response.Should().BeOfType(typeof(CreatedResult));
            // assert response is instance of NoteCreatedResponseObject
            (response as CreatedResult).Value.Should().BeOfType(typeof(NoteCreatedResponseObject));
            // assert response contains valid Guid
            ((response as CreatedResult).Value as NoteCreatedResponseObject).Id.Should().Be(mockNote.Id);
        }

        [Fact]
        public async Task UpdateNote_WhenItDoesntExist_ReturnsNotFound()
        {
            // generate random Guid
            var randomId = Guid.NewGuid();

            // create updateNoteRequestObject 
            var requestObject = _fixture.Create<UpdateNoteRequest>();

            // setup usecase
            _mockUpdateNoteUseCase.Setup(x => x.Execute(randomId, requestObject)).ReturnsAsync(false);

            // call controller method
            var response = await _notesController.UpdateNote(randomId, requestObject);

            // assert response is 404 not found
            response.Should().BeOfType(typeof(NotFoundResult));
        }

        [Fact]
        public async Task UpdateNote_WhenValidRequest_Returns200NoContent()
        {
            // create request object
            var requestObject = _fixture.Create<UpdateNoteRequest>();

            // create random id
            var randomId = Guid.NewGuid();

            // setup usecase
            _mockUpdateNoteUseCase.Setup(x => x.Execute(randomId, requestObject)).ReturnsAsync(true);

            // call controller method
            var response = await _notesController.UpdateNote(randomId, requestObject);

            // assert response is 200 no content
            response.Should().BeOfType(typeof(OkResult));
        }

        [Fact]
        public async Task DeleteNote_WhenItDoesntExist_ReturnsNotFound()
        {
            // create random id
            var randomId = Guid.NewGuid();

            // setup usecase
            _mockDeleteNoteUseCase.Setup(x => x.Execute(randomId)).ReturnsAsync(false);

            // call controller method
            var response = await _notesController.DeleteNote(randomId);

            // assert response is 404 not found
            response.Should().BeOfType(typeof(NotFoundResult));
        }

        [Fact]
        public async Task DeleteNote_WhenValidRequest_Returns200NoContent()
        {
            // generate random id
            var randomId = Guid.NewGuid();

            // setup usecase
            _mockDeleteNoteUseCase.Setup(x => x.Execute(randomId)).ReturnsAsync(true);

            // call controller method
            var response = await _notesController.DeleteNote(randomId);

            // assert response is 200 no content
            response.Should().BeOfType(typeof(OkResult));
        }
    }
}
