using System;
using System.Collections.Generic;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace LessonFile
{
    /// <summary>
    /// Classe principal que implementa a interface IExternalCommand para executar uma ação no Autodesk Revit.
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        /// <summary>
        /// Método principal que executa a lógica do comando externo.
        /// </summary>
        /// <param name="commandData">Os dados do comando externo.</param>
        /// <param name="message">Uma mensagem que pode ser definida para relatar ao usuário.</param>
        /// <param name="elements">Conjunto de elementos.</param>
        /// <returns>O resultado da execução do comando externo.</returns>
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData,
            ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;

            // Instancia um objeto da classe Selection para manipular seleções de elementos.
            var selection = new Revit.SH.Study.Selection();

            // Obtém os IDs dos elementos selecionados no documento.
            ICollection<ElementId> selectedIds = selection.GetIDOfSelectedElements(uidoc);

            // Filtra os elementos selecionados para selecionar apenas paredes.
            ICollection<ElementId> selectedWallIds = selection.FilterSelectedElementsByType(uidoc, typeof(Wall), selectedIds);

            // Mostra os IDs de todos os elementos selecionados.
            selection.ShowIDOfSelectedElements(uidoc, selectedIds);

            // Mostra os IDs apenas das paredes selecionadas.
            selection.ShowIDOfSelectedElements(uidoc, selectedWallIds);

            // Lê e exibe as propriedades das paredes selecionadas.
            selection.ReadPropertiesOfSelectedElements(uidoc, selectedWallIds);

            // Retorna o resultado da execução do comando externo.
            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}
