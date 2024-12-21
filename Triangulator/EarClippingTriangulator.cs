using System.Drawing;
using System.Runtime.InteropServices.JavaScript;
using FinalProject.Shape;

namespace FinalProject.Triangulator;

public class EarClippingTriangulator : ITriangulator {

    private List<Triangle> extractedTriangles = new List<Triangle>();

    public List<Triangle> computeTriangles(Polygon polygon) {
        if (isEarRemaining(polygon)) {
            extractedTriangles.Add(new Triangle(polygon.getVertices()[0], polygon.getVertices()[1],
                                       polygon.getVertices()[2], polygon.getVertices()[3],
                                       polygon.getVertices()[4], polygon.getVertices()[5]));
            
            foreach (Triangle triangle in extractedTriangles) { 
                Console.WriteLine("[Triangles at Close]: " + "(" + triangle.getVertices()[0] + "," + triangle.getVertices()[1] + ") " +
                                                                    "(" + triangle.getVertices()[2] + "," + triangle.getVertices()[3] + ") " +
                                                                    "(" + triangle.getVertices()[4] + "," + triangle.getVertices()[5] + ")");
            }
            return extractedTriangles;
        } else {
            int earIndex = seekToEar(polygon);
            Triangle triangleToRemove = getAdjacentVertecies(polygon, earIndex);
            extractedTriangles.Add(triangleToRemove);
            clipPolygon(polygon, earIndex);
            Console.WriteLine("[Triangle Extracted]: [{0}]", string.Join(", ",triangleToRemove.getVertices()));
            computeTriangles(polygon);
        }
        
        
        return extractedTriangles;
    }

    
    private bool isEarRemaining(Polygon polygon) {
        if (polygon.getVertices().Count == 6) {
            return true;
        }
        return false;
    }

    private int seekToEar(Polygon polygon) {
        for (int i = 0; i < polygon.getVertices().Count; i += 2) {
            Triangle currentVertexAngle = getAdjacentVertecies(polygon, i);
            if (isEar(currentVertexAngle)) {
                return i;
            }
        }
        
        return -1;
    }

    private bool isEar(Triangle triangle) {
        return isPositive(crossProduct(triangle));
    }

    
    private Triangle getAdjacentVertecies(Polygon polygon, int index) {
        int FIRST_INDEX = 0;
        int LAST_INDEX = polygon.getVertices().Count - 2;

        float[] vertices = polygon.getVertices().ToArray();
        if (index == FIRST_INDEX) { 
            return new Triangle(vertices[index], vertices[index + 1],
                                vertices[index + 2], vertices[index + 3],
                                vertices[polygon.getVertices().Count - 2], vertices[polygon.getVertices().Count - 1]);
        } else if (index == LAST_INDEX) {

            return new Triangle(vertices[index], vertices[index + 1],
                                vertices[0], vertices[1],
                                vertices[index - 2], vertices[index - 1]);
        } else {
            return new Triangle(vertices[index], vertices[index + 1],
                                vertices[index + 2], vertices[index + 3],
                                vertices[index - 2], vertices[index - 1]);
        }
    }

    private Polygon clipPolygon(Polygon polygon, int index) {
        Polygon newPolygon = polygon;
        Console.WriteLine("[Clipping Polygon]: [{0}]"  + " - Removing: (" + polygon.getVertices()[index] + "," + polygon.getVertices()[index + 1] + ")", string.Join(", ", polygon.getVertices()));
        newPolygon.getVertices().RemoveRange(index, 2);
        return newPolygon;
    }
    private float crossProduct(Triangle triangle) {
        float x1 = triangle.getVertices()[0]; // xi  (A)
        float y1 = triangle.getVertices()[1]; // yi  (A)
        float x2 = triangle.getVertices()[2]; // ix + 1  (B)
        float y2 = triangle.getVertices()[3]; // iy + 1  (B)
        float x3 = triangle.getVertices()[4]; // ix - 1  (C)
        float y3 = triangle.getVertices()[5]; // iy - 1  (C)

        float product = (x2 - x1) * (y3 - y1) - (y2 - y1) * (x3 - x1);
        return product;
    }

    public bool isVertexInClip(Triangle triangle, float[] vertex) {
        Triangle ABP = new Triangle(triangle.getVertices()[0], triangle.getVertices()[1],
                                    triangle.getVertices()[2], triangle.getVertices()[3],
                                    vertex[0], vertex[1]);
        
        Triangle BCP = new Triangle(triangle.getVertices()[2], triangle.getVertices()[3],
                                    triangle.getVertices()[4], triangle.getVertices()[5],
                                    vertex[0], vertex[1]);
        
        Triangle CAP = new Triangle(triangle.getVertices()[4], triangle.getVertices()[5],
                                    triangle.getVertices()[0], triangle.getVertices()[1],
                                    vertex[0], vertex[1]);
        
        if (isPositive(crossProduct(ABP)) && isPositive(crossProduct(CAP)) && isPositive(crossProduct(BCP))) {
            return true;
        }
        
        if (!isPositive(crossProduct(ABP)) && !isPositive(crossProduct(CAP)) && !isPositive(crossProduct(BCP))) {
            return true;
        }
        return false;
    }

    private bool isPositive(float number) {
        return number > 0;
    }
}