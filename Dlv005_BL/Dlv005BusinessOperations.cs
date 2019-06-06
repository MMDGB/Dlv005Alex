using Dlv005_DL;
using System;
using System.Data;

namespace Dlv005_BL
{
    /// <summary>
    ///
    /// </summary>
    public partial class Dlv005BusinessOperations
    {
        public Dlv005DataSet Dlv005DataSet { get; set; }
        public Dlv005SelectionTableDataSet Dlv005SelectionTableDataSet { get; set; }
        private decimal? allocationGridInsertPosition = 1;
        private bool confirmIsNewCopy;
        private decimal auxID;

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <returns></returns>
        public Dlv005DataSet GetData()
        {
            Dlv005DataSet = new Dlv005DataSet();
            Dlv005DataSet.Initialize(Dlv005DataSet);
            Dlv005DataSet.AcceptChanges();
            return Dlv005DataSet;
        }

        /// <summary>
        /// Creates the new allocation row if needed d for update.
        /// </summary>
        /// <param name="row">The row.</param>
        public void CreateNewAllocationRowIfNeededDForUpdate(Dlv005DataSet.MainTableRow row)
        {
            Dlv005DataSet.AllocationGridTableRow allocDataRow = Dlv005DataSet.AllocationGridTable.NewAllocationGridTableRow();
            allocDataRow.DL32_KONTIERUNG = string.Empty;
            allocDataRow.DL32_EXT_KOMM_ANFORDERUNG_ID = row.DL31_KOMM_ANFORDERUNG_ID;
            allocDataRow.DL32_KOMM_ANFORDERUNG_KONTO_ID = -1;
            Dlv005DataSet.AllocationGridTable.Rows.Add(allocDataRow);
        }

        /// <summary>
        /// Creates the new allocation row if needed for new.
        /// </summary>
        public void CreateNewAllocationRowIfNeededForNew()
        {
            Dlv005DataSet.AllocationGridTableRow allocDataRow = Dlv005DataSet.AllocationGridTable.NewAllocationGridTableRow();
            allocDataRow.DL32_KONTIERUNG = string.Empty;
            allocDataRow.DL32_EXT_KOMM_ANFORDERUNG_ID = -1;
            allocDataRow.DL32_KOMM_ANFORDERUNG_KONTO_ID = -1;
            Dlv005DataSet.AllocationGridTable.Rows.Add(allocDataRow);
        }

        /// <summary>
        /// Create a new record in the DB.
        /// </summary>
        public void CreateNew()
        {
            Dlv005DataSet.MainTableRow dataRow = Dlv005DataSet.MainTable.NewMainTableRow();
            dataRow.DL31_ERPROBUNGSINHALT = string.Empty;
            dataRow.DL37_BEZEICHNUNG = string.Empty;
            dataRow.DL31_START_DATUM = default(DateTime);
            dataRow.DL31_ENDE_DATUM = default(DateTime);
            dataRow.DL31_KOMM_ERPROBUNGSORT_ID = -1;
            dataRow.DL31_KOMM_STRECKENART_ID = -1;
            dataRow.DL31_KOMM_ERPROBUNGSART_ID = -1;
            dataRow.DL40_BEZEICHNUNG = string.Empty;
            dataRow.DL31_BAUREIHEN = string.Empty;
            dataRow.DL31_AUFTRAGGEBER_OE = -1;
            dataRow.DL31_AUFTRAGGEBER_PERSID = -1;
            dataRow.DL31_FAHRTENLEITER_PERSID = -1;
            dataRow.DL31_ENGINEERING_AST_PERSID = -1;
            dataRow.DL31_FAHRBERECHTIGUNG_ID = -1;
            dataRow.DL31_HV_QUALIFIKATION_ID = -1;
            dataRow.DL31_SONDERQUALIFIKATION_ID = -1;
            dataRow.DL31_KOMM__STATUS_ID = 1;
            dataRow.DL31_SAMSTAGSARBEIT = "n";
            dataRow.DL31_SONNTAGSARBEIT = "n";
            Dlv005DataSet.MainTable.Rows.Add(dataRow);

            Dlv005DataSet.AllocationGridTableRow allocDataRow = Dlv005DataSet.AllocationGridTable.NewAllocationGridTableRow();
            allocDataRow.DL32_KONTIERUNG = string.Empty;
            allocDataRow.DL32_EXT_KOMM_ANFORDERUNG_ID = -1;
            Dlv005DataSet.AllocationGridTable.Rows.Add(allocDataRow);
        }

