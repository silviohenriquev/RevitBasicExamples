using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;

namespace Revit.SH.Study.libs
{
    /// <summary>
    /// Esta classe é usada para implementar a logica para selecionar elementos de links e do documento atual
    /// </summary>
    public class BothDocumentOption : IPickElementsOption
    {
        public List<Element> PickElements(UIDocument uiDocument, Func<Element, bool> validateElement)
        {
            var doc = uiDocument.Document;
            var references = uiDocument.Selection.PickObjects(
                ObjectType.PointOnElement,
                SelectionFilterFactory.CreateLinkableSelectionFilter(doc, validateElement));
            var elements = new List<Element>(); 
            foreach (var reference in references)
            {
                if(doc.GetElement(reference.ElementId) is RevitLinkInstance linkInstance)
                {
                    var element = linkInstance.GetLinkDocument().GetElement(reference.LinkedElementId);
                    elements.Add(element);
                }
                else
                {
                    elements.Add(doc.GetElement(reference.ElementId));
                }
            }
            return elements;
        }
    }
}

