using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;

namespace Revit.SH.Study.libs
{
    public static class SelectionExtensions
    {
        public static List<Element> PickElements(
            this UIDocument uiDocument,
            Func<Element, bool> validateElement,
            IPickElementsOption pickElementsOption)
        {
            return pickElementsOption.PickElements(uiDocument,  validateElement);
            
        }
    }
}

