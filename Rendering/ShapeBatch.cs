namespace FinalProject.Rendering;

using OpenTK.Graphics.OpenGL4;
using FinalProject.Shape;

public class ShapeBatch : IBatch {
    
    List<Shape> drawabels = new();

    public void compileAll() {
        
    }
    
    private void draw(Shape shape) {
        int vertexBufferObject;
        int vertexArrayObject;
        float[] vertices = shape.batch().ToArray();
        
        vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(vertexArrayObject);

        vertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
        
        GL.BindVertexArray(vertexArrayObject);
        GL.DrawArrays(PrimitiveType.LineLoop, 0, vertices.Length);
        GL.BindVertexArray(0);
        GL.DeleteBuffer(vertexBufferObject);
        GL.DeleteVertexArray(vertexArrayObject);
    }

    public void drawAll() {
        foreach (Shape shape in drawabels) draw(shape);
    }

    public void add(Shape shape) {
        drawabels.Add(shape);
    }

    public void remove(Shape shape) {
        drawabels.Remove(shape);
    }

    public void clear() {
        drawabels.Clear();
    }

    public void dispose() {
        drawabels.Clear();
    }
}