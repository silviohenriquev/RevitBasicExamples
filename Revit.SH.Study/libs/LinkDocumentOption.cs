using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Revit.SH.Study.libs
{
    /// <summary>
    /// Essa classe esta implementando a logica da classe IPickElementsOption para selecionar elementos de links
    /// </summary>
    public class LinkDocumentOption : IPickElementsOption
    {
        public List<Element> PickElements(UIDocument uiDocument, Func<Element, bool> validateElement)
        {
            var doc = uiDocument.Document;
            var references = uiDocument.Selection.PickObjects(
                ObjectType.LinkedElement,
                SelectionFilterFactory.CreateLinkableSelectionFilter(doc, validateElement));
            var elements = references
                .Select(r => (doc.GetElement(r.ElementId) as RevitLinkInstance)
                    ?.GetLinkDocument().GetElement(r.LinkedElementId))
                .ToList();
            return elements;

        }
    }
}

