using Dlv005_BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static Dlv005_UI.Dlv005View;

namespace Dlv005_UI
{
    public partial class Dlv005SelectionTableView : Form
    {
        public delegate void CallBackSelectionTableDelegate(SelectionTablesUsed tablesUsed, List<KeyValuePair<decimal, string>> keyValuePairs);

        private Dlv005BusinessOperationsSelectionTable businessOperationSelectionTable = new Dlv005BusinessOperationsSelectionTable();

        private SelectionTablesUsed tablesUsed;
        private List<KeyValuePair<decimal, string>> keyValuePairs;
        private Dlv005View view;
        private bool existSelectedRow;
        internal int correctRowWhenFilterApplied;
        internal int rowIndexContor;
        internal bool gridWasSorted;
       

        /// <summary>
        /// Initializes a new instance of the <see cref="Dlv005SelectionTableView"/> class.
        /// </summary>
        public Dlv005SelectionTableView()
        {
            InitializeComponent();
            InitializeEvents();
        }

        /// <summary>
        /// Initializes the ComboBox.
        /// </summary>
        private void InitializeComboBox()
        {
            foreach (DataGridViewColumn column in dataGridView3.Columns)
            {
                if (column.Visible == true)
                {
                    SelectionComboBox.Items.Add(column.Name);
                }
            }
        }

        /// <summary>
        /// Counts the items on selection table.
        /// </summary>
        private void CountItemsOnSelectionTable()
        {
            SelectionNumberTextBox.Text = dataGridView3.Rows.Count.ToString();
        }

        /// <summary>
        /// Initializes the events.
        /// </summary>
        private void InitializeEvents()
        {
            this.Load += Dlv005SelectionTableView_Load;
            AdoptButton.Click += AdoptButton_Click;
            CancelSortButton.Click += CancelSortButton_Click;
            SelectionComboBox.SelectedValueChanged += SelectionComboBox_SelectedValueChanged;
            SelectionUpdateButton.Click += SelectionUpdateButton_Click;
            dataGridView3.CellDoubleClick += AdoptButton_Click;
        }

