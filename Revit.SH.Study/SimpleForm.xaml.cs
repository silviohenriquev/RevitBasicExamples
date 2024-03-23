using System.Collections;
using System.Windows;

namespace Revit.SH.Study
{
    /// <summary>
    /// Interação lógica para UserControl1.xam
    /// </summary>
    public partial class SimpleForm : Window
    {
        public SimpleForm(IEnumerable collection)
        {
            InitializeComponent();
            Box.ItemsSource = collection;
        }
    }
}
