using System.Collections.Generic;

namespace MultiCountryValidationPoc.Api.Commands
{
    public class ProcessFileCommandResult
    {
        public bool Succeeded { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
