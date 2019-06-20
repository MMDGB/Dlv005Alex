using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Dlv005_BL
{
    /// <summary>
    ///
    /// </summary>
    public partial class Dlv005Validations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Dlv005Validations"/> class.
        /// </summary>
        /// <param name="auxDictionary">The aux dictionary.</param>
        public Dlv005Validations()
        {
        }

        /// <summary>
        /// Validates the special qualification.
        /// </summary>
        /// <param name="checkedRow">The checked row.</param>
        public void ValidateSpecialQualification(Control control, ErrorProvider errorProvider, CancelEventArgs cancelEvent)
        {
            string message = (control.Text == string.Empty) ? ErrorMessage.emptyMandatory : (IsSpecialQualificationValid(control.Text.ToUpper())) ? string.Empty : ErrorMessage.incorrectValueDropdown;
            errorProvider.SetError(control, message);
            cancelEvent.Cancel = (message == string.Empty) ? false : true;
        }

        /// <summary>
        /// Validates the hv qualification.
        /// </summary>
        /// <param name="checkedRow">The checked row.</param>
        public void ValidateHVQualification(Control control, ErrorProvider errorProvider, CancelEventArgs cancelEvent)
        {
            string message = (control.Text == string.Empty) ? ErrorMessage.emptyMandatory : (IsHVQualificationValid(control.Text.ToUpper())) ? string.Empty : ErrorMessage.incorrectValueDropdown;
            errorProvider.SetError(control, message);
            cancelEvent.Cancel = (message == string.Empty) ? false : true;
        }

        public void ValidateDrivingAuthorization(Control control, ErrorProvider errorProvider, CancelEventArgs cancelEvent)
        {
            string message = (control.Text == string.Empty) ? ErrorMessage.emptyMandatory : (IsDrivingAuthorizationValid(control.Text.ToUpper())) ? string.Empty : ErrorMessage.incorrectValueDropdown;
            errorProvider.SetError(control, message);
            cancelEvent.Cancel = (message == string.Empty) ? false : true;
        }

        public void ValidateCustomerOE(Control button, Control control, ErrorProvider errorProvider, CancelEventArgs cancelEvent)
        {
            string message = (control.Text == string.Empty) ? ErrorMessage.emptyMandatory : (IsCustomerOEValid(control.Text.ToUpper())) ? string.Empty : ErrorMessage.incorrectValueSelectionTable;
            errorProvider.SetError(button, message);
            cancelEvent.Cancel = (message == string.Empty) ? false : true;
        }

        public void ValidateSeries(Control button, Control control, ErrorProvider errorProvider, CancelEventArgs cancelEvent)
        {
            string message = (control.Text == string.Empty) ? ErrorMessage.emptyMandatory : (IsSeriesValid(control.Text.ToUpper())) ? string.Empty : ErrorMessage.incorrectValueSelectionTable;
            errorProvider.SetError(button, message);
            cancelEvent.Cancel = (message == string.Empty) ? false : true;
        }

        public void ValidateBD09SelectionTable(Control button, Control control, ErrorProvider errorProvider, CancelEventArgs cancelEvent)
        {
            string message = (control.Text == string.Empty) ? ErrorMessage.emptyMandatory : (IsBD09SelectionTableValid(control.Text.ToUpper())) ? string.Empty : ErrorMessage.incorrectValueSelectionTable;
            errorProvider.SetError(button, message);
            cancelEvent.Cancel = (message == string.Empty) ? false : true;
        }

        /// <summary>
        /// Validates the type of the routes.
        /// </summary>
        /// <param name="checkedRow">The checked row.</param>
        public void ValidateRoutesType(Control control, ErrorProvider errorProvider, CancelEventArgs cancelEvent)
        {
            string message = (control.Text == string.Empty) ? ErrorMessage.emptyMandatory : (IsRoutesTypeValid(control.Text.ToUpper())) ? string.Empty : ErrorMessage.incorrectValueDropdown;
            errorProvider.SetError(control, message);
            cancelEvent.Cancel = (message == string.Empty) ? false : true;
        }

        /// <summary>
        /// Validates the type of the testing.
        /// </summary>
        /// <param name="checkedRow">The checked row.</param>
        public void ValidateTestingType(Control control, ErrorProvider errorProvider, CancelEventArgs cancelEvent)
        {
            string message = (control.Text == string.Empty) ? ErrorMessage.emptyMandatory : (IsTestingTypeValid(control.Text.ToUpper())) ? string.Empty : ErrorMessage.incorrectValueDropdown;
            errorProvider.SetError(control, message);
            cancelEvent.Cancel = (message == string.Empty) ? false : true;
        }

        /// <summary>
        /// Validates the sort test.
        /// </summary>
        /// <param name="checkedRow">The checked row.</param>
        public void ValidateSortTest(Control control, ErrorProvider errorProvider, CancelEventArgs cancelEvent)
        {
            string message = (control.Text == string.Empty) ? ErrorMessage.emptyMandatory : (IsSortTestValid(control.Text.ToUpper())) ? string.Empty : ErrorMessage.incorrectValueDropdown;
            errorProvider.SetError(control, message);
            cancelEvent.Cancel = (message == string.Empty) ? false : true;
        }

        /// <summary>
        /// Validates the content of the testing.
        /// </summary>
        /// <param name="checkedRow">The checked row.</param>
        public void ValidateTestingContent(Control control, ErrorProvider errorProvider, CancelEventArgs cancelEvent)
        {
            string message = (control.Text == string.Empty) ? ErrorMessage.emptyMandatory : (IsTestingContentValid(control.Text.ToUpper())) ? string.Empty : ErrorMessage.incorrectValueDropdown;
            errorProvider.SetError(control, message);
            cancelEvent.Cancel = (message == string.Empty) ? false : true;
        }

        /// <summary>
        /// Validates the start date.
        /// </summary>
        /// <param name="checkedRow">The checked row.</param>
        public void ValidateStartDate(DateTimePicker control, ErrorProvider errorProvider, CancelEventArgs cancelEvent)
        {
            string message = (control.Text == string.Empty) ? ErrorMessage.emptyMandatory : (IsStartDateValid(control)) ? string.Empty : ErrorMessage.incorrectFromDate;
            errorProvider.SetError(control, message);
            cancelEvent.Cancel = (message == string.Empty) ? false : true;
        }

        /// <summary>
        /// Validates the end date.
        /// </summary>
        /// <param name="checkedRow">The checked row.</param>
        public void ValidateEndDate(DateTimePicker control1, DateTimePicker control2, ErrorProvider errorProvider, CancelEventArgs cancelEvent)
        {
            string message = (control1.Text == string.Empty) ? ErrorMessage.emptyMandatory : (IsEndDateValid(control1, control2)) ? string.Empty : ErrorMessage.incorrectFromDate;
            errorProvider.SetError(control1, message);
            cancelEvent.Cancel = (message == string.Empty) ? false : true;
        }

        public bool IsSpecialQualificationValid(string textToValidate)
        {
            if (textToValidate == "OFFROAD" || textToValidate == "WINTER" || textToValidate == "BRENNSTOFFZELLE" || textToValidate == "ELEKTROANTRIEB" || textToValidate == "-")
            {
                return true;
            }
            return false;
        }

        public bool IsCustomerOEValid(string textToValidate)
        {
            if (textToValidate == "RD/AST" || textToValidate == "RD/BP" || textToValidate == "ITP/DT")
            {
                return true;
            }
            return false;
        }

        public bool IsHVQualificationValid(string textToValidate)
        {
            if (textToValidate == "HOCHVOLT 1" || textToValidate == "HOCHVOLT 2" || textToValidate == "HOCHVOLT 3")
            {
                return true;
            }
            return false;
        }

        public bool IsSeriesValid(string textToValidate)
        {
            if (textToValidate == "205" || textToValidate == "210" || textToValidate == "415" || textToValidate == "205,210" ||
                textToValidate == "205,415" || textToValidate == "205,210,415" || textToValidate == "210,415")
            {
                return true;
            }
            return false;
        }

        public bool IsBD09SelectionTableValid(string textToValidate)
        {
            if (textToValidate == "ALEX,FLESHER RD/BP" || textToValidate == "TEODORA,DICOIU RD/AST" || textToValidate == "DENIS,MARCHIS ITP/DT")
            {
                return true;
            }
            return false;
        }

        public bool IsDrivingAuthorizationValid(string textToValidate)
        {
            if (textToValidate == "T1" || textToValidate == "T2" || textToValidate == "T3")
            {
                return true;
            }
            return false;
        }

        public bool IsTestingTypeValid(string textToValidate)
        {
            if (textToValidate == "EXAM" || textToValidate == "WORLD-DL" || textToValidate == "FULL LOAD DL" || textToValidate == "E/E" || textToValidate == "DRIVING ASSISTANCE" || textToValidate == "DRIVING DYNAMICS" ||
               textToValidate == "RAFF-DL")
            {
                return true;
            }
            return false;
        }

        public bool IsRoutesTypeValid(string textToValidate)
        {
            if (textToValidate == "TEST AREA" || textToValidate == "PUBLIC ROAD" || textToValidate == "NÜRBURGRING" || textToValidate == "BAD ROAD")
            {
                return true;
            }
            return false;
        }

        public bool IsSortTestValid(string textToValidate)
        {
            if (textToValidate == "IMMENDINGEN")
            {
                return true;
            }
            return false;
        }

        public bool IsTestingContentValid(string textToValidate)
        {
            if (textToValidate != string.Empty && textToValidate.Length <= 200)
            {
                return true;
            }
            return false;
        }

        public bool IsStartDateValid(DateTimePicker dateFromValidate)
        {
            if (dateFromValidate.CustomFormat != " ")
            {
                if (dateFromValidate.Value > DateTime.Now)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public bool IsEndDateValid(DateTimePicker dateToValidate, DateTimePicker dateFromValidate)
        {
            if (dateToValidate.CustomFormat != " " && dateFromValidate.CustomFormat != " ")
            {
                if (dateFromValidate.Value <= dateToValidate.Value)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private static bool SetAllocationError(DataGridView dataGridView2, ErrorProvider errorProvider, CancelEventArgs cancelEvent, string messageDisplayed)
        {
            cancelEvent.Cancel = true;
            errorProvider.SetError(dataGridView2, messageDisplayed);
            return false;
        }

        /// <summary>
        /// Validates the allocation grid.
        /// </summary>
        /// <param name="dataGridView2">The data grid view2.</param>
        public bool ValidateAllocationGrid(DataGridView dataGridView2, ErrorProvider errorProvider, CancelEventArgs cancelEvent, string emptyMandatory, string allocationProcent, string incorrectFormat)
        {
            if (dataGridView2.Rows.Count < 1)
            {
                return SetAllocationError(dataGridView2, errorProvider, cancelEvent, emptyMandatory);
            }
            else
            {
                decimal procent = 0;
                for (int i = 0; i <= dataGridView2.Rows.Count - 1; i++)
                {
                    if (dataGridView2.Rows[i].DataBoundItem != null)
                    {
                        if (dataGridView2.Rows[i].Cells[1].Value.ToString() != string.Empty)
                        {
                            if (OnlyNumber(dataGridView2.Rows[i].Cells[1].Value.ToString()) == false)
                            {
                                return SetAllocationError(dataGridView2, errorProvider, cancelEvent, incorrectFormat);
                            }
                            procent += Convert.ToDecimal(dataGridView2.Rows[i].Cells[1].Value);
                            if (dataGridView2.Rows[i].Cells[0].Value.ToString() == string.Empty)
                            {
                                return SetAllocationError(dataGridView2, errorProvider, cancelEvent, emptyMandatory);
                            }
                            foreach (char character in dataGridView2.Rows[i].Cells[0].Value.ToString())
                            {
                                if (char.IsLetter(character) == false && char.IsDigit(character) == false)
                                {
                                    return SetAllocationError(dataGridView2, errorProvider, cancelEvent, incorrectFormat);
                                }
                            }
                            if (Convert.ToDecimal(dataGridView2.Rows[i].Cells[1].Value) > 100)
                            {
                                return SetAllocationError(dataGridView2, errorProvider, cancelEvent, allocationProcent);
                            }
                            if (Convert.ToDecimal(dataGridView2.Rows[i].Cells[1].Value) < 0)
                            {
                                return SetAllocationError(dataGridView2, errorProvider, cancelEvent, incorrectFormat);
                            }
                        }
                        else
                        {
                            return SetAllocationError(dataGridView2, errorProvider, cancelEvent, emptyMandatory);
                        }
                    }
                }
                if (procent != 100)
                {
                    return SetAllocationError(dataGridView2, errorProvider, cancelEvent, allocationProcent);
                }
                cancelEvent.Cancel = false;
                errorProvider.SetError(dataGridView2, null);
                return true;
            }
        }

        /// <summary>
        /// Called when [number].
        /// </summary>
        /// <param name="Number">The number.</param>
        /// <returns></returns>
        private bool OnlyNumber(string Number)
        {
            foreach (char character in Number)
            {
                if (character < '0' || character > '9')
                    return false;
            }
            return true;
        }
    }
}