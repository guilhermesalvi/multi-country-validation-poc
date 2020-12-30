using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiCountryValidationPoc.Api.Commands;
using System;
using System.Threading.Tasks;

namespace MultiCountryValidationPoc.Api.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FilesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            var result = await _mediator.Send(new ProcessFileCommand(file));

            return result.Succeeded
                ? Ok(result)
                : UnprocessableEntity(result);
        }
    }
}
