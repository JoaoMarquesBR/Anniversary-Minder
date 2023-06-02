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

        public Address? Address { get; set; }

        //Print main information of anniversary.
        public void printInfo()
        {
            Console.WriteLine(AnniversaryDate + " | " + Names + " | " + AnniversaryType + " | " + Description + " | ");
        }
    }

    public struct Address
    {
        public string? StreetAddress;

        public string? Municipality;

        public string? Province;

        public string? PostalCode;

    }
}
