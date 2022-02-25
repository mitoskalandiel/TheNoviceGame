namespace TNG;

using TNG.Engine;

public class Program {

    public static void Main(string[] args) {
        // call our library to open a window and start rendering
        AppWindow App = AppWindow.Instance;
        AppWindow.VertexShaderPath = @"assets/shaders/defaultShader.vert";
        AppWindow.FragmentShaderPath = @"assets/shaders/defaultShader.frag";
        AppWindow.TexturePath = @"assets/images/testImage.png";
        App.Run();
    }
}