using Dlv005_DL;

namespace Dlv005_BL
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Data.DataSet" />
    public partial class Dlv005DataSet
    {
        partial class MainTableDataTable
        {
        }

        public void InitializeMainTable(Dlv005DataSet dataModel)
        {
            Dlv005DataAccessComponent dataAccessComponent = new Dlv005DataAccessComponent();
            dataAccessComponent.GetOverviewData(dataModel.MainTable);
        }

        public void InitializeAllocationTable(Dlv005DataSet dataModel)
        {
            Dlv005DataAccessComponent dataAccessComponent = new Dlv005DataAccessComponent();
            dataAccessComponent.GetAllocationGridData(dataModel.AllocationGridTable);
        }

        /// <summary>
        /// Initializes the specified data model.
        /// </summary>
        /// <param name="dataModel">The data model.</param>
        public void Initialize(Dlv005DataSet dataModel)
        {
            Dlv005DataAccessComponent dataAccessComponent = new Dlv005DataAccessComponent();
            dataAccessComponent.GetOverviewData(dataModel.MainTable);
            dataAccessComponent.GetDL38Data(dataModel.DL38Table);
            dataAccessComponent.GetDL39Data(dataModel.DL39Table);
            dataAccessComponent.GetDL40Data(dataModel.DL40Table);
            dataAccessComponent.GetSD111Data(dataModel.SD111Table);
            dataAccessComponent.GetBD09Data(dataModel.BD09Table);
            dataAccessComponent.GetBD06Data(dataModel.BD06Table);
            dataAccessComponent.GetAllocationGridData(dataModel.AllocationGridTable);
        }

        /// <summary>
        /// Inserts the specified input model.
        /// </summary>
        /// <param name="inputModel">The input model.</param>
        public void Insert(InputModel inputModel)
        {
            Dlv005DataAccessComponent dataAccessComponent = new Dlv005DataAccessComponent();
            dataAccessComponent.InsertData(inputModel);
        }

        /// <summary>
        /// Inserts the DL32.
        /// </summary>
        /// <param name="allocationInputModel">The allocation input model.</param>
        public void InsertDl32(AllocationInputModel allocationInputModel)
        {
            Dlv005DataAccessComponent dataAccessComponent = new Dlv005DataAccessComponent();
            dataAccessComponent.InsertDataDl32(allocationInputModel);
        }

        /// <summary>
        /// Updates the specified input model.
        /// </summary>
        /// <param name="inputModel">The input model.</param>
        public void Update(InputModel inputModel)
        {
            Dlv005DataAccessComponent dataAccessComponent = new Dlv005DataAccessComponent();
            dataAccessComponent.UpdateData(inputModel);
        }

        /// <summary>
        /// Deletes the specified delete identifier.
        /// </summary>
        /// <param name="deleteID">The delete identifier.</param>
        public void Delete(decimal deleteID)
        {
            Dlv005DataAccessComponent dataAccessComponent = new Dlv005DataAccessComponent();
            dataAccessComponent.DeleteData(deleteID);
        }

        /// <summary>
        /// Updates the DL32.
        /// </summary>
        /// <param name="allocationInputModel">The allocation input model.</param>
        internal void UpdateDl32(AllocationInputModel allocationInputModel)
        {
            Dlv005DataAccessComponent dataAccessComponent = new Dlv005DataAccessComponent();
            dataAccessComponent.UpdateDl32(allocationInputModel);
        }

        /// <summary>
        /// Deletes the allocation.
        /// </summary>
        /// <param name="deleteID">The delete identifier.</param>
        internal void DeleteAllocation(decimal deleteID)
        {
            Dlv005DataAccessComponent dataAccessComponent = new Dlv005DataAccessComponent();
            dataAccessComponent.DeleteAllocationData(deleteID);
        }
    }
}