        /// <summary>
        /// Creates the new copy.
        /// </summary>
        /// <param name="dataCopyRow">The data copy row.</param>
        public void CreateNewCopy(DataRow dataCopyRow, bool newCopy)
        {
            Dlv005DataSet.MainTableRow dataCRow = Dlv005DataSet.MainTable.NewMainTableRow();
            dataCRow.DL31_KOMM_ANFORDERUNG_NR = string.Empty;
            dataCRow.DL31_ERPROBUNGSINHALT = dataCopyRow["DL31_ERPROBUNGSINHALT"].ToString(); 
            dataCRow.DL37_BEZEICHNUNG = string.Empty;
            dataCRow.DL31_START_DATUM = default(DateTime);
            dataCRow.DL31_ENDE_DATUM = default(DateTime);
            dataCRow.DL31_KOMM__STATUS_ID = 1;
            dataCRow.DL31_KOMM_ERPROBUNGSORT_ID = Convert.ToDecimal(dataCopyRow["DL31_KOMM_ERPROBUNGSORT_ID"]);
            dataCRow.DL31_KOMM_STRECKENART_ID = Convert.ToDecimal(dataCopyRow["DL31_KOMM_STRECKENART_ID"]);
            dataCRow.DL31_KOMM_ERPROBUNGSART_ID = Convert.ToDecimal(dataCopyRow["DL31_KOMM_ERPROBUNGSART_ID"]);
            dataCRow.DL40_BEZEICHNUNG = dataCopyRow["DL40_BEZEICHNUNG"].ToString();
            dataCRow.DL31_BAUREIHEN = dataCopyRow["DL31_BAUREIHEN"].ToString();
            dataCRow.DL31_AUFTRAGGEBER_OE = Convert.ToDecimal(dataCopyRow["DL31_AUFTRAGGEBER_OE"]);
            dataCRow.DL31_AUFTRAGGEBER_PERSID = Convert.ToDecimal(dataCopyRow["DL31_AUFTRAGGEBER_PERSID"]);
            dataCRow.DL31_FAHRTENLEITER_PERSID = Convert.ToDecimal(dataCopyRow["DL31_FAHRTENLEITER_PERSID"]);
            dataCRow.DL31_ENGINEERING_AST_PERSID = Convert.ToDecimal(dataCopyRow["DL31_ENGINEERING_AST_PERSID"]);
            dataCRow.DL31_FAHRBERECHTIGUNG_ID = Convert.ToDecimal(dataCopyRow["DL31_FAHRBERECHTIGUNG_ID"]);
            dataCRow.DL31_HV_QUALIFIKATION_ID = Convert.ToDecimal(dataCopyRow["DL31_HV_QUALIFIKATION_ID"]);
            dataCRow.DL31_SONDERQUALIFIKATION_ID = Convert.ToDecimal(dataCopyRow["DL31_SONDERQUALIFIKATION_ID"]);
            dataCRow.DL31_SAMSTAGSARBEIT = dataCopyRow["DL31_SAMSTAGSARBEIT"].ToString();
            dataCRow.DL31_SONNTAGSARBEIT = dataCopyRow["DL31_SONNTAGSARBEIT"].ToString();
            dataCRow.DL31_KOMM_ANFORDERUNG_ID = Convert.ToDecimal(dataCopyRow["DL31_KOMM_ANFORDERUNG_ID"]);
            Dlv005DataSet.MainTable.Rows.Add(dataCRow);

            confirmIsNewCopy = newCopy;
            auxID = dataCRow.DL31_KOMM_ANFORDERUNG_ID;
        }

