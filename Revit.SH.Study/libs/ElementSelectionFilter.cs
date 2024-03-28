using Autodesk.Revit.DB;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Revit.SH.Study.libs
{
    public class ElementSelectionFilter : BaseSelectionFilter
    {
        //private readonly Func<Element, bool> _validateElement;
        //private readonly Func<Reference, bool> _validateReference;
        //public ElementSelectionFilter(Func<Element, bool> validateElement)
        //{
        //    _validateElement = validateElement;
        //}

        //public ElementSelectionFilter(
        //    Func<Element, bool> validateElement, Func<Reference, bool> validateReference)
        //    :this(validateElement)
        //{
        //    _validateReference = validateReference;
        //}

        //public bool AllowElement(Element elem)
        //{
        //    return _validateElement(elem);
        //}

        //public bool AllowReference(Reference reference, XYZ position)
        //{
        //    // Valida todo tipo de referencia
        //    //return true;

        //    //Valida apenas as linhas
        //    //return reference.ElementReferenceType == ElementReferenceType.REFERENCE_TYPE_LINEAR;

        //    // Valida apenas as superficies
        //    //return reference.ElementReferenceType == ElementReferenceType.REFERENCE_TYPE_SURFACE;
        //    return _validateReference?.Invoke(reference) ?? true;

        private readonly Func<Reference, bool> _validateReference;
        public ElementSelectionFilter(Func<Element, bool> validateElement)
            :base(validateElement)
        {
        }

        public ElementSelectionFilter(
            Func<Element, bool> validateElement,
            Func<Reference, bool> validateReference)
            : base(validateElement)
        {
            _validateReference = validateReference;
        }

        public override bool AllowElement(Element elem)
        {
            return ValidateElement(elem);
        }

        public override bool AllowReference(Reference reference, XYZ position)
        {
            return _validateReference?.Invoke(reference) ?? true;
        }
    }
}

