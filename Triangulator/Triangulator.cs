using FinalProject.Shape;

namespace FinalProject.Triangulator;

public interface ITriangulator { 
    List<Triangle> computeTriangles(Polygon polygon);
}