using System;
using FinalProject.Benchmark;
using FinalProject.Rendering;
using FinalProject.Shape;
using FinalProject.Triangulator;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
class Program {

    private static ShapeBatch batch = new();
    
    public static void Main(string[] args) {

        TriangulatorBenchmark benchmark = new TriangulatorBenchmark();
        benchmark.run();
        
        //this part is broken
        GLWindow window = new GLWindow();
        window.Run();
    }
}