        /// <summary>
        /// Saves the specific data in DB.
        /// </summary
        public void SaveCommision(DataRow dataRowForInsertNewCommision)
        {
            InputModel inputModel = new InputModel();

            inputModel.DL31_KOMM_ANFORDERUNG_NR = dataRowForInsertNewCommision["DL31_KOMM_ANFORDERUNG_NR"].ToString();
            inputModel.DL31_ERPROBUNGSINHALT = dataRowForInsertNewCommision["DL31_ERPROBUNGSINHALT"].ToString();
            inputModel.DL31_START_DATUM = Convert.ToDateTime(dataRowForInsertNewCommision["DL31_START_DATUM"]);
            inputModel.DL31_ENDE_DATUM = Convert.ToDateTime(dataRowForInsertNewCommision["DL31_ENDE_DATUM"]);
            inputModel.DL31_BAUREIHEN = dataRowForInsertNewCommision["DL31_BAUREIHEN"].ToString();
            inputModel.DL31_AUFTRAGGEBER_PERSID = Convert.ToDecimal(dataRowForInsertNewCommision["DL31_AUFTRAGGEBER_PERSID"]);
            inputModel.DL31_KOMM_ERPROBUNGSORT_ID = Convert.ToDecimal(dataRowForInsertNewCommision["DL31_KOMM_ERPROBUNGSORT_ID"]);
            inputModel.DL31_KOMM_STRECKENART_ID = Convert.ToDecimal(dataRowForInsertNewCommision["DL31_KOMM_STRECKENART_ID"]);
            inputModel.DL31_KOMM_ERPROBUNGSART_ID = Convert.ToDecimal(dataRowForInsertNewCommision["DL31_KOMM_ERPROBUNGSART_ID"]);
            inputModel.DL31_AUFTRAGGEBER_OE = Convert.ToDecimal(dataRowForInsertNewCommision["DL31_AUFTRAGGEBER_OE"]);
            inputModel.DL31_FAHRTENLEITER_PERSID = Convert.ToDecimal(dataRowForInsertNewCommision["DL31_FAHRTENLEITER_PERSID"]);
            inputModel.DL31_ENGINEERING_AST_PERSID = Convert.ToDecimal(dataRowForInsertNewCommision["DL31_ENGINEERING_AST_PERSID"]);
            inputModel.DL31_HV_QUALIFIKATION_ID = Convert.ToDecimal(dataRowForInsertNewCommision["DL31_HV_QUALIFIKATION_ID"]);
            inputModel.DL31_SONDERQUALIFIKATION_ID = Convert.ToDecimal(dataRowForInsertNewCommision["DL31_SONDERQUALIFIKATION_ID"]);
            inputModel.DL31_FAHRBERECHTIGUNG_ID = Convert.ToDecimal(dataRowForInsertNewCommision["DL31_FAHRBERECHTIGUNG_ID"]);
            inputModel.DL31_KOMM__STATUS_ID = Convert.ToDecimal(dataRowForInsertNewCommision["DL31_KOMM__STATUS_ID"]);
            inputModel.DL31_SAMSTAGSARBEIT = dataRowForInsertNewCommision["DL31_SAMSTAGSARBEIT"].ToString();
            inputModel.DL31_SONNTAGSARBEIT = dataRowForInsertNewCommision["DL31_SONNTAGSARBEIT"].ToString();

            Dlv005DataSet.Insert(inputModel);
            ReloadOverview();

            DataRow lastRow = Dlv005DataSet.MainTable.Rows[Dlv005DataSet.MainTable.Rows.Count - 1];
            allocationGridInsertPosition = Convert.ToDecimal(lastRow["DL31_KOMM_ANFORDERUNG_ID"]);


            AllocationInputModel allocationInputModel = new AllocationInputModel();

            if (confirmIsNewCopy)
            {
                foreach (Dlv005DataSet.AllocationGridTableRow row in Dlv005DataSet.AllocationGridTable)
                {
                    var rowValue = Convert.ToDecimal(row.DL32_EXT_KOMM_ANFORDERUNG_ID);
                    if (rowValue == auxID)
                    {
                        allocationInputModel.DL32_KONTIERUNG = row.IsDL32_KONTIERUNGNull() ? string.Empty : row.DL32_KONTIERUNG;
                        allocationInputModel.DL32_ANTEIL_PROZENT = row.IsDL32_ANTEIL_PROZENTNull() ? null : row.DL32_ANTEIL_PROZENT as decimal?;
                        allocationInputModel.DL32_EXT_KOMM_ANFORDERUNG_ID = allocationGridInsertPosition;

                        Dlv005DataSet.InsertDl32(allocationInputModel);
                    }
                }
            }

            foreach (Dlv005DataSet.AllocationGridTableRow row in Dlv005DataSet.AllocationGridTable)
            {
                var rowValue = Convert.ToDecimal(row.DL32_EXT_KOMM_ANFORDERUNG_ID);
                if (rowValue < 0)
                {
                    allocationInputModel.DL32_KONTIERUNG = row.IsDL32_KONTIERUNGNull() ? string.Empty : row.DL32_KONTIERUNG;
                    allocationInputModel.DL32_ANTEIL_PROZENT = row.IsDL32_ANTEIL_PROZENTNull() ? null : row.DL32_ANTEIL_PROZENT as decimal?;
                    allocationInputModel.DL32_EXT_KOMM_ANFORDERUNG_ID = allocationGridInsertPosition;

                    Dlv005DataSet.InsertDl32(allocationInputModel);
                }
            }
            confirmIsNewCopy = false;
            auxID = 0;
        }

