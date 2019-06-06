using Dlv005_DL;

namespace Dlv005_BL
{
    partial class Dlv005SelectionTableDataSet
    {
        public void Initialize(Dlv005SelectionTableDataSet dataModel, string tableUsed)
        {
            Dlv005SelectionTableDataAccessComponent selectionTableDataAccessComponent = new Dlv005SelectionTableDataAccessComponent();

            switch (tableUsed)
            {
                case "BD12":
                    selectionTableDataAccessComponent.GetBD12Data(dataModel.BD12Table);
                    break;

                case "BD09":
                    selectionTableDataAccessComponent.GetBD09Data(dataModel.BD09Table);
                    break;

                case "BD06":
                    selectionTableDataAccessComponent.GetBD06Data(dataModel.BD06Table);
                    break;

                default:
                    break;
            }
        }
    }
}