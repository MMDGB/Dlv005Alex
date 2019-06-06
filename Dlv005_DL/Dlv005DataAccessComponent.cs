using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Text;

namespace Dlv005_DL
{
    /// <summary>
    ///
    /// </summary>
    public class Dlv005DataAccessComponent
    {
        

                protected const string ConnectionString = "server=localhost;uid=root;" +
                "pwd=nzec3ecz;database=dlv_005;";


       // protected const string ConnectionString = "Server=localhost;Port=3306;Database=dlv_005db;Uid=root;password=root123";

        /// <summary>
        /// Fills the data table.
        /// </summary>
        /// <param name="dataModel">The data model.</param>
        /// <param name="selectStatement">The select statement.</param>
        protected void FillDataTable(DataTable dataModel, string selectStatement)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(selectStatement, conn))
                {
                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd))
                    {
                        dataAdapter.SelectCommand = cmd;
                        dataAdapter.Fill(dataModel);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the b D09 data.
        /// </summary>
        /// <param name="bd09DataTable">The BD09 data table.</param>
        public void GetBD09Data(DataTable bd09DataTable)
        {
            FillDataTable(bd09DataTable, CreateQueryForDb09Table());
        }

        /// <summary>
        /// Gets the b D06 data.
        /// </summary>
        /// <param name="bd06DataTable">The BD06 data table.</param>
        public void GetBD06Data(DataTable bd06DataTable)
        {
            FillDataTable(bd06DataTable, CreateQueryForDb06Table());
        }

        /// <summary>
        /// Updates the data.
        /// </summary>
        /// <param name="inputModel">The input model.</param>
        public void UpdateData(InputModel inputModel)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(UpdateStatement(), conn))
                {
                    conn.Open();

                    cmd.Parameters.AddWithValue("@DL31_KOMM_ANFORDERUNG_NR", inputModel.DL31_KOMM_ANFORDERUNG_NR);
                    cmd.Parameters.AddWithValue("@DL31_KOMM__STATUS_ID", inputModel.DL31_KOMM__STATUS_ID);
                    cmd.Parameters.AddWithValue("@DL31_ERPROBUNGSINHALT", inputModel.DL31_ERPROBUNGSINHALT);
                    cmd.Parameters.AddWithValue("@DL31_START_DATUM", inputModel.DL31_START_DATUM);
                    cmd.Parameters.AddWithValue("@DL31_ENDE_DATUM", inputModel.DL31_ENDE_DATUM);
                    cmd.Parameters.AddWithValue("@DL31_KOMM_ERPROBUNGSORT_ID", inputModel.DL31_KOMM_ERPROBUNGSORT_ID);
                    cmd.Parameters.AddWithValue("@DL31_KOMM_STRECKENART_ID", inputModel.DL31_KOMM_STRECKENART_ID);
                    cmd.Parameters.AddWithValue("@DL31_KOMM_ERPROBUNGSART_ID", inputModel.DL31_KOMM_ERPROBUNGSART_ID);
                    cmd.Parameters.AddWithValue("@DL31_BAUREIHEN", inputModel.DL31_BAUREIHEN);
                    cmd.Parameters.AddWithValue("@DL31_AUFTRAGGEBER_OE", inputModel.DL31_AUFTRAGGEBER_OE);
                    cmd.Parameters.AddWithValue("@DL31_AUFTRAGGEBER_PERSID", inputModel.DL31_AUFTRAGGEBER_PERSID);
                    cmd.Parameters.AddWithValue("@DL31_FAHRTENLEITER_PERSID", inputModel.DL31_FAHRTENLEITER_PERSID);
                    cmd.Parameters.AddWithValue("@DL31_ENGINEERING_AST_PERSID", inputModel.DL31_ENGINEERING_AST_PERSID);
                    cmd.Parameters.AddWithValue("@DL31_FAHRBERECHTIGUNG_ID", inputModel.DL31_FAHRBERECHTIGUNG_ID);
                    cmd.Parameters.AddWithValue("@DL31_HV_QUALIFIKATION_ID", inputModel.DL31_HV_QUALIFIKATION_ID);
                    cmd.Parameters.AddWithValue("@DL31_SONDERQUALIFIKATION_ID", inputModel.DL31_SONDERQUALIFIKATION_ID);
                    cmd.Parameters.AddWithValue("@DL31_KOMM_ANFORDERUNG_ID", inputModel.DL31_KOMM_ANFORDERUNG_ID);
                    cmd.Parameters.AddWithValue("@DL31_SAMSTAGSARBEIT", inputModel.DL31_SAMSTAGSARBEIT);
                    cmd.Parameters.AddWithValue("@DL31_SONNTAGSARBEIT", inputModel.DL31_SONNTAGSARBEIT);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Deletes the allocation data.
        /// </summary>
        /// <param name="deleteID">The delete identifier.</param>
        public void DeleteAllocationData(decimal deleteID)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(DeleteStatementAllocation(deleteID), conn))
                {
                    conn.Open();

                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Updates the DL32.
        /// </summary>
        /// <param name="allocationInputModel">The allocation input model.</param>
        public void UpdateDl32(AllocationInputModel allocationInputModel)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(UpdateDl32Statement(), conn))
                {
                    conn.Open();

                    cmd.Parameters.AddWithValue("@DL32_KONTIERUNG", allocationInputModel.DL32_KONTIERUNG);
                    cmd.Parameters.AddWithValue("@DL32_ANTEIL_PROZENT", allocationInputModel.DL32_ANTEIL_PROZENT);
                    cmd.Parameters.AddWithValue("@DL32_KOMM_ANFORDERUNG_KONTO_ID", allocationInputModel.DL32_KOMM_ANFORDERUNG_KONTO_ID);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Inserts the data DL32.
        /// </summary>
        /// <param name="allocationInputModel">The allocation input model.</param>
        public void InsertDataDl32(AllocationInputModel allocationInputModel)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(InsertDl32Statement(), conn))
                {
                    conn.Open();

                    cmd.Parameters.AddWithValue("@DL32_KONTIERUNG", allocationInputModel.DL32_KONTIERUNG);
                    cmd.Parameters.AddWithValue("@DL32_ANTEIL_PROZENT", allocationInputModel.DL32_ANTEIL_PROZENT);
                    cmd.Parameters.AddWithValue("@DL32_EXT_KOMM_ANFORDERUNG_ID", allocationInputModel.DL32_EXT_KOMM_ANFORDERUNG_ID);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    conn.Close();
                }
            }
        }

        /// <summary>
        ///  Fills the overview table with data from DB.
        /// </summary>
        /// <param name="mainDataTable"></param>
        public void GetOverviewData(DataTable mainDataTable)
        {
            FillDataTable(mainDataTable, CreateQueryForOverviewTable());
        }

        /// <summary>
        /// Gets the DL38 data.
        /// </summary>
        /// <param name="dl38DataTable">The DL38 data table.</param>
        public void GetDL38Data(DataTable dl38DataTable)
        {
            FillDataTable(dl38DataTable, CreateQueryForDl38Table());
        }

        /// <summary>
        /// Gets the DL39 data.
        /// </summary>
        /// <param name="dl39DataTable">The DL38 data table.</param>
        public void GetDL39Data(DataTable dl39DataTable)
        {
            FillDataTable(dl39DataTable, CreateQueryForDl39Table());
        }

        /// <summary>
        /// Gets the DL40 data.
        /// </summary>
        /// <param name="dl40DataTable">The DL38 data table.</param>
        public void GetDL40Data(DataTable dl40DataTable)
        {
            FillDataTable(dl40DataTable, CreateQueryForDl40Table());
        }

        /// <summary>
        /// Gets the s D111 data.
        /// </summary>
        /// <param name="sd111DataTable">The SD111 data table.</param>
        public void GetSD111Data(DataTable sd111DataTable)
        {
            FillDataTable(sd111DataTable, CreateQueryForSd111Table());
        }

        /// <summary>
        /// Deletes the data.
        /// </summary>
        /// <param name="deleteID">The delete identifier.</param>
        public void DeleteData(decimal deleteID)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(DeleteStatement(deleteID), conn))
                {
                    conn.Open();

                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Gets the allocation grid data.
        /// </summary>
        /// <param name="AllocationGridTable">The allocation grid table.</param>
        public void GetAllocationGridData(DataTable AllocationGridTable)
        {
            FillDataTable(AllocationGridTable, CreateQueryForAllocationTable());
        }

        /// <summary>
        /// Inserts the data.
        /// </summary>
        /// <param name="inputModel">The input model.</param>
        public void InsertData(InputModel inputModel)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(InsertStatement(), conn))
                {
                    conn.Open();

                    cmd.Parameters.AddWithValue("@DL31_KOMM_ANFORDERUNG_NR", inputModel.DL31_KOMM_ANFORDERUNG_NR);
                    cmd.Parameters.AddWithValue("@DL31_KOMM__STATUS_ID", inputModel.DL31_KOMM__STATUS_ID);
                    cmd.Parameters.AddWithValue("@DL31_ERPROBUNGSINHALT", inputModel.DL31_ERPROBUNGSINHALT);
                    cmd.Parameters.AddWithValue("@DL31_START_DATUM", inputModel.DL31_START_DATUM);
                    cmd.Parameters.AddWithValue("@DL31_ENDE_DATUM", inputModel.DL31_ENDE_DATUM);
                    cmd.Parameters.AddWithValue("@DL31_KOMM_ERPROBUNGSORT_ID", inputModel.DL31_KOMM_ERPROBUNGSORT_ID);
                    cmd.Parameters.AddWithValue("@DL31_KOMM_STRECKENART_ID", inputModel.DL31_KOMM_STRECKENART_ID);
                    cmd.Parameters.AddWithValue("@DL31_KOMM_ERPROBUNGSART_ID", inputModel.DL31_KOMM_ERPROBUNGSART_ID);
                    cmd.Parameters.AddWithValue("@DL31_BAUREIHEN", inputModel.DL31_BAUREIHEN);
                    cmd.Parameters.AddWithValue("@DL31_AUFTRAGGEBER_OE", inputModel.DL31_AUFTRAGGEBER_OE);
                    cmd.Parameters.AddWithValue("@DL31_AUFTRAGGEBER_PERSID", inputModel.DL31_AUFTRAGGEBER_PERSID);
                    cmd.Parameters.AddWithValue("@DL31_FAHRTENLEITER_PERSID", inputModel.DL31_FAHRTENLEITER_PERSID);
                    cmd.Parameters.AddWithValue("@DL31_ENGINEERING_AST_PERSID", inputModel.DL31_ENGINEERING_AST_PERSID);
                    cmd.Parameters.AddWithValue("@DL31_FAHRBERECHTIGUNG_ID", inputModel.DL31_FAHRBERECHTIGUNG_ID);
                    cmd.Parameters.AddWithValue("@DL31_HV_QUALIFIKATION_ID", inputModel.DL31_HV_QUALIFIKATION_ID);
                    cmd.Parameters.AddWithValue("@DL31_SONDERQUALIFIKATION_ID", inputModel.DL31_SONDERQUALIFIKATION_ID);
                    cmd.Parameters.AddWithValue("@DL31_SAMSTAGSARBEIT", inputModel.DL31_SAMSTAGSARBEIT);
                    cmd.Parameters.AddWithValue("@DL31_SONNTAGSARBEIT", inputModel.DL31_SONNTAGSARBEIT);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Script de INSERT IN DL31.
        /// </summary>
        /// <returns></returns>
        private string InsertStatement()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO DL31_KOMM_ANFORDERUNG ");
            sb.Append("(DL31_KOMM_ANFORDERUNG_NR, ");
            sb.Append("DL31_KOMM__STATUS_ID, ");
            sb.Append("DL31_ERPROBUNGSINHALT, ");
            sb.Append("DL31_START_DATUM, ");
            sb.Append("DL31_ENDE_DATUM, ");
            sb.Append("DL31_KOMM_ERPROBUNGSORT_ID, ");
            sb.Append("DL31_KOMM_STRECKENART_ID, ");
            sb.Append("DL31_KOMM_ERPROBUNGSART_ID, ");
            sb.Append("DL31_BAUREIHEN, ");
            sb.Append("DL31_AUFTRAGGEBER_OE, ");
            sb.Append("DL31_AUFTRAGGEBER_PERSID, ");
            sb.Append("DL31_FAHRTENLEITER_PERSID, ");
            sb.Append("DL31_ENGINEERING_AST_PERSID, ");
            sb.Append("DL31_FAHRBERECHTIGUNG_ID, ");
            sb.Append("DL31_HV_QUALIFIKATION_ID, ");
            sb.Append("DL31_SONDERQUALIFIKATION_ID, ");
            sb.Append("DL31_SAMSTAGSARBEIT, ");
            sb.Append("DL31_SONNTAGSARBEIT) ");
            sb.Append("VALUES ");
            sb.Append("(@DL31_KOMM_ANFORDERUNG_NR, ");
            sb.Append("@DL31_KOMM__STATUS_ID , ");
            sb.Append("@DL31_ERPROBUNGSINHALT, ");
            sb.Append("@DL31_START_DATUM, ");
            sb.Append("@DL31_ENDE_DATUM, ");
            sb.Append("@DL31_KOMM_ERPROBUNGSORT_ID , ");
            sb.Append("@DL31_KOMM_STRECKENART_ID, ");
            sb.Append("@DL31_KOMM_ERPROBUNGSART_ID , ");
            sb.Append("@DL31_BAUREIHEN, ");
            sb.Append("@DL31_AUFTRAGGEBER_OE, ");
            sb.Append("@DL31_AUFTRAGGEBER_PERSID, ");
            sb.Append("@DL31_FAHRTENLEITER_PERSID, ");
            sb.Append("@DL31_ENGINEERING_AST_PERSID, ");
            sb.Append("@DL31_FAHRBERECHTIGUNG_ID, ");
            sb.Append("@DL31_HV_QUALIFIKATION_ID, ");
            sb.Append("@DL31_SONDERQUALIFIKATION_ID, ");
            sb.Append("@DL31_SAMSTAGSARBEIT, ");
            sb.Append("@DL31_SONNTAGSARBEIT) ");

            return sb.ToString();
        }

        /// <summary>
        /// Create a SQL query string.
        /// </summary>
        /// <returns></returns>
        private string CreateQueryForOverviewTable()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            sb.Append(" DL31_KOMM_ANFORDERUNG_NR,");
            sb.Append(" DL31_ERPROBUNGSINHALT,");
            sb.Append(" DL31_START_DATUM,");
            sb.Append(" DL31_ENDE_DATUM,");
            sb.Append(" DL38_BEZEICHNUNG,");
            sb.Append(" DL39_BEZEICHNUNG,");
            sb.Append(" DL40_BEZEICHNUNG,");
            sb.Append(" DL31_BAUREIHEN,");
            sb.Append(" DL31_AUFTRAGGEBER_PERSID, ");
            sb.Append(" DL37_BEZEICHNUNG, ");
            sb.Append(" DL31_KOMM__STATUS_ID,");
            sb.Append(" DL31_KOMM_ANFORDERUNG_NR,");
            sb.Append(" (SELECT BD255_BEZEICHNUNG FROM bd255_reise_art_tbl WHERE bd255_reise_art_tbl.BD255_REISE_ART_ID = DL31_REISE_ART_ID)as BD255_BEZEICHNUNG,");
            sb.Append(" CONCAT(BD09_NAME,'  ',BD09_VORNAME,'  ',BD06_KURZ_BEZ) as 'CustomerOEString',");
            sb.Append(" DL31_KOMM_ERPROBUNGSORT_ID,");
            sb.Append(" DL31_KOMM_STRECKENART_ID,");
            sb.Append(" DL31_KOMM_ERPROBUNGSART_ID,");
            sb.Append(" DL31_AUFTRAGGEBER_OE,");
            sb.Append(" DL31_FAHRTENLEITER_PERSID,");
            sb.Append(" DL31_ENGINEERING_AST_PERSID,");
            sb.Append(" DL31_FAHRBERECHTIGUNG_ID,");
            sb.Append(" DL31_HV_QUALIFIKATION_ID,");
            sb.Append(" DL31_SONDERQUALIFIKATION_ID,");
            sb.Append(" DL31_PERSID_AENDERNG,");
            sb.Append(" DL31_PERSID_ERFASSNG,");
            sb.Append(" DL31_DATUM_AENDERNG,");
            sb.Append(" DL31_DATUM_ERFASSNG, ");
            sb.Append(" DL31_SAMSTAGSARBEIT, ");
            sb.Append(" DL31_SONNTAGSARBEIT, ");
            sb.Append(" DL31_KOMM_ANFORDERUNG_ID ");
            sb.Append(" FROM dl31_komm_anforderung ");
            sb.Append(" JOIN bd09_person on DL31_AUFTRAGGEBER_PERSID = BD09_PERSID ");
            sb.Append(" JOIN bd06_org_einheit_tbl on BD09_OE = BD06_OE ");
            sb.Append(" INNER JOIN dl38_komm_erprobungsort_tbl ON DL38_KOMM_ERPROBUNGSORT_ID = DL31_KOMM_ERPROBUNGSORT_ID ");
            sb.Append(" INNER JOIN dl39_komm_streckenart_tbl  ON  DL39_KOMM_STRECKENART_ID = DL31_KOMM_STRECKENART_ID ");
            sb.Append(" INNER JOIN dl40_komm_erprobungsart_tbl ON DL40_KOMM_ERPROBUNGSART_ID = DL31_KOMM_ERPROBUNGSART_ID ");
            sb.Append(" INNER JOIN dl37_komm_status_tbl ON DL37_KOMM_STATUS_ID = DL31_KOMM__STATUS_ID ");

            return sb.ToString();
        }

        /// <summary>
        /// Creates the query for DL38 table.
        /// </summary>
        /// <returns></returns>
        private string CreateQueryForDl38Table()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT ");
            sb.Append(" DL38_KOMM_ERPROBUNGSORT_ID,");
            sb.Append(" DL38_BEZEICHNUNG,");
            sb.Append(" DL38_PERSID_AENDERNG,");
            sb.Append(" DL38_PERSID_ERFASSNG,");
            sb.Append(" DL38_DATUM_AENDERNG,");
            sb.Append(" DL38_DATUM_ERFASSNG");
            sb.Append(" FROM dl38_komm_erprobungsort_tbl");

            return sb.ToString();
        }

        /// <summary>
        /// Creates the query for DL39 table.
        /// </summary>
        /// <returns></returns>
        private string CreateQueryForDl39Table()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT ");
            sb.Append(" DL39_KOMM_STRECKENART_ID,");
            sb.Append(" DL39_BEZEICHNUNG,");
            sb.Append(" DL39_PERSID_AENDERNG,");
            sb.Append(" DL39_PERSID_ERFASSNG,");
            sb.Append(" DL39_DATUM_AENDERNG,");
            sb.Append(" DL39_DATUM_ERFASSNG");
            sb.Append(" FROM dl39_komm_streckenart_tbl");

            return sb.ToString();
        }

        /// <summary>
        /// Creates the query for DL40 table.
        /// </summary>
        /// <returns></returns>
        private string CreateQueryForDl40Table()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT ");
            sb.Append(" DL40_KOMM_ERPROBUNGSART_ID,");
            sb.Append(" DL40_BEZEICHNUNG,");
            sb.Append(" DL40_PERSID_AENDERNG,");
            sb.Append(" DL40_PERSID_ERFASSNG,");
            sb.Append(" DL40_DATUM_AENDERNG,");
            sb.Append(" DL40_DATUM_ERFASSNG");
            sb.Append(" FROM dl40_komm_erprobungsart_tbl");

            return sb.ToString();
        }

        /// <summary>
        /// Creates the query for SD111 table.
        /// </summary>
        /// <returns></returns>
        private string CreateQueryForSd111Table()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT ");
            sb.Append(" SD111_QUALIFIKATIONEN_ID,");
            sb.Append(" SD111_TYP,");
            sb.Append(" SD111_WERT,");
            sb.Append(" SD111_PERSID_AENDERNG,");
            sb.Append(" SD111_PERSID_ERFASSNG,");
            sb.Append(" SD111_DATUM_AENDERNG");
            sb.Append("  SD111_DATUM_ERFASSNG");
            sb.Append(" FROM sd111_qualifikationen");

            return sb.ToString();
        }

        /// <summary>
        /// Creates the query for allocation table.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private string CreateQueryForAllocationTable()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT ");
            sb.Append(" DL32_KOMM_ANFORDERUNG_KONTO_ID,");
            sb.Append(" DL32_KONTIERUNG,");
            sb.Append(" DL32_EXT_KOMM_ANFORDERUNG_ID,");
            sb.Append(" DL32_ANTEIL_PROZENT");
            sb.Append(" FROM dl32_ext_komm_konto");

            return sb.ToString();
        }

        /// <summary>
        /// Creates the query for DB06 table.
        /// </summary>
        /// <returns></returns>
        private string CreateQueryForDb06Table()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT ");
            sb.Append(" BD06_OE,");
            sb.Append(" BD06_KURZ_BEZ,");
            sb.Append(" BD06_PERSID_AENDERNG,");
            sb.Append(" BD06_PERSID_ERFASSNG,");
            sb.Append(" BD06_DATUM_AENDERNG,");
            sb.Append(" BD06_DATUM_ERFASSNG");
            sb.Append(" FROM bd06_org_einheit_tbl");

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

        /// <summary>
        /// Crate delete string for overview table.
        /// </summary>
        /// <param name="deleteID">The delete identifier.</param>
        /// <returns></returns>
        private string DeleteStatement(decimal deleteID)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" DELETE FROM dl31_komm_anforderung ");
            sb.Append(" WHERE DL31_KOMM_ANFORDERUNG_ID = {0} ");

            return String.Format(sb.ToString(), deleteID);
        }

        /// <summary>
        /// Create delete string for allocation table.
        /// </summary>
        /// <param name="deleteID">The delete identifier.</param>
        /// <returns></returns>
        private string DeleteStatementAllocation(decimal deleteID)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" DELETE FROM dl32_ext_komm_konto ");
            sb.Append(" WHERE DL32_EXT_KOMM_ANFORDERUNG_ID = {0} ");

            return String.Format(sb.ToString(), deleteID);
        }

        private string UpdateDl32Statement()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE DL32_EXT_KOMM_KONTO ");
            sb.Append("SET DL32_KONTIERUNG=@DL32_KONTIERUNG, ");
            sb.Append("DL32_ANTEIL_PROZENT=@DL32_ANTEIL_PROZENT ");
            sb.Append("WHERE DL32_KOMM_ANFORDERUNG_KONTO_ID=@DL32_KOMM_ANFORDERUNG_KONTO_ID");

            return sb.ToString();
        }

        /// <summary>
        /// Updates the statement.
        /// </summary>
        /// <returns></returns>
        private string UpdateStatement()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE DL31_KOMM_ANFORDERUNG ");
            sb.Append("SET DL31_KOMM_ANFORDERUNG_NR=@DL31_KOMM_ANFORDERUNG_NR, ");
            sb.Append("DL31_KOMM__STATUS_ID=@DL31_KOMM__STATUS_ID, ");
            sb.Append("DL31_ERPROBUNGSINHALT=@DL31_ERPROBUNGSINHALT, ");
            sb.Append("DL31_START_DATUM=@DL31_START_DATUM, ");
            sb.Append("DL31_ENDE_DATUM=@DL31_ENDE_DATUM, ");
            sb.Append("DL31_KOMM_ERPROBUNGSORT_ID=@DL31_KOMM_ERPROBUNGSORT_ID, ");
            sb.Append("DL31_KOMM_STRECKENART_ID=@DL31_KOMM_STRECKENART_ID, ");
            sb.Append("DL31_KOMM_ERPROBUNGSART_ID=@DL31_KOMM_ERPROBUNGSART_ID, ");
            sb.Append("DL31_BAUREIHEN=@DL31_BAUREIHEN, ");
            sb.Append("DL31_AUFTRAGGEBER_OE=@DL31_AUFTRAGGEBER_OE, ");
            sb.Append("DL31_AUFTRAGGEBER_PERSID=@DL31_AUFTRAGGEBER_PERSID, ");
            sb.Append("DL31_FAHRTENLEITER_PERSID=@DL31_FAHRTENLEITER_PERSID, ");
            sb.Append("DL31_ENGINEERING_AST_PERSID=@DL31_ENGINEERING_AST_PERSID, ");
            sb.Append("DL31_FAHRBERECHTIGUNG_ID=@DL31_FAHRBERECHTIGUNG_ID, ");
            sb.Append("DL31_HV_QUALIFIKATION_ID=@DL31_HV_QUALIFIKATION_ID, ");
            sb.Append("DL31_SONDERQUALIFIKATION_ID=@DL31_SONDERQUALIFIKATION_ID, ");
            sb.Append("DL31_SAMSTAGSARBEIT=@DL31_SAMSTAGSARBEIT, ");
            sb.Append("DL31_SONNTAGSARBEIT=@DL31_SONNTAGSARBEIT ");
            sb.Append("WHERE DL31_KOMM_ANFORDERUNG_ID=@DL31_KOMM_ANFORDERUNG_ID");

            return sb.ToString();
        }

        private string InsertDl32Statement()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO dl32_ext_komm_konto ");
            sb.Append("(DL32_KONTIERUNG , ");
            sb.Append("DL32_ANTEIL_PROZENT, ");
            sb.Append("DL32_EXT_KOMM_ANFORDERUNG_ID) ");
            sb.Append("VALUES ");
            sb.Append("(@DL32_KONTIERUNG, ");
            sb.Append("@DL32_ANTEIL_PROZENT, ");
            sb.Append("@DL32_EXT_KOMM_ANFORDERUNG_ID)");

            return sb.ToString();
        }
    }
}