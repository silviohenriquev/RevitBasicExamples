using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.SH.Study.libs
{
    public static class CurveExtensions
    {
        /// <summary>
        /// This method is used to visualize curves in a revit document
        /// </summary>
        /// <param name="curve"></param>
        /// <param name="document"></param>
        public static void Visualize(
            this Curve curve, Autodesk.Revit.DB.Document document)
        {
            document.CreateDirectShape(new List<GeometryObject>() { curve });
        }
    }
}
