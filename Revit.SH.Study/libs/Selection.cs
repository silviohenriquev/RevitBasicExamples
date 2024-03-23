using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Revit.SH.Study
{
    public class Selection
    {
        public ICollection<ElementId> GetSelectedElementIds(UIDocument uidoc)
        {
            return uidoc.Selection.GetElementIds();
        }

        public IEnumerable<Element> GetElementsTypeInDocByCategory(Document doc, BuiltInCategory category)
        {
            return new FilteredElementCollector(doc)
                .OfCategory(category)
                .WhereElementIsElementType();
        }

        public ICollection<ElementId> GetElementsTypeIdInDocByCategory(Document doc, BuiltInCategory category)
        {
            return new FilteredElementCollector(doc)
                .OfCategory(category)
                .WhereElementIsElementType()
                .ToElementIds();
        }

        private ElementQuickFilter GetElementQuickFilter(ICollection<BuiltInCategory> categories)
        {
            return new ElementMulticategoryFilter(categories);
        }

        public IEnumerable<Element> GetElementsTypeFilterByMultiCategories(Document doc, ICollection<BuiltInCategory> categories)
        {
            return new FilteredElementCollector(doc)
                .WherePasses(GetElementQuickFilter(categories));
        }

        public void ShowSelectedElementIds(UIDocument uidoc, ICollection<ElementId> selectedIds)
        {
            if (!selectedIds.Any())
            {
                TaskDialog.Show("Revit", "Nenhum elemento selecionado.");
            }
            else
            {
                string info = $"Número de elementos selecionados: {selectedIds.Count}\n\nIDs dos elementos selecionados no documento são: ";
                foreach (ElementId id in selectedIds)
                {
                    Element element = uidoc.Document.GetElement(id);
                    info += $"\nID: {id.IntegerValue} | Tipo: {element.GetType()}";
                }
                TaskDialog.Show("Revit", info);
            }
        }

        public ICollection<ElementId> FilterSelectedElementsByType(UIDocument uidoc, Type type, ICollection<ElementId> selectedIds)
        {
            var selectedElementIds = new List<ElementId>();

            foreach (ElementId id in selectedIds)
            {
                Element element = uidoc.Document.GetElement(id);
                if (element.GetType() == type)
                {
                    selectedElementIds.Add(id);
                }
            }

            return selectedElementIds;
        }
        
        public void ReadPropertiesOfSelectedElements(UIDocument uidoc, ICollection<ElementId> selectedIds)
        {
            foreach (ElementId id in selectedIds)
            {
                Element element = uidoc.Document.GetElement(id);
                string info = $"Informações sobre o Elemento {id}\n" +
                              $"\tElementId: {element.Id}\n" +
                              $"\tCategoria: {element.Category.Name}\n" +
                              $"\tNível: {uidoc.Document.GetElement(element.LevelId).Name}";
                TaskDialog.Show("Revit", info);
            }
        }
    }
}