        /// <summary>
        /// Updates a commision.
        /// </summary>
        /// <param name="position">The position.</param>
        public void UpdateCommision(DataRow dataUpdateRow)
        {
            InputModel inputModel = new InputModel
            {
                DL31_KOMM_ANFORDERUNG_NR = dataUpdateRow["DL31_KOMM_ANFORDERUNG_NR"].ToString(),
                DL31_KOMM__STATUS_ID = Convert.ToDecimal(dataUpdateRow["DL31_KOMM__STATUS_ID"]),
                DL31_ERPROBUNGSINHALT = dataUpdateRow["DL31_ERPROBUNGSINHALT"].ToString(),
                DL31_START_DATUM = Convert.ToDateTime(dataUpdateRow["DL31_START_DATUM"]),
                DL31_ENDE_DATUM = Convert.ToDateTime(dataUpdateRow["DL31_ENDE_DATUM"]),
                DL31_KOMM_ERPROBUNGSORT_ID = Convert.ToDecimal(dataUpdateRow["DL31_KOMM_ERPROBUNGSORT_ID"]),
                DL31_KOMM_STRECKENART_ID = Convert.ToDecimal(dataUpdateRow["DL31_KOMM_STRECKENART_ID"]),
                DL31_KOMM_ERPROBUNGSART_ID = Convert.ToDecimal(dataUpdateRow["DL31_KOMM_ERPROBUNGSART_ID"]),
                DL31_BAUREIHEN = dataUpdateRow["DL31_BAUREIHEN"].ToString(),
                DL31_AUFTRAGGEBER_OE = Convert.ToDecimal(dataUpdateRow["DL31_AUFTRAGGEBER_OE"]),
                DL31_AUFTRAGGEBER_PERSID = Convert.ToDecimal(dataUpdateRow["DL31_AUFTRAGGEBER_PERSID"]),
                DL31_FAHRTENLEITER_PERSID = Convert.ToDecimal(dataUpdateRow["DL31_FAHRTENLEITER_PERSID"]),
                DL31_ENGINEERING_AST_PERSID = Convert.ToDecimal(dataUpdateRow["DL31_ENGINEERING_AST_PERSID"]),
                DL31_FAHRBERECHTIGUNG_ID = Convert.ToDecimal(dataUpdateRow["DL31_FAHRBERECHTIGUNG_ID"]),
                DL31_HV_QUALIFIKATION_ID = Convert.ToDecimal(dataUpdateRow["DL31_HV_QUALIFIKATION_ID"]),
                DL31_SONDERQUALIFIKATION_ID = Convert.ToDecimal(dataUpdateRow["DL31_SONDERQUALIFIKATION_ID"]),
                DL31_SAMSTAGSARBEIT = dataUpdateRow["DL31_SAMSTAGSARBEIT"].ToString(),
                DL31_SONNTAGSARBEIT = dataUpdateRow["DL31_SONNTAGSARBEIT"].ToString(),
                DL31_KOMM_ANFORDERUNG_ID = Convert.ToDecimal(dataUpdateRow["DL31_KOMM_ANFORDERUNG_ID"]),
            };

            Dlv005DataSet.Update(inputModel);

            AllocationInputModel allocationInputModel = new AllocationInputModel();

            var dl31ForeingKeyId = Convert.ToDecimal(inputModel.DL31_KOMM_ANFORDERUNG_ID);

            foreach (Dlv005DataSet.AllocationGridTableRow row in Dlv005DataSet.AllocationGridTable)
            {
                var dl32ForeignKeyId = Convert.ToDecimal(row.DL32_EXT_KOMM_ANFORDERUNG_ID);
                var dl32PrimaryKey = Convert.ToDecimal(row.DL32_KOMM_ANFORDERUNG_KONTO_ID);

                if (dl32ForeignKeyId == dl31ForeingKeyId && dl32PrimaryKey > 0)
                {
                    allocationInputModel.DL32_KONTIERUNG = row.IsDL32_KONTIERUNGNull() ? string.Empty : row.DL32_KONTIERUNG;
                    allocationInputModel.DL32_ANTEIL_PROZENT = row.IsDL32_ANTEIL_PROZENTNull() ? null : row.DL32_ANTEIL_PROZENT as decimal?;
                    allocationInputModel.DL32_KOMM_ANFORDERUNG_KONTO_ID = row.IsDL32_KOMM_ANFORDERUNG_KONTO_IDNull() ? null : row.DL32_KOMM_ANFORDERUNG_KONTO_ID as decimal?;

                    Dlv005DataSet.UpdateDl32(allocationInputModel);
                }
                else if (dl32ForeignKeyId == dl31ForeingKeyId && dl32PrimaryKey < 0)
                {
                    allocationInputModel.DL32_KONTIERUNG = row.IsDL32_KONTIERUNGNull() ? string.Empty : row.DL32_KONTIERUNG;
                    allocationInputModel.DL32_ANTEIL_PROZENT = row.IsDL32_ANTEIL_PROZENTNull() ? null : row.DL32_ANTEIL_PROZENT as decimal?;
                    allocationInputModel.DL32_EXT_KOMM_ANFORDERUNG_ID = row.IsDL32_EXT_KOMM_ANFORDERUNG_IDNull() ? null : row.DL32_EXT_KOMM_ANFORDERUNG_ID as decimal?;

                    Dlv005DataSet.InsertDl32(allocationInputModel);
                }
            }
            confirmIsNewCopy = false;
            auxID = 0;
            ReloadAllocation();
        }

