using Autodesk.Revit.DB;
using System;

namespace Revit.SH.Study.libs
{
    /// <summary>
    /// Esta classe e usada para cirar instancias de SeectionFilter
    /// </summary>
    public static class SelectionFilterFactory
    {
        public static ElementSelectionFilter CreateElementSelectionFilter(Func<Element, bool> validateElement)
        {
            return new ElementSelectionFilter(validateElement); 
        }
        public static LinkableSelectionFiler CreateLinkableSelectionFilter(
            Document doc, 
            Func<Element, bool> validateElement)
        {
            return new LinkableSelectionFiler(doc, validateElement);
        }
    }
}

