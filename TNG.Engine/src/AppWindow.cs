using System.Numerics;
using System.Drawing;
using Silk.NET.GLFW;
using Silk.NET.Core;
using Silk.NET.Windowing;
using Silk.NET.Maths;
using Silk.NET.Input;
using Silk.NET.OpenGL;

namespace TNG.Engine;

public sealed class AppWindow {
    private Vector2D<int> _size = new(1280, 720);
    private string _title;
    private long glfwWindowInstance;

    private Vector4 CanvasColor;

    private static AppWindow? Win = null;
    private static readonly Lazy<AppWindow> LazySingleton = new Lazy<AppWindow>(() => new AppWindow());
    public static AppWindow Instance => LazySingleton.Value;

    public static string VertexShaderPath { get; set; } = string.Empty;
    public static string FragmentShaderPath { get; set; } = string.Empty;

    private static IWindow? RenderWindow;
    private static GL glContext;

    private static uint ShaderProgramID;

    private static BufferObject<float> Vbo;
    private static BufferObject<uint> Ebo;
    private static VertexArrayObject<float, uint> Vao;
    private static Shader shader;

    internal static int _iterator = 0;

    //This is the vertex data uploaded to the vbo
    private static readonly float[] Vertices = {
        //X    Y      Z     R  G  B  A
        0.5f,  0.5f, 0.0f, 1.0f, 0.0f, 0.0f, 1.0f,
        0.5f, -0.5f, 0.0f, 0.0f, 1.0f, 0.0f, 1.0f,
       -0.5f, -0.5f, 0.0f, 0.0f, 0.0f, 1.0f, 1.0f,
       -0.5f,  0.5f, 0.0f, 0.3f, 0.3f, 0.3f, 1.0f
    };

    //Index data, uploaded to the ebo
    private static readonly uint[] Indices = {
        0,1,3,
        1,2,3
    };

    private AppWindow() {
        var options = WindowOptions.Default;
        options.Size = _size;
        this._title = "TheNoviceGame.Engine";
        options.Title = _title;
        this.CanvasColor.X = 1;
        this.CanvasColor.Y = 1;
        this.CanvasColor.Z = 1;
        this.CanvasColor.W = 1;
        RenderWindow = Window.Create(options);

        RenderWindow.Load += OnLoad;
        RenderWindow.Render += OnRender;
        RenderWindow.Update += OnUpdate;
        RenderWindow.Closing += OnClose;
    }

    public void Run() {
        Silk.NET.GLFW.Glfw.GetApi().GetVersion(out int versionMajor, out int versionMinor, out int versionPatch);
        Console.WriteLine("Initializing Silk.NET.GLFW v" + versionMajor + "." + versionMinor + "." + versionPatch);
        RenderWindow.Run();
    }

    private static unsafe void OnLoad() {
        IInputContext input = RenderWindow.CreateInput();
        for (int i = 0; i < input.Keyboards.Count; i++) {
            input.Keyboards[i].KeyDown += KeyDown;
        }
        glContext = GL.GetApi(RenderWindow);

        Ebo = new BufferObject<uint>(glContext, Indices, BufferTargetARB.ElementArrayBuffer);
        Vbo = new BufferObject<float>(glContext, Vertices, BufferTargetARB.ArrayBuffer);
        Vao = new VertexArrayObject<float, uint>(glContext, Vbo, Ebo);

        Vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 7, 0);
        Vao.VertexAttributePointer(1, 4, VertexAttribPointerType.Float, 7, 3);

        shader = new Shader(glContext, VertexShaderPath, FragmentShaderPath);
    }

    public AppWindow Get() {
        return Instance;
    }

    private static unsafe void OnRender(double obj) {
        glContext.Clear((uint)ClearBufferMask.ColorBufferBit);
        Vao.Bind();
        shader.Use();
        switch (_iterator)
        {
            case 0:
                shader.SetUniform("uRed", (float)Math.Sin(DateTime.Now.Millisecond / 1000f * Math.PI));
                shader.SetUniform("uGreen", 1);
                shader.SetUniform("uBlue", 1);
                break;
            case 1:
                shader.SetUniform("uRed", 1);
                shader.SetUniform("uGreen", (float)Math.Sin(DateTime.Now.Millisecond / 1000f * Math.PI));
                shader.SetUniform("uBlue", 1);
                break;
            case 2:
                shader.SetUniform("uRed", 1);
                shader.SetUniform("uGreen", 1);
                shader.SetUniform("uBlue", (float)Math.Sin(DateTime.Now.Millisecond / 1000f * Math.PI));
                _iterator = 0;
                break;
            default:
                throw new Exception();
        }
        shader.SetUniform("uAlpha", 1);
        _iterator++;
        glContext.DrawElements(PrimitiveType.Triangles, (uint)Indices.Length, DrawElementsType.UnsignedInt, null);
    }

    private static void OnUpdate(double obj) {
    }

    private static void OnClose() {
        Vbo.Dispose();
        Ebo.Dispose();
        Vao.Dispose();
        shader.Dispose();
    }

    private static void KeyDown(IKeyboard arg1, Key arg2, int arg3) {
        if (arg2 == Key.Escape) {
            RenderWindow.Close();
        }
    }
}