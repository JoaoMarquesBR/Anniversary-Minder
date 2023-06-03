using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anniversary_Minder
{
    public class Anniversary
    {
        public string Names { get; set; } = "";

        public string AnniversaryDate { get; set; } = "";

        public string AnniversaryType { get; set; } = "";

        public string? Description { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public Address Address { get; set; } = new Address();

        override
        public string ToString()
        {
            return $"{Names}\t\t\t\t\t{AnniversaryDate}\t{AnniversaryType}";
        }
    }
    
    public class Address
    {
        public string? StreetAddress;

        public string? Municipality;

        public string? Province;

        public string? PostalCode;

        override
        public string ToString()
        {
            return $"{StreetAddress}, {Municipality} {Province}, {PostalCode}";
        }
    }
}