        /// <summary>
        /// Deletes the data DL32.
        /// </summary>
        /// <param name="deleteID">The delete identifier.</param>
        public void DeleteDataDl32(decimal deleteID)
        {
            Dlv005DataSet.DeleteAllocation(deleteID);
        }

        /// <summary>
        /// Cancels the save operation and may perform some rollback operations.
        /// </summary>
        public void CancelSave()
        {
            Dlv005DataSet.RejectChanges();
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        public void DeleteData(decimal deleteID)
        {
            Dlv005DataSet.Delete(deleteID);
        }

        public void ReloadAllocation()
        {
            Dlv005DataSet.AllocationGridTable.Clear();
            Dlv005DataSet.InitializeAllocationTable(Dlv005DataSet);
            Dlv005DataSet.AcceptChanges();
        }

        /// <summary>
        /// Reloads the overview.
        /// </summary>
        public void ReloadOverview()
        {
            Dlv005DataSet.MainTable.Clear();
            Dlv005DataSet.InitializeMainTable(Dlv005DataSet);
            Dlv005DataSet.AcceptChanges();
        }

        /// <summary>
        /// Reloads this instance.
        /// </summary>
        public void Reload()
        {
            Dlv005DataSet.Clear();
            Dlv005DataSet.Initialize(Dlv005DataSet);
            Dlv005DataSet.AcceptChanges();
        }
    }
}