using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Revit.SH.Study;
using Revit.SH.Study.libs;

namespace LessonFile
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Main : IExternalCommand
    {
        public bool IsWall(Element element)
        {
            return element is Wall;
        }
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            var selectedElements = uidoc.PickElements(
                e => e is Wall, new BothDocumentOption());

            TaskDialog.Show("Message", selectedElements.Count.ToString());

            //var references = uidoc.Selection.PickObjects(
            //    ObjectType.LinkedElement, new ElementSelectionFilter(
            //        e=>e is Wall,
            //        r => r.ElementReferenceType == ElementReferenceType.REFERENCE_TYPE_LINEAR));
            
            //var references = uidoc.Selection.PickObjects(
            //    ObjectType.LinkedElement, new LinkableSelectionFiler(
            //        doc,
            //        e=>e is Wall));

            return Result.Succeeded;
        }
    }
}