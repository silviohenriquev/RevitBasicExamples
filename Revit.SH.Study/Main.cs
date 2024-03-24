using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Revit.SH.Study;

namespace LessonFile
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Main : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var parameters = new Revit.SH.Study.libs.Parameters();
            parameters.ShowCommentsInSelection(uidoc, doc);


            parameters.ChangeComments(uidoc, doc, "New Value");

            parameters.GetBuiltInCategoryById(BuiltInCategory.OST_Walls);

            return Result.Succeeded;
        }
    }
}