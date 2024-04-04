using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit.SH.Study.libs
{
    /// <summary>
    /// Esse metodo e usado para criar um DirectShape no document
    /// </summary>
    public static class DocumentExtensions
    {
        public static DirectShape CreateDirectShape(
            this Document doc,
            List<GeometryObject> geometryObjects,
            BuiltInCategory builtInCategory = BuiltInCategory.OST_GenericModel)
        {
            var directShape = DirectShape.CreateElement(
                doc,
                new ElementId(builtInCategory));
            directShape.SetShape(
                geometryObjects); 
            return directShape;
        }
    }
}
