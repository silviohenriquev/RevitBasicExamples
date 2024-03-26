using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.SH.Study.libs
{
    //public enum SelectionOption
    //{
    //    Current,
    //    Link,
    //    Both
    //}

    public static class SelectionExtensions
    {
        public static List<Element> PickElements(
            this UIDocument uiDocument,
            Func<Element, bool> validateElement,
            IPickElementsOption pickElementsOption)
        {
            return pickElementsOption.PickElements(uiDocument,  validateElement);
            //if (selectionOption == SelectionOption.Current)
            //{
            //    uIDocument.Selection.PickObjects()
            //}
            
        }
    }

    public interface IPickElementsOption
    {
        List<Element> PickElements(
            UIDocument uiDocument, Func<Element, bool> validateElement);
    }

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

