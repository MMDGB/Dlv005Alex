using Dlv005_BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Dlv005_UI
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Dlv005View : Form
    {
        public delegate void CallSelectionTableDelegate(SelectionTablesUsed tablesUsed, List<KeyValuePair<decimal, string>> keyValuePairs, Dlv005View view);

        public enum SelectionTablesUsed
        { BD12, BD09, BD06 };

        private int selectionTableAuxiliarIndex;
        private int rowIndex;
        public bool isNew = false;
        public bool isNewCopy = false;
        private Dlv005BusinessOperations businessOperations;
        private Dlv005Validations businessValidations = new Dlv005Validations();
        private List<KeyValuePair<decimal, string>> keyValuePairs;
        public DataRow newcopyrow;
        private int requestContor;
        private string testingNumber;
        private string auxStatusCheck;
        private int auxAllocRowMaxContor = 1;
        private bool gridHasRows;
        private bool isInEditMode = false;
        private int mousePositionX;
        private int mousePositionXForAllocation;
        private int mousePositionYForAllocation;
        private int numberOfCommisionsDisplayOnOverview;
        private int mousePositionY;
        private bool firstCommision;
        private bool isFirstLoadRoutesType = true;
        private bool isFirstLoadTestingType = true;
        private bool isFirstLoadSortTests = true;
        private bool isFirstLoadSpecialQualification = true;
        private bool isFirstLoadHVQualification = true;
        private bool isFirstLoadDrivingAuthorization = true;
        private bool isFirstLoadSeriesText = true;
        private bool isFirstLoadCustomerOE = true;
        private bool isFirstLoadCustomer = true;
        private bool isFirstLoadChief = true;
        private bool isFirstLoadEngineeringAST = true;
        private List<decimal> allocationDeletedRows = new List<decimal>();
        private bool isRequeestedInBasicData;
        private bool isConfirmedInBasicData;

        /// <summary>
        /// Initializes a new instance of the <see cref="Dlv005View"/> class.
        /// </summary>
        public Dlv005View()
        {
            InitializeComponent();
            InitializeEvents();
            InitializeValidations();
        }

        /// <summary>
        /// Set the combo-boxes null for new commission.
        /// </summary>
        private void SetEmptyComboBox()
        {
            DrivingAuthorizationComboBox.SelectedItem = null;
            SortTestsComboBox.SelectedItem = null;
            RoutesTypeComboBox.SelectedItem = null;
            SpecialQualificationComboBox.SelectedItem = null;
            HVQualificationComboBox.SelectedItem = null;
            TestingTypeComboBox.SelectedItem = null;
        }

        /// <summary>
        /// Sets the empty text box.
        /// </summary>
        private void SetEmptyTextBox()
        {
            TestingContentTextBox.Text = string.Empty;
            SeriesTextBox.Text = string.Empty;
            CustomerOETextBox.Text = string.Empty;
            CustomerTextBox.Text = string.Empty;
            ChiefTextBox.Text = string.Empty;
            EngineeringASTTextBox.Text = string.Empty;
            BasicDataNumberTextBox.Text = string.Empty;
        }

        ///
        /// <summary>
        /// Initialize all the interface events
        /// </summary>
        private void InitializeEvents()
        {
            this.Load += Dlv005View_Load;
            NewButton.Click += New_Click;
            NewCopyButton.Click += NewCopy_Click;
            RequestButton.Click += Request_Click;
            ConfirmButton.Click += ConfirmButton_Click;
            CustomerOEButton.Click += CustomerOE_Click;
            CustomerButton.Click += Customer_Click;
            ExitCancelButton.Click += ExitCancelButton_Click;
            SeriesButton.Click += SeriesButton_Click;
            ChiefButton.Click += ChiefButton_Click;
            EngineeringButton.Click += EngineeringButton_Click;
            SaveButton.Click += SaveButton_Click;
            Overview.Enter += Overview_Enter;
            TestingTypeComboBox.TextChanged += TestingTypeComboBox_TextChanged;
            RoutesTypeComboBox.TextChanged += RoutesTypeComboBox_TextChanged;
            SortTestsComboBox.TextChanged += SortTestsComboBox_TextChanged;
            DrivingAuthorizationComboBox.TextChanged += DrivingAuthorizationComboBox_TextChanged;
            SpecialQualificationComboBox.TextChanged += SpecialQualificationComboBox_TextChanged;
            HVQualificationComboBox.TextChanged += HVQualificationComboBox_TextChanged;
            DeleteButton.Click += DeleteButton_Click;
            includeSaturdayworkCheckBox.CheckedChanged += IncludeSaturdayworkCheckBox_CheckedChanged;
            includeSundayworkCheckBox.CheckedChanged += IncludeSundayworkCheckBox_CheckedChanged;
            dataGridView1.CellClick += DataGridView1_CellClick;
            HideRequestedOnesCheckBox.CheckedChanged += HideRequestedOnesCheckBox_CheckedChanged;
            HideFinishedCheckBox.CheckedChanged += HideFinishedCheckBox_CheckedChanged;
            BasicData.Enter += BasicData_Enter;
            dataGridView2.KeyDown += DataGridView2_KeyDown;
            dataGridView2.CellValueChanged += DataGridView2_CellValueChanged;
            dataGridView1.MouseClick += DataGridView1_MouseClick;
            dataGridView1.MouseDoubleClick += DataGridView1_MouseDoubleClick;
            TestingContentTextBox.TextChanged += TestingContentTextBox_TextChanged;
            dataGridView2.MouseClick += DataGridView2_MouseClick;
            FromDateTimePicker.ValueChanged += FromDateTimePicker_ValueChanged;
            ToDateTimePicker.ValueChanged += ToDateTimePicker_ValueChanged;
            SeriesTextBox.TextChanged += SeriesTextBox_TextChanged;
            CustomerOETextBox.TextChanged += CustomerOETextBox_TextChanged;
            CustomerTextBox.TextChanged += CustomerTextBox_TextChanged;
            ChiefTextBox.TextChanged += ChiefTextBox_TextChanged;
            EngineeringASTTextBox.TextChanged += EngineeringASTTextBox_TextChanged;
            dataGridView2.UserDeletingRow += DataGridView2_UserDeletingRow;
            dataGridView2.UserAddedRow += DataGridView2_UserAddedRow;
        }

        /// <summary>
        /// Prepare a new row for allocation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView2_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            allocationBindingSource.EndEdit();
            if (isNew || isNewCopy)
            {
                businessOperations.CreateNewAllocationRowIfNeededForNew();
            }
            else
            {
                businessOperations.CreateNewAllocationRowIfNeededDForUpdate(((DataRowView)overviewBindingSource.Current).Row as Dlv005DataSet.MainTableRow);
            }
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.Rows[0].Selected = true;
            dataGridView2.CurrentCell = dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[0];
            businessOperations.Dlv005DataSet.AllocationGridTable.Rows[businessOperations.Dlv005DataSet.AllocationGridTable.Rows.Count - 2].Delete();
        }

        /// <summary>
        ///Delete an existing empty row from allocation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView2_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            EnableEditMode(true);
            decimal deletedRowId = Convert.ToDecimal(e.Row.Cells[2].Value);
            allocationDeletedRows.Add(deletedRowId);
        }

        /// <summary>
        ///Set "To" the datetime picker empty for new and newcopy opperations.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            SetEmptyToDateTimePicker("dd/MM/yyyy");
            if (overviewBindingSource.Position < 0)
            {
                return;
            }
            else
            {
                Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();
                row.DL31_ENDE_DATUM = ToDateTimePicker.Value;
            }
        }

        /// <summary>
        ///Set "From" the datetime picker empty for new and newcopy opperations.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FromDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            SetEmptyFromDateTimePicker("dd/MM/yyyy");
            if (overviewBindingSource.Position < 0)
            {
                return;
            }
            else
            {
                Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();
                row.DL31_START_DATUM = FromDateTimePicker.Value;
            }
        }

        /// <summary>
        /// Create a context menui for allocation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
            mousePositionXForAllocation = e.X;
            mousePositionYForAllocation = e.Y;
            if (e.Button == MouseButtons.Right && (dataGridView2.HitTest(e.X, e.Y).RowIndex) >= 0)
            {
                dataGridView2.ClearSelection();
                dataGridView2.Rows[dataGridView2.HitTest(e.X, e.Y).RowIndex].Selected = true;
                ContextMenu contextMenu = new ContextMenu();
                contextMenu.MenuItems.Add(new MenuItem("Copy Row", CopyRowDataForAllocation));
                contextMenu.MenuItems.Add(new MenuItem("Copy Column", CopyColumnDataForAllocation));
                contextMenu.Show(dataGridView2, new Point(e.X, e.Y));
            }
        }

        /// <summary>
        /// Copy the cell value from allocation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyColumnDataForAllocation(object sender, EventArgs e)
        {
            dataGridView2.CurrentCell = dataGridView2.Rows[dataGridView2.HitTest(mousePositionXForAllocation, mousePositionYForAllocation).RowIndex].Cells[dataGridView2.HitTest(mousePositionXForAllocation, mousePositionYForAllocation).ColumnIndex];
            if (dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                try
                {
                    Clipboard.SetDataObject(dataGridView2.CurrentCell.Value.ToString());
                    Clipboard.GetText();
                }
                catch (System.Runtime.InteropServices.ExternalException)
                {
                    MessageBox.Show("There was an error to your request!");
                }
            }
        }

        /// <summary>
        /// Copy the selected row from allocation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyRowDataForAllocation(object sender, EventArgs e)
        {
            if (dataGridView2.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                try
                {
                    Clipboard.SetDataObject(dataGridView2.GetClipboardContent());
                    Clipboard.GetText();
                }
                catch (System.Runtime.InteropServices.ExternalException)
                {
                    MessageBox.Show("There was an error to your request!");
                }
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the TestingContentTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TestingContentTextBox_TextChanged(object sender, EventArgs e)
        {
            if (overviewBindingSource.Position < 0)
            {
                return;
            }
            else if (overviewBindingSource.Position >= 0)
            {
                Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();
                row.DL31_ERPROBUNGSINHALT = TestingContentTextBox.Text;
            }
        }

        /// <summary>
        /// Send commision to basic data tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (overviewBindingSource.Position < 0)
            {
                return;
            }
            if (dataGridView1.HitTest(e.X, e.Y).RowIndex >= 0)
            {
                dataGridView1.Rows[dataGridView1.HitTest(e.X, e.Y).RowIndex].Selected = true;
            }
            else
            {
                return;
            }
            tabControl.SelectedTab = BasicData;
        }

        /// <summary>
        ///Create a context menui for overview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            mousePositionX = e.X;
            mousePositionY = e.Y;
            if (e.Button == MouseButtons.Right && (dataGridView1.HitTest(e.X, e.Y).RowIndex) >= 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[dataGridView1.HitTest(e.X, e.Y).RowIndex].Selected = true;
                ContextMenu contextMenu = new ContextMenu();
                contextMenu.MenuItems.Add(new MenuItem("Copy Row", CopyRowData));
                contextMenu.MenuItems.Add(new MenuItem("Copy Column", CopyColumnData));
                contextMenu.Show(dataGridView1, new Point(e.X, e.Y));
            }
        }

        /// <summary>
        /// Copy the selected row from overview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyRowData(object sender, EventArgs e)
        {
            if (dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                try
                {
                    Clipboard.SetDataObject(dataGridView1.GetClipboardContent());
                    Clipboard.GetText();
                }
                catch (System.Runtime.InteropServices.ExternalException)
                {
                    MessageBox.Show("There was an error to your request!");
                }
            }
        }

        /// <summary>
        /// Copy the cell value from overview.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyColumnData(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.HitTest(mousePositionX, mousePositionY).RowIndex].Cells[dataGridView1.HitTest(mousePositionX, mousePositionY).ColumnIndex];
            if (dataGridView1.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                try
                {
                    Clipboard.SetDataObject(dataGridView1.CurrentCell.Value.ToString());
                    Clipboard.GetText();
                }
                catch (System.Runtime.InteropServices.ExternalException)
                {
                    MessageBox.Show("There was an error to your request!");
                }
            }
        }

        /// <summary>
        /// Enable EditMode if allocation is moddified.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isNew == false && tabControl.SelectedTab == BasicData)
            {
                EnableEditMode(true);
            }
        }

        /// <summary>
        /// Adds a new row in Allocation grid table when key down is pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void DataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (isInEditMode && dataGridView2.AllowUserToAddRows==false)
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Cells[0].Value.ToString() == string.Empty || row.Cells[1].Value.ToString() == string.Empty)
                    {
                        auxAllocRowMaxContor = 0;
                        break;
                    }
                    else
                    {
                        auxAllocRowMaxContor = 1;
                    }
                }
            }
           
            if (e.KeyData == Keys.Down && auxAllocRowMaxContor == 1)
            {
                if (isNew || isNewCopy)
                {
                    businessOperations.CreateNewAllocationRowIfNeededForNew();
                }
                else
                {
                    businessOperations.CreateNewAllocationRowIfNeededDForUpdate(((DataRowView)overviewBindingSource.Current).Row as Dlv005DataSet.MainTableRow);
                }
            }
            else
            {
                return;
            }
           
        }

        /// <summary>
        /// Enables the correct buttons.
        /// </summary>
        private void EnableCorrectButtons()
        {
            ConfirmButton.Enabled = false;
            DeleteButton.Enabled = false;
            RequestButton.Enabled = false;
            NewCopyButton.Enabled = false;
            ExitCancelButton.Text = "Close";
        }

        /// <summary>
        /// Sets the correct value for the testing number.
        /// </summary>
        private void InitializeTestingNumber()
        {
            if (overviewBindingSource.Position < 0)
            {
                EnableCorrectButtons();
                requestContor = 0;
            }
            else
            {
                var auxRequestContor = 0;
                foreach (Dlv005DataSet.MainTableRow row in businessOperations.Dlv005DataSet.MainTable)
                {
                    if (row.DL31_KOMM_ANFORDERUNG_NR != string.Empty)
                    {
                        requestContor = Convert.ToInt16(row.DL31_KOMM_ANFORDERUNG_NR.Substring(row.DL31_KOMM_ANFORDERUNG_NR.Length - 3));
                        if (requestContor >= auxRequestContor)
                        {
                            auxRequestContor = requestContor;
                        }
                    }
                }
                requestContor = auxRequestContor;
            }
        }

        /// <summary>
        /// Resets the data when cancel or enter basic data.
        /// </summary>
        private void ResetDataWhenCancelOrEnterBasicData()
        {
            var position = overviewBindingSource.Position;

            if (position >= 0)
            {
                if (isRequeestedInBasicData)
                {
                    var row = businessOperations.Dlv005DataSet.MainTable.Where(r => r.DL31_KOMM_ANFORDERUNG_NR == testingNumber).First();
                    DisplayRowInBasicData(row);
                }
                else
                {
                    DisplayRowInBasicData(GetTheCorrectActiveRow() as Dlv005DataSet.MainTableRow);
                }
            }
        }

        /// <summary>
        /// Display correct data from selected row in basic data tab.
        /// </summary>
        /// <param name="row"></param>
        private void DisplayRowInBasicData(Dlv005DataSet.MainTableRow row)
        {
            CheckForStatusChange();
            if (isNew)
            {
                allocationBindingSource.Filter = string.Format("DL32_EXT_KOMM_ANFORDERUNG_ID= {0}", -1);
            }
            else
            {
                allocationBindingSource.Filter = string.Concat(String.Format("DL32_EXT_KOMM_ANFORDERUNG_ID= {0}",
                    Convert.ToInt16(row.DL31_KOMM_ANFORDERUNG_ID)), " OR ", String.Format("DL32_EXT_KOMM_ANFORDERUNG_ID= {0}", -1));
            }

            if (isNew == false && (row.DL31_SAMSTAGSARBEIT).ToString() == "j")
            {
                includeSaturdayworkCheckBox.Checked = true;
                row.AcceptChanges();
            }
            else
            {
                includeSaturdayworkCheckBox.Checked = false;
                row.AcceptChanges();
            }
            if (isNew == false && (row.DL31_SONNTAGSARBEIT).ToString() == "j")
            {
                includeSundayworkCheckBox.Checked = true;
                row.AcceptChanges();
            }
            else
            {
                includeSundayworkCheckBox.Checked = false;
                row.AcceptChanges();
            }
            if (row.RowState != DataRowState.Added)
            {
                SortTestsComboBox.Text = businessOperations.Dlv005DataSet.DL38Table.
                    Where(r => r.DL38_KOMM_ERPROBUNGSORT_ID == row.DL31_KOMM_ERPROBUNGSORT_ID).
                    Select(r => r.DL38_BEZEICHNUNG).ToArray()[0];

                RoutesTypeComboBox.Text = businessOperations.Dlv005DataSet.DL39Table.
                    Where(r => r.DL39_KOMM_STRECKENART_ID == row.DL31_KOMM_STRECKENART_ID).
                    Select(r => r.DL39_BEZEICHNUNG).ToArray()[0];

                TestingTypeComboBox.Text = businessOperations.Dlv005DataSet.DL40Table.
                    Where(r => r.DL40_KOMM_ERPROBUNGSART_ID == row.DL31_KOMM_ERPROBUNGSART_ID).
                    Select(r => r.DL40_BEZEICHNUNG).ToArray()[0];

                DrivingAuthorizationComboBox.Text = businessOperations.Dlv005DataSet.SD111Table.
                    Where(r => r.SD111_QUALIFIKATIONEN_ID == row.DL31_FAHRBERECHTIGUNG_ID).
                    Select(r => r.SD111_WERT).ToArray()[0];

                HVQualificationComboBox.Text = businessOperations.Dlv005DataSet.SD111Table.
                    Where(r => r.SD111_QUALIFIKATIONEN_ID == row.DL31_HV_QUALIFIKATION_ID).
                    Select(r => r.SD111_WERT).ToArray()[0];

                SpecialQualificationComboBox.Text = businessOperations.Dlv005DataSet.SD111Table.
                    Where(r => r.SD111_QUALIFIKATIONEN_ID == row.DL31_SONDERQUALIFIKATION_ID).
                    Select(r => r.SD111_WERT).ToArray()[0];

                CustomerOETextBox.Text = businessOperations.Dlv005DataSet.BD06Table.
                   Where(r => r.BD06_OE == row.DL31_AUFTRAGGEBER_OE).
                   Select(r => r.BD06_KURZ_BEZ).ToArray()[0];

                CustomerTextBox.Text = businessOperations.Dlv005DataSet.BD09Table.
                    Where(r => r.BD09_PERSID == row.DL31_AUFTRAGGEBER_PERSID).
                    Select(r => (r.BD09_NAME + "," + r.BD09_VORNAME + " " + r.BD09_OE_KURZ_BEZ)).ToArray()[0];

                ChiefTextBox.Text = businessOperations.Dlv005DataSet.BD09Table.
                    Where(r => r.BD09_PERSID == row.DL31_FAHRTENLEITER_PERSID).
                    Select(r => (r.BD09_NAME + "," + r.BD09_VORNAME + " " + r.BD09_OE_KURZ_BEZ)).ToArray()[0];

                EngineeringASTTextBox.Text = businessOperations.Dlv005DataSet.BD09Table.
                    Where(r => r.BD09_PERSID == row.DL31_ENGINEERING_AST_PERSID).
                    Select(r => (r.BD09_NAME + "," + r.BD09_VORNAME + " " + r.BD09_OE_KURZ_BEZ)).ToArray()[0];

                SeriesTextBox.Text = businessOperations.Dlv005DataSet.MainTable.
                    Where(r => r.DL31_KOMM_ANFORDERUNG_ID == row.DL31_KOMM_ANFORDERUNG_ID).
                    Select(r => r.DL31_BAUREIHEN).ToArray()[0];

                StatusTextBox.Text = businessOperations.Dlv005DataSet.MainTable.
                     Where(r => r.DL31_KOMM__STATUS_ID == row.DL31_KOMM__STATUS_ID).
                     Select(r => r.DL37_BEZEICHNUNG).ToArray()[0];

                BasicDataNumberTextBox.Text = businessOperations.Dlv005DataSet.MainTable.
                     Where(r => r.DL31_KOMM_ANFORDERUNG_ID == row.DL31_KOMM_ANFORDERUNG_ID).
                     Select(r => r.DL31_KOMM_ANFORDERUNG_NR).ToArray()[0];

                TestingContentTextBox.Text = businessOperations.Dlv005DataSet.MainTable.
                     Where(r => r.DL31_KOMM_ANFORDERUNG_ID == row.DL31_KOMM_ANFORDERUNG_ID).
                     Select(r => r.DL31_ERPROBUNGSINHALT).ToArray()[0];

                FromDateTimePicker.Text = (businessOperations.Dlv005DataSet.MainTable.
                     Where(r => r.DL31_KOMM_ANFORDERUNG_ID == row.DL31_KOMM_ANFORDERUNG_ID).
                     Select(r => r.DL31_START_DATUM).ToArray()[0]).ToString();

                ToDateTimePicker.Text = (businessOperations.Dlv005DataSet.MainTable.
                    Where(r => r.DL31_KOMM_ANFORDERUNG_ID == row.DL31_KOMM_ANFORDERUNG_ID).
                     Select(r => r.DL31_ENDE_DATUM).ToArray()[0]).ToString();

                row.AcceptChanges();
            }
        }

        /// <summary>
        /// Disable Basic data tab when there are no commission.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BasicData_Enter(object sender, EventArgs e)
        {
            if (overviewBindingSource.Position < 0 && isNew == false)
            {
                tabControl.SelectedTab = Overview;
            }
            if (isInEditMode)
            {
                tabControl.SelectedTab = BasicData;
                return;
            }
            ResetDataWhenCancelOrEnterBasicData();
            if (isNew || isNewCopy)
            {
                foreach (Control c in BasicData.Controls)
                {
                    c.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Sorts the overview grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HideFinishedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (HideFinishedCheckBox.Checked && HideRequestedOnesCheckBox.Checked == false)
            {
                overviewBindingSource.Filter = "DL37_BEZEICHNUNG <> 'Confirmed'";
            }
            else if (HideFinishedCheckBox.Checked && HideRequestedOnesCheckBox.Checked)
            {
                overviewBindingSource.Filter = "DL37_BEZEICHNUNG <> 'Confirmed' AND  DL37_BEZEICHNUNG <> 'Requested' ";
            }
            else if (HideFinishedCheckBox.Checked == false && HideRequestedOnesCheckBox.Checked)
            {
                overviewBindingSource.Filter = "DL37_BEZEICHNUNG <> 'Requested'";
            }
            else
            {
                overviewBindingSource.Filter = null;
            }
            if (overviewBindingSource.Position >= 0)
            {
                dataGridView1.Rows[0].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[1];
            }
            numberOfCommisionsDisplayOnOverview = dataGridView1.RowCount;
            OverviewNumbetTextBox.Text = numberOfCommisionsDisplayOnOverview.ToString();
            DisableButtonsWhenNoComissions();
            CheckForStatusChange();
        }

        /// <summary>
        /// Disable unnecesary buttons if there is no commiosion displayed.
        /// </summary>
        public void DisableButtonsWhenNoComissions()
        {
            if (numberOfCommisionsDisplayOnOverview <= 0 && tabControl.SelectedTab == Overview)
            {
                DeleteButton.Enabled = false;
                ConfirmButton.Enabled = false;
                RequestButton.Enabled = false;
                NewCopyButton.Enabled = false;
            }
            else
            {
                DeleteButton.Enabled = true;
                ConfirmButton.Enabled = true;
                RequestButton.Enabled = true;
                NewCopyButton.Enabled = true;
            }
        }

        /// <summary>
        /// Sorts the overview grid.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HideRequestedOnesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (HideFinishedCheckBox.Checked && HideRequestedOnesCheckBox.Checked == false)
            {
                overviewBindingSource.Filter = "DL37_BEZEICHNUNG <> 'Confirmed'";
            }
            else if (HideFinishedCheckBox.Checked && HideRequestedOnesCheckBox.Checked)
            {
                overviewBindingSource.Filter = "DL37_BEZEICHNUNG <> 'Confirmed' AND  DL37_BEZEICHNUNG <> 'Requested' ";
            }
            else if (HideFinishedCheckBox.Checked == false && HideRequestedOnesCheckBox.Checked)
            {
                overviewBindingSource.Filter = "DL37_BEZEICHNUNG <> 'Requested'";
            }
            else
            {
                overviewBindingSource.Filter = null;
            }
            if (overviewBindingSource.Position >= 0)
            {
                dataGridView1.Rows[0].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[1];
            }
            numberOfCommisionsDisplayOnOverview = dataGridView1.RowCount;
            OverviewNumbetTextBox.Text = numberOfCommisionsDisplayOnOverview.ToString();
            DisableButtonsWhenNoComissions();
            CheckForStatusChange();
        }

        /// <summary>
        /// Selects the correct row for status check.
        /// </summary>
        /// <param name="dataRow">The data row.</param>
        /// <returns></returns>
        private string SelectCorrectRowForStatusCheck(DataRow dataRow)
        {
            return auxStatusCheck = dataRow["DL37_BEZEICHNUNG"].ToString();
        }

        private void CheckForStatusChangeInOverview()
        {
            if (overviewBindingSource.Position >= 0)
            {
                SelectCorrectRowForStatusCheck((dataGridView1.Rows[0].DataBoundItem as DataRowView).Row);

                if (auxStatusCheck == "Confirmed" && tabControl.SelectedTab == Overview)
                {
                    RequestButton.Enabled = false;
                    ConfirmButton.Enabled = false;
                    DeleteButton.Enabled = false;
                }
                if (auxStatusCheck == "Unchecked")
                {
                    RequestButton.Enabled = true;
                    ConfirmButton.Enabled = false;
                    DeleteButton.Enabled = true;
                }
                if (auxStatusCheck == "Requested")
                {
                    RequestButton.Enabled = false;
                    ConfirmButton.Enabled = true;
                    DeleteButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Check the commision status and enable/disable needed buttons.
        /// </summary>
        private void CheckForStatusChange()
        {
            if (overviewBindingSource.Position >= 0)
            {
                if (isRequeestedInBasicData)
                {
                    auxStatusCheck = "Requested";
                }
                if (isConfirmedInBasicData)
                {
                    auxStatusCheck = "Confirmed";
                }
                if (!isRequeestedInBasicData && !isConfirmedInBasicData)
                {
                    SelectCorrectRowForStatusCheck(DuplicateSelectedRow());
                }

                if (auxStatusCheck == "Confirmed" && tabControl.SelectedTab == BasicData)
                {
                    RequestButton.Enabled = false;
                    ConfirmButton.Enabled = false;
                    DeleteButton.Enabled = false;

                    foreach (Control c in BasicData.Controls)
                    {
                        c.Enabled = false;
                    }
                }
                else if (auxStatusCheck != "Confirmed" && tabControl.SelectedTab == BasicData)
                {
                    RequestButton.Enabled = true;
                    ConfirmButton.Enabled = true;
                    DeleteButton.Enabled = true;

                    foreach (Control c in BasicData.Controls)
                    {
                        c.Enabled = true;
                    }
                }
                if (auxStatusCheck == "Confirmed" && tabControl.SelectedTab == Overview)
                {
                    RequestButton.Enabled = false;
                    ConfirmButton.Enabled = false;
                    DeleteButton.Enabled = false;
                }
                if (auxStatusCheck == "Unchecked")
                {
                    RequestButton.Enabled = true;
                    ConfirmButton.Enabled = false;
                    DeleteButton.Enabled = true;
                }
                if (auxStatusCheck == "Requested")
                {
                    RequestButton.Enabled = false;
                    ConfirmButton.Enabled = true;
                    DeleteButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Enable/disable buttons depending of the status.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CheckForStatusChange();
        }

        /// <summary>
        /// Set value for checkbok.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IncludeSundayworkCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (overviewBindingSource.Position < 0)
            {
                return;
            }
            Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();
            if (includeSundayworkCheckBox.Checked)
            {
                row.DL31_SONNTAGSARBEIT = "j";
            }
            else
            {
                row.DL31_SONNTAGSARBEIT = "n";
            }
        }

        /// <summary>
        /// Set value for checkbok.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IncludeSaturdayworkCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (overviewBindingSource.Position < 0)
            {
                return;
            }
            Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();
            if (includeSaturdayworkCheckBox.Checked)
            {
                row.DL31_SAMSTAGSARBEIT = "j";
            }
            else
            {
                row.DL31_SAMSTAGSARBEIT = "n";
            }
        }

        /// <summary>
        /// Delete selected commission when delete button is pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void DeleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(ErrorMessage.makeSureWannaDelete, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();
                var deleteID = row.DL31_KOMM_ANFORDERUNG_ID;
                businessOperations.DeleteDataDl32(deleteID);
                businessOperations.DeleteData(deleteID);
                CheckForExistingRows();
                if (tabControl.SelectedTab == BasicData)
                {
                    tabControl.SelectedTab = Overview;
                    tabControl.SelectedTab = BasicData;
                }
                else
                {
                    businessOperations.ReloadAllocation();
                    businessOperations.Reload();
                }
                numberOfCommisionsDisplayOnOverview = dataGridView1.RowCount;
                OverviewNumbetTextBox.Text = numberOfCommisionsDisplayOnOverview.ToString();
                DisableButtonsWhenNoComissions();
            }
        }

        /// <summary>
        /// Enables the edit mode buttons.
        /// </summary>
        private void EnableEditMode(bool state)
        {
            ExitCancelButton.Text = (state == true) ? "Cancel" : "Close";
            isInEditMode = state;
            DeleteButton.Visible = !state;
            NewButton.Visible = !state;
            NewCopyButton.Visible = !state;
            ConfirmButton.Visible = !state;
            RequestButton.Visible = !state;
            SaveButton.Visible = state;
        }

        /// <summary>
        /// Disables the edit mode buttons.
        /// </summary>
        private void DisableEditMode()
        {
            SaveButton.Visible = false;
            NewButton.Visible = true;
            NewCopyButton.Visible = true;
            DeleteButton.Visible = true;
            RequestButton.Visible = true;
            ConfirmButton.Visible = true;
            ExitCancelButton.Text = "Close";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeriesTextBox_TextChanged(object sender, EventArgs e)
        {
            Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();
            string textToCheck = SeriesTextBox.Text;
            if (textToCheck == string.Empty && !isFirstLoadSeriesText)
            {
                EnableEditMode(true);
            }
            row.DL31_BAUREIHEN = textToCheck;
            if (!isFirstLoadSeriesText)
            {
                EnableEditMode(true);
            }
            isFirstLoadSeriesText = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerOETextBox_TextChanged(object sender, EventArgs e)
        {
            Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();
            string textToCheck = CustomerOETextBox.Text.ToUpper();
            if (textToCheck == string.Empty && !isFirstLoadCustomerOE)
            {
                EnableEditMode(true);
            }
            switch (textToCheck)
            {
                case "RD/AST":
                    row.DL31_AUFTRAGGEBER_OE = 1;
                    break;

                case "RD/BP":
                    row.DL31_AUFTRAGGEBER_OE = 2;
                    break;

                case "ITP/DT":
                    row.DL31_AUFTRAGGEBER_OE = 3;
                    break;

                default:
                    break;
            }
            if (!isFirstLoadCustomerOE)
            {
                EnableEditMode(true);
            }
            isFirstLoadCustomerOE = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerTextBox_TextChanged(object sender, EventArgs e)
        {
            Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();
            string textToCheck = CustomerTextBox.Text.ToUpper();
            if (textToCheck == string.Empty && !isFirstLoadCustomer)
            {
                EnableEditMode(true);
            }
            switch (textToCheck)
            {
                case "TEODORA,DICOIU RD/AST":
                    row.DL31_AUFTRAGGEBER_PERSID = 2;
                    break;

                case "DENIS,MARCHIS ITP/DT":
                    row.DL31_AUFTRAGGEBER_PERSID = 1;
                    break;

                case "ALEX,FLESHER RD/BP":
                    row.DL31_AUFTRAGGEBER_PERSID = 3;
                    break;

                default:
                    break;
            }
            if (!isFirstLoadCustomer)
            {
                EnableEditMode(true);
            }
            isFirstLoadCustomer = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChiefTextBox_TextChanged(object sender, EventArgs e)
        {
            Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();
            string textToCheck = ChiefTextBox.Text.ToUpper();
            if (textToCheck == string.Empty && !isFirstLoadCustomer)
            {
                EnableEditMode(true);
            }
            switch (textToCheck)
            {
                case "TEODORA,DICOIU RD/AST":
                    row.DL31_FAHRTENLEITER_PERSID = 2;
                    break;

                case "DENIS,MARCHIS ITP/DT":
                    row.DL31_FAHRTENLEITER_PERSID = 1;
                    break;

                case "ALEX,FLESHER RD/BP":
                    row.DL31_FAHRTENLEITER_PERSID = 3;
                    break;

                default:
                    break;
            }
            if (!isFirstLoadChief)
            {
                EnableEditMode(true);
            }
            isFirstLoadChief = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EngineeringASTTextBox_TextChanged(object sender, EventArgs e)
        {
            Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();
            string textToCheck = EngineeringASTTextBox.Text.ToUpper();
            if (textToCheck == string.Empty && !isFirstLoadEngineeringAST)
            {
                EnableEditMode(true);
            }
            switch (textToCheck)
            {
                case "TEODORA,DICOIU RD/AST":
                    row.DL31_ENGINEERING_AST_PERSID = 2;
                    break;

                case "DENIS,MARCHIS ITP/DT":
                    row.DL31_ENGINEERING_AST_PERSID = 1;
                    break;

                case "ALEX,FLESHER RD/BP":
                    row.DL31_ENGINEERING_AST_PERSID = 3;
                    break;

                default:
                    break;
            }
            if (!isFirstLoadEngineeringAST)
            {
                EnableEditMode(true);
            }
            isFirstLoadEngineeringAST = false;
        }

        #region ComboBox TextChange Events

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestingTypeComboBox_TextChanged(object sender, EventArgs e)
        {
            Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();
            string textToCheck = TestingTypeComboBox.Text.ToUpper();
            if (textToCheck == string.Empty && !isFirstLoadTestingType)
            {
                EnableEditMode(true);
            }
            switch (textToCheck)
            {
                case "EXAM":
                    row.DL31_KOMM_ERPROBUNGSART_ID = 1;
                    break;

                case "WORLD-DL":
                    row.DL31_KOMM_ERPROBUNGSART_ID = 2;
                    break;

                case "FULL LOAD DL":
                    row.DL31_KOMM_ERPROBUNGSART_ID = 3;
                    break;

                case "E/E":
                    row.DL31_KOMM_ERPROBUNGSART_ID = 4;
                    break;

                case "DRIVING ASSISTANCE":
                    row.DL31_KOMM_ERPROBUNGSART_ID = 5;
                    break;

                case "DRIVING DYNAMICS":
                    row.DL31_KOMM_ERPROBUNGSART_ID = 6;
                    break;

                case "RAFF-DL":
                    row.DL31_KOMM_ERPROBUNGSART_ID = 7;
                    break;

                default:
                    break;
            }
            if (!isFirstLoadTestingType)
            {
                EnableEditMode(true);
            }
            isFirstLoadRoutesType = false;
        }

        /// <summary>
        ///Adds the key ID to the new row .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoutesTypeComboBox_TextChanged(object sender, EventArgs e)
        {
            Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();

            string textToCheck = RoutesTypeComboBox.Text.ToUpper();
            if (textToCheck == string.Empty && !isFirstLoadRoutesType)
            {
                EnableEditMode(true);
            }
            switch (textToCheck)
            {
                case "TEST AREA":
                    row.DL31_KOMM_STRECKENART_ID = 1;
                    break;

                case "PUBLIC ROAD":
                    row.DL31_KOMM_STRECKENART_ID = 2;
                    break;

                case "NÜRBURGRING":
                    row.DL31_KOMM_STRECKENART_ID = 3;
                    break;

                case "BAD ROAD":
                    row.DL31_KOMM_STRECKENART_ID = 4;
                    break;

                default:
                    break;
            }
            if (!isFirstLoadRoutesType)
            {
                EnableEditMode(true);
            }
            isFirstLoadRoutesType = false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortTestsComboBox_TextChanged(object sender, EventArgs e)
        {
            Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();
            string textToCheck = SortTestsComboBox.Text.ToUpper();
            if (textToCheck == string.Empty && !isFirstLoadSortTests)
            {
                EnableEditMode(true);
            }
            else if (textToCheck == "IMMENDINGEN")
            {
                row.DL31_KOMM_ERPROBUNGSORT_ID = 1;
            }
            if (!isFirstLoadSortTests)
            {
                EnableEditMode(true);
            }
            isFirstLoadSortTests = false;
        }

        /// <summary>
        /// Adds the HVQualificationComboBox key value in the new created row for insertion in DB.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpecialQualificationComboBox_TextChanged(object sender, EventArgs e)
        {
            Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();
            string textToCheck = SpecialQualificationComboBox.Text.ToUpper();
            if (textToCheck == string.Empty && !isFirstLoadSpecialQualification)
            {
                EnableEditMode(true);
            }
            switch (textToCheck)
            {
                case "OFFROAD":
                    row.DL31_SONDERQUALIFIKATION_ID = 7;
                    break;

                case "WINTER":
                    row.DL31_SONDERQUALIFIKATION_ID = 8;
                    break;

                case "BRENNSTOFFZELLE":
                    row.DL31_SONDERQUALIFIKATION_ID = 9;
                    break;

                case "ELEKTROANTRIEB":
                    row.DL31_SONDERQUALIFIKATION_ID = 10;
                    break;

                case "-":
                    row.DL31_SONDERQUALIFIKATION_ID = 11;
                    break;

                default:
                    break;
            }
            if (!isFirstLoadSpecialQualification)
            {
                EnableEditMode(true);
            }
            isFirstLoadSpecialQualification = false;
        }

        /// <summary>
        /// Adds the HVQualificationComboBox key value in the new created row for insertion in DB.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HVQualificationComboBox_TextChanged(object sender, EventArgs e)
        {
            Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();
            string textToCheck = HVQualificationComboBox.Text.ToUpper();
            if (textToCheck == string.Empty && !isFirstLoadHVQualification)
            {
                EnableEditMode(true);
            }
            switch (textToCheck)
            {
                case "HOCHVOLT 1":
                    row.DL31_HV_QUALIFIKATION_ID = 4;
                    break;

                case "HOCHVOLT 2":
                    row.DL31_HV_QUALIFIKATION_ID = 5;
                    break;

                case "HOCHVOLT 3":
                    row.DL31_HV_QUALIFIKATION_ID = 6;
                    break;

                default:
                    break;
            }
            if (!isFirstLoadHVQualification)
            {
                EnableEditMode(true);
            }
            isFirstLoadHVQualification = false;
        }

        /// <summary>
        /// Adds the DrivingAuthorizationComboBox key value in the new created row for insertion in DB.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrivingAuthorizationComboBox_TextChanged(object sender, EventArgs e)
        {
            Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();
            string textToCheck = DrivingAuthorizationComboBox.Text.ToUpper();
            if (textToCheck == string.Empty && !isFirstLoadDrivingAuthorization)
            {
                EnableEditMode(true);
            }
            switch (textToCheck)
            {
                case "T1":
                    row.DL31_FAHRBERECHTIGUNG_ID = 1;
                    break;

                case "T2":
                    row.DL31_FAHRBERECHTIGUNG_ID = 2;
                    break;

                case "T3":
                    row.DL31_FAHRBERECHTIGUNG_ID = 3;
                    break;

                default:
                    break;
            }
            if (!isFirstLoadDrivingAuthorization)
            {
                EnableEditMode(true);
            }
            isFirstLoadDrivingAuthorization = false;
        }

        /// <summary>
        ///
        /// </summary>
        private void InitializeValidations()
        {
            TestingContentTextBox.Validating += TestingContentTextBox_ValidateTestingContent;
            FromDateTimePicker.Validating += FromDateTimePicker_ValidateStartDate;
            ToDateTimePicker.Validating += ToDateTimePicker_ValidateEndDate;
            SortTestsComboBox.Validating += SortTestsComboBox_ValidateSortTests;
            RoutesTypeComboBox.Validating += RoutesTypeComboBox_ValidateRoutesType;
            TestingTypeComboBox.Validating += TestingTypeComboBox_ValidateTestingType;
            SeriesTextBox.Validating += SeriesTextBox_ValidateSeries;
            CustomerOETextBox.Validating += CustomerOETextBox_ValidateCustomerOE;
            CustomerTextBox.Validating += CustomerTextBox_Validating;
            ChiefTextBox.Validating += ChiefTextBox_ValidateChief;
            EngineeringASTTextBox.Validating += EngineeringASTTextBox_ValidateEngineeringAST;
            DrivingAuthorizationComboBox.Validating += DrivingAuthorizationComboBox_ValidateDrivingAuthorization;
            HVQualificationComboBox.Validating += HVQualificationComboBox_ValidateHVQualification;
            SpecialQualificationComboBox.Validating += SpecialQualificationComboBox_ValidateSpecialQualification;
            dataGridView2.Validating += DataGridView2_ValidateAllocationgrid;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView2_ValidateAllocationgrid(object sender, System.ComponentModel.CancelEventArgs e)
        {
            businessValidations.ValidateAllocationGrid(dataGridView2, AllocationErrorProvider, e, ErrorMessage.emptyMandatory, ErrorMessage.allocationProcent, ErrorMessage.incorrectFormat);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpecialQualificationComboBox_ValidateSpecialQualification(object sender, System.ComponentModel.CancelEventArgs e)
        {
            businessValidations.ValidateSpecialQualification(SpecialQualificationComboBox, AllocationErrorProvider, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HVQualificationComboBox_ValidateHVQualification(object sender, System.ComponentModel.CancelEventArgs e)
        {
            businessValidations.ValidateHVQualification(HVQualificationComboBox, AllocationErrorProvider, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrivingAuthorizationComboBox_ValidateDrivingAuthorization(object sender, System.ComponentModel.CancelEventArgs e)
        {
            businessValidations.ValidateDrivingAuthorization(DrivingAuthorizationComboBox, AllocationErrorProvider, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EngineeringASTTextBox_ValidateEngineeringAST(object sender, System.ComponentModel.CancelEventArgs e)
        {
            businessValidations.ValidateBD09SelectionTable(EngineeringButton, CustomerTextBox, AllocationErrorProvider, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChiefTextBox_ValidateChief(object sender, System.ComponentModel.CancelEventArgs e)
        {
            businessValidations.ValidateBD09SelectionTable(ChiefButton, CustomerTextBox, AllocationErrorProvider, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            businessValidations.ValidateBD09SelectionTable(CustomerButton, CustomerTextBox, AllocationErrorProvider, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerOETextBox_ValidateCustomerOE(object sender, System.ComponentModel.CancelEventArgs e)
        {
            businessValidations.ValidateCustomerOE(CustomerOEButton, CustomerOETextBox, AllocationErrorProvider, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeriesTextBox_ValidateSeries(object sender, System.ComponentModel.CancelEventArgs e)
        {
            businessValidations.ValidateSeries(SeriesButton, SeriesTextBox, AllocationErrorProvider, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestingTypeComboBox_ValidateTestingType(object sender, System.ComponentModel.CancelEventArgs e)
        {
            businessValidations.ValidateTestingType(TestingTypeComboBox, AllocationErrorProvider, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoutesTypeComboBox_ValidateRoutesType(object sender, System.ComponentModel.CancelEventArgs e)
        {
            businessValidations.ValidateRoutesType(RoutesTypeComboBox, AllocationErrorProvider, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortTestsComboBox_ValidateSortTests(object sender, System.ComponentModel.CancelEventArgs e)
        {
            businessValidations.ValidateSortTest(SortTestsComboBox, AllocationErrorProvider, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToDateTimePicker_ValidateEndDate(object sender, System.ComponentModel.CancelEventArgs e)
        {
            businessValidations.ValidateEndDate(ToDateTimePicker, FromDateTimePicker, AllocationErrorProvider, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FromDateTimePicker_ValidateStartDate(object sender, System.ComponentModel.CancelEventArgs e)
        {
            businessValidations.ValidateStartDate(FromDateTimePicker, AllocationErrorProvider, e);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestingContentTextBox_ValidateTestingContent(object sender, System.ComponentModel.CancelEventArgs e)
        {
            businessValidations.ValidateTestingContent(TestingContentTextBox, AllocationErrorProvider, e);
        }

        #endregion ComboBox TextChange Events

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private DataRow GetTheCorrectActiveRow()
        {
            BindingManagerBase bm = dataGridView1.BindingContext[dataGridView1.DataSource, dataGridView1.DataMember];
            DataRow row = ((DataRowView)bm.Current).Row;

            return row;
        }

        /// <summary>
        /// Handles the Click event of the SaveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void SaveButton_Click(object sender, EventArgs e)
        {
            if (ValidateChildren())
            {
                if (this.FromDateTimePicker.Value < DateTime.Now.AddDays(7))
                {
                    if (!(MessageBox.Show(ErrorMessage.shortTermDate, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                    {
                        return;
                    }
                }
                if (isInEditMode && !isNewCopy && !isNew)
                {
                    businessOperations.UpdateCommision(DuplicateSelectedRow());
                    foreach (decimal id in allocationDeletedRows)
                    {
                        businessOperations.DeleteDataDl32OnlyAllocation(id);
                    }
                    allocationDeletedRows.Clear();
                    DisableEditMode();
                    businessOperations.ReloadAllocation();
                }
                else
                {
                    businessOperations.SaveCommision(GetTheCorrectActiveRow());
                    businessOperations.Reload();
                    DisableEditMode();
                    tabControl.SelectedTab = Overview;
                    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
                    tabControl.SelectedTab = BasicData;
                    numberOfCommisionsDisplayOnOverview = dataGridView1.RowCount;
                    OverviewNumbetTextBox.Text = numberOfCommisionsDisplayOnOverview.ToString();
                    CheckForStatusChange();
                }
                isNew = false;
                isNewCopy = false;
                isInEditMode = false;
                dataGridView2.AllowUserToAddRows = true;
                Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();
                allocationBindingSource.Filter = String.Format(("DL32_EXT_KOMM_ANFORDERUNG_ID= {0}"), Convert.ToInt16(row.DL31_KOMM_ANFORDERUNG_ID));
            }
        }

        ///// <summary>
        ///// Handles the Enter event of the Overview control.
        ///// </summary>
        ///// <param name="sender">The source of the event.</param>
        ///// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Overview_Enter(object sender, EventArgs e)
        {
            if (isNew || isNewCopy || isInEditMode && tabControl.SelectedTab == Overview)
            {
                tabControl.SelectTab(BasicData);
            }
            else
            {
                businessOperations.Reload();
                businessOperations.ReloadAllocation();
                if (overviewBindingSource.Position >= 0)
                {
                    dataGridView1.Rows[0].Selected = true;
                    dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[1];
                }
            }
            DisableButtonsWhenNoComissions();
            CheckForStatusChangeInOverview();
            isConfirmedInBasicData = false;
            isRequeestedInBasicData = false;
        }

        /// <summary>
        /// Handles the Click event of the EngineeringButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void EngineeringButton_Click(object sender, EventArgs e)
        {
            selectionTableAuxiliarIndex = 3;
            Dlv005SelectionTableView form = new Dlv005SelectionTableView();
            keyValuePairs = new List<KeyValuePair<decimal, string>>();
            CallSelectionTableDelegate callSelectionTableDelegate = new CallSelectionTableDelegate(form.CallSelectionTable);
            callSelectionTableDelegate(SelectionTablesUsed.BD09, keyValuePairs, this);
            form.Show();
        }

        /// <summary>
        /// Handles the Click event of the ChiefButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ChiefButton_Click(object sender, EventArgs e)
        {
            selectionTableAuxiliarIndex = 2;
            Dlv005SelectionTableView form = new Dlv005SelectionTableView();
            keyValuePairs = new List<KeyValuePair<decimal, string>>();
            CallSelectionTableDelegate callSelectionTableDelegate = new CallSelectionTableDelegate(form.CallSelectionTable);
            callSelectionTableDelegate(SelectionTablesUsed.BD09, keyValuePairs, this);
            form.Show();
        }

        /// <summary>
        /// Handles the Click event of the SeriesButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SeriesButton_Click(object sender, EventArgs e)
        {
            Dlv005SelectionTableView form = new Dlv005SelectionTableView();
            keyValuePairs = new List<KeyValuePair<decimal, string>>();
            CallSelectionTableDelegate callSelectionTableDelegate = new CallSelectionTableDelegate(form.CallSelectionTable);
            callSelectionTableDelegate(SelectionTablesUsed.BD12, keyValuePairs, this);
            form.Show();
        }

        /// <summary>
        /// Display in basic data the selection made on selection table.
        /// </summary>
        /// <param name="keyValuePairs"></param>
        public void CallBackSelectionTable(SelectionTablesUsed tablesUsed, List<KeyValuePair<decimal, string>> keyValuePairs)
        {
            Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();
            switch (tablesUsed)
            {
                case SelectionTablesUsed.BD12:
                    SeriesTextBox.Text = keyValuePairs[0].Value;
                    row.DL31_BAUREIHEN = keyValuePairs[0].Value;
                    break;

                case SelectionTablesUsed.BD09:
                    if (selectionTableAuxiliarIndex == 1)
                    {
                        row.DL31_AUFTRAGGEBER_PERSID = keyValuePairs[0].Key;
                        CustomerTextBox.Text = keyValuePairs[0].Value;
                    }
                    else if (selectionTableAuxiliarIndex == 2)
                    {
                        row.DL31_FAHRTENLEITER_PERSID = keyValuePairs[0].Key;
                        ChiefTextBox.Text = keyValuePairs[0].Value;
                    }
                    else
                    {
                        row.DL31_ENGINEERING_AST_PERSID = keyValuePairs[0].Key;
                        EngineeringASTTextBox.Text = keyValuePairs[0].Value;
                    }
                    break;

                case SelectionTablesUsed.BD06:
                    row.DL31_AUFTRAGGEBER_OE = keyValuePairs[0].Key;
                    CustomerOETextBox.Text = keyValuePairs[0].Value;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Customer_Click(object sender, EventArgs e)
        {
            selectionTableAuxiliarIndex = 1;
            Dlv005SelectionTableView form = new Dlv005SelectionTableView();
            keyValuePairs = new List<KeyValuePair<decimal, string>>();
            CallSelectionTableDelegate callSelectionTableDelegate = new CallSelectionTableDelegate(form.CallSelectionTable);
            callSelectionTableDelegate(SelectionTablesUsed.BD09, keyValuePairs, this);
            form.Show();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerOE_Click(object sender, EventArgs e)
        {
            Dlv005SelectionTableView form = new Dlv005SelectionTableView();
            keyValuePairs = new List<KeyValuePair<decimal, string>>();
            CallSelectionTableDelegate callSelectionTableDelegate = new CallSelectionTableDelegate(form.CallSelectionTable);
            callSelectionTableDelegate(SelectionTablesUsed.BD06, keyValuePairs, this);
            form.Show();
        }

        /// <summary>
        /// Change the status from request to confirmed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            var updatedRow = DuplicateSelectedRow();
            if (tabControl.SelectedTab == BasicData)
            {
                isConfirmedInBasicData = true;
                updatedRow["DL31_KOMM__STATUS_ID"] = 3;
                businessOperations.UpdateCommision(updatedRow);
                businessOperations.ReloadOverview();
                ResetDataWhenCancelOrEnterBasicData();
            }
            else
            {
                updatedRow["DL31_KOMM__STATUS_ID"] = 3;
                businessOperations.UpdateCommision(updatedRow);
                businessOperations.ReloadOverview();
                CheckForStatusChange();
                DisableButtonsWhenNoComissions();
            }
            if (overviewBindingSource.Position >= 0)
            {
                dataGridView1.Rows[0].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[1];
            }
        }

        /// <summary>
        /// Change the status from confirmed to request.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Request_Click(object sender, EventArgs e)
        {
            var updatedRow = DuplicateSelectedRow();
            requestContor++;
            var requestContorString = string.Format("{0:000}", requestContor);
            var currentYear = DateTime.Now.ToString("yy");
            testingNumber = String.Concat(currentYear, "/", requestContorString);
            updatedRow["DL31_KOMM_ANFORDERUNG_NR"] = testingNumber;
            updatedRow["DL31_KOMM__STATUS_ID"] = 2;
            if (tabControl.SelectedTab == BasicData)
            {
                isRequeestedInBasicData = true;
                businessOperations.UpdateCommision(updatedRow);
                businessOperations.ReloadOverview();
                ResetDataWhenCancelOrEnterBasicData();
            }
            else
            {
                businessOperations.UpdateCommision(updatedRow);
                businessOperations.ReloadOverview();
                CheckForStatusChange();
                DisableButtonsWhenNoComissions();
            }
            if (overviewBindingSource.Position >= 0 && tabControl.SelectedTab == Overview)
            {
                dataGridView1.Rows[0].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[1];
            }
        }

        /// <summary>
        /// Checks for existing rows.
        /// </summary>
        private bool CheckForExistingRows()
        {
            if (overviewBindingSource.Position > 0)
            {
                gridHasRows = false;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Visible == true)
                    {
                        gridHasRows = true;
                    }
                }
                if (gridHasRows == false)
                {
                    EnableCorrectButtons();
                }
                return gridHasRows;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewCopy_Click(object sender, EventArgs e)
        {
            foreach (Control c in BasicData.Controls)
            {
                c.Enabled = true;
            }

            isNewCopy = true;
            tabControl.SelectTab(BasicData);
            businessOperations.CreateNewCopy(DuplicateSelectedRow(), isNewCopy);
            dataGridView1.RowsAdded += DataGridView1_RowsAdded;
            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
            BasicDataNumberTextBox.Text = string.Empty;
            SetEmptyFromDateTimePicker(" ");
            SetEmptyToDateTimePicker(" ");
            EnableEditMode(true);
        }

        /// <summary>
        /// Gets the selected row
        /// </summary>
        /// <returns></returns>
        public Dlv005DataSet.MainTableRow DuplicateSelectedRow()
        {
            if (overviewBindingSource.Position < 0)
            {
                return null;
            }
            else if (isRequeestedInBasicData)
            {
                var row = businessOperations.Dlv005DataSet.MainTable.Where(r => r.DL31_KOMM_ANFORDERUNG_NR == testingNumber).First();
                return row as Dlv005DataSet.MainTableRow;
            }
            else
            {
                return (dataGridView1.Rows[overviewBindingSource.Position].DataBoundItem as DataRowView).Row as Dlv005DataSet.MainTableRow;
            }
        }

        /// <summary>
        ///Change date time picker format.
        /// </summary>
        /// <param name="format"></param>
        private void SetEmptyFromDateTimePicker(string format)
        {
            FromDateTimePicker.CustomFormat = format;
            FromDateTimePicker.Format = DateTimePickerFormat.Custom;
        }

        /// <summary>
        /// Change date time picker format.
        /// </summary>
        /// <param name="format"></param>
        private void SetEmptyToDateTimePicker(string format)
        {
            ToDateTimePicker.CustomFormat = format;
            ToDateTimePicker.Format = DateTimePickerFormat.Custom;
        }

        /// <summary>
        /// Prepare the function for a new commision.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void New_Click(object sender, EventArgs e)
        {
            foreach (Control c in BasicData.Controls)
            {
                c.Enabled = true;
            }

            CheckForFirstCommision();
            isNew = true;
            includeSaturdayworkCheckBox.Checked = false;
            includeSundayworkCheckBox.Checked = false;
            tabControl.SelectTab(BasicData);
            dataGridView1.RowsAdded += DataGridView1_RowsAdded;
            businessOperations.CreateNew();
            allocationBindingSource.Filter = String.Format(("DL32_EXT_KOMM_ANFORDERUNG_ID= {0}"), -1);
            SetEmptyComboBox();
            SetEmptyTextBox();
            SetEmptyFromDateTimePicker(" ");
            SetEmptyToDateTimePicker(" ");
            dataGridView1.CurrentCell = dataGridView1.Rows[rowIndex].Cells[0];
            EnableEditMode(true);
        }

        /// <summary>
        /// Handles the RowsAdded event of the DataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewRowsAddedEventArgs"/> instance containing the event data.</param>
        private void DataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            rowIndex = e.RowIndex;
            dataGridView1.Rows[rowIndex].Selected = true;
        }

        /// <summary>
        /// Handles the Click event of the ExitCancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ExitCancelButton_Click(object sender, EventArgs e)
        {
            if (ExitCancelButton.Text == "Close")
            {
                Close();
            }
            AllocationErrorProvider.Clear();
            if (firstCommision)
            {
                isNew = false;
                firstCommision = false;
                businessOperations.CancelSave();
                tabControl.SelectedTab = Overview;
            }

            if (isNew || isNewCopy)
            {
                isNew = false;
                isNewCopy = false;
                businessOperations.CancelSave();
                tabControl.SelectedTab = Overview;
                DisableEditMode();
                SetEmptyFromDateTimePicker("dd/MM/yyyy");
                SetEmptyToDateTimePicker("dd/MM/yyyy");
                isInEditMode = false;
            }
            if (overviewBindingSource.Position <= 0)
            {
                businessOperations.CancelSave();
                CheckForExistingRows();
            }
            if (tabControl.SelectedIndex == 1 && isInEditMode)
            {
                businessOperations.CancelSave();
                ResetDataWhenCancelOrEnterBasicData();
                DisableEditMode();
                isNew = false;
                isNewCopy = false;
                isInEditMode = false;
            }
            else
            {
                businessOperations.CancelSave();
                ResetDataWhenCancelOrEnterBasicData();
            }
            dataGridView2.AllowUserToAddRows = true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dlv005View_Load(object sender, EventArgs e)
        {
            AutoValidate = AutoValidate.EnableAllowFocusChange;
            BindData();
            SetUpHeaderName();
            HideUnnecesaryColumns();
            InitializeTestingNumber();
            HideRequestedOnesCheckBox.Checked = true;
            HideFinishedCheckBox.Checked = true;

            ((DataTable)businessOperations.Dlv005DataSet.MainTable).RowChanged += Dlv005View_RowChanged;
        }

        /// <summary>
        /// Handles the RowChanged event of the Dlv005View control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataRowChangeEventArgs"/> instance containing the event data.</param>
        private void Dlv005View_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Row.RowState == DataRowState.Modified)
            {
                EnableEditMode(true);
            }
            else
            {
                if (isNew == false && isNewCopy == false)
                {
                    EnableEditMode(false);
                }
            }
        }

        /// <summary>
        ///Hide unnecessary columns from overview grid.
        /// </summary>
        private void HideUnnecesaryColumns()
        {
            for (int i = 10; i <= 30; i++)
            {
                dataGridView1.Columns[i].Visible = false;
            }
            dataGridView2.Columns[2].Visible = false;
            dataGridView2.Columns[3].Visible = false;
        }

        /// <summary>
        /// Bind data to grid views. ()
        /// </summary>
        private void BindData()
        {
            businessOperations = new Dlv005BusinessOperations();
            Dlv005DataSet dataSource = businessOperations.GetData();
            overviewBindingSource.DataSource = dataSource;
            dataGridView1.DataSource = overviewBindingSource;
            allocationBindingSource.DataSource = dataSource;
            dataGridView2.DataSource = allocationBindingSource;
            numberOfCommisionsDisplayOnOverview = dataGridView1.RowCount;
            OverviewNumbetTextBox.Text = numberOfCommisionsDisplayOnOverview.ToString();

            BindComboBoxes();
        }

        /// <summary>
        /// Return true case is first commision.
        /// </summary>
        private void CheckForFirstCommision()
        {
            if ((businessOperations.Dlv005DataSet.MainTable?.Rows?.Count ?? 0) == 0)
            {
                firstCommision = true;
            }
        }

        /// <summary>
        /// Binds the combo boxes.
        /// </summary>
        private void BindComboBoxes()
        {
            BindDrivingAuthorizationComboBox();
            BindHVQualificationcombobox();
            BindSpecialQualificationCombobox();
            BindSortTestsComboBox();
            BindRoutesTypeComboBox();
            BindTestingTypeComboBox();
        }

        /// <summary>
        /// Binds the special qualification combobox.
        /// </summary>
        private void BindSpecialQualificationCombobox()
        {
            string restriction = "SONDQUALIFIKATION";
            string columnNameRestriction = businessOperations.Dlv005DataSet.SD111Table.SD111_TYPColumn.ColumnName;
            string columnNameValue = businessOperations.Dlv005DataSet.SD111Table.SD111_WERTColumn.ColumnName;
            string columnIdValue = businessOperations.Dlv005DataSet.SD111Table.SD111_QUALIFIKATIONEN_IDColumn.ColumnName;

            SetBindingForGivenComboBox(
                SpecialQualificationComboBox,
                businessOperations.Dlv005DataSet.SD111Table,
                restriction, columnNameRestriction,
                columnNameValue,
                columnIdValue);
        }

        /// <summary>
        /// Binds the hv qualificationcombobox.
        /// </summary>
        private void BindHVQualificationcombobox()
        {
            string restriction = "HVQUALIFIKATION";
            string columnNameRestriction = businessOperations.Dlv005DataSet.SD111Table.SD111_TYPColumn.ColumnName;
            string columnNameValue = businessOperations.Dlv005DataSet.SD111Table.SD111_WERTColumn.ColumnName;
            string columnIdValue = businessOperations.Dlv005DataSet.SD111Table.SD111_QUALIFIKATIONEN_IDColumn.ColumnName;

            SetBindingForGivenComboBox(
                HVQualificationComboBox,
                businessOperations.Dlv005DataSet.SD111Table,
                restriction, columnNameRestriction,
                columnNameValue,
                columnIdValue);
        }

        /// <summary>
        /// Binds the driving authorization ComboBox.
        /// </summary>
        private void BindDrivingAuthorizationComboBox()
        {
            string restriction = "FAHRBERECHTIGUNG3";
            string columnNameRestriction = businessOperations.Dlv005DataSet.SD111Table.SD111_TYPColumn.ColumnName;
            string columnNameValue = businessOperations.Dlv005DataSet.SD111Table.SD111_WERTColumn.ColumnName;
            string columnIdValue = businessOperations.Dlv005DataSet.SD111Table.SD111_QUALIFIKATIONEN_IDColumn.ColumnName;

            SetBindingForGivenComboBox(
                DrivingAuthorizationComboBox,
                businessOperations.Dlv005DataSet.SD111Table,
                restriction,
                columnNameRestriction,
                columnNameValue,
                columnIdValue);
        }

        /// <summary>
        /// Binds the sort tests ComboBox.
        /// </summary>
        private void BindSortTestsComboBox()
        {
            string columnNameValue = businessOperations.Dlv005DataSet.DL38Table.DL38_BEZEICHNUNGColumn.ColumnName;
            string columnIdValue = businessOperations.Dlv005DataSet.DL38Table.DL38_KOMM_ERPROBUNGSORT_IDColumn.ColumnName;

            SetBindingForGivenComboBox(
                SortTestsComboBox,
                businessOperations.Dlv005DataSet.DL38Table,
                null,
                null,
                columnNameValue,
                columnIdValue);
        }

        /// <summary>
        /// Binds the routes type ComboBox.
        /// </summary>
        private void BindRoutesTypeComboBox()
        {
            string columnNameValue = businessOperations.Dlv005DataSet.DL39Table.DL39_BEZEICHNUNGColumn.ColumnName;
            string columnIdValue = businessOperations.Dlv005DataSet.DL39Table.DL39_KOMM_STRECKENART_IDColumn.ColumnName;

            SetBindingForGivenComboBox(
                RoutesTypeComboBox,
                businessOperations.Dlv005DataSet.DL39Table,
                null,
                null,
                columnNameValue,
                columnIdValue);
        }

        /// <summary>
        /// Binds the testing type ComboBox.
        /// </summary>
        private void BindTestingTypeComboBox()
        {
            string columnNameValue = businessOperations.Dlv005DataSet.DL40Table.DL40_BEZEICHNUNGColumn.ColumnName;
            string columnIdValue = businessOperations.Dlv005DataSet.DL40Table.DL40_KOMM_ERPROBUNGSART_IDColumn.ColumnName;

            SetBindingForGivenComboBox(
                TestingTypeComboBox,
                businessOperations.Dlv005DataSet.DL40Table,
                null,
                null,
                columnNameValue,
                columnIdValue);
        }

        /// <summary>
        /// Sets the binding for given ComboBox.
        /// </summary>
        /// <param name="comboBox">The combo box.</param>
        /// <param name="dataTable">The data table.</param>
        /// <param name="restriction">The restriction.</param>
        /// <param name="columnNameRestriction">The column name restriction.</param>
        /// <param name="columnNameValue">The column name value.</param>
        /// <param name="columnIdValue">The column identifier value.</param>
        private void SetBindingForGivenComboBox(
            ComboBox comboBox,
            DataTable dataTable,
            string restriction,
            string columnNameRestriction,
            string columnNameValue,
            string columnIdValue)
        {
            comboBox.DataSource = null;
            comboBox.Items.Clear();
            //key-value pair to hold our data
            List<KeyValuePair<decimal, string>> KeyValueList = new List<KeyValuePair<decimal, string>>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i];
                if (restriction != null && columnNameRestriction != null)
                {
                    if (row[columnNameRestriction].Equals(restriction))
                    {
                        KeyValueList.Add(new KeyValuePair<decimal, string>(Convert.ToDecimal(row[columnIdValue]), row[columnNameValue].ToString()));
                    }
                }
                else
                {
                    KeyValueList.Add(
                        new KeyValuePair<decimal, string>(Convert.ToDecimal(row[columnIdValue]), row[columnNameValue].ToString()));
                }
            }
            comboBox.DataSource = new BindingSource(KeyValueList, null);
            comboBox.DisplayMember = "Value"; // holds the text
            comboBox.ValueMember = "Key";     // holds the id
        }

        /// <summary>
        /// Add proper names for overview grid columns.
        /// </summary>
        private void SetUpHeaderName()
        {
            dataGridView1.Columns[0].HeaderText = "Nr";
            dataGridView1.Columns[1].HeaderText = "Testing content";
            dataGridView1.Columns[2].HeaderText = "From";
            dataGridView1.Columns[3].HeaderText = "To";
            dataGridView1.Columns[4].HeaderText = "Sort tests";
            dataGridView1.Columns[5].HeaderText = "Routes type";
            dataGridView1.Columns[6].HeaderText = "Testing type";
            dataGridView1.Columns[7].HeaderText = "Series";
            dataGridView1.Columns[8].HeaderText = "Customer";
            dataGridView1.Columns[9].HeaderText = "Status";
            dataGridView2.Columns[0].HeaderCell.Style.Font = new Font("Tahoma", 8F, FontStyle.Bold);
            dataGridView2.Columns[1].HeaderCell.Style.Font = new Font("Tahoma", 8F, FontStyle.Bold);
            dataGridView2.Columns[0].HeaderText = "Allocation nr";
            dataGridView2.Columns[1].HeaderText = "Percent";
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (dataGridView1 != null)
            {
                dataGridView1.MouseClick -= DataGridView1_MouseClick;
            }
            if (BasicData != null)
            {
                BasicData.Enter -= BasicData_Enter;
            }
            if (dataGridView1 != null)
            {
                dataGridView1.CellClick -= DataGridView1_CellClick;
            }
            if (HideRequestedOnesCheckBox != null)
            {
                HideRequestedOnesCheckBox.CheckedChanged -= HideRequestedOnesCheckBox_CheckedChanged;
            }
            if (HideFinishedCheckBox != null)
            {
                HideFinishedCheckBox.CheckedChanged -= HideFinishedCheckBox_CheckedChanged;
            }
            if (includeSundayworkCheckBox != null)
            {
                includeSundayworkCheckBox.CheckedChanged += IncludeSundayworkCheckBox_CheckedChanged;
            }
            if (includeSaturdayworkCheckBox != null)
            {
                includeSaturdayworkCheckBox.CheckedChanged += IncludeSaturdayworkCheckBox_CheckedChanged;
            }
            if (DeleteButton != null)
            {
                DeleteButton.Click -= DeleteButton_Click;
            }
            if (Overview != null)
            {
                Overview.Enter -= Overview_Enter;
            }
            if (TestingTypeComboBox != null)
            {
                TestingTypeComboBox.TextChanged -= TestingTypeComboBox_TextChanged;
            }
            if (RoutesTypeComboBox != null)
            {
                RoutesTypeComboBox.TextChanged -= RoutesTypeComboBox_TextChanged;
            }
            if (SortTestsComboBox != null)
            {
                SortTestsComboBox.TextChanged -= SortTestsComboBox_TextChanged;
            }
            if (SpecialQualificationComboBox != null)
            {
                SpecialQualificationComboBox.TextChanged -= SpecialQualificationComboBox_TextChanged;
            }
            if (HVQualificationComboBox != null)
            {
                HVQualificationComboBox.TextChanged -= HVQualificationComboBox_TextChanged;
            }
            if (DrivingAuthorizationComboBox != null)
            {
                DrivingAuthorizationComboBox.TextChanged -= DrivingAuthorizationComboBox_TextChanged;
            }
            if (dataGridView1 != null)
            {
                dataGridView1.RowsAdded -= DataGridView1_RowsAdded;
            }
            if (SaveButton != null)
            {
                SaveButton.Click -= SaveButton_Click;
            }
            if (EngineeringButton != null)
            {
                EngineeringButton.Click -= EngineeringButton_Click;
            }
            if (ChiefButton != null)
            {
                ChiefButton.Click -= ChiefButton_Click;
            }
            if (SeriesButton != null)
            {
                SeriesButton.Click -= SeriesButton_Click;
            }
            if (ConfirmButton != null)
            {
                ConfirmButton.Click -= ConfirmButton_Click;
            }
            if (CustomerOEButton != null)
            {
                CustomerOEButton.Click -= CustomerOE_Click;
            }
            if (CustomerButton != null)
            {
                CustomerButton.Click -= Customer_Click;
            }
            if (RequestButton != null)
            {
                RequestButton.Click -= Request_Click;
            }
            if (NewCopyButton != null)
            {
                NewCopyButton.Click -= NewCopy_Click;
            }
            if (NewButton != null)
            {
                NewButton.Click += New_Click;
            }
            if (ExitCancelButton != null)
            {
                ExitCancelButton.Click += ExitCancelButton_Click;
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}