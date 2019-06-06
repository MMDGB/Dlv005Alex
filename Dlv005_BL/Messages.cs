using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dlv005_BL
{
    public static class Messages
    {
        public const string allocationProcent = "The total of the account assignment shares must be 100%.";

        public const string incorrectValueSelectionTable = "The value is not contained into the selection table. Please select a valid value.";

        public const string emptyMandatory = "The mandatory field does not contain any data. Please enter a value.";

        public const string incorrectValueDropdown = "The value is incorrect. Please correct your entry.";

        public const string incorrectFromDate = "The date must be in the future!";

        public const string incorrectToDate = "Bis date must be greater or equal with von date!";

        public const string incorrectFormat = "The field has incorrect format! Please correct your entry.";
    }
}
