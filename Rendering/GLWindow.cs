using FinalProject.Rendering;
using FinalProject.Shape;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

class GLWindow : GameWindow {
    
    ShaderBatch shaderBatch = new ShaderBatch();
    ShapeBatch shapeBatch = new ShapeBatch();

    public GLWindow() : base(GameWindowSettings.Default, NativeWindowSettings.Default) { }

    protected override void OnLoad() {
        base.OnLoad();
        GL.ClearColor(Color4.CornflowerBlue);
        shapeBatch.add(new Polygon(new float[] {
           0.0f,  0.5f,    // Top
           0.475f,  0.154f, // Top-right
           0.293f, -0.404f, // Bottom-right
           -0.293f, -0.404f, // Bottom-left
           -0.475f,  0.154f  // Top-left
        }.ToList()));
        
        shapeBatch.add(new Polygon(new float[] {
            0.75f, -0.3f,       // Vertex 1 (right)
            0.425f, -0.083f,    // Vertex 2 (top-right)
            0.175f, -0.083f,    // Vertex 3 (top-left)
            0.05f, -0.3f,       // Vertex 4 (left)
            0.175f, -0.517f,    // Vertex 5 (bottom-left)
            0.425f, -0.517f   // Vertex 6 (bottom-right)
        }.ToList()));
        shaderBatch.compileAll();
    }

    protected override void OnRenderFrame(FrameEventArgs e) {
        base.OnRenderFrame(e);
        GL.Clear(ClearBufferMask.ColorBufferBit);
        shaderBatch.prepareShaderProgram();
        shapeBatch.drawAll();
        SwapBuffers();
    }

    protected override void OnUnload() {
        shaderBatch.dispose();
        shapeBatch.dispose();
        base.OnUnload();
    }
}
