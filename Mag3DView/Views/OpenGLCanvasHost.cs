using Avalonia.Controls;
using Avalonia.OpenGL;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;

namespace Mag3DView.Views
{
    public class OpenGLCanvasHost : Control
    {
        private AvaloniaTkContext glContext; // Context to manage OpenGL interface
        private GlInterface glInterface; // Direct interface to interact with OpenGL

        public OpenGLCanvasHost()
        {
            this.Loaded += (sender, e) => InitializeOpenGL();
        }

        private void InitializeOpenGL()
        {
            // Use GlProfileType.OpenGL for OpenGL rendering
            GlProfileType profile = GlProfileType.OpenGL; // Use OpenGL profile

            // Create a GlVersion for OpenGL 4.5 with the chosen profile
            GlVersion glVersion = new GlVersion(profile, 4, 5); // Using GlProfileType.OpenGL

            // Create the GlInterface with the GlVersion and a simple ProcAddress lookup
            glInterface = new GlInterface(glVersion, proc => IntPtr.Zero); // Replace IntPtr.Zero with actual proc address lookup logic
            glContext = new AvaloniaTkContext(glInterface);

            // Load OpenGL bindings using the context
            GL.LoadBindings(glContext);

            // Query OpenGL version
            string version = GL.GetString(StringName.Version);
            Console.WriteLine("OpenGL Version: " + version);

            // Now OpenGL functions can be safely called
            // Example: Generate a vertex array object
            int vertexArrayObject = GL.GenVertexArray();

            // Continue with your OpenGL initialization...
        }
    }
}
