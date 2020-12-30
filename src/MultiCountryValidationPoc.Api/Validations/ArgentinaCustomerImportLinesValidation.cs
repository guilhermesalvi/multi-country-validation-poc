using FluentValidation;
using MultiCountryValidationPoc.Api.Models;
using System.Linq;

namespace MultiCountryValidationPoc.Api.Validations
{
    public class ArgentinaCustomerImportLinesValidation : AbstractValidator<CustomerImportLines>
    {
        public ArgentinaCustomerImportLinesValidation()
        {
            // Here all file validations for Argentina must be implemented

            When(x => x.Country == Enums.ECountryFile.Argentina, () =>
            {
                RuleFor(x => x.Lines).Must(x => x.Any()).WithMessage("No lines was informed.");

                RuleForEach(x => x.Lines).ChildRules(x =>
                {
                    x.RuleFor(x => x.Name).NotEmpty().WithMessage("Customer Name should not be null.");
                    x.RuleFor(x => x.Document).NotEmpty().WithMessage("The DNI document should not be null.");
                });
            });
        }
    }
}
