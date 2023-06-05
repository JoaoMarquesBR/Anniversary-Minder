/**
 * Coder: Gui Miranda, Joao Marques
 * Date: 06/05/2023
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anniversary_Minder
{
    /**
	 * Class Name: Anniversary
	 * Purpose: Creates an Anniversary object template
     */
    public class Anniversary
    {
        public string Names { get; set; } = "";

        public string AnniversaryDate { get; set; } = "";

        public string AnniversaryType { get; set; } = "";

        public string? Description { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public Address Address { get; set; } = new Address();

        /*
         * Method Name: ToString
         * Purpose: Returns a string representation of an Anniversary object
         * Accepts: Nothing
         * Returns: A string representation of an Anniversary object
         */
        override
        public string ToString()
        {
            return Names.PadRight(34) + AnniversaryDate.PadRight(17) + AnniversaryType;
        }
    }

    /**
	 * Class Name: Address
	 * Purpose: Creates an Address object template
     */
    public class Address
    {
        public string? StreetAddress;

        public string? Municipality;

        public string? Province;

        public string? PostalCode;

        /*
         * Method Name: ToString
         * Purpose: Returns a string representation of an Address object
         * Accepts: Nothing
         * Returns: A string representation of an Address object
         */
        override
        public string ToString()
        {
            return $"{(!string.IsNullOrEmpty(StreetAddress) ? StreetAddress + " " : "")}" +
                   $"{(!string.IsNullOrEmpty(Municipality)  ? Municipality  + " " : "")}" +
                   $"{(!string.IsNullOrEmpty(Province)      ? Province      + " " : "")}" +
                   $"{(!string.IsNullOrEmpty(PostalCode)    ? PostalCode    + " " : "")}";
        }
    }
}
