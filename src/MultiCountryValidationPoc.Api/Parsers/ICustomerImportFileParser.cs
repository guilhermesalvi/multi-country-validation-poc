using MultiCountryValidationPoc.Api.Models;

namespace MultiCountryValidationPoc.Api.Parsers
{
    public interface ICustomerImportFileParser
    {
        bool CanHandle(string fileExtension);
        CustomerImportLines Parse(string filePath);
    }
}
