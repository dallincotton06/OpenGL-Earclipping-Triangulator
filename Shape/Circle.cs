namespace FinalProject.Shape;

public class Circle {
    private float radius = new();
    

    public Circle(float radius) : base() {
        this.radius = radius;
    }

    public float getRadius() {
        return this.radius;
    }
}