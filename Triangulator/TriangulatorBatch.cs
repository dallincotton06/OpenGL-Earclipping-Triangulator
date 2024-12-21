using System.Drawing;
using FinalProject.Rendering;
using FinalProject.Shape;

namespace FinalProject.Triangulator;

public class TriangulatorBatch : IBatch {
    
    private List<Polygon> polygons;
    private Dictionary<Polygon, ITriangulator> methodMap;

    public TriangulatorBatch(List<Polygon> polygons) {
        this.polygons = polygons;
    }

    public TriangulatorBatch() {
        this.polygons = new List<Polygon>();
        this.methodMap = new Dictionary<Polygon, ITriangulator>();
    }

    private int SEIDELS_THRESHOLD = 60; // The amount of verticies that makes Seidels algorithm faster than Ear clipping.
    
    public void compileAll() {
        foreach (var polygon in this.polygons) {
            compile(polygon);
        }
    }

    private void compile(Polygon polygon) {
        if (polygon.getVertices().Count / 2 >= SEIDELS_THRESHOLD || IntersectionChecker.HasIntersectingLines(polygon.getVertices().ToArray())) {
            methodMap.Add(polygon, new SeidelTriangulator());
        } else {
            methodMap.Add(polygon, new EarClippingTriangulator());
        }
    }

    public void dispose() {
        clear();
    }

    public void computeTrianglesForAll() {
        foreach (var entry in methodMap) {
            Console.WriteLine("\n[Running Triangulator Type]: " + entry.Value);
            entry.Value.computeTriangles(entry.Key);
        }
    }

    public void add(Polygon polygon) {
        polygons.Add(polygon);
    }

    public void addAll(List<Polygon> polygons) {
        polygons.AddRange(polygons);
    }

    public void remove(Polygon polygon) {
        polygons.Remove(polygon);
    }

    public void clear() {
        polygons.Clear();
    }
}