using DesafioDvD.Application.Features.Directors.Commands.CreateDirector;
using DesafioDvD.Application.Features.Directors.Commands.DeleteDirector;
using DesafioDvD.Application.Features.Directors.Commands.UpdateDirector;
using DesafioDvD.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DesafioDvD.WebApi.Controllers
{
    public class DirectorController : ApiController
    {
        private readonly IMediator _mediator;

        public DirectorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]/{fullName}", Name = "GetDirector")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetDirector([FromRoute] string fullName)
        {
            var query = new GetDirectorQuery(fullName);

            var response = await _mediator.Send(query, HttpContext.RequestAborted);

            if (response is null)
                return CustomResponse((int)HttpStatusCode.NotFound, false);

            return CustomResponse((int)HttpStatusCode.OK, true, response);
        }

        [HttpPost("CreateDirector")]
        [ProducesResponseType(typeof(CreateDirectorResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CreateDirectorResponse>> createDirector([FromBody] CreateDirectorCommand command)
        {
            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if(response is null)
                return CustomResponse((int)HttpStatusCode.BadRequest, false);

            return CustomResponse((int)HttpStatusCode.Created, true, response);
        }


        [HttpPost("UpdateDirector")]
        [ProducesResponseType(typeof(UpdateDirectorResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<UpdateDirectorResponse>> UpdateDirector([FromBody] UpdateDirectorCommand command)
        {
            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if(response is null)
                return CustomResponse((int)HttpStatusCode.BadRequest, false);

            return CustomResponse((int)HttpStatusCode.OK, true, response);
        }

        [HttpPost("DeleteDirector/{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> DeleteDirector([FromRoute] Guid id)
        {
            var command = new DeleteDirectorCommand(id);
            var response = await _mediator.Send(command, HttpContext.RequestAborted);

            if(!response)
                return CustomResponse((int)HttpStatusCode.BadRequest, response);

            return CustomResponse((int)HttpStatusCode.OK, response);
        }
    }
}
