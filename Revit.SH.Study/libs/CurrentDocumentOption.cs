using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Revit.SH.Study.libs
{
    /// <summary>
    /// Esta classe é usada para implementar a logica para selecionar elementos do documento atual
    /// </summary>
    public class CurrentDocumentOption : IPickElementsOption
    {
        public List<Element> PickElements(UIDocument uiDocument, Func<Element, bool> validateElement)
        {
            return uiDocument.Selection.PickObjects(
                ObjectType.Element,
                SelectionFilterFactory.CreateElementSelectionFilter(validateElement))
                .Select(r => uiDocument.Document.GetElement(r.ElementId))
                .ToList();
        }
    }
}

