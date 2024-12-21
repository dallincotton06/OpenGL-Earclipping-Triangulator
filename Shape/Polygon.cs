namespace FinalProject.Shape;

public class Polygon : Shape {
    private List<float> vertices = new();


    public Polygon(List<float> vertices) : base() {
        this.vertices = vertices;
    }

    public List<float> getVertices() {
        return vertices;
    }

    public override List<float> batch() {
        return vertices;
    }
}