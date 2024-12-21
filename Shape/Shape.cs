namespace FinalProject.Shape;

public abstract class Shape : Drawable {
    private float originX;
    private float originY;

    public Shape(float originX, float originY) {
        this.originX = originX;
        this.originY = originY;
    }

    public Shape() {
        this.originX = 0;
        this.originY = 0;
    }

    public abstract List<float> batch();

    public virtual void getBounds() {
        
    }

    public float getOriginX() {
        return originX;
    }

    public float getOriginY() {
        return originY;
    }

    public void setOrigin(float originX, float originY) {
        this.originX = originX;
        this.originY = originY;
    }

    public void setOriginX(float originX) {
        this.originX = originX;
    }

    public void setOriginY(float originY) {
        this.originY = originY;
    }
    
}