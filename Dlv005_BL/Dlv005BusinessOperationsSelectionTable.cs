namespace Dlv005_BL
{
    public class Dlv005BusinessOperationsSelectionTable
    {
        /// <summary>
        /// Gets or sets the DLV005 selection table data set.
        /// </summary>
        /// <value>
        /// The DLV005 selection table data set.
        /// </value>
        public Dlv005SelectionTableDataSet Dlv005SelectionTableDataSet { get; set; }

        public Dlv005SelectionTableDataSet.BD12TableDataTable GetBD12()
        {
            Dlv005SelectionTableDataSet.BD12Table.BD12_BAUREIHEColumn.ColumnName = "Series";
            Dlv005SelectionTableDataSet.BD12Table.BD12_BENENNUNGColumn.ColumnName = "Name";
            return Dlv005SelectionTableDataSet.BD12Table;
        }

        /// <summary>
        /// Gets the selection table data.
        /// </summary>
        /// <param name="tableUsed">The table used.</param>
        /// <returns></returns>
        public Dlv005SelectionTableDataSet GetSelectionTableData(string tableUsed)
        {
            Dlv005SelectionTableDataSet = new Dlv005SelectionTableDataSet();
            Dlv005SelectionTableDataSet.Initialize(Dlv005SelectionTableDataSet, tableUsed);
            return Dlv005SelectionTableDataSet;
        }

        /// <summary>
        /// Gets the b D09.
        /// </summary>
        /// <returns></returns>
        public object GetBD09()
        {
            Dlv005SelectionTableDataSet.BD09Table.BD09_PERSIDColumn.ColumnName = "Series";
            Dlv005SelectionTableDataSet.BD09Table.BD09_NAMEColumn.ColumnName = "Name";
            Dlv005SelectionTableDataSet.BD09Table.BD09_VORNAMEColumn.ColumnName = "Lastname";
            return Dlv005SelectionTableDataSet.BD09Table;
        }

        /// <summary>
        /// Gets the b D06.
        /// </summary>
        /// <returns></returns>
        public object GetBD06()
        {
            Dlv005SelectionTableDataSet.BD06Table.BD06_OEColumn.ColumnName = "Series";
            Dlv005SelectionTableDataSet.BD06Table.BD06_KURZ_BEZColumn.ColumnName = "Shortdesignation";
            return Dlv005SelectionTableDataSet.BD06Table;
        }
    }
}