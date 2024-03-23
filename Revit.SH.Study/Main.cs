using System;
using System.Collections.Generic;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Revit.SH.Study;

namespace LessonFile
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            // Instancia um objeto da classe Selection para manipular seleções de elementos.
            var selection = new Revit.SH.Study.Selection();

            // Obtém os IDs dos elementos selecionados no documento.
            var selectedIds = selection.GetSelectedElementIds(uidoc);

            // Obtém os elementos do tipo parede no documento.
            var wallsInDoc = selection.GetElementsTypeInDocByCategory(doc, BuiltInCategory.OST_Walls);

            // Obtém os IDs dos elementos do tipo parede no documento.
            var wallIdsInDoc = selection.GetElementsTypeIdInDocByCategory(doc, BuiltInCategory.OST_Walls);

            // Filtra os elementos por múltiplas categorias.
            var categories = new List<BuiltInCategory> { BuiltInCategory.OST_Walls, BuiltInCategory.OST_Doors };
            var elementsMulticategoryFiltered = selection.GetElementsTypeFilterByMultiCategories(doc, categories);

            // Cria e exibe um formulário simples com os elementos filtrados.
            var simpleForm = new SimpleForm(elementsMulticategoryFiltered);
            simpleForm.Show();

            // Filtra os elementos selecionados para selecionar apenas paredes.
            var selectedWallIds = selection.FilterSelectedElementsByType(uidoc, typeof(Wall), selectedIds);

            // Mostra os IDs de todos os elementos selecionados.
            selection.ShowSelectedElementIds(uidoc, selectedIds);

            // Mostra os IDs apenas das paredes selecionadas.
            selection.ShowSelectedElementIds(uidoc, selectedWallIds);

            // Lê e exibe as propriedades das paredes selecionadas.
            selection.ReadPropertiesOfSelectedElements(uidoc, selectedWallIds);

            // Retorna o resultado da execução do comando externo.
            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}

