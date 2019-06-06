using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Dlv005_BL
{
    /// <summary>
    ///
    /// </summary>
    public partial class Dlv005Validations
    {
        private Dictionary<string, bool> errorDictionary = new Dictionary<string, bool>();

        public Dictionary<string, bool> GetDictionary()
        {
            return errorDictionary;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dlv005Validations"/> class.
        /// </summary>
        /// <param name="auxDictionary">The aux dictionary.</param>
        public Dlv005Validations(Dictionary<string, bool> auxDictionary)
        {
            this.errorDictionary = auxDictionary;
        }

        /// <summary>
        /// Validates the special qualification.
        /// </summary>
        /// <param name="checkedRow">The checked row.</param>
        public void ValidateSpecialQualification(Control control, ErrorProvider errorProvider, CancelEventArgs cancelEvent)
        {
            string message = (control.Text == string.Empty) ? Messages.emptyMandatory : (IsSpecialQualificationValid(control.Text)) ? string.Empty : Messages.incorrectValueDropdown;
            errorProvider.SetError(control, message);
            cancelEvent.Cancel = (message == string.Empty) ? false : true;
        }

        /// <summary>
        /// Validates the hv qualification.
        /// </summary>
        /// <param name="checkedRow">The checked row.</param>
        public void ValidateHVQualification(Control control, ErrorProvider errorProvider, CancelEventArgs cancelEvent)
        {
            string message = (control.Text == string.Empty) ? Messages.emptyMandatory : (IsHVQualificationValid(control.Text)) ? string.Empty : Messages.incorrectValueDropdown;
            errorProvider.SetError(control, message);
            cancelEvent.Cancel = (message == string.Empty) ? false : true;
        }

        public void ValidateDrivingAuthorization(Control control, ErrorProvider errorProvider, CancelEventArgs cancelEvent)
        {
            string message = (control.Text == string.Empty) ? Messages.emptyMandatory : (IsDrivingAuthorizationValid(control.Text)) ? string.Empty : Messages.incorrectValueDropdown;
            errorProvider.SetError(control, message);
            cancelEvent.Cancel = (message == string.Empty) ? false : true;
        }

        public void ValidateCustomerOE(Control button, Control control, ErrorProvider errorProvider, CancelEventArgs cancelEvent)
        {
            string message = (control.Text == string.Empty) ? Messages.emptyMandatory : (IsCustomerOEValid(control.Text)) ? string.Empty : Messages.incorrectValueSelectionTable;
            errorProvider.SetError(button, message);
            cancelEvent.Cancel = (message == string.Empty) ? false : true;
        }

        public void ValidateSeries(Control button, Control control, ErrorProvider errorProvider, CancelEventArgs cancelEvent)
        {
            string message = (control.Text == string.Empty) ? Messages.emptyMandatory : (IsSeriesValid(control.Text)) ? string.Empty : Messages.incorrectValueSelectionTable;
            errorProvider.SetError(button, message);
            cancelEvent.Cancel = (message == string.Empty) ? false : true;
        }

        public void ValidateBD09SelectionTable(Control button, Control control, ErrorProvider errorProvider, CancelEventArgs cancelEvent)
        {
            string message = (control.Text == string.Empty) ? Messages.emptyMandatory : (IsBD09SelectionTableValid(control.Text)) ? string.Empty : Messages.incorrectValueSelectionTable;
            errorProvider.SetError(button, message);
            cancelEvent.Cancel = (message == string.Empty) ? false : true;
        }

        /// <summary>
        /// Validates the type of the routes.
        /// </summary>
        /// <param name="checkedRow">The checked row.</param>
        public void ValidateRoutesType(Control control, ErrorProvider errorProvider, CancelEventArgs cancelEvent)
        {
            string message = (control.Text == string.Empty) ? Messages.emptyMandatory : (IsRoutesTypeValid(control.Text)) ? string.Empty : Messages.incorrectValueDropdown;
            errorProvider.SetError(control, message);
            cancelEvent.Cancel = (message == string.Empty) ? false : true;
        }

        /// <summary>
        /// Validates the type of the testing.
        /// </summary>
        /// <param name="checkedRow">The checked row.</param>
        public void ValidateTestingType(Control control, ErrorProvider errorProvider, CancelEventArgs cancelEvent)
        {
            string message = (control.Text == string.Empty) ? Messages.emptyMandatory : (IsTestingTypeValid(control.Text)) ? string.Empty : Messages.incorrectValueDropdown;
            errorProvider.SetError(control, message);
            cancelEvent.Cancel = (message == string.Empty) ? false : true;
        }

        /// <summary>
        /// Validates the sort test.
        /// </summary>
        /// <param name="checkedRow">The checked row.</param>
        public void ValidateSortTest( Control control, ErrorProvider errorProvider, CancelEventArgs cancelEvent)
        {
            string message = (control.Text == string.Empty) ? Messages.emptyMandatory : (IsSortTestValid(control.Text)) ? string.Empty : Messages.incorrectValueDropdown;
            errorProvider.SetError(control, message);
            cancelEvent.Cancel = (message == string.Empty) ? false : true;
        }

        /// <summary>
        /// Validates the content of the testing.
        /// </summary>
        /// <param name="checkedRow">The checked row.</param>
        public void ValidateTestingContent( Control control, ErrorProvider errorProvider, CancelEventArgs cancelEvent)
        {
            string message = (control.Text == string.Empty) ? Messages.emptyMandatory : (IsTestingContentValid(control.Text)) ? string.Empty : Messages.incorrectValueDropdown;
            errorProvider.SetError(control, message);
            cancelEvent.Cancel = (message == string.Empty) ? false : true;
        }
        /// <summary>
        /// Validates the start date.
        /// </summary>
        /// <param name="checkedRow">The checked row.</param>
        public void ValidateStartDate( DateTimePicker control, ErrorProvider errorProvider, CancelEventArgs cancelEvent)
        {
            string message = (control.Text == string.Empty) ? Messages.emptyMandatory : (IsStartDateValid(control)) ? string.Empty : Messages.incorrectFromDate;
            errorProvider.SetError(control, message);
            cancelEvent.Cancel = (message == string.Empty) ? false : true;
        }

        /// <summary>
        /// Validates the end date.
        /// </summary>
        /// <param name="checkedRow">The checked row.</param>
        public void ValidateEndDate(DateTimePicker control1, DateTimePicker control2, ErrorProvider errorProvider, CancelEventArgs cancelEvent)
        {
            string message = (control1.Text == string.Empty) ? Messages.emptyMandatory : (IsEndDateValid(control1,control2)) ? string.Empty : Messages.incorrectFromDate;
            errorProvider.SetError(control1, message);
            cancelEvent.Cancel = (message == string.Empty) ? false : true;
        }

        public bool IsSpecialQualificationValid(string textToValidate)
        {
            if (textToValidate == "Offroad" || textToValidate == "Winter" || textToValidate == "Brennstoffzelle" || textToValidate == "Elektroantrieb" || textToValidate == "-")
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
            if (textToValidate == "Hochvolt 1" || textToValidate == "Hochvolt 2" || textToValidate == "Hochvolt 3")
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
            if (textToValidate == "Alex,Flesher RD/BP" || textToValidate == "Teodora,Dicoiu RD/AST" || textToValidate == "Denis,Marchis ITP/DT")
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
            if (textToValidate == "Exam" || textToValidate == "World-DL" || textToValidate == "Full load DL" || textToValidate == "E/E" || textToValidate == "Driving assistance" || textToValidate == "Driving dynamics" ||
               textToValidate == "Raff-DL")
            {
                return true;
            }
            return false;
        }

        public bool IsRoutesTypeValid(string textToValidate)
        {
            if (textToValidate == "Test area" || textToValidate == "Public road" || textToValidate == "Nürburgring" || textToValidate == "Bad road")
            {
                return true;
            }
            return false;
        }

        public bool IsSortTestValid(string textToValidate)
        {
            if (textToValidate == "Immendingen")
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

     

        /// <summary>
        /// Validates the allocation grid.
        /// </summary>
        /// <param name="dataGridView2">The data grid view2.</param>
        public void ValidateAllocationGrid(DataGridView dataGridView2, ErrorProvider errorProvider, CancelEventArgs cancelEvent)
        {
            if (dataGridView2.Rows.Count < 1)
            {
                string message = Messages.emptyMandatory;
                errorProvider.SetError(dataGridView2, message);
                cancelEvent.Cancel = false;

               
            }
            else
            {
                cancelEvent.Cancel = true;
                decimal procent = 0;
                for (int i = 0; i <= dataGridView2.Rows.Count - 1; i++)
                {
                    if (dataGridView2.Rows[i].Cells[1].Value.ToString() != string.Empty)
                    {
                        procent += Convert.ToDecimal(dataGridView2.Rows[i].Cells[1].Value);

                        if (OnlyNumber(dataGridView2.Rows[i].Cells[1].Value.ToString()) == false)
                        {
                            string message = Messages.incorrectFormat;
                            errorProvider.SetError(dataGridView2, message);
                            cancelEvent.Cancel = false;
                        }
                        else
                        {
                            cancelEvent.Cancel = true;
                        }

                        if (dataGridView2.Rows[i].Cells[0].Value.ToString() == string.Empty || dataGridView2.Rows[i].Cells[1].Value == null)
                        {
                            string message = Messages.incorrectFormat;
                            errorProvider.SetError(dataGridView2, message);
                            cancelEvent.Cancel = false;
                        }
                        else
                        {
                            cancelEvent.Cancel = true;
                        }
                        foreach (char character in dataGridView2.Rows[i].Cells[0].Value.ToString())
                        {
                            if (char.IsLetter(character)==false && char.IsDigit(character)==false)
                            {
                                string message = Messages.incorrectFormat;
                                errorProvider.SetError(dataGridView2, message);
                                cancelEvent.Cancel = false;
                            }
                            else if (char.IsLetter(character) == true && char.IsDigit(character) == true)
                            {
                                cancelEvent.Cancel = true;
                            }
                        }
                        if (((Convert.ToDecimal(dataGridView2.Rows[i].Cells[1].Value) > 100 || Convert.ToDecimal(dataGridView2.Rows[i].Cells[1].Value) < 0)))
                        {
                            string message = Messages.incorrectFormat;
                            errorProvider.SetError(dataGridView2, message);
                            cancelEvent.Cancel = false;
                        }
                        else
                        {
                            cancelEvent.Cancel = true;
                        }
                    }
                }
                if (procent != 100)
                {
                    string message = Messages.allocationProcent;
                    errorProvider.SetError(dataGridView2, message);
                    cancelEvent.Cancel = false;
                }
                else
                {
                    cancelEvent.Cancel = true;
                }
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