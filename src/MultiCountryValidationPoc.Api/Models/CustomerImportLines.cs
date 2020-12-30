using MultiCountryValidationPoc.Api.Enums;
using System.Collections.Generic;

namespace MultiCountryValidationPoc.Api.Models
{
    public class CustomerImportLines
    {
        public ECountryFile Country { get; set; }
        public IEnumerable<CustomerImportEntry> Lines { get; set; }
    }
}
