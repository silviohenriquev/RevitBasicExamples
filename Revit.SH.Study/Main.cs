using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
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
            UIApplication uiApplication = commandData.Application;
            UIDocument uiDocument = uiApplication.ActiveUIDocument;
            Document document = uiDocument.Document;

            //var references = uidoc.Selection.PickObjects(
            //    ObjectType.LinkedElement, new ElementSelectionFilter(
            //        e=>e is Wall,
            //        r => r.ElementReferenceType == ElementReferenceType.REFERENCE_TYPE_LINEAR));

            //var references = uidoc.Selection.PickObjects(
            //    ObjectType.LinkedElement, new LinkableSelectionFiler(
            //        doc,
            //        e=>e is Wall));

            //var selectedElements = uidoc.PickElements(
            //    e => e is FamilyInstance,
            //    PickElementsOptionFactory.CreateBothDocumentOption());

            //TaskDialog.Show("Message", selectedElements.Count.ToString());

            //var selectedElement = uidoc.PickElements(
            //    e => e is FamilyInstance,
            //    PickElementsOptionFactory.CreateCurrentDocumentOption()).First();
            //var familyLocation = (selectedElement.Location as LocationPoint).Point;
            //var vector = new XYZ(1,1.5,0.15) * 3.28084;

            //using (var transaction = new Transaction(doc, "Create Points"))
            //{
            //    transaction.Start();
            //    var movedPoint = familyLocation + vector;
            //    //ElementTransformUtils.MoveElement(
            //    //    doc, selectedElement.Id, vector);
            //    movedPoint.VisualizeAsPoint(doc);
            //    familyLocation.VisualizeAsPoint(doc);
            //    vector.VisualizeAsLine(doc);
            //    //doc.CreateDirectShape(new List<GeometryObject>() { Point.Create(vector) });

            //    transaction.Commit();
            //}

            //var firstVector = new XYZ(2 , 0.5, 0.1).Normalize();
            //var firstVector = XYZ.BasisY;
            //var basisZ = XYZ.BasisZ;
            //var perpendicularVector = firstVector.CrossProduct(basisZ);
            //var secondVector = new XYZ(-2, 0.5,0).Normalize().Negate();

            var fromPoint = new XYZ(1,3,0);
            var toPoint = new XYZ(3,0.5,0);
            var vector = XYZ.BasisY;
            var distance = fromPoint.DistanceToAlongVector(toPoint, vector);
            //var vectorBtwPoints = (toPoint - fromPoint);
            //var vectorToMeasureBy = XYZ.BasisY;
            //var distanceAlongX = vectorBtwPoints.DotProduct(vectorToMeasureBy); 



            using (var transaction = new Transaction(document, "Create points"))
            {
                transaction.Start();
                //firstVector.AsCurve().Visualize(document);
                //secondVector.AsCurve().Visualize(document);
                //var dotProduct = firstVector.DotProduct(secondVector);
                //TaskDialog.Show("Message", dotProduct.ToString());

                fromPoint.Visualize(document);
                //vectorBtwPoints.AsCurve().Visualize(document);
                toPoint.Visualize(document);
                vector.AsCurve().Visualize(document);
                TaskDialog.Show("Message", distance.ToString());
                

                transaction.Commit();
            }



            return Result.Succeeded;
        }
    }
}