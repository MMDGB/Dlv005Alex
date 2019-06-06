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
        private Dlv005Validations businessValidations;
        private List<KeyValuePair<decimal, string>> keyValuePairs;
        public DataRow newcopyrow;
        private int requestContor;
        private string testingNumber;
        private string auxStatusCheck;
        private int auxAllocRowMaxContor = 1;
        private int comboboxBool1;
        private int comboboxBool2;
        private int comboboxBool3;
        private int comboboxBool4;
        private int comboboxBool5;
        private bool gridHasRows;
        private bool isInEditMode = false;
        private int mousePositionX;
        private int mousePositionXForAllocation;
        private int mousePositionYForAllocation;
        private int numberOfCommisionsDisplayOnOverview;
        private int mousePositionY;
        private bool firstCommision;
        private Dictionary<string, bool> errorsDictionary = new Dictionary<string, bool>();

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
        }

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
        ///
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
        ///
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
        ///
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
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (overviewBindingSource.Position < 0)
            {
                return;
            }
            dataGridView1.Rows[dataGridView1.HitTest(e.X, e.Y).RowIndex].Selected = true;
            tabControl.SelectedTab = BasicData;
        }

        /// <summary>
        ///
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
        ///
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
        ///
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
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ExitCancelButton.Text = "Cancel";
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
                CheckForStatusChange();
                Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();

                if (isNew)
                {
                    allocationBindingSource.Filter = String.Format(("DL32_EXT_KOMM_ANFORDERUNG_ID= {0}"), -1);
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
            numberOfCommisionsDisplayOnOverview = dataGridView1.RowCount;
            OverviewNumbetTextBox.Text = numberOfCommisionsDisplayOnOverview.ToString();
            DisableButtonsWhenNoComissions();
            CheckForStatusChange();
        }

        /// <summary>
        ///
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
        ///
        /// </summary>
        private void CheckForStatusChange()
        {
            if (overviewBindingSource.Position >= 0)
            {
                SelectCorrectRowForStatusCheck(DuplicateSelectedRow());

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
        ///
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
        ///
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
            if (MessageBox.Show("Should the external picking really be deleted? ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();
                var deleteID = row.DL31_KOMM_ANFORDERUNG_ID;
                businessOperations.DeleteDataDl32(deleteID);
                businessOperations.DeleteData(deleteID);
                numberOfCommisionsDisplayOnOverview = dataGridView1.RowCount;
                OverviewNumbetTextBox.Text = numberOfCommisionsDisplayOnOverview.ToString();
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

        #region ComboBox TextChange Events

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestingTypeComboBox_TextChanged(object sender, EventArgs e)
        {
            //if (((ComboBox)sender).SelectedItem == null && overviewBindingSource.Position >= 0)
            //{
            //    return;
            //}
            //if (overviewBindingSource.Position >= 0 && ((ComboBox)sender).SelectedItem != null)
            //{
            //    Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();

            //    string auxStringToGetTheValue = (((ComboBox)sender).SelectedValue).ToString();
            //    if (comboboxBool == 0)
            //    {
            //        if (auxStringToGetTheValue[2] == Convert.ToChar(","))
            //        {
            //            row.DL31_KOMM_ERPROBUNGSART_ID = Convert.ToDecimal(auxStringToGetTheValue.Substring(1, 1));
            //        }
            //        else
            //        {
            //            row.DL31_KOMM_ERPROBUNGSART_ID = Convert.ToDecimal(auxStringToGetTheValue.Substring(1, 2));
            //        }
            //    }
            //    else
            //    {
            //        row.DL31_KOMM_ERPROBUNGSART_ID = Convert.ToDecimal(auxStringToGetTheValue);
            //    }

            //}
            Dlv005DataSet.MainTableRow rows = DuplicateSelectedRow();
            string textToCheck = TestingTypeComboBox.Text;
            if (TestingTypeComboBox.Text == string.Empty)
            {
                return;
            }
            else if (textToCheck == "Exam")
            {
                rows.DL31_KOMM_ERPROBUNGSART_ID = 1;
            }
            else if (textToCheck == "World-DL")
            {
                rows.DL31_KOMM_ERPROBUNGSART_ID = 2;
            }
            else if (textToCheck == "Full load DL")
            {
                rows.DL31_KOMM_ERPROBUNGSART_ID = 3;
            }
            else if (textToCheck == "E/E")
            {
                rows.DL31_KOMM_ERPROBUNGSART_ID = 4;
            }
            else if (textToCheck == "Driving assistance")
            {
                rows.DL31_KOMM_ERPROBUNGSART_ID = 5;
            }
            else if (textToCheck == "Driving dynamics")
            {
                rows.DL31_KOMM_ERPROBUNGSART_ID = 6;
            }
            else if (textToCheck == "Raff-DL")
            {
                rows.DL31_KOMM_ERPROBUNGSART_ID = 7;
            }
            else
                return;

            //Dlv005DataSet.MainTableRow rows = DuplicateSelectedRow();

            //if (rows != null && rows.DL31_KOMM_ERPROBUNGSART_ID < 0)
            //{
            //    string textToCheck = TestingTypeComboBox.Text;

            //    if (textToCheck == "Exam")//|| textToCheck == "World-DL" || textToCheck == "Full load DL" || textToCheck == "E/E" || textToCheck == "Driving assistance" || textToCheck == "Driving dynamics" ||
            //                              //textToCheck == "Raff-DL" || textToCheck == "Flight" || textToCheck == "Hired car" || textToCheck == "Car" || textToCheck == "Test vehicle")
            //    {
            //        rows.DL31_KOMM_ERPROBUNGSART_ID = 1;
            //    }

            //}
            //comboboxBool = 2;
        }

        /// <summary>
        ///Adds the key ID to the new row .
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoutesTypeComboBox_TextChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedItem == null)
            {
                return;
            }
            if (overviewBindingSource.Position >= 0)
            {
                Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();
                string auxStringToGetTheValue = (((ComboBox)sender).SelectedValue).ToString();
                if (comboboxBool1 == 0)
                {
                    row.DL31_KOMM_STRECKENART_ID = Convert.ToDecimal(auxStringToGetTheValue.Substring(1, 1));
                }
                else
                {
                    row.DL31_KOMM_STRECKENART_ID = Convert.ToDecimal(auxStringToGetTheValue);
                }
            }
            comboboxBool1 = 2;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortTestsComboBox_TextChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedItem == null)
            {
                return;
            }
            if (overviewBindingSource.Position >= 0)
            {
                Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();
                string auxStringToGetTheValue = (((ComboBox)sender).SelectedValue).ToString();
                if (comboboxBool2 == 0)
                {
                    row.DL31_KOMM_ERPROBUNGSORT_ID = Convert.ToDecimal(auxStringToGetTheValue.Substring(1, 1));
                }
                else
                {
                    row.DL31_KOMM_ERPROBUNGSORT_ID = Convert.ToDecimal(auxStringToGetTheValue);
                }
            }
            comboboxBool2 = 2;
        }

        /// <summary>
        /// Adds the HVQualificationComboBox key value in the new created row for insertion in DB.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpecialQualificationComboBox_TextChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedItem == null)
            {
                return;
            }
            if (overviewBindingSource.Position >= 0)
            {
                Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();
                string auxStringToGetTheValue = (((ComboBox)sender).SelectedValue).ToString();
                if (comboboxBool3 == 0)
                {
                    if (auxStringToGetTheValue[2] == Convert.ToChar(","))
                    {
                        row.DL31_SONDERQUALIFIKATION_ID = Convert.ToDecimal(auxStringToGetTheValue.Substring(1, 1));
                    }
                    else
                    {
                        row.DL31_SONDERQUALIFIKATION_ID = Convert.ToDecimal(auxStringToGetTheValue.Substring(1, 2));
                    }
                }
                else
                {
                    row.DL31_SONDERQUALIFIKATION_ID = Convert.ToDecimal(auxStringToGetTheValue);
                }
            }
            comboboxBool3 = 2;
        }

        /// <summary>
        /// Adds the HVQualificationComboBox key value in the new created row for insertion in DB.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HVQualificationComboBox_TextChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedItem == null)
            {
                return;
            }

            if (overviewBindingSource.Position >= 0)
            {
                Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();
                string auxStringToGetTheValue = (((ComboBox)sender).SelectedValue).ToString();
                if (comboboxBool4 == 0)
                {
                    row.DL31_HV_QUALIFIKATION_ID = Convert.ToDecimal(auxStringToGetTheValue.Substring(1, 1));
                }
                else
                {
                    row.DL31_HV_QUALIFIKATION_ID = Convert.ToDecimal(auxStringToGetTheValue);
                }
            }
            comboboxBool4 = 2;
        }

        /// <summary>
        /// Adds the DrivingAuthorizationComboBox key value in the new created row for insertion in DB.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrivingAuthorizationComboBox_TextChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedItem == null && overviewBindingSource.Position >= 0)
            {
                return;
            }

            if (overviewBindingSource.Position >= 0)
            {
                Dlv005DataSet.MainTableRow row = DuplicateSelectedRow();
                string auxStringToGetTheValue = (((ComboBox)sender).SelectedValue).ToString();
                if (comboboxBool5 == 0)
                {
                    row.DL31_FAHRBERECHTIGUNG_ID = Convert.ToDecimal(auxStringToGetTheValue.Substring(1, 1));
                }
                else
                {
                    row.DL31_FAHRBERECHTIGUNG_ID = Convert.ToDecimal(auxStringToGetTheValue);
                }
            }
            comboboxBool5 = 2;
        }

        /// <summary>
        ///
        /// </summary>
        private void InitializeValidations()
        {
            errorsDictionary.Add("TestingContent", false);
            errorsDictionary.Add("StartDate", false);
            errorsDictionary.Add("EndDate", false);
            errorsDictionary.Add("SortTests", false);
            errorsDictionary.Add("RoutesType", false);
            errorsDictionary.Add("TestingType", false);
            errorsDictionary.Add("Series", false);
            errorsDictionary.Add("CustomerOE", false);
            errorsDictionary.Add("Customer", false);
            errorsDictionary.Add("Chief", false);
            errorsDictionary.Add("EngineeringAST", false);
            errorsDictionary.Add("DrivingAuthorization", false);
            errorsDictionary.Add("HVQualification", false);
            errorsDictionary.Add("SpecialQualification", false);
            errorsDictionary.Add("Allocation", false);

            businessValidations = new Dlv005Validations(errorsDictionary);

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
            businessValidations.ValidateAllocationGrid(dataGridView2, AllocationErrorProvider, e);
           
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
        private DataRow GetTheNewCommisionRow()
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
                if (isInEditMode && !isNewCopy && !isNew)
                {
                    businessOperations.UpdateCommision(DuplicateSelectedRow());
                    DisableEditMode();
                    isInEditMode = false;
                }
                else
                {
                    businessOperations.SaveCommision(GetTheNewCommisionRow());
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
        ///
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
                updatedRow["DL31_KOMM__STATUS_ID"] = 3;
                businessOperations.UpdateCommision(updatedRow);
                businessOperations.ReloadOverview();
                CheckForStatusChange();
            }
            else
            {
                updatedRow["DL31_KOMM__STATUS_ID"] = 3;
                businessOperations.UpdateCommision(updatedRow);
                businessOperations.ReloadOverview();
                dataGridView1.Rows[0].Selected = true;
                CheckForStatusChange();
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
                businessOperations.UpdateCommision(updatedRow);
                businessOperations.ReloadOverview();
                CheckForStatusChange();
            }
            else
            {
                businessOperations.UpdateCommision(updatedRow);
                businessOperations.ReloadOverview();
                CheckForStatusChange();
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
            ExitCancelButton.Text = "Cancel";
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
            else
            {
                return (dataGridView1.Rows[overviewBindingSource.Position].DataBoundItem as DataRowView).Row as Dlv005DataSet.MainTableRow;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="format"></param>
        private void SetEmptyFromDateTimePicker(string format)
        {
            FromDateTimePicker.CustomFormat = format;
            FromDateTimePicker.Format = DateTimePickerFormat.Custom;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="format"></param>
        private void SetEmptyToDateTimePicker(string format)
        {
            ToDateTimePicker.CustomFormat = format;
            ToDateTimePicker.Format = DateTimePickerFormat.Custom;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void New_Click(object sender, EventArgs e)
        {
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
            if (firstCommision)
            {
                isNew = false;
                firstCommision = false;
                businessOperations.CancelSave();
                tabControl.SelectedTab = Overview;
            }
            if (overviewBindingSource.Position <= 0)
            {
                businessOperations.CancelSave();
                CheckForExistingRows();
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

            var dataSource = businessOperations.GetData();

            overviewBindingSource.DataSource = dataSource;
            dataGridView1.DataSource = overviewBindingSource;

            allocationBindingSource.DataSource = dataSource;
            dataGridView2.DataSource = allocationBindingSource;
            numberOfCommisionsDisplayOnOverview = dataGridView1.RowCount;
            OverviewNumbetTextBox.Text = numberOfCommisionsDisplayOnOverview.ToString();

            BindComboBoxes();
        }

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