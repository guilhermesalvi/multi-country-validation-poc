using MultiCountryValidationPoc.Api.Models;
using System;

namespace MultiCountryValidationPoc.Api.Parsers
{
    public class ExcelCustomerImportFileParser : ICustomerImportFileParser
    {
        public bool CanHandle(string fileExtension)
        {
            return fileExtension == "xls" || fileExtension == "xlsx";
        }

        public CustomerImportLines Parse(string filePath)
        {
            // Conversion to excel files must be implemented
            throw new NotImplementedException();
        }
    }
}
