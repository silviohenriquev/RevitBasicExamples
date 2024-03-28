using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System;

namespace Revit.SH.Study.libs
{
    public abstract class BaseSelectionFilter : ISelectionFilter
    {
        protected readonly Func<Element, bool> ValidateElement;

        public BaseSelectionFilter(Func<Element, bool> validateElement)
        {
            ValidateElement = validateElement;
        }
        public abstract bool AllowElement(Element elem);
        public abstract bool AllowReference(Reference reference, XYZ position);
    }
}

