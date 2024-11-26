using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Mag3DView.Nzy3dAPI.Plot3D.Rendering.View;
using Mag3DView.Nzy3dAPI.Events.Keyboard;
using Mag3DView.Nzy3dAPI.Events.Mouse;
using Mag3DView.Nzy3dAPI.Plot3D.Rendering.Canvas;
using System;

namespace Mag3DView.Views.Nzy3d.Avalonia.ChartView
{
    public class OpenGLCanvas : ICanvas
    {
        public int RendererWidth { get; private set; }
        public int RendererHeight { get; private set; }
        public View View { get; set; }

        private GameWindow gameWindow;

        public OpenGLCanvas(int width = 800, int height = 600)
        {
            RendererWidth = width;
            RendererHeight = height;

            // Initialize OpenGL context
            InitializeOpenGL();
        }

        private void InitializeOpenGL()
        {
            var gameWindowSettings = new GameWindowSettings();
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new OpenTK.Mathematics.Vector2i(RendererWidth, RendererHeight),
                Title = "OpenGL Canvas"
            };

            // Initialize the GameWindow
            gameWindow = new GameWindow(gameWindowSettings, nativeWindowSettings);

            // Subscribe to the RenderFrame event
            gameWindow.RenderFrame += OnRenderFrame;

            // Optionally, handle the Load event for setting up OpenGL context
            gameWindow.Load += OnLoad;

            // Run the GameWindow to start the rendering loop
            gameWindow.Run();
        }

        private void OnLoad()
        {
            // This is called when the window is loaded and OpenGL context is ready
            GL.ClearColor(0f, 0f, 0f, 1f); // Set background color (black)
        }

        private void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Your rendering logic here, e.g., drawing 3D objects

            // Swap the buffers to display the rendered content
            gameWindow.SwapBuffers();
        }

        public void SetView(View value)
        {
            View = value;
        }

        public void Dispose()
        {
            // Clean up OpenGL resources
            gameWindow?.Dispose();
        }

        // Remove the redundant event handler subscription
        public void Render()
        {
            // You can trigger the render logic manually here if required, but generally GameWindow handles it
        }

        public void Resize(int width, int height)
        {
            RendererWidth = width;
            RendererHeight = height;
            GL.Viewport(0, 0, width, height);
        }

        public void AddMouseListener(IBaseMouseListener listener) { }
        public void RemoveMouseListener(IBaseMouseListener listener) { }
        public void AddMouseWheelListener(IBaseMouseWheelListener listener) { }
        public void RemoveMouseWheelListener(IBaseMouseWheelListener listener) { }
        public void AddMouseMotionListener(IBaseMouseMotionListener listener) { }
        public void RemoveMouseMotionListener(IBaseMouseMotionListener listener) { }
        public void AddKeyListener(IBaseKeyListener listener) { }
        public void RemoveKeyListener(IBaseKeyListener listener) { }

        public void ForceRepaint()
        {
            Render();
        }

        public object Screenshot()
        {
            throw new NotImplementedException();
        }
    }
}
