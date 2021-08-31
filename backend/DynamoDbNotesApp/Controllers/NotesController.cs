using DynamoDbNotesApp.Boundary.Request;
using DynamoDbNotesApp.Boundary.Response;
using DynamoDbNotesApp.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DynamoDbNotesApp.Controllers
{
    [ApiController]
    [Route("api/notes")]
    [Produces("application/json")]
    public class NotesController : ControllerBase
    {
        private readonly IGetByIdUseCase _getByIdUseCase;
        private readonly IGetAllUseCase _getAllUseCase;
        private readonly ICreateNoteUseCase _createNoteUseCase;
        private readonly IUpdateNoteUseCase _udateNoteUseCase;
        private readonly IDeleteNoteUseCase _deleteNoteUseCase;

        public NotesController(
            IGetByIdUseCase getByIdUseCase, 
            IGetAllUseCase getAllUseCase, 
            ICreateNoteUseCase createNoteUseCase, 
            IUpdateNoteUseCase updateNoteUseCase, 
            IDeleteNoteUseCase deleteNoteUseCase)
        {
            _getByIdUseCase = getByIdUseCase;
            _getAllUseCase = getAllUseCase;
            _createNoteUseCase = createNoteUseCase;
            _udateNoteUseCase = updateNoteUseCase;
            _deleteNoteUseCase = deleteNoteUseCase;
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(NoteResponseObject), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<NoteResponseObject>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ProducesResponseType(typeof(NoteCreatedResponseObject), StatusCodes.Status201Created)] // Needs to return Id of created note
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateNote([FromBody] CreateNoteRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateNote([FromRoute] Guid id, [FromBody] UpdateNoteRequest request)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteNote([FromRoute] Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
