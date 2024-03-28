using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;

namespace Revit.SH.Study.libs
{
    /// <summary>
    /// Interface para implementar a logica de PickElements
    /// </summary>
    public interface IPickElementsOption
    {
        List<Element> PickElements(
            UIDocument uiDocument, Func<Element, bool> validateElement);
    }
}

