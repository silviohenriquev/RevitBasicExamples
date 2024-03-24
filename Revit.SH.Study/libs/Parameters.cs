using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows;

namespace Revit.SH.Study.libs
{
    internal class Parameters
    {
        public Element element;
        public void ShowCommentsInSelection(UIDocument uidoc, Document doc)
        {
            element = uidoc.Selection.GetElementIds()
                .Select(x => doc.GetElement(x)).First();

            var value = element.
                get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).AsString();

            TaskDialog.Show("Message", value);
        }
        
        public void ChangeComments(UIDocument uidoc, Document doc, String comment)
        {
            using (var transaction = new Transaction(doc, "Set Values"))
            {
                transaction.Start();

                element.get_Parameter(
                    BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS)
                    .Set(comment);

                transaction.Commit();
            }
        }

        public void GetBuiltInCategoryById(BuiltInCategory category)
        {
            var builtInCategoryId = new ElementId(category);
            var builtInCategory = Enum.GetValues(typeof(BuiltInCategory))
                .OfType<BuiltInCategory>()
                .Where(x => (int)x == builtInCategoryId.IntegerValue);

            var simpleForm = new SimpleForm(builtInCategory);
            simpleForm.ShowDialog();
        }
    }
}
