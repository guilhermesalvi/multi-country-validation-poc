using FluentValidation;
using MultiCountryValidationPoc.Api.Models;
using System.Linq;

namespace MultiCountryValidationPoc.Api.Validations
{
    public class BrazilCustomerImportLinesValidation : AbstractValidator<CustomerImportLines>
    {
        public BrazilCustomerImportLinesValidation()
        {
            // Here all file validations for Brazil must be implemented

            When(x => x.Country == Enums.ECountryFile.Brazil, () =>
            {
                RuleFor(x => x.Lines).Must(x => x.Any()).WithMessage("No lines was informed.");

                RuleForEach(x => x.Lines).ChildRules(x =>
                {
                    x.RuleFor(x => x.Name).NotEmpty().WithMessage("Customer Name should not be null.");
                    x.RuleFor(x => x.Document).NotEmpty().WithMessage("The CPF document should not be null.");
                });
            });
        }
    }
}
