using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;

namespace DMUTest
{
    [Transaction(TransactionMode.Manual)]
    public class DMUTest : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication app = commandData.Application;
            AddInId addInId = app.ActiveAddInId;
            Guid updaterGuid = new Guid("BC0CC3B1-03B8-4072-B167-4B69C1FBEAED");
            ParameterUpdater parameterUpdater = new ParameterUpdater(addInId, updaterGuid);
            // 注册updater
            UIDocument activeUIDoc = commandData.Application.ActiveUIDocument;
            Document activeDoc = activeUIDoc.Document;
            UpdaterRegistry.RegisterUpdater(parameterUpdater, true);
            Element elementEle = activeDoc.GetElement(new ElementId(529759));
            var parameter = elementEle.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED);
            UpdaterRegistry.AddTrigger(parameterUpdater.GetUpdaterId(), activeDoc, new List<ElementId> { new ElementId(529759) }, Element.GetChangeTypeAny());
            TaskDialog.Show("note", "123");
            using (Transaction trans = new Transaction(activeDoc, "aaa"))
            {   
                trans.Start();
                activeDoc.Delete(new ElementId(529759));
                trans.Commit();
            }
            return Result.Succeeded;
        }
    }
}
