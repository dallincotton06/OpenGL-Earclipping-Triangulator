using OpenTK.Graphics.OpenGL4;

namespace FinalProject.Rendering;

public class ShaderBatch : IBatch {
    
    private int shaderProgram;
    
    
    //Thanks OpenTK Examples Github
    private string vertexShaderSource = @"
            #version 330 core
            layout (location = 0) in vec2 aPosition;
            void main()
            {
                gl_Position = vec4(aPosition, 0.0, 1.0);
            }
        ";
    
    //Thanks OpenTK Examples Github
    private string fragmentShaderSource = @"
            #version 330 core
            out vec4 FragColor;
            uniform vec4 uColor;
            void main()
            {
                FragColor = uColor;
            }
        ";
    
    public void compileAll() {
        int vertexShader = compileShader(ShaderType.VertexShader, vertexShaderSource);
        int fragmentShader = compileShader(ShaderType.FragmentShader, fragmentShaderSource);
        
        shaderProgram = GL.CreateProgram();
        GL.AttachShader(shaderProgram, vertexShader);
        GL.AttachShader(shaderProgram, fragmentShader);
        GL.LinkProgram(shaderProgram);

        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);

        GL.UseProgram(shaderProgram);
    }
    
    
    private int compileShader(ShaderType type, string source) {
        int shader = GL.CreateShader(type);
        GL.ShaderSource(shader, source);
        GL.CompileShader(shader);

        GL.GetShader(shader, ShaderParameter.CompileStatus, out int success);

        return shader;
    }

    public void prepareShaderProgram() {
        GL.UseProgram(shaderProgram);
        GL.Uniform4(GL.GetUniformLocation(shaderProgram, "uColor"), 1.0f, 1.0f, 1.0f, 1.0f);
    }

    public void dispose() {
        GL.DeleteProgram(shaderProgram);
    }
}