using FluentValidation;
using MediatR;
using MultiCountryValidationPoc.Api.Models;
using MultiCountryValidationPoc.Api.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MultiCountryValidationPoc.Api.Commands
{
    public class ProcessFileCommandHandler : IRequestHandler<ProcessFileCommand, ProcessFileCommandResult>
    {
        private readonly IEnumerable<ICustomerImportFileParser> _parsers;
        private readonly IEnumerable<IValidator<CustomerImportLines>> _validators;

        public ProcessFileCommandHandler(
            IEnumerable<ICustomerImportFileParser> parsers,
            IEnumerable<IValidator<CustomerImportLines>> validators)
        {
            _parsers = parsers ?? throw new ArgumentNullException(nameof(parsers));
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        public async Task<ProcessFileCommandResult> Handle(ProcessFileCommand request, CancellationToken cancellationToken)
        {
            if (request.File.Length <= 0)
            {
                return new ProcessFileCommandResult
                {
                    Succeeded = false,
                    Errors = new List<string>
                    {
                        "File not informed."
                    }
                };
            }

            // Copy the file to a directory (here it can also be a storage)
            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot",
                request.File.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await request.File.CopyToAsync(stream, cancellationToken);
            }

            // Checks whether the file has a valid parser for its extension
            var parser = _parsers.FirstOrDefault(x => x.CanHandle(Path.GetExtension(path)));

            if (parser == null)
            {
                return new ProcessFileCommandResult
                {
                    Succeeded = false,
                    Errors = new List<string>
                        {
                            "Invalid file extension."
                        }
                };
            }

            // Convert the file to an object
            var customerImportLines = parser.Parse(path);

            // Here the country definition must be dynamic according to the user's country
            customerImportLines.Country = Enums.ECountryFile.Brazil;

            // Performs validations
            var errors = new List<string>();

            foreach (var validator in _validators)
            {
                var result = await validator.ValidateAsync(customerImportLines, cancellationToken);

                foreach (var error in result.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
            }

            if (errors.Count > 0)
            {
                return new ProcessFileCommandResult { Succeeded = false, Errors = errors };
            }

            return new ProcessFileCommandResult { Succeeded = true };
        }
    }
}
