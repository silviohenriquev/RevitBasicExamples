using Autodesk.Revit.DB;
using System;

namespace Revit.SH.Study.libs
{
    /// <summary>
    /// Esta classe e usada para implementar a logica para selecionar elementos
    /// </summary>
    public class LinkableSelectionFiler : BaseSelectionFilter
    {
        private readonly Document _doc;

        public LinkableSelectionFiler(
            Document doc, 
            Func<Element, bool> validateElement)
            : base(validateElement)
        {
            _doc = doc;
        }
        public override bool AllowElement(Element elem) => true;

        public override bool AllowReference(
            Reference reference,
            XYZ position)
        {
            if (!(_doc.GetElement(reference.ElementId) is RevitLinkInstance linkInstance))
                return ValidateElement(_doc.GetElement(reference.ElementId));
        
            var element = linkInstance.GetLinkDocument()
                        .GetElement(reference.LinkedElementId);
            return ValidateElement(element);
        }

    }
}

