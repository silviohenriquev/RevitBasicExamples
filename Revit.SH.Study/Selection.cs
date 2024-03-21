using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;

namespace Revit.SH.Study
{
    /// <summary>
    /// Classe que fornece métodos para lidar com seleções de elementos no Autodesk Revit.
    /// </summary>
    public class Selection
    {
        /// <summary>
        /// Obtém os IDs dos elementos selecionados no documento atual.
        /// </summary>
        /// <param name="uidoc">O UIDocument atual.</param>
        /// <returns>Uma coleção de ElementIds representando os elementos selecionados.</returns>
        public ICollection<ElementId> GetIDOfSelectedElements(UIDocument uidoc)
        {
            // Seleciona alguns elementos no Revit antes de invocar este comando

            // Obtém a seleção de elementos do documento atual.
            Autodesk.Revit.UI.Selection.Selection selection = uidoc.Selection;
            ICollection<ElementId> selectedIds = uidoc.Selection.GetElementIds();

            return selectedIds;
        }

        /// <summary>
        /// Mostra os IDs e tipos dos elementos selecionados.
        /// </summary>
        /// <param name="uidoc">O UIDocument atual.</param>
        /// <param name="selectedIds">Uma coleção de ElementIds representando os elementos selecionados.</param>
        public void ShowIDOfSelectedElements(UIDocument uidoc, ICollection<ElementId> selectedIds)
        {
            if (0 == selectedIds.Count)
            {
                // Se nenhum elemento foi selecionado.
                TaskDialog.Show("Revit", "Você não selecionou nenhum elemento.");
            }
            else
            {
                String info = "Número de elementos selecionados: " + selectedIds.Count.ToString() + "\n\t\n\tIDs dos elementos selecionados no documento são: ";
                foreach (ElementId id in selectedIds)
                {
                    info += "\n\tID: " + id.IntegerValue;
                    info += " | Tipo: " + uidoc.Document.GetElement(id).GetType();
                }

                TaskDialog.Show("Revit", info);
            }
        }

        /// <summary>
        /// Filtra os elementos selecionados pelo tipo especificado.
        /// </summary>
        /// <param name="uidoc">O UIDocument atual.</param>
        /// <param name="type">O tipo de elemento para filtrar.</param>
        /// <param name="selectedIds">Uma coleção de ElementIds representando os elementos selecionados.</param>
        /// <returns>Uma coleção de ElementIds representando os elementos do tipo especificado.</returns>
        public ICollection<ElementId> FilterSelectedElementsByType(UIDocument uidoc, Type type, ICollection<ElementId> selectedIds)
        {
            ICollection<ElementId> selectedWallIds = new List<ElementId>();

            foreach (ElementId id in selectedIds)
            {
                Element element = uidoc.Document.GetElement(id);
                if (element.GetType() == type)
                {
                    selectedWallIds.Add(id);
                }
            }

            return selectedWallIds;
        }

        /// <summary>
        /// Lê as propriedades dos elementos selecionados e exibe-as em um diálogo de tarefa.
        /// </summary>
        /// <param name="uidoc">O UIDocument atual.</param>
        /// <param name="selectedIds">Uma coleção de ElementIds representando os elementos selecionados.</param>
        public void ReadPropertiesOfSelectedElements(UIDocument uidoc, ICollection<ElementId> selecteIds)
        {
            foreach (ElementId id in selecteIds)
            {
                Element element = uidoc.Document.GetElement(id);
                string info = "Informações sobre o Elemento " + id;
                info += "\n\tElementId: " + element.Id;
                info += "\n\tCategoria: " + element.Category.Name;
                info += "\n\tNível: " + uidoc.Document.GetElement(element.LevelId).Name;

                TaskDialog.Show("Revit", info);
            }
        }
    }
}
