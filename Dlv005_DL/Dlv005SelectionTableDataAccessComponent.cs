using System.Data;
using System.Text;

namespace Dlv005_DL
{
    public class Dlv005SelectionTableDataAccessComponent : Dlv005DataAccessComponent
    {
        /// <summary>
        /// Gets the bD12 data.
        /// </summary>
        /// <param name="bd12DataTable">The BD12 data table.</param>
        public void GetBD12Data(DataTable bd12DataTable)
        {
            FillDataTable(bd12DataTable, CreateQueryForDb12Table());
        }

        /// <summary>
        /// Creates the query for DB12 table.
        /// </summary>
        /// <returns></returns>
        private string CreateQueryForDb12Table()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT ");
            sb.Append(" BD12_BAUREIHE,");
            sb.Append(" BD12_BENENNUNG,");
            sb.Append(" BD12_PERSID_AENDERNG,");
            sb.Append(" BD12_PERSID_ERFASSNG,");
            sb.Append(" BD12_DATUM_AENDERNG,");
            sb.Append(" BD12_DATUM_ERFASSNG");
            sb.Append(" FROM bd12_baureihe ");

            return sb.ToString();
        }

        /// <summary>
        /// Creates the query for DB09 table.
        /// </summary>
        /// <returns></returns>
        private string CreateQueryForDb09Table()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" SELECT ");
            sb.Append(" BD09.BD09_PERSID,");
            sb.Append(" BD09.BD09_OE,");
            sb.Append(" BD09.BD09_NAME,");
            sb.Append(" BD09.BD09_VORNAME,");
            sb.Append(" BD09.BD09_PERSID_AENDERNG,");
            sb.Append(" BD09.BD09_PERSID_ERFASSNG,");
            sb.Append(" BD09.BD09_DATUM_AENDERNG,");
            sb.Append(" BD09.BD09_DATUM_ERFASSNG,");
            sb.Append(" (SELECT BD06_KURZ_BEZ FROM BD06_ORG_EINHEIT_TBL WHERE BD06_OE = BD09.BD09_OE) AS BD09_OE_KURZ_BEZ");
            sb.Append(" FROM BD09_PERSON BD09");

            return sb.ToString();
        }
    }
}