using MediatR;
using Microsoft.AspNetCore.Http;

namespace MultiCountryValidationPoc.Api.Commands
{
    public class ProcessFileCommand : IRequest<ProcessFileCommandResult>
    {
        public IFormFile File { get; private set; }

        public ProcessFileCommand(IFormFile file)
        {
            File = file;
        }
    }
}