        /// <summary>
        /// Handles the Click event of the SelectionUpdateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SelectionUpdateButton_Click(object sender, EventArgs e)
        {
            gridWasSorted = true;
            switch (tablesUsed)
            {
                case SelectionTablesUsed.BD12:
                    SelectionTableBindingSource.DataSource = businessOperationSelectionTable.GetBD12();
                    foreach (DataRow row in businessOperationSelectionTable.Dlv005SelectionTableDataSet.BD12Table.Rows)
                    {
                        for (int i = 0; i < businessOperationSelectionTable.Dlv005SelectionTableDataSet.BD12Table.Count; i++)
                        {
                            if (row[i].ToString() == SelectionFilterTextBox.Text && row[i].ToString() != string.Empty)
                            {
                                SelectionTableBindingSource.Filter = (SelectionComboBox.Text + " = " + "'" + SelectionFilterTextBox.Text + "'");
                                dataGridView3.Rows[0].Selected = true;
                                break;
                            }
                            else if (SelectionFilterTextBox.Text == string.Empty)
                            {
                                SelectionTableBindingSource.Filter = null;
                            }
                        }
                    }
                    CountItemsOnSelectionTable();
                    break;

                case SelectionTablesUsed.BD09:
                    SelectionTableBindingSource.DataSource = businessOperationSelectionTable.GetBD09();
                    rowIndexContor = -1;
                    foreach (DataRow row in businessOperationSelectionTable.Dlv005SelectionTableDataSet.BD09Table.Rows)
                    {
                        rowIndexContor++;
                        for (int i = 0; i < businessOperationSelectionTable.Dlv005SelectionTableDataSet.BD09Table.Count + 1; i++)
                        {
                            if (row[i].ToString() == SelectionFilterTextBox.Text && row[i].ToString() != string.Empty)
                            {
                                correctRowWhenFilterApplied = Convert.ToInt16(dataGridView3.Rows[rowIndexContor].Cells[0].Value) - 1;
                                SelectionTableBindingSource.Filter = (SelectionComboBox.Text + " = " + "'" + SelectionFilterTextBox.Text + "'");
                                dataGridView3.Rows[0].Selected = true;
                                break;
                            }
                            else if (SelectionFilterTextBox.Text == string.Empty)
                            {
                                correctRowWhenFilterApplied = dataGridView3.CurrentCell.RowIndex;
                                SelectionTableBindingSource.Filter = null;
                            }
                        }
                    }
                    CountItemsOnSelectionTable();
                    break;

                case SelectionTablesUsed.BD06:
                    SelectionTableBindingSource.DataSource = businessOperationSelectionTable.GetBD06();
                    rowIndexContor = -1;
                    foreach (DataRow row in businessOperationSelectionTable.Dlv005SelectionTableDataSet.BD06Table.Rows)
                    {
                        rowIndexContor++;
                        for (int i = 0; i < businessOperationSelectionTable.Dlv005SelectionTableDataSet.BD06Table.Count; i++)
                        {
                            if (row[i].ToString() == SelectionFilterTextBox.Text && row[i].ToString() != string.Empty)
                            {
                                correctRowWhenFilterApplied = Convert.ToInt16(dataGridView3.Rows[rowIndexContor].Cells[0].Value) - 1;
                                SelectionTableBindingSource.Filter = (SelectionComboBox.Text + " = " + "'" + SelectionFilterTextBox.Text + "'");
                                dataGridView3.Rows[0].Selected = true;
                                break;
                            }
                            else if (SelectionFilterTextBox.Text == string.Empty)
                            {
                                correctRowWhenFilterApplied = dataGridView3.CurrentCell.RowIndex;
                                SelectionTableBindingSource.Filter = null;
                            }
                        }
                    }
                    CountItemsOnSelectionTable();
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Handles the SelectedValueChanged event of the SelectionComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SelectionComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            SelectionFilterTextBox.ReadOnly = false;
        }

        /// <summary>
        /// Handles the Click event of the CancelSortButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CancelSortButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the AdoptButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void AdoptButton_Click(object sender, EventArgs e)
        {
            switch (tablesUsed)
            {
                case SelectionTablesUsed.BD12:
                    existSelectedRow = false;
                    string multipleValueBD12 = " ";
                    for (int i = 0; i < dataGridView3.Rows.Count; i++)
                    {
                        if (dataGridView3.Rows[i].Selected)
                        {
                            multipleValueBD12 += dataGridView3.Rows[i].Cells[0].Value.ToString() + ',';
                            existSelectedRow = true;
                        }
                    }
                    if (existSelectedRow)
                    {
                        multipleValueBD12 = multipleValueBD12.Remove(multipleValueBD12.Length - 1);
                        multipleValueBD12 = multipleValueBD12.Substring(1);
                    }
                    else
                        multipleValueBD12 = "205";

                    keyValuePairs.Add(new KeyValuePair<decimal, string>(-1, multipleValueBD12));
                    CallBackSelectionTableDelegate callBackSelectionTableDelegateBD12 = new CallBackSelectionTableDelegate(view.CallBackSelectionTable);
                    callBackSelectionTableDelegateBD12(tablesUsed, keyValuePairs);
                    Close();
                    break;

                case SelectionTablesUsed.BD09:
                    if (gridWasSorted)
                    {
                        var rowBD09 = (Dlv005SelectionTableDataSet.BD09TableRow)businessOperationSelectionTable.Dlv005SelectionTableDataSet.BD09Table.Rows[correctRowWhenFilterApplied];
                        keyValuePairs.Add(new KeyValuePair<decimal, string>(rowBD09.BD09_PERSID, (rowBD09.BD09_NAME + "," + rowBD09.BD09_VORNAME + " " + rowBD09.BD09_OE_KURZ_BEZ)));
                        CallBackSelectionTableDelegate callBackSelectionTableDelegateBD09 = new CallBackSelectionTableDelegate(view.CallBackSelectionTable);
                        callBackSelectionTableDelegateBD09(tablesUsed, keyValuePairs);
                        Close();
                        break;
                    }
                    else
                    {
                        var rowBD09 = (Dlv005SelectionTableDataSet.BD09TableRow)businessOperationSelectionTable.Dlv005SelectionTableDataSet.BD09Table.Rows[dataGridView3.CurrentCell.RowIndex];
                        keyValuePairs.Add(new KeyValuePair<decimal, string>(rowBD09.BD09_PERSID, (rowBD09.BD09_NAME + "," + rowBD09.BD09_VORNAME + " " + rowBD09.BD09_OE_KURZ_BEZ)));
                        CallBackSelectionTableDelegate callBackSelectionTableDelegateBD09 = new CallBackSelectionTableDelegate(view.CallBackSelectionTable);
                        callBackSelectionTableDelegateBD09(tablesUsed, keyValuePairs);
                        Close();
                        break;
                    }
                case SelectionTablesUsed.BD06:
                    if (gridWasSorted)
                    {
                        var rowBD06 = (Dlv005SelectionTableDataSet.BD06TableRow)businessOperationSelectionTable.Dlv005SelectionTableDataSet.BD06Table.Rows[correctRowWhenFilterApplied];
                        keyValuePairs.Add(new KeyValuePair<decimal, string>(rowBD06.BD06_OE, rowBD06.BD06_KURZ_BEZ));
                        CallBackSelectionTableDelegate callBackSelectionTableDelegateBD06 = new CallBackSelectionTableDelegate(view.CallBackSelectionTable);
                        callBackSelectionTableDelegateBD06(tablesUsed, keyValuePairs);
                        Close();
                        break;
                    }
                    else
                    {
                        var rowBD06 = (Dlv005SelectionTableDataSet.BD06TableRow)businessOperationSelectionTable.Dlv005SelectionTableDataSet.BD06Table.Rows[dataGridView3.CurrentCell.RowIndex];
                        keyValuePairs.Add(new KeyValuePair<decimal, string>(rowBD06.BD06_OE, rowBD06.BD06_KURZ_BEZ));
                        CallBackSelectionTableDelegate callBackSelectionTableDelegateBD06 = new CallBackSelectionTableDelegate(view.CallBackSelectionTable);
                        callBackSelectionTableDelegateBD06(tablesUsed, keyValuePairs);
                        Close();
                        break;
                    }

                default:
                    break;
            }
        }

        /// <summary>
        /// Handles the Load event of the Dlv005SelectionTableView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Dlv005SelectionTableView_Load(object sender, EventArgs e)
        {
            SelectionFilterTextBox.ReadOnly = true;
            SelectionTableBindingSource.DataSource = businessOperationSelectionTable.GetSelectionTableData(tablesUsed.ToString());
            dataGridView3.DataSource = SelectionTableBindingSource;
            switch (tablesUsed)
            {
                case SelectionTablesUsed.BD12:
                    dataGridView3.DataMember = "BD12Table";
                    break;

                case SelectionTablesUsed.BD09:
                    dataGridView3.DataMember = "BD09Table";
                    break;

                case SelectionTablesUsed.BD06:
                    dataGridView3.DataMember = "BD06Table";
                    break;

                default:
                    break;
            }
            if (dataGridView3.DataMember == "BD12Table")
            {
                DisplayCorrectColumnsBD12();
                InitializeComboBox();
            }
            else if (dataGridView3.DataMember == "BD09Table")
            {
                DisplayCorrectColumnsBD09();
                InitializeComboBox();
            }
            else
            {
                DisplayCorrectColumnsBD06();
                InitializeComboBox();
            }

            CountItemsOnSelectionTable();
        }

        /// <summary>
        /// Change column names and hide unnecesary columns for BD12 table used in selection table.
        /// </summary>
        private void DisplayCorrectColumnsBD12()
        {
            dataGridView3.Columns[0].HeaderText = "Series";
            dataGridView3.Columns[1].HeaderText = "Name";
            dataGridView3.Columns[0].Name = "Series";
            dataGridView3.Columns[1].Name = "Name";

            for (int i = 2; i < 6; i++)
            {
                dataGridView3.Columns[i].Visible = false;
            }
        }

        /// <summary>
        /// Change column names and hide unnecesary columns for BD09 table used in selection table.
        /// </summary>
        private void DisplayCorrectColumnsBD09()
        {
            dataGridView3.Columns[0].HeaderText = "Series";
            dataGridView3.Columns[2].HeaderText = "Name";
            dataGridView3.Columns[3].HeaderText = "Last name";
            dataGridView3.Columns[0].Name = "Series";
            dataGridView3.Columns[2].Name = "Name";
            dataGridView3.Columns[3].Name = "Lastname";

            for (int i = 4; i < 8; i++)
            {
                dataGridView3.Columns[i].Visible = false;
            }
            dataGridView3.Columns[1].Visible = false;
            dataGridView3.Columns[8].Visible = false;
        }

        /// <summary>
        /// Change column names and hide unnecesary columns for BD06 table used in selection table.
        /// </summary>
        private void DisplayCorrectColumnsBD06()
        {
            dataGridView3.Columns[0].HeaderText = "Series";
            dataGridView3.Columns[1].HeaderText = "Short designation";
            dataGridView3.Columns[0].Name = "Series";
            dataGridView3.Columns[1].Name = "Shortdesignation";

            for (int i = 2; i < 6; i++)
            {
                dataGridView3.Columns[i].Visible = false;
            }
        }

        /// <summary>
        /// Calls the selection table.
        /// </summary>
        /// <param name="tablesUsed">The tables used.</param>
        /// <param name="keyValuePairs">The key value pairs.</param>
        /// <param name="view">The view.</param>
        public void CallSelectionTable(SelectionTablesUsed tablesUsed, List<KeyValuePair<decimal, string>> keyValuePairs, Dlv005View view)
        {
            this.tablesUsed = tablesUsed;
            this.keyValuePairs = keyValuePairs;
            this.view = view;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (CancelSortButton != null)
            {
                CancelSortButton.Click -= CancelSortButton_Click;
            }
            if (AdoptButton != null)
            {
                AdoptButton.Click -= AdoptButton_Click;
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}