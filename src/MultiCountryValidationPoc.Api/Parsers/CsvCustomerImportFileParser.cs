using MultiCountryValidationPoc.Api.Models;
using System.Collections.Generic;
using System.IO;

namespace MultiCountryValidationPoc.Api.Parsers
{
    public class CsvCustomerImportFileParser : ICustomerImportFileParser
    {
        public bool CanHandle(string fileExtension)
        {
            return fileExtension == ".txt" || fileExtension == ".csv";
        }

        public CustomerImportLines Parse(string filePath)
        {
            string line;
            var customerLines = new CustomerImportLines { Lines = new List<CustomerImportEntry>() };
            var counter = 0;

            using var fs = File.OpenRead(filePath);
            using var reader = new StreamReader(fs);
            while ((line = reader.ReadLine()) != null)
            {
                if (counter == 0)
                {
                    counter++;
                    continue;
                }

                var splitedLine = line.Split(';');

                ((List<CustomerImportEntry>)customerLines.Lines).Add(new CustomerImportEntry
                {
                    Name = splitedLine[0],
                    Document = splitedLine[1],
                });

                counter++;
            }

            return customerLines;
        }
    }
}
