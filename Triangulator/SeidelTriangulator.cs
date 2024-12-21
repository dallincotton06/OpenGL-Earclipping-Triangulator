using System.Drawing;
using FinalProject.Shape;

namespace FinalProject.Triangulator;

//psuedocode public class SeidelTriangulator
public class SeidelTriangulator : ITriangulator {
    private float highestY = 0;
    private float lowestY = 0;

    public List<Triangle> computeTriangles(Polygon polygon) {
        List<Triangle> triangles = new List<Triangle>();
        foreach (Polygon index in seperatePolygonHorizontally(polygon, highestY - lowestY)) {
            orderVertexesForClipping(index);
            triangles.AddRange(new EarClippingTriangulator().computeTriangles(polygon));
        }
        return triangles;
    }

    private List<Polygon> seperatePolygonHorizontally(Polygon polygon, float interval) {
        List<Polygon> polygons = new List<Polygon>();
        Polygon newPolygon = null; //update
        if (!isTrapazoid(newPolygon)) {
            seperatePolygonHorizontally(newPolygon, interval / 2);
        } else {
            polygons.Add(newPolygon);
        }
        return polygons;
    }

    private bool isTrapazoid(Polygon polygon) {
        return false;
    }

    private void orderVertexesForClipping(Polygon polygon) {
        
    }
}