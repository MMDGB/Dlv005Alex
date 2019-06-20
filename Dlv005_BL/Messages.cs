using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dlv005_BL
{
    public static class ErrorMessage
    {
        /// <summary>
        /// The allocation procent
        /// </summary>
        public static readonly string allocationProcent = "The total of the account assignment shares must be 100%.";
        /// <summary>
        /// The incorrect value selection table
        /// </summary>
        public static readonly string incorrectValueSelectionTable = "The value is not contained into the selection table. Please select a valid value.";
        /// <summary>
        /// The empty mandatory
        /// </summary>
        public static readonly string emptyMandatory = "The mandatory field does not contain any data. Please enter a value.";
        /// <summary>
        /// The incorrect value dropdown
        /// </summary>
        public static readonly string incorrectValueDropdown = "The value is incorrect. Please correct your entry.";
        /// <summary>
        /// The incorrect from date
        /// </summary>
        public static readonly string incorrectFromDate = "The date must be in the future!";
        /// <summary>
        /// The incorrect to date
        /// </summary>
        public static readonly string incorrectToDate = "Bis date must be greater or equal with von date!";
        /// <summary>
        /// The incorrect format/
        /// </summary>
        public static readonly string incorrectFormat = "The field has incorrect format! Please correct your entry.";
        /// <summary>
        /// The short term date
        /// </summary>
        public static readonly string shortTermDate = "The date entered for starting picking is very short-term. Do you really want to save the data? ";
        /// <summary>
        /// The make sure wanna delete
        /// </summary>
        public static readonly string makeSureWannaDelete = "Should the external picking really be deleted? ";
    }
}
