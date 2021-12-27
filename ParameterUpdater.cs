using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace DMUTest
{
    internal class ParameterUpdater : IUpdater
    {
        UpdaterId _uid;
        public ParameterUpdater(AddInId addinID, Guid updaterGuid)
        {

            _uid = new UpdaterId(addinID, updaterGuid);
        }
        public void Execute(UpdaterData data)
        {
            Func<ICollection<ElementId>, string> toString = ids => ids.Aggregate("", (ss, id) => ss + "," + id).TrimStart(',');
            var sb = new StringBuilder();
            sb.AppendLine("added:" + toString(data.GetAddedElementIds()));
            sb.AppendLine("modified:" + toString(data.GetModifiedElementIds()));
            sb.AppendLine("deleted:" + toString(data.GetDeletedElementIds()));
            TaskDialog.Show("Changes", sb.ToString());
        }

        public string GetAdditionalInformation()
        {
            return "N/A";
        }

        public ChangePriority GetChangePriority()
        {
            return ChangePriority.FreeStandingComponents;
        }

        public UpdaterId GetUpdaterId()
        {
            return _uid;
        }

        public string GetUpdaterName()
        {
            return "ParameterUpdater";
        }
    }
}
