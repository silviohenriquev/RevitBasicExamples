using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;

namespace Revit.SH.Study.libs
{
    public static class XYZExtensions
    {
        /// <summary>
        /// This method is used to visualize XYZ in a document
        /// </summary>
        /// <param name="point"></param>
        /// <param name="doc"></param>
        public static void Visualize(
            this XYZ point, Document doc)
        {
            doc.CreateDirectShape(new List<GeometryObject>() { Point.Create(point) });
        }

        /// <summary>
        /// This method is used to get a curve from vector
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="origin"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static Curve AsCurve(
            this XYZ vector, XYZ origin=null, double?length = null)
        {
            if (origin == null)
                origin = XYZ.Zero;
            if (length == null)
                length = vector.GetLength();
            return Line.CreateBound(
                origin,
                origin.MoveAlongVector(vector.Normalize(), length.GetValueOrDefault()));
        }

        /// <summary>
        /// This method is udes to move point along a given vector and distante
        /// </summary>
        /// <param name="pointToMove"></param>
        /// <param name="vector"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public static XYZ MoveAlongVector(
            this XYZ pointToMove, XYZ vector, double distance) => pointToMove.Add(vector*distance);
        
        /// <summary>
        /// This method is used to get normalized vector by curve
        /// </summary>
        /// <param name="pointToMove"></param>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static XYZ MoveAlongVector(
            this XYZ pointToMove, XYZ vector) => pointToMove.Add(vector);
        
        public static XYZ ToNormalizedVector(
            this Curve curve)
        {
            return (curve.GetEndPoint(1)-curve.GetEndPoint(0)).Normalize();
        }
        
        public static void VisualizeAsPoint(
            this XYZ point, Document document)
        {
            document.CreateDirectShape(new List<GeometryObject>() { Point.Create(point) });
        }

        public static Line VisualizeAsLine(
            this XYZ vector, Document document, XYZ origin = null)
        {
            if (origin == null) origin = XYZ.Zero;

            var endPoint = origin + vector;

            var line = Line.CreateBound(origin, endPoint);
            document.CreateDirectShape(new List<GeometryObject>() { line });
            return line;
        }

        public static XYZ ToVector(
            this XYZ fromPoint, XYZ toPoint) => toPoint-fromPoint;

        public static XYZ ToNormalizedVector(
            this XYZ fromPoint, XYZ toPoint) => (fromPoint-toPoint).Normalize();

        public static double DistanceToAlongVector(
            this XYZ fromPoint, XYZ toPoint, XYZ vectorToMeasureBy)
        {
            return Math.Abs(
                fromPoint.ToVector(toPoint).DotProduct(vectorToMeasureBy));

        }
    }
}
