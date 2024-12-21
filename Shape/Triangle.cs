namespace FinalProject.Shape;

public class Triangle : Polygon {
    public Triangle(float x1, float y1, float x2, float y2, float x3, float y3) : base(new []{x1, y1, x2, y2, x3, y3}.ToList()) {
    }
}